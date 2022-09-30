using System.Collections.Generic;
using Defenders;

namespace Res.Scripts.Defenders._Lumen
{
    public class Lumen : DefenderHealer
    {
        protected override void Awake()
        {
            base.Awake();
            skillPoint = 5;
            maxSkillPoint = 30;
        }

        protected override void AttackUpdate()
        {
            if (isDead)
                return;

            currentHealTarget = GetPriorityHealTarget(GetAllHealTargetInRange());

            if (currentHealTarget != null)
                targetToHeal = currentHealTarget;

            if (targetToHeal.maxHealth - targetToHeal.currentHealth < 0.1f)
                targetToHeal = null;

            if (attackTimer > 0)
                return;

            if (targetToHeal != null && CanAttack() && CheckInRange(targetToHeal.transform))
            {
                if (!targetToHeal.isDead)
                {
                    attackTimer = attackTimerStandard;

                    if (skillReady)
                    {
                        HandleSkill();
                        if (skillTargets == null || skillTargets.Count == 0)
                        {
                            return;
                        }

                        skillPoint = 0;
                        animatorManager.PlayTargetAnimation("Skill_2", true);
                        RefreshRotation();
                    }
                    else
                    {
                        animatorManager.PlayTargetAnimation("Attack", true);
                        RefreshRotation();
                    }
                }
                else
                {
                    //目标死亡时切换目标
                    currentHealTarget = null;
                }
            }
        }

        public List<Defender> skillTargets;

        private void HandleSkill()
        {
            skillTargets = new List<Defender>();

            List<Defender> defenders = GetAllHealTargetInRange();

            if (defenders.Count == 0)
                return;

            defenders.Sort(new DefenderHealthStunComp());

            for (int i = 0; i < 2; i++)
            {
                if (defenders[i] != null)
                {
                    skillTargets.Add(defenders[i]);
                }
            }
        }
    }
}