using Res.Scripts.Attackers.Extension;
using Res.Scripts.Defenders;

namespace Res.Scripts.Attackers.Launcher
{
    public class LauncherAm : AnimatorManagerAttackerRange
    {
        private Launcher launcher;

        protected override void Awake()
        {
            base.Awake();
            launcher = GetComponentInParent<Launcher>();
        }

        public override void OnAttack()
        {
            if (launcher.targetsToDeal != null)
            {
                for (int i = 0; i < launcher.targetsToDeal.Count; i++)
                {
                    Defender currentTarget = launcher.targetsToDeal[i];
                    if (currentTarget == null || currentTarget.isDead)
                        continue;

                    rangeAttackTracerAttacker =
                        Instantiate(tracerFXPrefeb, tracerPivot).GetComponent<RangeAttackTracerAttacker>();

                    if (rangeAttackTracerAttacker != null)
                    {
                        rangeAttackTracerAttacker.target = currentTarget;
                        rangeAttackTracerAttacker.magicDamage = attacker.magicDamage;
                        rangeAttackTracerAttacker.realDamage = attacker.realDamage;
                        rangeAttackTracerAttacker.physicDamage = attacker.attackDamage;
                        rangeAttackTracerAttacker.sanityDamage = attacker.sanityDamage;
                        rangeAttackTracerAttacker.hitFXPrefeb = hitFXPrefeb;
                    }
                }

                launcher.targetsToDeal.Clear();
            }
        }
    }
}