using UnityEngine;

namespace Defenders._Mon3tr
{
    public class Mon3trAm : AnimatorManagerDefender
    {
        public GameObject skillAttackEffect;

        public void AttackEffect()
        {
            Instantiate(skillAttackEffect, transform);
        }

        public override void OnAttack()
        {
            if (hitFXPrefeb != null)
            {
                Instantiate(hitFXPrefeb, defender.targetToDeal.hitPoint);
            }

            defender.targetToDeal.TakeDamage(defender.attackDamage, defender.magicDamage,
                defender.realDamageToDeal);
        }
    }
}