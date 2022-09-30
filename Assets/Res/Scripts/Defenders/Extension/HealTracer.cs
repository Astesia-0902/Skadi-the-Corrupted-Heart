using UnityEngine;

namespace Res.Scripts.Defenders.Extension
{
    /// <summary>
    /// 挂载在医疗干员的发射物上
    /// </summary>
    public class HealTracer : MonoBehaviour
    {
        public bool isElementHeal;
        public Defender healTarget;
        public float healAmount;
        public GameObject healHitPrefeb;

        /// <summary>
        /// 持续向治疗目标前进，并且检测是否已经到达目标
        /// </summary>
        protected virtual void Update()
        {
            if (healTarget == null)
            {
                Destroy(this.gameObject);
            }

            transform.position =
                Vector3.MoveTowards(transform.position, healTarget.hitPoint.position, 20f * Time.deltaTime);
            if (Vector3.Distance(transform.position, healTarget.hitPoint.position) < 0.01f)
            {
                HealTarget();
                if (isElementHeal)
                    HealTargetSanity(healAmount);

                Destroy(this.gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (healTarget != null)
                Instantiate(healHitPrefeb, healTarget.hitPoint);
        }

        public void HealTarget()
        {
            healTarget.GetHeal(healAmount);
        }

        public void HealTargetSanity(float value)
        {
            healTarget.TakeNeuralHeal(value * 0.5f);
        }
    }
}