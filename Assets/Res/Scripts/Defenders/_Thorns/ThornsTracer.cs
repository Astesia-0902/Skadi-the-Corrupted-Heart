using Res.Scripts.Defenders.Extension;
using UnityEngine;

namespace Res.Scripts.Defenders._Thorns
{
    public class ThornsTracer : RangeAttackTracerDefender
    {
        public GameObject nerualDamagePrefeb;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Instantiate(nerualDamagePrefeb, target.hitPoint);
        }
    }
}
