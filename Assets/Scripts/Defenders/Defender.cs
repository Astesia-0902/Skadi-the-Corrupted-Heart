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
        public bool isRange;
        public int blockNumStandard;
        public float attackDamage;
        [Header("Defence Paras")] public float armor;
        public float magicResistance;

        [Header("Status Flag")] public bool isDead;
        public bool isInteracting;

        protected Transform RangeParent;
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
            RangeParent = transform.GetChild(1);
        }

        private void Start()
        {
            GameManager.Instance.AddDefender(this);
            AnimatorManager.PlayTargetAnimation("Start");
        }

        protected virtual void Update()
        {
            UpdateAttackTimer();
            AttackUpdate();
        }


        #region Take Damage

        public virtual void TakeDamage(float physicDamage, float magicDamage)
        {
            currentHealth -= physicDamage - armor > 0.05f * physicDamage
                ? physicDamage - armor
                : 0.05f * physicDamage;
            currentHealth -= magicDamage * (1 - magicResistance);
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            isDead = true;
            AnimatorManager.PlayTargetAnimation("Die");
            Unblock();
        }

        /// <summary>
        /// ????????????????????????
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
            if(isDead)
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
                    //???????????????????????????
                    currentTarget = null;
                }
            }
        }


        /// <summary>
        /// ?????????????????????????????????????????????????????????
        /// </summary>
        /// <returns></returns>
        protected virtual List<Attacker> GetAllTargetsInRange()
        {
            List<Attacker> targetsInRange = new List<Attacker>();
            foreach (Attacker attacker in EntitySummoner.Instance.attackersInGame)
            {
                if (attacker == null || !attacker.isActiveAndEnabled || attacker.isDead)
                    continue;

                //??????????????????????????????
                if (CheckInRange(attacker.transform))
                    targetsInRange.Add(attacker);
                if (isRange)
                    continue;

                //????????????????????????
                Vector3 attackerPosition = attacker.transform.position;
                Vector3 myPosition = transform.position;
                if (GetBlockNum() > 0 && GetBlockStatus())
                {
                    //?????????????????????????????????
                    if (attackerPosition.x + 0.5f > myPosition.x && attackerPosition.x - 0.5f < myPosition.x &&
                        attackerPosition.z + 0.5f > myPosition.z && attackerPosition.z - 0.5f < myPosition.z)
                    {
                        if (!attackersBlocked.Contains(attacker))
                        {
                            attackersBlocked.Add(attacker);
                            //????????????????????????????????????
                            attacker.GetBlocked(this);
                        }
                    }
                }
            }

            //???????????????????????????????????????????????????
            if (isRange)
            {
                return targetsInRange;
            }
            
            //??????????????????????????????
            if (attackersBlocked.Count > 0)
            {
                return attackersBlocked.Concat(targetsInRange).ToList();
            }

            return targetsInRange;
        }

        /// <summary>
        /// ????????????????????????????????????
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
        /// ?????????????????????????????????????????????
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
        /// ????????????????????????????????????
        /// </summary>
        /// <param name="targetTransform">?????????Transform</param>
        /// <returns>????????????True?????????False</returns>
        protected virtual bool CheckInRange(Transform targetTransform)
        {
            Vector3 targetCenter = targetTransform.position;
            //rangeParent??????????????????????????????????????????????????????????????????
            for (int i = 0; i < RangeParent.childCount; i++)
            {
                if (!RangeParent.GetChild(i).gameObject.activeSelf)
                    continue;
                
                Vector3 rangeCenter = RangeParent.GetChild(i).position;
                if (targetCenter.x < rangeCenter.x + 0.5f && targetCenter.x > rangeCenter.x - 0.5f &&
                    targetCenter.z < rangeCenter.z + 0.5f && targetCenter.z > rangeCenter.z - 0.5f)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// ?????????????????????
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

        #region Combat Status

        /// <summary>
        /// ????????????????????????????????????
        /// </summary>
        /// <returns></returns>
        public virtual bool GetBlockStatus()
        {
            return true;
        }

        #endregion
        
    }
}