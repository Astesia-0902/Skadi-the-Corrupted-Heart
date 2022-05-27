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

            if (currentTarget != null && !isInteracting)
            {
                if (!currentTarget.isDead)
                {
                    if (currentTarget.transform.position.z < transform.position.z)
                    {
                        AnimatorManager.PlayTargetAnimation("Attack_Down");
                    }
                    else
                    {
                        AnimatorManager.PlayTargetAnimation("Attack");
                    }
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
