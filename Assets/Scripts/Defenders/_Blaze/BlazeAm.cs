using Attackers;

namespace Defenders._Blaze
{
    public class BlazeAm : AnimatorManagerDefender
    {
        public override void OnAttack()
        {
            foreach (Attacker attacker in Defender.GetAllTargetsInRange())
            {
                if (attacker != null && attacker.isActiveAndEnabled)
                {
                    attacker.TakeDamage(Defender.attackDamage, 0f, 0f);
                }
            }
            //TODO:攻击特效和敌人的受击特效
        }
    }
}
