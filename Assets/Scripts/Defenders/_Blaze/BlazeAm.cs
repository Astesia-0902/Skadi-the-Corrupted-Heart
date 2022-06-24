using Attackers;

namespace Defenders._Blaze
{
    public class BlazeAm : AnimatorManagerDefender
    {
        public override void OnAttack()
        {
            foreach (Attacker attacker in defender.GetAllTargetsInRange())
            {
                if (attacker != null && attacker.isActiveAndEnabled)
                {
                    attacker.TakeDamage(defender.attackDamage, 0f, 0f);
                }
            }
            //TODO:攻击特效和敌人的受击特效
        }
    }
}
