using Defenders;
using UnityEngine;

namespace Res.Scripts.Defenders._Blaze
{
    public class Blaze : Defender
    {
        private bool chainSawExtensionOn;

        protected override void Awake()
        {
            chainSawExtensionOn = false;
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();
            
            if (skillReady && !chainSawExtensionOn)
            {
                ChainSawExtension();
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
                    if (Mathf.Abs(targetToDeal.transform.position.z - transform.position.z) > 0.2f)
                    {
                        PlayRandomSFX(attackSFX);
                        animatorManager.PlayTargetAnimation(chainSawExtensionOn ? "Skill_1_Down" : "Attack_Down", true);
                    }
                    else
                    {
                        PlayRandomSFX(attackSFX);
                        animatorManager.PlayTargetAnimation(chainSawExtensionOn ? "Skill_1" : "Attack", true);
                    }
                }
                else
                {
                    //目标死亡时切换目标
                    currentTarget = null;
                }
            }
        }

        private void ChainSawExtension()
        {
            for (int i = 0; i < rangeParent.childCount; i++)
            {
                rangeParent.GetChild(i).gameObject.SetActive(true);
            }

            animatorManager.PlayTargetAnimation("Skill_1_Begin", true);
            attackDamage *= 2f;
            skillPoint = 0;
            chainSawExtensionOn = true;
        }

        public override void SkillPointUpdate()
        {
            if (chainSawExtensionOn)
                return;

            base.SkillPointUpdate();
        }
    }
}