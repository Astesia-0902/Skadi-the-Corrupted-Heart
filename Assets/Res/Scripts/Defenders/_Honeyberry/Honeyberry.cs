using System;
using System.Collections.Generic;
using Defenders;

namespace Res.Scripts.Defenders._Honeyberry
{
    public class Honeyberry : DefenderHealer
    {
        public Defender secondaryTarget;
        public Defender secondaryTargetToHeal;

        protected override void AttackUpdate()
        {
            if (isDead)
                return;

            currentHealTarget = GetPriorityHealTarget(GetAllHealTargetInRange());
            
            if (currentHealTarget != null)
            {
                targetToHeal = currentHealTarget;
            }

            if (attackTimer > 0)
                return;

            if (currentHealTarget != null && CanAttack())
            {
                if (!targetToHeal.isDead && CheckInRange(targetToHeal.transform))
                {
                    if (skillReady)
                    {
                        currentHealTarget = GetPriorityHealTargetBySanity(GetAllHealTargetInRange());
                        if (currentHealTarget != null)
                        {
                            targetToHeal = currentHealTarget;
                        }
                        
                        secondaryTarget = GetSecondaryHealTargetBySanity(GetAllHealTargetInRange());
                        if (secondaryTarget != null)
                        {
                            secondaryTargetToHeal = secondaryTarget;
                        }

                        if (targetToHeal != null)
                        {
                            animatorManager.PlayTargetAnimation("Skill", true);
                            RefreshRotation();
                            skillPoint = 0;
                            skillReady = false;
                            attackTimer = attackTimerStandard;
                        }
                    }
                    else
                    {
                        attackTimer = attackTimerStandard;
                        RefreshRotation();
                        animatorManager.PlayTargetAnimation("Attack", true);
                    }
                }
                else
                {
                    //目标死亡时切换目标
                    currentHealTarget = null;
                }
            }
        }

        private Defender GetPriorityHealTargetBySanity(List<Defender> defenders)
        {
            if (defenders.Count == 0)
                return null;

            defenders.Sort(new DefenderNeuralComp());

            foreach (Defender defender in defenders)
            {
                if (Math.Abs(defender.currentHealth - defender.maxHealth) < 0.01f &&
                    Math.Abs(defender.sanity - 1000) < 0.01f)
                    continue;

                return defender;
            }

            return null;
        }

        private Defender GetSecondaryHealTargetBySanity(List<Defender> defenders)
        {
            if (defenders.Count == 0)
                return null;

            defenders.Sort(new DefenderNeuralComp());

            foreach (Defender defender in defenders)
            {
                if ((Math.Abs(defender.currentHealth - defender.maxHealth) < 0.01f &&
                     Math.Abs(defender.sanity - 1000) < 0.01f) || defender == currentHealTarget)
                    continue;
                return defender;
            }

            return null;
        }

        protected override Defender GetPriorityHealTarget(List<Defender> defenders)
        {
            if (defenders.Count == 0)
                return null;

            defenders.Sort(new DefenderHealthComp());

            foreach (Defender defender in defenders)
            {
                if (Math.Abs(defender.currentHealth - defender.maxHealth) < 0.01f &&
                    Math.Abs(defender.sanity - 1000) < 0.01f)
                    continue;

                return defender;
            }

            return null;
        }
    }
}