using Defenders.Extension;
using Res.Scripts.Defenders._Honeyberry;
using UnityEngine;

namespace Defenders._Honeyberry
{
    public class HoneyberryAnimator : AnimatorManagerDefenderHealer
    {
        public GameObject skillPrefeb;
        
        private HealTracer secondaryTracer;
        private Honeyberry honeyberry;

        protected override void Awake()
        {
            base.Awake();
            honeyberry = GetComponentInParent<Honeyberry>();
        }
        

        public override void OnAttack()
        {
            healTracer = Instantiate(healTracerPrefeb, healTracerPivot).GetComponent<HealTracer>();
            healTracer.healTarget = defenderHealer.targetToHeal;
            healTracer.healAmount = defenderHealer.attackDamage;
            healTracer.healHitPrefeb = healHitPrefeb;
            healTracer.isElementHeal = true;
        }

        public void OnSkill()
        {
            healTracer = Instantiate(skillPrefeb, healTracerPivot).GetComponent<HealTracer>();
            healTracer.healTarget = defenderHealer.targetToHeal;
            healTracer.healAmount = defenderHealer.attackDamage;
            healTracer.healHitPrefeb = healHitPrefeb;
            healTracer.isElementHeal = true;

            if (honeyberry.secondaryTarget != null)
            {
                secondaryTracer = Instantiate(skillPrefeb, healTracerPivot).GetComponent<HealTracer>();
                secondaryTracer.healTarget = honeyberry.secondaryTargetToHeal;
                secondaryTracer.healAmount = honeyberry.attackDamage;
                secondaryTracer.healHitPrefeb = healHitPrefeb;
                secondaryTracer.isElementHeal = true;
            }
        }
    }
}
