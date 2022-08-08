using UnityEngine;

namespace Res.Scripts.Defenders._Amiya
{
    public class Amiya : Defender
    {
        private bool isSkillOn;
        private float skillTimer;

        protected override void Update()
        {
            base.Update();
            SkillUpdate();
        }

        public void CastSkill()
        {
            if (!skillReady)
                return;

            if (skillEffectPrefeb1 != null)
            {
                skillEffectPointer = Instantiate(skillEffectPrefeb1, transform);
            }
            
            skillPoint = 0;
            skillReady = false;
            isSkillOn = true;
            magicDamage *= 1.8f;
        }

        private void SkillUpdate()
        {
            if (!isSkillOn)
                return;

            skillTimer += Time.deltaTime;
            if (skillTimer >= 30f)
            {
                isSkillOn = false;
                skillTimer = 0;
                magicDamage /= 1.8f;
                DestroySkillEffect();
            }
        }

        public override void SkillPointUpdate()
        {
            if (isSkillOn)
                return;

            base.SkillPointUpdate();
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
                    attackTimer = attackTimerStandard;
                    float attackAnimationSpeed = attackTimerStandard < 1f ? 1 / attackTimerStandard : 1f;
                    animatorManager.PlayTargetAnimation(isSkillOn ? "Skill_1" : "Attack", true, attackAnimationSpeed);
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