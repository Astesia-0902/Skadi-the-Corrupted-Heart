using Res.Scripts.Defenders;
using UnityEngine;

namespace Res.Scripts.Attackers.Extension
{
    public class RangeAttackTracerAttacker : MonoBehaviour
    {
        public Defender target;
        public float physicDamage;
        public float magicDamage;
        public float realDamage;
        public int sanityDamage;

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
                target.TakeNeuralDamage(sanityDamage);
                Destroy(this.gameObject);
            }
        }
    
        protected virtual void OnDestroy()
        {
            Instantiate(hitFXPrefeb, target.hitPoint);
        }
    }
}
