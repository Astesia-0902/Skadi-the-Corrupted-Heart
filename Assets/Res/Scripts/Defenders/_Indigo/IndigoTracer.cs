using Res.Scripts.Defenders.Extension;
using UnityEngine;

namespace Res.Scripts.Defenders._Indigo
{
    public class IndigoTracer : RangeAttackTracerDefender
    {
        public bool isImprisoning;

        protected override void Update()
        {
            if (target == null)
            {
                Destroy(this.gameObject);
            }
            
            transform.position = Vector3.MoveTowards(transform.position, target.hitPoint.position, 20f * Time.deltaTime);
            if (Vector3.Distance(transform.position, target.hitPoint.position) < 0.1f)
            {
                if (isImprisoning)
                {
                    target.GetImprisoned(4f);
                }
                target.TakeDamage(physicDamage, magicDamage, realDamage);
                Destroy(this.gameObject);
            }
        }
    }
}
