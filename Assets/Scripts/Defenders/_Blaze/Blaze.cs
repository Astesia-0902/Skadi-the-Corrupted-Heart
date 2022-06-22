using UnityEngine;

namespace Defenders._Blaze
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

            if (AttackTimer > 0)
                return;

            if (currentTarget != null && CanAttack())
            {
                if (!currentTarget.isDead)
                {
                    AttackTimer = attackTimerStandard;
                    if (Mathf.Abs(currentTarget.transform.position.z - transform.position.z) > 0.2f)
                    {
                        AnimatorManager.PlayTargetAnimation(chainSawExtensionOn ? "Skill_1_Down" : "Attack_Down", true);
                    }
                    else
                    {
                        AnimatorManager.PlayTargetAnimation(chainSawExtensionOn ? "Skill_1" : "Attack", true);
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
            for (int i = 0; i < RangeParent.childCount; i++)
            {
                RangeParent.GetChild(i).gameObject.SetActive(true);
            }

            AnimatorManager.PlayTargetAnimation("Skill_1_Begin", true);
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