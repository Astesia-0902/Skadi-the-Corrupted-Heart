using Defenders;
using Res.Scripts.Defenders.Extension;

namespace Res.Scripts.Defenders._Thorns
{
    public class ThornsAm : AnimatorManagerDefender
    {
        public void OnAttackRange()
        {
            if (defender.targetToDeal == null || defender.targetToDeal.isDead)
                return;
            
            defender.PlayRandomSFX(defender.attackSFX);
            rangeAttackTracerDefender = Instantiate(tracerFXPrefeb, tracerPivot).GetComponent<RangeAttackTracerDefender>();
            if (rangeAttackTracerDefender != null)
            {
                rangeAttackTracerDefender.target = defender.targetToDeal;
                rangeAttackTracerDefender.magicDamage = 0;
                rangeAttackTracerDefender.physicDamage = defender.attackDamage * 0.8f;
                rangeAttackTracerDefender.realDamage = 0;
                rangeAttackTracerDefender.hitFXPrefeb = hitFXPrefeb;
            }
        }
    }
}
