using Res.Scripts.Defenders.Extension;
using UnityEngine;

namespace Res.Scripts.Defenders._Indigo
{
    public class IndigoTracer : RangeAttackTracerDefender
    {
        public bool isImprisoning;

        protected override void Update()
        {
            if (target != null && !target.isDead && target.gameObject.activeInHierarchy)
            {
                targetPosition = target.hitPoint.position;
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 20f * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                if (isImprisoning)
                {
                    if (target != null && !target.isDead && target.gameObject.activeInHierarchy)
                        target.GetImprisoned(4f);
                }

                if (target != null && !target.isDead && target.gameObject.activeInHierarchy)
                    target.TakeDamage(physicDamage, magicDamage, realDamage);
                Destroy(this.gameObject);
            }
        }
    }
}