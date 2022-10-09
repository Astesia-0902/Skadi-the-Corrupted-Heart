using Res.Scripts.Attackers;
using UnityEngine;

namespace Res.Scripts.Defenders.Extension
{
    /// <summary>
    /// 防守方远程攻击的投射物
    /// </summary>
    public class RangeAttackTracerDefender : MonoBehaviour
    {
        public Attacker target;
        public float physicDamage;
        public float magicDamage;
        public float realDamage;

        public GameObject hitFXPrefeb;

        public Vector3 targetPosition;

        protected virtual void Update()
        {
            GuideToTarget();
        }

        private void GuideToTarget()
        {
            if (target != null && !target.isDead && target.gameObject.activeInHierarchy)
            {
                targetPosition = target.hitPoint.position;
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 20f * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                if (target != null && !target.isDead && target.gameObject.activeInHierarchy)
                    target.TakeDamage(physicDamage, magicDamage, realDamage);
                Destroy(this.gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            Instantiate(hitFXPrefeb, target.hitPoint);
        }
    }
}