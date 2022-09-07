using System;
using Res.Scripts.Defenders;
using Res.Scripts.Game_Managers;
using Spine.Unity;
using UI;
using UnityEngine;

namespace Res.Scripts.Attackers
{
    public class Attacker : MonoBehaviour
    {
        public int id;

        [Header("Combat Status")] public float maxHealth;
        //海嗣当前的血量
        public float currentHealth;
        public float attackDamage;
        public float magicDamage;
        public float realDamage;
        public int sanityDamage;
        public int blockPara;
        public int weightLevel;

        [Header("Defence Paras")] public float armor;
        public float magicResistance;

        [Header("Movements Paras")] public float standardMoveSpeed;
        public float moveSpeed;
        public int nodeIndex;
        public int spawnPoint;

        [Header("Status Flags")] public bool isDead;
        public bool isRange;
        public bool isInteracting;
        protected static readonly int IsInteracting = Animator.StringToHash("isInteracting");
        public bool isBlocked;

        public Transform hitPoint;

        public float attackTimerStandard;
        protected float attackTimer;

        public Defender currentAttackTarget;
        public NodeLoopManager nodeLoopManager;

        protected AnimatorManagerAttacker animatorManager;
        public UiForUnits uiForUnits;

        public Defender defenderWhoBlockMe;

        public Action<float, float> onHealthChanged;

        public Quaternion targetRotation = Quaternion.Euler(45, 0, 0);
        protected Quaternion defaultRotation = Quaternion.Euler(45, 0, 0);

        protected virtual void Awake()
        {
            hitPoint = transform.GetChild(2);
            animatorManager = GetComponentInChildren<AnimatorManagerAttacker>();
            uiForUnits = GetComponentInChildren<UiForUnits>();
            targetRotation = Quaternion.Euler(71.6f, 0, 0);
            defaultRotation = Quaternion.Euler(71.6f, 0, 0);
        }

        protected virtual void Update()
        {
            isInteracting = animatorManager.anim.GetBool(IsInteracting);
            UpdateAttackTimer();
            AttackUpdate();
            BlowUpCountDown();
            StunTimerUpdate();
            Rotate();
            ImprisonUpdate();
        }

        public virtual void Initialize(NodeLoopManager node)
        {
            isInteracting = false;
            isBlocked = false;
            isDead = false;
            isBlownUp = false;
            isStunned = false;
            imprisoned = false;
            defenderWhoBlockMe = null;
            spawnPoint = node.spawnPointID;
            gameObject.SetActive(true);
            currentHealth = maxHealth;
            nodeIndex = 0;
            nodeLoopManager = node;
            transform.position = node.nodesPosition[0];
            moveSpeed = standardMoveSpeed;
            currentAttackTarget = null;
        }

        #region Take Damage

        /// <summary>
        /// 角色受到伤害
        /// </summary>
        /// <param name="physicDamage"></param>
        /// <param name="magicDamage1"></param>
        /// <param name="realDamage1"></param>
        public virtual void TakeDamage(float physicDamage, float magicDamage1, float realDamage1)
        {
            currentHealth -= physicDamage - armor > 0.05f * physicDamage
                ? physicDamage - armor
                : 0.05f * physicDamage;
            currentHealth -= magicDamage1 * (1 - magicResistance);
            currentHealth -= realDamage1;
            onHealthChanged.Invoke(currentHealth, maxHealth);

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

            animatorManager.anim.speed = 1f;
            animatorManager.PlayTargetAnimation("Die", true);
        }

        #endregion

        #region Attack Helper

        protected virtual void AttackUpdate()
        {
            if (!CanAttack())
                return;

            currentAttackTarget = GetPriorityTarget();

            if (attackTimer > 0)
                return;

            if (currentAttackTarget != null && !currentAttackTarget.isDead)
            {
                attackTimer = attackTimerStandard;
                animatorManager.PlayTargetAnimation("Attack", true);
                RefreshRotation();
            }
        }

        protected void RefreshRotation()
        {
            if (transform.position.x - currentAttackTarget.transform.position.x < 0)
            {
                targetRotation = Quaternion.Euler(-45, 180, 0);
            }
            else
            {
                targetRotation = defaultRotation;
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

            return null;
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
            attackTimer -= Time.deltaTime;
            if (attackTimer < 0)
                attackTimer = 0;
        }

        private void Rotate()
        {
            if (!isInteracting)
            {
                animatorManager.transform.rotation = Quaternion.Slerp(animatorManager.transform.rotation,
                    defaultRotation, 20f * Time.deltaTime);
            }
            else
            {
                animatorManager.transform.rotation = Quaternion.Slerp(animatorManager.transform.rotation,
                    targetRotation, 20f * Time.deltaTime);
            }
        }

        #endregion

        #region Status

        public bool isBlownUp;
        public bool isStunned;
        public bool imprisoned;
        private float imprisonedTimer;
        private float blowUpTimer;
        protected float stunTimer;

        public void GetImprisoned(float time)
        {
            imprisoned = true;
            imprisonedTimer = time;
        }

        protected virtual void ImprisonUpdate()
        {
            if (!imprisoned)
                return;

            imprisonedTimer -= Time.deltaTime;

            if (imprisonedTimer <= 0f)
            {
                imprisoned = false;
            }
        }

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

        public virtual void GetStunned(float stunTime)
        {
            isStunned = true;
            animatorManager.anim.speed = 0;
            stunTimer = stunTime;
        }

        protected virtual void StunTimerUpdate()
        {
            if (isStunned)
            {
                stunTimer -= Time.deltaTime;
                if (stunTimer <= 0f)
                {
                    isStunned = false;
                    animatorManager.anim.speed = 1;
                }
            }
        }

        public virtual bool CanMove()
        {
            return !isInteracting && !isBlocked && !isBlownUp && !imprisoned && !isStunned;
        }
        
        public virtual bool CanAttack()
        {
            return !isInteracting && !isBlownUp && !isStunned;
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