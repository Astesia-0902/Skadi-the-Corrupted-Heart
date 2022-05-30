using System.Collections.Generic;
using System.Linq;
using Attackers;
using Game_Managers;
using UnityEngine;

namespace Defenders
{
    public class Defender : MonoBehaviour
    {
        [Header("Combat Status")] public float maxHealth;
        public float currentHealth;
        public int blockNumStandard;
        public float attackDamage;

        [Header("Defence Paras")] public float armor;
        public float magicResistance;

        [Header("Skills")] public int skillPoint;
        public int maxSkillPoint;

        [Header("Neural Damage")] public float maxNeuralDamage = 100f;
        public float neuralDamage;
        public float neuralRecoveryTimer;
        public float neuralRecoveryThreshold = 4f;

        [Header("Status Flag")] public bool isDead;
        public bool isInteracting;
        public bool isRange;
        public bool isStunned;

        private Transform rangeParent;
        public Attacker currentTarget;
        protected AnimatorManagerDefender AnimatorManager;

        public float attackTimerStandard;
        protected float AttackTimer;

        public List<Attacker> attackersBlocked;

        protected virtual void Awake()
        {
            isDead = false;
            currentHealth = maxHealth;
            AnimatorManager = GetComponentInChildren<AnimatorManagerDefender>();
            attackersBlocked = new List<Attacker>();
            AttackTimer = attackTimerStandard;
            rangeParent = transform.GetChild(1);
        }

        private void Start()
        {
            GameManager.Instance.AddDefender(this);
            AnimatorManager.PlayTargetAnimation("Start");
        }

        protected virtual void Update()
        {
            NeuralDamageUpdate();
            UpdateAttackTimer();
            AttackUpdate();
            SkillPointUpdate();
        }

        #region Take Damage

