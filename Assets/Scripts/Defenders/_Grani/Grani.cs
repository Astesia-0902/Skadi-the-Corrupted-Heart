using UnityEngine;

namespace Defenders._Grani
{
    public class Grani : Defender
    {
        protected override void AttackUpdate()
        {
            if(isDead)
                return;

            currentTarget = GetPriorityTarget(GetAllTargetsInRange());

            if (AttackTimer > 0)
                return;

            if (currentTarget != null && CanAttack())
            {
                if (!currentTarget.isDead)
                {
                    AttackTimer = attackTimerStandard;
                    AnimatorManager.PlayTargetAnimation(
                        Mathf.Abs(currentTarget.transform.position.z - transform.position.z) > 0.2f
                            ? "Attack_Down"
                            : "Attack", true);
                }
                else
                {
                    //目标死亡时切换目标
                    currentTarget = null;
                }
            }
        }
    }
}
