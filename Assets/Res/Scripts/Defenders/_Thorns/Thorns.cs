using System.Collections.Generic;
using Res.Scripts.Attackers;
using UnityEngine;

namespace Res.Scripts.Defenders._Thorns
{
    public class Thorns : Defender
    {
        private int skillCount;
        private float skillTimer;
        private bool isSkillOn;

        private float idleTimer;
        private int idleTimerHelper;

        private float attackStandard;
        private float attackTimeBuffer;

        protected override void Start()
        {
            base.Start();
            attackStandard = attackDamage;
            attackTimeBuffer = attackTimerStandard;
        }

        protected override void Update()
        {
            base.Update();
            SkillUpdate();
            IdleTimerUpdate();
        }

        private void IdleTimerUpdate()
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= 1f)
            {
                idleTimer = 0f;
                idleTimerHelper++;
            }

            if (idleTimerHelper > 3)
            {
                GetHeal(Time.deltaTime * maxHealth * 0.04f);
            }
        }

        private void ResetIdleTimer()
        {
            idleTimer = 0f;
            idleTimerHelper = 0;
        }

        public void CastSkill()
        {
            if (!skillReady || skillCount >= 2)
                return;

            skillReady = false;
            skillPoint = 0;

            for (int i = 8; i < rangeParent.childCount; i++)
            {
                rangeParent.GetChild(i).gameObject.SetActive(true);
            }

            if (skillEffectPrefeb1 != null && skillEffectPrefeb2 != null)
            {
                skillEffectPointer = Instantiate(skillCount >= 1 ? skillEffectPrefeb2 : skillEffectPrefeb1, transform);
            }

            attackDamage = skillCount >= 1 ? attackDamage * 2.2f : attackDamage * 1.6f;
            attackTimerStandard = attackTimerStandard * skillCount >= 1 ? 0.66f : 0.8f;
            isSkillOn = true;
            skillCount++;
        }

        private void SkillUpdate()
        {
            if (skillCount >= 2)
            {
                return;
            }

            if (!isSkillOn)
                return;

            skillTimer += Time.deltaTime;
            if (skillTimer >= 30f)
            {
                skillTimer = 0f;
                SkillEnd();
            }
        }

        private void SkillEnd()
        {
            if (skillCount >= 2)
                return;

            for (int i = 8; i < rangeParent.childCount; i++)
            {
                rangeParent.GetChild(i).gameObject.SetActive(false);
            }

            isSkillOn = false;
            attackDamage = attackStandard;
            attackTimerStandard = attackTimeBuffer;

            DestroySkillEffect();
        }

        public override void SkillPointOnAttack()
        {
            if (isSkillOn)
                return;

            base.SkillPointOnAttack();
            
            if (skillPoint == maxSkillPoint)
            {
                CastSkill();
            }
        }

        protected override void AttackUpdate()
        {
            currentTarget = GetPriorityTarget(GetAllTargetsInRange());

            if (currentTarget != null)
                targetToDeal = currentTarget;

            if (attackTimer > 0)
                return;

            if (targetToDeal != null && CanAttack())
            {
                if (!targetToDeal.isDead && CheckInRange(targetToDeal.transform))
                {
                    ResetIdleTimer();
                    SkillPointOnAttack();
                    attackTimer = attackTimerStandard;
                    float attackAnimationSpeed = attackTimerStandard < 1f ? 1 / attackTimerStandard : 1f;

                    if (!isSkillOn)
                    {
                        animatorManager.PlayTargetAnimation(
                            attackersBlocked.Contains(targetToDeal) ? "Attack_1" : "Attack_2", true,
                            attackAnimationSpeed);
                    }
                    else
                    {
                        animatorManager.PlayTargetAnimation(
                            attackersBlocked.Contains(targetToDeal) ? "Skill2_1" : "Skill2_2", true,
                            attackAnimationSpeed);
                    }

                    RefreshRotation();
                }
                else
                {
                    //目标死亡时切换目标
                    currentTarget = null;
                }
            }
        }

        protected override Attacker GetPriorityTarget(List<Attacker> attackers)
        {
            if (attackersBlocked.Count > 0)
            {
                return attackersBlocked[0];
            }

            return base.GetPriorityTarget(attackers);
        }
    }
}