        public virtual void TakeDamage(float physicDamage, float magicDamage, float realDamage)
        {
            currentHealth -= physicDamage - armor > 0.05f * physicDamage
                ? physicDamage - armor
                : 0.05f * physicDamage;
            currentHealth -= magicDamage * (1 - magicResistance);
            currentHealth -= realDamage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public virtual void TakeNeuralDamage(float neuralDamageToTake)
        {
            neuralDamage -= neuralDamageToTake;
            if (neuralDamage <= 0)
            {
                neuralDamage = 0;
                NeuralDamageBurst();
            }
        }

        protected virtual void NeuralDamageBurst()
        {
            isStunned = true;
            AnimatorManager.SetAnimatorBool("isStunned", isStunned);
            TakeDamage(0, 0, 0.4f * maxHealth);
            AnimatorManager.PlayTargetAnimation("Stun");
        }

        public virtual void NeuralDamageUpdate()
        {
            neuralDamage += 5f;

            if (neuralDamage >= 100f)
            {
                neuralDamage = 100f;
            }

            if (isStunned)
            {
                neuralRecoveryTimer += Time.deltaTime;
                if (neuralRecoveryTimer >= neuralRecoveryThreshold)
                {
                    isStunned = false;
                    AnimatorManager.SetAnimatorBool("isStunned", isStunned);
                    neuralRecoveryTimer = 0f;
                }
            }
            else
            {
                neuralRecoveryTimer = 0f;
            }
        }

        protected virtual void Die()
        {
            isDead = true;
            AnimatorManager.PlayTargetAnimation("Die");
            Unblock();
        }

        /// <summary>
        /// 释放被阻挡的目标
        /// </summary>
        protected virtual void Unblock()
        {
            if (attackersBlocked.Count == 0)
                return;

            foreach (Attacker attacker in attackersBlocked)
            {
                if (attacker != null)
                {
                    attacker.Unblocked();
                }
            }
        }

        #endregion

        #region Get Heal

        public virtual void GetHeal(float heal)
        {
            if (isDead)
                return;

            currentHealth += heal;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
        }

        #endregion

        #region Attack Helper

        protected virtual void AttackUpdate()
        {
            if (!CanAttack())
                return;

            currentTarget = GetPriorityTarget(GetAllTargetsInRange());

            if (AttackTimer > 0)
                return;

            if (currentTarget != null && !isInteracting)
            {
                if (!currentTarget.isDead)
                {
                    AnimatorManager.PlayTargetAnimation("Attack");
                }
                else
                {
                    //目标死亡时切换目标
                    currentTarget = null;
                }
            }
        }

        /// <summary>
        /// 获取攻击范围内，以及被阻挡的所有的敌人
        /// </summary>
        /// <returns></returns>
        protected virtual List<Attacker> GetAllTargetsInRange()
        {
            List<Attacker> targetsInRange = new List<Attacker>();
            foreach (Attacker attacker in EntitySummoner.Instance.attackersInGame)
            {
                if (attacker == null || !attacker.isActiveAndEnabled || attacker.isDead)
                    continue;

                //搜索在攻击范围的敌人
                if (CheckInRange(attacker.transform))
                    targetsInRange.Add(attacker);
                
                if (isRange)
                    continue;

                //统计被阻挡的敌人
                Vector3 attackerPosition = attacker.transform.position;
                Vector3 myPosition = transform.position;
                if (GetBlockNum() > 0 && CanBlock())
                {
                    //检查是否在所在的格子内
                    if (attackerPosition.x + 0.5f > myPosition.x && attackerPosition.x - 0.5f < myPosition.x &&
                        attackerPosition.z + 0.5f > myPosition.z && attackerPosition.z - 0.5f < myPosition.z)
                    {
                        if (!attackersBlocked.Contains(attacker))
                        {
                            attackersBlocked.Add(attacker);
                            //将当前敌人标记为已被阻挡
                            attacker.GetBlocked(this);
                        }
                    }
                }
            }

            //如果是远程干员，则不考虑阻挡的问题
            if (isRange)
            {
                return targetsInRange;
            }

            //优先攻击被阻挡的敌人
            if (attackersBlocked.Count > 0)
            {
                return attackersBlocked.Concat(targetsInRange).ToList();
            }

            return targetsInRange;
        }

        /// <summary>
        /// 获取当前干员剩余的阻挡数
        /// </summary>
        /// <returns></returns>
        public virtual int GetBlockNum()
        {
            int blockSum = 0;
            foreach (Attacker attacker in attackersBlocked)
            {
                blockSum += attacker.blockPara;
            }

            return blockNumStandard - blockSum;
        }

        /// <summary>
        /// 获取距离其目标点路程最近的敌人
        /// </summary>
        /// <param name="attackers"></param>
        /// <returns></returns>
        protected virtual Attacker GetPriorityTarget(List<Attacker> attackers)
        {
            if (attackers.Count == 0)
                return null;

            float min = float.MaxValue;
            int index = 0;
            for (int i = 0; i < attackers.Count; i++)
            {
                float current = attackers[i].GetDistanceFromDestination();
                if (current < min)
                {
                    min = current;
                    index = i;
                }
            }

            return attackers[index];
        }

        /// <summary>
        /// 检查目标是否在攻击范围内
        /// </summary>
        /// <param name="targetTransform">目标的Transform</param>
        /// <returns>在就返回True，反之False</returns>
        protected virtual bool CheckInRange(Transform targetTransform)
        {
            Vector3 targetCenter = targetTransform.position;
            //rangeParent物体下挂载了该单位的攻击范围中每个方块的中点
            for (int i = 0; i < rangeParent.childCount; i++)
            {
                if (!rangeParent.GetChild(i).gameObject.activeSelf)
                    continue;

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
        /// 移除阻挡的敌人
        /// </summary>
        /// <param name="attacker"></param>
        public virtual void RemoveBlockedEnemy(Attacker attacker)
        {
            if (attackersBlocked.Contains(attacker))
                attackersBlocked.Remove(attacker);
        }

        private void UpdateAttackTimer()
        {
            AttackTimer -= Time.deltaTime;
            if (AttackTimer < 0)
                AttackTimer = 0;
        }

        #endregion

        #region Skill

        private float skillPointTimer;
        public bool skillReady;
        public void SkillPointUpdate()
        {
            skillPointTimer += Time.deltaTime;
            if (skillPointTimer >= 1f)
            {
                skillPoint++;
            }

            if (skillPoint >= maxSkillPoint)
            {
                skillPoint = maxSkillPoint;
                skillReady = true;
            }
            else
            {
                skillReady = false;
            }
        }

        #endregion

        #region Combat Status

        /// <summary>
        /// 当前干员是否可以阻挡敌人
        /// </summary>
        /// <returns></returns>
        public virtual bool CanBlock()
        {
            return !(isStunned || isDead);
        }

        public virtual bool CanAttack()
        {
            return !(isStunned || isDead);
        }

        #endregion
    }
}