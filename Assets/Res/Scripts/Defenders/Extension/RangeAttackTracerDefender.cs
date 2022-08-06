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
    
        protected virtual void Update()
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
    
        protected virtual void OnDestroy()
        {
            Instantiate(hitFXPrefeb, target.hitPoint);
        }
    }
}
