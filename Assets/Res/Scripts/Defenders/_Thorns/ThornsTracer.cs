using Res.Scripts.Defenders.Extension;
using UnityEngine;

namespace Res.Scripts.Defenders._Thorns
{
    public class ThornsTracer : RangeAttackTracerDefender
    {
        public GameObject nerualDamagePrefeb;
        protected override void Update()
        {
            if (target == null)
            {
                Destroy(this.gameObject);
            }
            
            transform.position = Vector3.MoveTowards(transform.position, target.hitPoint.position, 20f * Time.deltaTime);
            if (Vector3.Distance(transform.position, target.hitPoint.position) < 0.1f)
            {
                target.TakeDamage(physicDamage, magicDamage, realDamage);
                Destroy(this.gameObject);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Instantiate(nerualDamagePrefeb, target.hitPoint);
        }
    }
}
