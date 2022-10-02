using Attackers;
using Defenders;
using Res.Scripts.Attackers;
using UnityEngine;

namespace Res.Scripts.Defenders._Grani
{
    public class GraniAm : AnimatorManagerDefender
    {
        private Grani grani;
        private static readonly int Multiplier = Animator.StringToHash("Multiplier");
        private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

        protected override void Awake()
        {
            base.Awake();
            grani = GetComponentInParent<Grani>();
        }

        private void Update()
        {
            anim.SetFloat(Multiplier, grani.attackAnimationSpeed);
            grani.isAttacking = anim.GetBool(IsAttacking);
        }

        public override void OnAttack()
        {
            if (grani.isSkillOn)
            {
                foreach (Attacker attacker in defender.GetAllTargetsInRange())
                {
                    if (attacker != null && attacker.isActiveAndEnabled)
                    {
                        if (hitFXPrefeb != null)
                        {
                            Instantiate(hitFXPrefeb, defender.targetToDeal.hitPoint);
                        }
                        attacker.TakeDamage(defender.attackDamage, 0f, 0f);
                        Instantiate(hitFXPrefeb, attacker.hitPoint);
                    }
                }
            }
            else
            {
                defender.targetToDeal.TakeDamage(defender.attackDamage, defender.magicDamage,
                    defender.realDamageToDeal);
                if (hitFXPrefeb != null)
                {
                    Instantiate(hitFXPrefeb, defender.targetToDeal.hitPoint);
                }
            }
        }

        public void RefreshRotation()
        {
            defender.RefreshRotation();
        }
    }
}
