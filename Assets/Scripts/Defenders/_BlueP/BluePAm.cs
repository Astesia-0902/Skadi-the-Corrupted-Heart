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
            if (Defender.currentTarget != null)
            {
                if (Defender.isRange)
                {
                    if (Defender.skillReady)
                    {
                        Defender.skillReady = false;
                        Defender.skillPoint = 0;
                        
                        rangeAttackTracer = Instantiate(tracerFXPrefeb, tracerPivot).GetComponent<RangeAttackTracer>();
                        if (rangeAttackTracer != null)
                        {
                            rangeAttackTracer.target = Defender.currentTarget;
                            rangeAttackTracer.magicDamage = Defender.magicDamage;
                            rangeAttackTracer.physicDamage = Defender.attackDamage * 1.5f;
                            rangeAttackTracer.realDamage = Defender.realDamageToDeal;
                            rangeAttackTracer.hitFXPrefeb = hitFXPrefeb;
                        }

                        if (blueP.secondaryTarget != null)
                        {
                            secondaryTracer = Instantiate(tracerFXPrefeb, tracerPivot)
                                .GetComponent<RangeAttackTracer>();
                            if (secondaryTracer != null)
                            {
                                secondaryTracer.target = Defender.currentTarget;
                                secondaryTracer.magicDamage = Defender.magicDamage;
                                secondaryTracer.physicDamage = Defender.attackDamage * 1.5f;
                                secondaryTracer.realDamage = Defender.realDamageToDeal;
                                secondaryTracer.hitFXPrefeb = hitFXPrefeb;
                            }
                        }
                    }
                    
                    rangeAttackTracer = Instantiate(tracerFXPrefeb, tracerPivot).GetComponent<RangeAttackTracer>();
                    if (rangeAttackTracer != null)
                    {
                        rangeAttackTracer.target = Defender.currentTarget;
                        rangeAttackTracer.magicDamage = Defender.magicDamage;
                        rangeAttackTracer.physicDamage = Defender.attackDamage;
                        rangeAttackTracer.realDamage = Defender.realDamageToDeal;
                        rangeAttackTracer.hitFXPrefeb = hitFXPrefeb;
                    }
                }
            }
        }
    }
}
