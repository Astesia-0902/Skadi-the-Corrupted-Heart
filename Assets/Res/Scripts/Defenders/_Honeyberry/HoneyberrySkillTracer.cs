using Res.Scripts.Defenders.Extension;
using UnityEngine;

namespace Res.Scripts.Defenders._Honeyberry
{
    public class HoneyberrySkillTracer : HealTracer
    {
        private bool sanityRecovery;
        private float recoveryTimer;
        
        protected override void Update()
        {
            if (healTarget != null)
            {
                target = healTarget.transform.position;
            }

            transform.position =
                Vector3.MoveTowards(transform.position, target, 20f * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                if (!sanityRecovery && healTarget != null && !healTarget.isDead)
                {
                    HealTarget();
                    sanityRecovery = true;
                    if (healTarget != null)
                        Instantiate(healHitPrefeb, healTarget.hitPoint);
                }

                SkillHeal();
            }
        }

        private void SkillHeal()
        {
            if (!sanityRecovery)
                return;

            if (healTarget != null && !healTarget.isDead)
            {
                HealTargetSanity(healAmount * Time.deltaTime);
                recoveryTimer += Time.deltaTime;
                if (recoveryTimer >= 2f)
                    Destroy(this.gameObject);
            }
        }

        protected override void OnDestroy()
        {
            
        }
    }
}
