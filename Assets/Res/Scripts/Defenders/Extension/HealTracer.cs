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
        protected Vector3 target;

        /// <summary>
        /// 持续向治疗目标前进，并且检测是否已经到达目标
        /// </summary>
        protected virtual void Update()
        {
            if (healTarget != null && !healTarget.isDead)
            {
                target = healTarget.transform.position;
            }

            transform.position =
                Vector3.MoveTowards(transform.position, target, 20f * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, target) < 0.01f)
            {
                if (healTarget != null && !healTarget.isDead)
                {
                    HealTarget();
                    if (isElementHeal)
                        HealTargetSanity(healAmount);
                }

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
            if (healTarget != null && !healTarget.isDead)
                healTarget.GetHeal(healAmount);
        }

        public void HealTargetSanity(float value)
        {
            if (healTarget != null && !healTarget.isDead)
                healTarget.TakeNeuralHeal(value * 0.5f);
        }
    }
}