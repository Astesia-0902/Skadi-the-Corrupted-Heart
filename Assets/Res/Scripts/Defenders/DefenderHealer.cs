using System;
using System.Collections.Generic;
using Res.Scripts.Defenders;
using Res.Scripts.Game_Managers;
using UnityEngine;

namespace Defenders
{
    /// <summary>
    /// 医疗干员的基类
    /// </summary>
    public class DefenderHealer : Defender
    {
        public Defender currentHealTarget;
        public Defender targetToHeal;

        #region Attack Helper

        protected override void AttackUpdate()
        {
            if(isDead)
                return;
            
            currentHealTarget = GetPriorityHealTarget(GetAllHealTargetInRange());
            if (currentHealTarget != null)
            {
                targetToHeal = currentHealTarget;
            }

            if (attackTimer > 0)
                return;

            if (targetToHeal != null && CanAttack())
            {
                if (!targetToHeal.isDead && Math.Abs(targetToHeal.currentHealth - targetToHeal.maxHealth) > 1f )
                {
                    attackTimer = attackTimerStandard;
                    animatorManager.PlayTargetAnimation("Attack", true);
                }
                else
                {
                    //目标死亡时切换目标
                    currentHealTarget = null;
                }
            }
        }

        protected override void RefreshRotation()
        {
            if (transform.position.x - targetToHeal.transform.position.x > 0)
            {
                targetRotation = Quaternion.Euler(45, 180, 0);
            }
            else
            {
                targetRotation = defaultRotation;
            }
        }

        protected List<Defender> GetAllHealTargetInRange()
        {
            List<Defender> targetsInRange = new List<Defender>();
            foreach (Defender defender in GameManager.Instance.defendersInGame)
            {
                if (defender == null || !defender.isActiveAndEnabled || defender.isDead)
                    continue;

                //搜索在攻击范围的敌人
                if (CheckInRange(defender.transform))
                    targetsInRange.Add(defender);
                
            }

            return targetsInRange;
        }

        protected virtual Defender GetPriorityHealTarget(List<Defender> defenders)
        {
            if (defenders.Count == 0)
                return null;

            //根据血量百分比升序排列
            defenders.Sort(new DefenderHealthComp());

            foreach (Defender defender in defenders)
            {
                if( Math.Abs(defender.currentHealth - defender.maxHealth) < 1f )
                    continue;
                return defender;
            }

            return null;
        }

        #endregion
    }
}
