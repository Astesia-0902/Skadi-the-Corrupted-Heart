using System.Collections.Generic;
using Defenders;
using Res.Scripts.Defenders._Mon3tr;

namespace Res.Scripts.Defenders._Kalts
{
    public class Kalts : DefenderHealer
    {
        private Mon3TR mon3TR;

        protected override void OnDisable()
        {
            if (mon3TR != null)
            {
                Destroy(mon3TR.gameObject);
            }

            base.OnDisable();
        }

        public void FindMons3TR()
        {
            mon3TR = FindObjectOfType<Mon3TR>();
        }

        protected override Defender GetPriorityHealTarget(List<Defender> defenders)
        {
            if (mon3TR != null && mon3TR.currentHealth < mon3TR.maxHealth)
            {
                return mon3TR;
            }

            return base.GetPriorityHealTarget(defenders);
        }

        public override void SkillPointUpdate()
        {
            if (mon3TR == null)
            {
                skillReady = false;
                skillPoint = 0;
                return;
            }

            base.SkillPointUpdate();
            
            if (skillPoint == maxSkillPoint)
            {
                CastSkill();
            }
        }

        public void CastSkill()
        {
            skillReady = false;
            skillPoint = 0;
            mon3TR.CastSkill();
        }
    }
}