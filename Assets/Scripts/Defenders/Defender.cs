using System.Collections.Generic;
using Attackers;
using Game_Managers;
using UnityEngine;

namespace Defenders
{
    public class Defender : MonoBehaviour
    {
        private Transform rangeParent;
        private Attacker currentTarget;

        public float attackTimerStandard;
        private float attackTimer;

        private void Awake()
        {
            attackTimer = attackTimerStandard;
            rangeParent = transform.GetChild(1);
        }

        private void Update()
        {
            UpdateAttackTimer();
            AttackUpdate();
        }

        private void AttackUpdate()
        {
            //TODO:如果敌人离开范围了，需要取消目标
            if (attackTimer > 0)
                return;

            if (currentTarget == null)
            {
                currentTarget = GetPriorityTarget(GetAllTargetsInRange());
            }

            //TODO:具体的攻击行为
        }

        /// <summary>
        /// 获取攻击范围内所有的敌人
        /// </summary>
        /// <returns></returns>
        private List<Attacker> GetAllTargetsInRange()
        {
            List<Attacker> targetsInRange = new List<Attacker>();
            foreach (Attacker attacker in EntitySummoner.Instance.attackersInGame)
            {
                if (CheckInRange(attacker.transform))
                    targetsInRange.Add(attacker);
            }

            return targetsInRange;
        }

        private Attacker GetPriorityTarget(List<Attacker> attackers)
        {
            //TODO:返回敌人中，离自身目标点路程最短的那个
            return attackers[0];
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

        private void UpdateAttackTimer()
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer < 0)
                attackTimer = 0;
        }
    }
}