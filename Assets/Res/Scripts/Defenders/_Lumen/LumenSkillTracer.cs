using Defenders.Extension;
using UnityEngine;

namespace Defenders._Lumen
{
    public class LumenSkillTracer : HealTracer
    {
        protected override void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, healTarget.hitPoint.position, 20f * Time.deltaTime);
            if (Vector3.Distance(transform.position, healTarget.hitPoint.position) < 0.1f)
            {
                HealTarget();
                healTarget.sanityRecoveryTimer = healTarget.sanityRecoveryThreshold;
                Destroy(this.gameObject);
            }
        }
    }
}
