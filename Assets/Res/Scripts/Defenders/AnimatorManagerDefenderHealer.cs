using Defenders.Extension;
using UnityEngine;

namespace Defenders
{
    /// <summary>
    /// 医疗干员动画控制器的基类
    /// </summary>
    public class AnimatorManagerDefenderHealer : AnimatorManagerDefender
    {
        protected DefenderHealer defenderHealer;
        protected HealTracer healTracer;
        [SerializeField] protected GameObject healTracerPrefeb;
        [SerializeField] protected GameObject healHitPrefeb;
        [SerializeField] protected Transform healTracerPivot;

        protected override void Awake()
        {
            base.Awake();
            defenderHealer = GetComponentInParent<DefenderHealer>();
        }

        public override void OnAttack()
        {
            //生成并初始化发射物
            healTracer = Instantiate(healTracerPrefeb, healTracerPivot).GetComponent<HealTracer>();
            healTracer.healTarget = defenderHealer.targetToHeal;
            healTracer.healAmount = defenderHealer.attackDamage;
            healTracer.healHitPrefeb = healHitPrefeb;
        }

        public virtual void OnDie()
        {
            Destroy(defender.gameObject);
        }
    }
}