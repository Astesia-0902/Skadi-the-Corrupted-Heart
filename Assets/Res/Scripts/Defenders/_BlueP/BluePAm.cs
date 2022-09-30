using Res.Scripts.Defenders._BlueP;
using Res.Scripts.Defenders.Extension;

namespace Defenders._BlueP
{
    public class BluePAm : AnimatorManagerDefender
    {
        public BlueP blueP;
        public RangeAttackTracerDefender secondaryTracerDefender;

        protected override void Awake()
        {
            base.Awake();
            blueP = GetComponentInParent<BlueP>();
        }

        public override void OnAttack()
        {
            //TODO:攻击特效和敌人的受击特效
            if (defender.targetToDeal != null)
            {
                if (defender.isRange)
                {
                    if (defender.skillReady)
                    {
                        defender.skillReady = false;
                        defender.skillPoint = 0;
                        
                        rangeAttackTracerDefender = Instantiate(tracerFXPrefeb, tracerPivot).GetComponent<RangeAttackTracerDefender>();
                        if (rangeAttackTracerDefender != null)
                        {
                            rangeAttackTracerDefender.target = defender.targetToDeal;
                            rangeAttackTracerDefender.magicDamage = defender.magicDamage;
                            rangeAttackTracerDefender.physicDamage = defender.attackDamage * 1.5f;
                            rangeAttackTracerDefender.realDamage = defender.realDamageToDeal;
                            rangeAttackTracerDefender.hitFXPrefeb = hitFXPrefeb;
                        }

                        if (blueP.secondaryTarget != null)
                        {
                            secondaryTracerDefender = Instantiate(tracerFXPrefeb, tracerPivot)
                                .GetComponent<RangeAttackTracerDefender>();
                            if (secondaryTracerDefender != null)
                            {
                                secondaryTracerDefender.target = blueP.secondaryTargetToDeal;
                                secondaryTracerDefender.magicDamage = defender.magicDamage;
                                secondaryTracerDefender.physicDamage = defender.attackDamage * 1.5f;
                                secondaryTracerDefender.realDamage = defender.realDamageToDeal;
                                secondaryTracerDefender.hitFXPrefeb = hitFXPrefeb;
                            }
                        }

                        return;
                    }
                    
                    rangeAttackTracerDefender = Instantiate(tracerFXPrefeb, tracerPivot).GetComponent<RangeAttackTracerDefender>();
                    if (rangeAttackTracerDefender != null)
                    {
                        rangeAttackTracerDefender.target = defender.targetToDeal;
                        rangeAttackTracerDefender.magicDamage = defender.magicDamage;
                        rangeAttackTracerDefender.physicDamage = defender.attackDamage;
                        rangeAttackTracerDefender.realDamage = defender.realDamageToDeal;
                        rangeAttackTracerDefender.hitFXPrefeb = hitFXPrefeb;
                    }
                }
            }
        }
    }
}
