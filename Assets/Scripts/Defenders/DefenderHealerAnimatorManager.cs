using Defenders.Extension;
using UnityEngine;

namespace Defenders
{
    public class DefenderHealerAnimatorManager : AnimatorManagerDefender
    {
        protected DefenderHealer DefenderHealer;
        protected HealTracer HealTracer;
        [SerializeField] protected GameObject healTracerPrefeb;
        [SerializeField] protected GameObject healHitPrefeb;
        [SerializeField] protected Transform healTracerPivot;

        protected override void Awake()
        {
            base.Awake();
            DefenderHealer = GetComponentInParent<DefenderHealer>();
        }

        public override void OnAttack()
        {
            HealTracer = Instantiate(healTracerPrefeb, healTracerPivot).GetComponent<HealTracer>();
            HealTracer.healTarget = DefenderHealer.targetToHeal;
            HealTracer.healAmount = DefenderHealer.attackDamage;
            HealTracer.healHitPrefeb = healHitPrefeb;
        }

        public virtual void OnDie()
        {
            Destroy(defender.gameObject);
        }
    }
}