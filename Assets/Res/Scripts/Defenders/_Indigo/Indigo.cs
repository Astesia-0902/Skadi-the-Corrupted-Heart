using UnityEngine;

namespace Res.Scripts.Defenders._Indigo
{
    public class Indigo : Defender
    {
        private bool isSkillOn;
        private float skillTimer;
        public int imprisonCount;
        public int chargeCount;


        protected override void Update()
        {
            base.Update();
            SkillUpdate();
        }

        public void CastSkill()
        {
            if (!skillReady)
                return;

            attackTimerStandard *= 0.6f;

            for (int i = 4; i < rangeParent.childCount; i++)
            {
                rangeParent.GetChild(i).gameObject.SetActive(false);
            }

            rangeParent.GetChild(rangeParent.childCount - 1).gameObject.SetActive(true);

            magicDamage *= 0.5f;
            attackTimerStandard *= 0.2f;
        }

        private void SkillUpdate()
        {
            if (!isSkillOn)
                return;
            
            skillTimer += Time.deltaTime;
            
            if (skillTimer >= 4f)
            {
                isSkillOn = false;
                skillTimer = 0;
                magicDamage *= 2f;
                attackTimerStandard *= 5f;
                
                for (int i = 4; i < rangeParent.childCount; i++)
                {
                    rangeParent.GetChild(i).gameObject.SetActive(true);
                }

                rangeParent.GetChild(rangeParent.childCount - 1).gameObject.SetActive(false);
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
                    attackTimer = attackTimerStandard;
                    float attackAnimationSpeed = attackTimerStandard < 1f ? 1 / attackTimerStandard : 1f;
                    animatorManager.PlayTargetAnimation("Attack", true, attackAnimationSpeed);
                    RefreshRotation();
                }
                else
                {
                    //目标死亡时切换目标
                    currentTarget = null;
                }
            }
            else if(targetToDeal == null && CanAttack() && chargeCount < 3)
            {
                attackTimer = attackTimerStandard;
                animatorManager.PlayTargetAnimation("Attack_Charge", true);
            }
        }
    }
}
