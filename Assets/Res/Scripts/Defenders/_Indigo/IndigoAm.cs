using System;
using Defenders;
using Res.Scripts.Defenders.Extension;

namespace Res.Scripts.Defenders._Indigo
{
    public class IndigoAm : AnimatorManagerDefender
    {
        private Indigo indigo;
        private IndigoTracer indigoTracer;

        private void OnEnable()
        {
            indigo = GetComponentInParent<Indigo>();
        }

        public override void OnAttack()
        {
            if (defender.targetToDeal == null || defender.targetToDeal.isDead)
            {
                return;
            }
            
            defender.PlayRandomSFX(defender.attackSFX);
            indigoTracer = Instantiate(tracerFXPrefeb, tracerPivot).GetComponent<IndigoTracer>();
            if (indigoTracer != null)
            {
                if (indigo.imprisonCount >= 5)
                {
                    indigo.imprisonCount = 0;
                    indigoTracer.isImprisoning = true;
                }
                indigoTracer.target = defender.targetToDeal;
                indigoTracer.magicDamage = defender.magicDamage * (indigo.chargeCount + 1);
                indigo.imprisonCount++;
                indigo.imprisonCount += indigo.chargeCount;
                indigo.chargeCount = 0;
                indigoTracer.physicDamage = defender.attackDamage;
                indigoTracer.realDamage = defender.realDamageToDeal;
                indigoTracer.hitFXPrefeb = hitFXPrefeb;
            }
        }

        public void OnCharge()
        {
            indigo.chargeCount++;
            if (indigo.chargeCount >= 3)
            {
                indigo.chargeCount = 3;
            }
        }
    }
}
