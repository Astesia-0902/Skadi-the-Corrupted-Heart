using System;
using Defenders;
using Game_Managers;
using Res.Scripts.Defenders;
using Res.Scripts.Game_Managers;
using UI;
using UnityEngine;

namespace Attackers
{
    public class Attacker : MonoBehaviour
    {
        public int id;
        
        [Header("Combat Status")] 
        public float maxHealth;
        public float currentHealth;
        public float attackDamage;
        public float nerualDamage;
        public int blockPara;
        public int weightLevel;
        
        [Header("Defence Paras")] 
        public float armor;
        public float magicResistance;

        [Header("Movements Paras")] 
        public float standardMoveSpeed;
        public float moveSpeed;
        public int nodeIndex;
        public int spawnPoint;
        
        [Header("Status Flags")] public bool isDead;
        public bool isRange;
        public bool isInteracting;
        private static readonly int IsInteracting = Animator.StringToHash("isInteracting");
        public bool isBlocked;

        public Transform hitPoint;

        public float attackTimerStandard;
        protected float AttackTimer;

        public Defender currentAttackTarget;
        public NodeLoopManager nodeLoopManager;
        
        private Transform rangeParent;
        private AnimatorManagerAttacker animatorManager;
        public UiForUnits uiForUnits;

        public Defender defenderWhoBlockMe;

        public Action<float, float> OnHealthChanged;

        protected virtual void Update()
        {
            isInteracting = animatorManager.anim.GetBool(IsInteracting);
            UpdateAttackTimer();
            AttackUpdate();
            BlowUpCountDown();
            StunTimerUpdate();
        }

        public virtual void Initialize(NodeLoopManager node)
        {
            hitPoint = transform.GetChild(3);
            animatorManager = GetComponentInChildren<AnimatorManagerAttacker>();
            uiForUnits = GetComponentInChildren<UiForUnits>();
            isInteracting = false;
            isBlocked = false;
            isDead = false;
            isBlownUp = false;
            defenderWhoBlockMe = null;
            spawnPoint = node.spawnPointID;
            rangeParent = transform.GetChild(1);
            gameObject.SetActive(true);
            currentHealth = maxHealth;
            nodeIndex = 0;
            nodeLoopManager = node;
            transform.position = node.nodesPosition[0];
            standardMoveSpeed = moveSpeed;
        }

        #region Take Damage

