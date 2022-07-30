using System;
using System.Collections.Generic;
using Game_Managers;
using Res.Scripts.Defenders;

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
                if (!currentHealTarget.isDead)
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
                if( Math.Abs(defender.currentHealth - defender.maxHealth) < 0.01f )
                    continue;
                return defender;
            }

            return null;
        }

        #endregion
    }
}
