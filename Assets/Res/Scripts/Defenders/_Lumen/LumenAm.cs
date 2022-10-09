using Res.Scripts.Defenders._Lumen;
using Res.Scripts.Defenders.Extension;
using UnityEngine;

namespace Defenders._Lumen
{
    public class LumenAm : AnimatorManagerDefenderHealer
    {
        public GameObject skillHitPrefeb;
        public GameObject skill2CastEffect;
        private HealTracer[] skillTracers;
        private Lumen lumen;

        protected override void Awake()
        {
            base.Awake();
            lumen = GetComponentInParent<Lumen>();
        }

        public void Skill2Effect()
        {
            Instantiate(skill2CastEffect, defender.hitPoint);
        }

        public void HandleSkill2()
        {
            skillTracers = new HealTracer[2];

            for (int i = 0; i < 2; i++)
            {
                if (lumen.skillTargets[i] == null)
                {
                    continue;
                }

                lumen.skillTargets[i].GetHeal(lumen.attackDamage * 2.2f);
                lumen.skillTargets[i].sanityRecoveryTimer = lumen.skillTargets[i].sanityRecoveryThreshold;
                Instantiate(skillHitPrefeb, lumen.skillTargets[i].hitPoint);
            }

            lumen.skillTargets = null;
        }
    }
}