using System.Collections.Generic;
using Defenders;
using Res.Scripts.Attackers;

namespace Res.Scripts.Defenders._Blaze
{
    public class BlazeAm : AnimatorManagerDefender
    {
        public override void OnAttack()
        {
            List<Attacker> attackersInRange = defender.GetAllTargetsInRange();
            for (int i = 0; i < attackersInRange.Count; i++)
            {
                if (attackersInRange[i] != null && attackersInRange[i].isActiveAndEnabled)
                {
                    if (hitFXPrefeb != null)
                    {
                        Instantiate(hitFXPrefeb, attackersInRange[i].hitPoint);
                    }
                    attackersInRange[i].TakeDamage(defender.attackDamage, 0f, 0f);
                }
            }
            //TODO:攻击特效和敌人的受击特效
        }
    }
}
