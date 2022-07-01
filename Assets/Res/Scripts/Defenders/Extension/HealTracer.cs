using UnityEngine;

namespace Defenders.Extension
{
    /// <summary>
    /// ������ҽ�Ƹ�Ա�ķ�������
    /// </summary>
    public class HealTracer : MonoBehaviour
    {
        public bool isElementHeal;
        public Defender healTarget;
        public float healAmount;
        public GameObject healHitPrefeb;

        /// <summary>
        /// ����������Ŀ��ǰ�������Ҽ���Ƿ��Ѿ�����Ŀ��
        /// </summary>
        protected virtual void Update()
        {
            if (healTarget == null)
            {
                Destroy(this.gameObject);
            }
            
            transform.position = Vector3.MoveTowards(transform.position, healTarget.hitPoint.position, 20f * Time.deltaTime);
            if (Vector3.Distance(transform.position, healTarget.hitPoint.position) < 0.1f)
            {
                HealTarget();
                if(isElementHeal)
                    HealTargetSanity(healAmount);
            
                Destroy(this.gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
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
