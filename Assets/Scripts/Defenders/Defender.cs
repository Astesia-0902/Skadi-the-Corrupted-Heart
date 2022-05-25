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

        public bool isInteracting;

        protected Transform RangeParent;
        public Attacker currentTarget;
        protected AnimatorManagerDefender AnimatorManager;

        public float attackTimerStandard;
        protected float AttackTimer;

        public List<Attacker> attackersBlocked;

        protected virtual void Awake()
        {
            currentHealth = maxHealth;
            AnimatorManager = GetComponentInChildren<AnimatorManagerDefender>();
            attackersBlocked = new List<Attacker>();
            AttackTimer = attackTimerStandard;
            RangeParent = transform.GetChild(1);
        }

        private void Start()
        {
            GameManager.Instance.AddDefender(this);
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
            AnimatorManager.PlayTargetAnimation("Die");
            Unblock();
        }

        /// <summary>
        /// 释放被阻挡的目标
        /// </summary>
        protected virtual void Unblock()
        {
            foreach (Attacker attacker in attackersBlocked)
            {
                if (attacker != null)
                {
                    attacker.Unblocked();
                }
            }
        }

        #endregion

        #region Attack Helper

        private void AttackUpdate()
        {
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
        private List<Attacker> GetAllTargetsInRange()
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
                if (GetBlockNum() > 0 && GetBlockStatus())
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
        private Attacker GetPriorityTarget(List<Attacker> attackers)
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
        private bool CheckInRange(Transform targetTransform)
        {
            Vector3 targetCenter = targetTransform.position;
            //rangeParent物体下挂载了该单位的攻击范围中每个方块的中点
            for (int i = 0; i < RangeParent.childCount; i++)
            {
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

        #region Combat Status

        /// <summary>
        /// 当前干员是否可以阻挡敌人
        /// </summary>
        /// <returns></returns>
        public virtual bool GetBlockStatus()
        {
            return true;
        }

        #endregion

        // protected virtual List<Attacker> CheckBlock()
        // {
        //     List<Attacker> attackersBlocked = new List<Attacker>();
        //     foreach (Attacker attacker in EntitySummoner.Instance.attackersInGame)
        //     {
        //         Vector3 attackerPosition = attacker.transform.position;
        //         Vector3 myPosition = transform.position;
        //         if (currentBlockNum > 0 && GetBlockStatus())
        //         {
        //             if (attackerPosition.x + 0.5f > myPosition.x && attackerPosition.x - 0.5f < myPosition.x &&
        //                 attackerPosition.z + 0.5f > myPosition.z && attackerPosition.z - 0.5f < myPosition.z)
        //             {
        //                 attackersBlocked.Add(attacker);
        //                 attacker.GetBlocked(this);
        //             }
        //         }
        //     }
        //
        //     currentBlockNum = blockNumStandard - attackersBlocked.Count;
        //     return attackersBlocked;
        // }
    }
}