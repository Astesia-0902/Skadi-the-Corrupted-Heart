namespace Res.Scripts.Attackers.Ghost
{
    public class GhostAm : AnimatorManagerAttackerRange
    {
        public void OnMelee()
        {
            if (attacker.currentAttackTarget != null)
            {
                attacker.currentAttackTarget.TakeDamage(attacker.attackDamage, attacker.magicDamage, attacker.realDamage);
                attacker.currentAttackTarget.TakeNeuralDamage(attacker.sanityDamage);
                if (hitFXPrefeb != null)
                {
                    Instantiate(hitFXPrefeb, attacker.currentAttackTarget.hitPoint);
                }
            }
        }
    }
}
