using Defenders.Extension;
using UnityEngine;

namespace Defenders._Honeyberry
{
    public class HoneyberrySkillTracer : HealTracer
    {
        private bool sanityRecovery;
        private float recoveryTimer;
        
        protected override void Update()
        {
            if (healTarget == null || healTarget.isDead)
                Destroy(gameObject);
            
            transform.position = Vector3.MoveTowards(transform.position, healTarget.hitPoint.position, 20f * Time.deltaTime);
            if (Vector3.Distance(transform.position, healTarget.hitPoint.position) < 0.1f)
            {
                if (!sanityRecovery)
                {
                    HealTarget();
                    sanityRecovery = true;
                    Instantiate(healHitPrefeb, healTarget.hitPoint);
                }

                SkillHeal();
            }
        }

        private void SkillHeal()
        {
            if (!sanityRecovery)
                return;

            HealTargetSanity(healAmount * Time.deltaTime);
            recoveryTimer += Time.deltaTime;
            if(recoveryTimer>=2f)
                Destroy(this.gameObject);
        }

        protected override void OnDestroy()
        {
            
        }
    }
}
