using UnityEngine;

namespace Res.Scripts.Defenders._Elysium
{
    public class Elysium : Defender
    {
        private bool isSkillOn;
        private float skillTimer;
        public GameObject skillEffect;
        private static readonly int SkillEnd = Animator.StringToHash("skillEnd");

        protected override void Update()
        {
            base.Update();
            SkillTimerUpdate();
        }

        public void CastSkill()
        {
            if (isSkillOn)
                return;
            
            isSkillOn = true;
            animatorManager.PlayTargetAnimation("Skill_1_Start", true);
        }

        public void SkillTimerUpdate()
        {
            if (!isSkillOn)
                return;
            
            skillTimer += Time.deltaTime;
            if (skillTimer >= 8f)
            {
                skillTimer = 0f;
                isSkillOn = false;
                animatorManager.anim.SetTrigger(SkillEnd);
            }
        }
    }
}