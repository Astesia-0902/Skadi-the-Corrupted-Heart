using System;
using System.Collections.Generic;
using Game_Managers;

namespace Defenders
{
    public class DefenderHealer : Defender
    {
        public Defender currentHealTarget;


        #region Attack Helper

        protected override void AttackUpdate()
        {
            if(isDead)
                return;
            
            currentHealTarget = GetPriorityHealTarget(GetAllHealTargetInRange());

            if (AttackTimer > 0)
                return;

            if (currentHealTarget != null && CanAttack())
            {
                if (!currentHealTarget.isDead)
                {
                    AttackTimer = attackTimerStandard;
                    AnimatorManager.PlayTargetAnimation("Attack", true);
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

        protected Defender GetPriorityHealTarget(List<Defender> defenders)
        {
            if (defenders.Count == 0)
                return null;

            float min = float.MaxValue;
            int index = -1;
            for (int i = 0; i < defenders.Count; i++)
            {
                float current = defenders[i].currentHealth / defenders[i].maxHealth;

                if (Math.Abs(current - 1f) < 0.00001f)
                {
                    continue;
                }

                if (current < min)
                {
                    min = current;
                    index = i;
                }
            }

            if (index == -1)
            {
                return null;
            }
            else
            {
                return defenders[index];
            }
        }

        #endregion
    }
}
