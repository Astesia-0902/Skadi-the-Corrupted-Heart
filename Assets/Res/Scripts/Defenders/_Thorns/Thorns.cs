using Game_Managers;
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

        protected override void Update()
        {
            base.Update();
            SkillUpdate();
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
            for (int i = 8; i < rangeParent.childCount; i++)
            {
                rangeParent.GetChild(i).gameObject.SetActive(true);
            }

            attackDamage = skillCount >= 2 ? attackDamage * 2.2f : attackDamage * 1.6f;
            attackTimerStandard = attackTimerStandard * skillCount >= 2 ? 0.66f : 0.8f;
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
            attackDamage /= 1.6f;
            attackTimerStandard /= 0.66f;
        }

        public override void SkillPointOnAttack()
        {
            if (isSkillOn)
                return;
            base.SkillPointOnAttack();
        }

        protected override void AttackUpdate()
        {
            currentTarget = GetPriorityTarget(GetAllTargetsInRange());
            if (CheckInRange(GameManager.Instance.skadi.transform))
                currentTarget = GameManager.Instance.skadi;

            if (currentTarget != null)
                targetToDeal = currentTarget;

            if (attackTimer > 0)
                return;

            if (targetToDeal != null && CanAttack())
            {
                if (!targetToDeal.isDead && CheckInRange(targetToDeal.transform))
                {
                    ResetIdleTimer();
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
    }
}
