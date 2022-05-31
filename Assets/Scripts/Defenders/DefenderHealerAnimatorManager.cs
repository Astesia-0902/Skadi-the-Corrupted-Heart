using UnityEngine;

namespace Defenders
{
    public class DefenderHealerAnimatorManager : AnimatorManagerDefender
    {
        protected DefenderHealer DefenderHealer;
        private HealTracer healTracer;
        [SerializeField] private GameObject healTracerPrefeb;
        [SerializeField] private GameObject healHitPrefeb;
        [SerializeField] private Transform healTracerPivot;

        protected override void Awake()
        {
            base.Awake();
            DefenderHealer = GetComponentInParent<DefenderHealer>();
        }

        public override void OnAttack()
        {
            healTracer = Instantiate(healTracerPrefeb, healTracerPivot).GetComponent<HealTracer>();
            healTracer.healTarget = DefenderHealer.currentHealTarget;
            healTracer.healAmount = DefenderHealer.attackDamage;
            healTracer.healHitPrefeb = healHitPrefeb;
        }
    }
}