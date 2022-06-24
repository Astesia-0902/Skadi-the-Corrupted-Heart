using Defenders.Extension;
using UnityEngine;

namespace Defenders._Honeyberry
{
    public class HoneyberryAnimator : DefenderHealerAnimatorManager
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
            HealTracer = Instantiate(healTracerPrefeb, healTracerPivot).GetComponent<HealTracer>();
            HealTracer.healTarget = DefenderHealer.targetToHeal;
            HealTracer.healAmount = DefenderHealer.attackDamage;
            HealTracer.healHitPrefeb = healHitPrefeb;
            HealTracer.isElementHeal = true;
        }

        public void OnSkill()
        {
            HealTracer = Instantiate(skillPrefeb, healTracerPivot).GetComponent<HealTracer>();
            HealTracer.healTarget = DefenderHealer.targetToHeal;
            HealTracer.healAmount = DefenderHealer.attackDamage;
            HealTracer.healHitPrefeb = healHitPrefeb;
            HealTracer.isElementHeal = true;

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