        /// <summary>
        /// 角色受到伤害
        /// </summary>
        /// <param name="physicDamage"></param>
        /// <param name="magicDamage"></param>
        /// <param name="realDamage"></param>
        public virtual void TakeDamage(float physicDamage, float magicDamage, float realDamage)
        {
            currentHealth -= physicDamage - armor > 0.05f * physicDamage
                ? physicDamage - armor
                : 0.05f * physicDamage;
            currentHealth -= magicDamage * (1 - magicResistance);
            currentHealth -= realDamage;
            OnHealthChanged.Invoke(currentHealth, maxHealth);
            
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// 角色嗝屁了
        /// </summary>
        protected virtual void Die()
        {
            isDead = true;
            
            if (defenderWhoBlockMe != null)
            {
                defenderWhoBlockMe.RemoveBlockedEnemy(this);
                defenderWhoBlockMe = null;
            }

            animatorManager.PlayTargetAnimation("Die", true);
        }

        #endregion

        #region Attack Helper

        private void AttackUpdate()
        {
            if(isDead)
                return;
            
            currentAttackTarget = GetPriorityTarget();
            
            if (AttackTimer > 0)
                return;

            if (currentAttackTarget != null && !isInteracting && !isBlownUp)
            {
                animatorManager.PlayTargetAnimation("Attack", true);
            }
        }

        /// <summary>
        /// 最后部署的干员为优先目标
        /// </summary>
        /// <returns></returns>
        protected virtual Defender GetPriorityTarget()
        {
            //优先攻击阻挡我的目标
            if (defenderWhoBlockMe != null)
                return defenderWhoBlockMe;

            if (isRange)
            {
                for (int i = GameManager.Instance.defendersInGame.Count - 1; i >= 0; i--)
                {
                    if (CheckInRange(GameManager.Instance.defendersInGame[i].transform))
                        return GameManager.Instance.defendersInGame[i];
                }
            }

            return null;
        }

        /// <summary>
        /// 检查目标是否在攻击范围内
        /// </summary>
        /// <param name="targetTransform">目标的Transform</param>
        /// <returns>在就返回True，反之False</returns>
        private bool CheckInRange(Transform targetTransform)
        {
            Vector3 targetCenter = targetTransform.position;
            //rangeParent物体下挂载了该单位的攻击范围中每个方块的中点
            for (int i = 0; i < rangeParent.childCount; i++)
            {
                Vector3 rangeCenter = rangeParent.GetChild(i).position;
                if (targetCenter.x < rangeCenter.x + 0.5f && targetCenter.x > rangeCenter.x - 0.5f &&
                    targetCenter.z < rangeCenter.z + 0.5f && targetCenter.z > rangeCenter.z - 0.5f)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 怪物被阻挡后被挤压到格子边缘的效果（大概
        /// </summary>
        /// <param name="defender"></param>
        public virtual void GetBlocked(Defender defender)
        {
            //更新被阻挡的信息
            isBlocked = true;
            defenderWhoBlockMe = defender;
            
            Vector3 defenderPosition = defender.transform.position;
            Vector3 myPosition = transform.position;
            Vector3 targetPosition = new Vector3(0, 0, 0);

            if (defenderPosition.x - myPosition.x >= 0)
            {
                targetPosition.x = defenderPosition.x + 0.5f;
            }
            else
            {
                targetPosition.x = defenderPosition.x - 0.5f;
            }

            if (targetPosition.z - myPosition.z >= 0)
            {
                targetPosition.z = defenderPosition.z + 0.5f;
            }
            else
            {
                targetPosition.z = defenderPosition.z - 0.5f;
            }

            transform.position = Vector3.Slerp(myPosition, defenderPosition, Time.deltaTime * 10f);
        }

        public virtual void Unblocked()
        {
            isBlocked = false;
            defenderWhoBlockMe = null;
        }

        private void UpdateAttackTimer()
        {
            AttackTimer -= Time.deltaTime;
            if (AttackTimer < 0)
                AttackTimer = 0;
        }

        #endregion

        #region Status

        public bool isBlownUp;
        private bool isStunned;
        private float blowUpTimer;
        private float stunTimer;

        public void BlowUpStart(float time)
        {
            if (weightLevel < 3)
            {
                blowUpTimer = time;
            }
            else
            {
                blowUpTimer = time / 2f;
            }

            animatorManager.PlayTargetAnimation("Stun", true);
            Transform transform1 = transform;
            Vector3 temp = transform1.position;
            temp.y += 1f;
            transform1.position = temp;
            isBlownUp = true;
        }
        public void BlowUpCountDown()
        {
            if (isBlownUp)
            {
                blowUpTimer -= Time.deltaTime;
                if (blowUpTimer <= 0)
                {
                    animatorManager.anim.SetBool(IsInteracting, false);
                    isBlownUp = false;
                    Transform transform1 = transform;
                    Vector3 temp = transform1.position;
                    temp.y -= 1f;
                    transform1.position = temp;
                }
            }
        }

        public void GetStunned(float stunTime)
        {
            isStunned = true;
            animatorManager.PlayTargetAnimation("Stun", true);
            stunTimer = stunTime;
        }

        public void StunTimerUpdate()
        {
            if (isStunned)
            {
                stunTimer -= Time.deltaTime;
                if (stunTimer <= 0f)
                {
                    isStunned = false;
                    animatorManager.anim.SetBool(IsInteracting, false);
                }
            }
        }

        public virtual bool CanMove()
        {
            return !isInteracting && !isBlocked && !isBlownUp;
        }
        

        /// <summary>
        /// 返回当前进攻方距离终点的路程
        /// </summary>
        /// <returns></returns>
        public virtual float GetDistanceFromDestination()
        {
            if (nodeLoopManager == null)
                return 0f;

            float res = 0f;

            for (int i = nodeIndex; i < nodeLoopManager.nodesPosition.Length - 1; i++)
            {
                res += Vector3.Distance(nodeLoopManager.nodesPosition[i], nodeLoopManager.nodesPosition[i + 1]);
            }

            return res;
        }

        #endregion

        // protected virtual List<Defender> GetAllTargetsInRange()
        // {
        //     List<Defender> targetsInRange = new List<Defender>();
        //     foreach (Defender defender in GameManager.Instance.defendersInGame)
        //     {
        //         if (CheckInRange(defender.transform))
        //             targetsInRange.Add(defender);
        //     }
        //
        //     return targetsInRange;
        // }

        // protected virtual bool CheckBlock()
        // {
        //     foreach (Defender defender in GameManager.Instance.defendersInGame)
        //     {
        //         Vector3 defenderPosition = defender.transform.position;
        //         Vector3 myPosition = transform.position;
        //         if (defender.currentBlockNum > 0 && defender.GetBlockStatus())
        //         {
        //             if (defenderPosition.x + 0.5f > myPosition.x && defenderPosition.x - 0.5f < myPosition.x &&
        //                 defenderPosition.z + 0.5f > myPosition.z && defenderPosition.z - 0.5f < myPosition.z)
        //             {
        //                 currentAttackTarget = defender;
        //                 defender.currentBlockNum--;
        //                 GetBlocked(defenderPosition);
        //                 return true;
        //             }
        //         }
        //     }
        //
        //     return false;
        // }
    }
}