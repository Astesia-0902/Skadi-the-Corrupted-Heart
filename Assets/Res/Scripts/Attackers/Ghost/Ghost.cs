using Res.Scripts.Defenders;

namespace Res.Scripts.Attackers.Ghost
{
    public class Ghost : AttackerRange
    {
        protected override void AttackUpdate()
        {
            if (!CanAttack())
                return;

            currentAttackTarget = GetPriorityTarget();

            if (attackTimer > 0)
                return;

            if (currentAttackTarget != null && !currentAttackTarget.isDead)
            {
                attackTimer = attackTimerStandard;
                animatorManager.PlayTargetAnimation(isBlocked ? "Attack_2" : "Attack", true);
                RefreshRotation();
            }
        }
    }
}
