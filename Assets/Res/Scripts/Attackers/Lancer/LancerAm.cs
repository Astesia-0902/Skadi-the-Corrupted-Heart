namespace Res.Scripts.Attackers.Lancer
{
    public class LancerAm : AttackerRangeAm
    {
        public override void OnAttack()
        {
            if (attacker.currentAttackTarget != null)
            {
                attacker.currentAttackTarget.TakeDamage(attacker.attackDamage, attacker.magicDamage,
                    attacker.realDamage);
            }
        }
    }
}
