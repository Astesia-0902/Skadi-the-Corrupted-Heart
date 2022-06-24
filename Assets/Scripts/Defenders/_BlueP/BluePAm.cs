namespace Defenders._BlueP
{
    public class BluePAm : AnimatorManagerDefender
    {
        public BlueP blueP;
        public RangeAttackTracer secondaryTracer;

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
                        
                        rangeAttackTracer = Instantiate(tracerFXPrefeb, tracerPivot).GetComponent<RangeAttackTracer>();
                        if (rangeAttackTracer != null)
                        {
                            rangeAttackTracer.target = defender.targetToDeal;
                            rangeAttackTracer.magicDamage = defender.magicDamage;
                            rangeAttackTracer.physicDamage = defender.attackDamage * 1.5f;
                            rangeAttackTracer.realDamage = defender.realDamageToDeal;
                            rangeAttackTracer.hitFXPrefeb = hitFXPrefeb;
                        }

                        if (blueP.secondaryTarget != null)
                        {
                            secondaryTracer = Instantiate(tracerFXPrefeb, tracerPivot)
                                .GetComponent<RangeAttackTracer>();
                            if (secondaryTracer != null)
                            {
                                secondaryTracer.target = blueP.secondaryTargetToDeal;
                                secondaryTracer.magicDamage = defender.magicDamage;
                                secondaryTracer.physicDamage = defender.attackDamage * 1.5f;
                                secondaryTracer.realDamage = defender.realDamageToDeal;
                                secondaryTracer.hitFXPrefeb = hitFXPrefeb;
                            }
                        }

                        return;
                    }
                    
                    rangeAttackTracer = Instantiate(tracerFXPrefeb, tracerPivot).GetComponent<RangeAttackTracer>();
                    if (rangeAttackTracer != null)
                    {
                        rangeAttackTracer.target = defender.targetToDeal;
                        rangeAttackTracer.magicDamage = defender.magicDamage;
                        rangeAttackTracer.physicDamage = defender.attackDamage;
                        rangeAttackTracer.realDamage = defender.realDamageToDeal;
                        rangeAttackTracer.hitFXPrefeb = hitFXPrefeb;
                    }
                }
            }
        }
    }
}
