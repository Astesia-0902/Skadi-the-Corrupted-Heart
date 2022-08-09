using Attackers;
using Res.Scripts.Attackers.Extension;
using UnityEngine;

namespace Res.Scripts.Attackers
{
    public class AnimatorManagerAttackerRange : AnimatorManagerAttacker
    {
        [Header("Tracer Effect")] public GameObject tracerFXPrefeb;
        public GameObject hitFXPrefeb;
        public Transform tracerPivot;

        public RangeAttackTracerAttacker rangeAttackTracerAttacker;
        
        public override void OnAttack()
        {
            if (attacker.currentAttackTarget != null)
            {
                rangeAttackTracerAttacker =
                    Instantiate(tracerFXPrefeb, tracerPivot).GetComponent<RangeAttackTracerAttacker>();

                if (rangeAttackTracerAttacker != null)
                {
                    rangeAttackTracerAttacker.target = attacker.currentAttackTarget;
                    rangeAttackTracerAttacker.magicDamage = attacker.magicDamage;
                    rangeAttackTracerAttacker.realDamage = attacker.realDamage;
                    rangeAttackTracerAttacker.physicDamage = attacker.attackDamage;
                    rangeAttackTracerAttacker.sanityDamage = attacker.sanityDamage;
                    rangeAttackTracerAttacker.hitFXPrefeb = hitFXPrefeb;
                }
            }
        }
    }
}
