using UnityEngine;

namespace Defenders._Lumen
{
    public class LumenAm : DefenderHealerAnimatorManager
    {
        [SerializeField] private GameObject skillTracerPrefeb;
        [SerializeField] private GameObject skillHitPrefeb;
        private HealTracer[] skillTracers;
        private Lumen lumen;

        protected override void Awake()
        {
            base.Awake();
            lumen = GetComponentInParent<Lumen>();
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
                skillTracers[i] = Instantiate(skillTracerPrefeb, healTracerPivot).GetComponent<HealTracer>();
                skillTracers[i].healTarget = lumen.skillTargets[i];
                skillTracers[i].healAmount = lumen.attackDamage * 2.2f;
                skillTracers[i].healHitPrefeb = skillHitPrefeb;
            }

            skillTracers = null;
            lumen.skillTargets = null;
        }
    }
}
