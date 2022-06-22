using System.Collections.Generic;
using Attackers;

namespace Defenders._BlueP
{
    public class BlueP : Defender
    {
        public Attacker secondaryTarget;
        
        protected override void AttackUpdate()
        {
            currentTarget = GetPriorityTarget(GetAllTargetsInRange());
            secondaryTarget = GetSecondaryTarget();

            if (AttackTimer > 0)
                return;

            if (currentTarget != null && CanAttack())
            {
                if (!currentTarget.isDead)
                {
                    AttackTimer = attackTimerStandard;
                    AnimatorManager.PlayTargetAnimation("Attack", true);
                }
                else
                {
                    //目标死亡时切换目标
                    currentTarget = null;
                }
            }
        }

        private Attacker GetSecondaryTarget()
        {
            List<Attacker> attackers = GetAllTargetsInRange();
            
            foreach (Attacker attacker in attackers)
            {
                if (attacker != currentTarget)
                {
                    return attacker;
                }
            }

            return null;
        }
    }
}
