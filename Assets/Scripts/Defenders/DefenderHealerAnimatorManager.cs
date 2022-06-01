using UnityEngine;

namespace Defenders
{
    public class DefenderHealerAnimatorManager : AnimatorManagerDefender
    {
        protected DefenderHealer DefenderHealer;
        protected HealTracer healTracer;
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
            healTracer = Instantiate(healTracerPrefeb, healTracerPivot).GetComponent<HealTracer>();
            healTracer.healTarget = DefenderHealer.currentHealTarget;
            healTracer.healAmount = DefenderHealer.attackDamage;
            healTracer.healHitPrefeb = healHitPrefeb;
        }

        public virtual void OnDie()
        {
            Destroy(Defender.gameObject);
        }
    }
}