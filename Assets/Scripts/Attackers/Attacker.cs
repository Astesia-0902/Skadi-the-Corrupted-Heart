using Game_Managers;
using UnityEngine;

namespace Attackers
{
    public class Attacker : MonoBehaviour
    {
        public int nodeIndex;
        public float maxHealth;
        public float currentHealth;
        public float moveSpeed;
        public int id;

        private Transform rangeParent;

        public void Initialize()
        {
            rangeParent = transform.GetChild(1);
            gameObject.SetActive(true);
            currentHealth = maxHealth;
            nodeIndex = 0;
            //TODO:由于有多个出生点，这里可能需要修改
            transform.position = GameLoopManager.NodesPosition[0];
        }
        
        /// <summary>
        /// 检查目标是否在攻击范围内
        /// </summary>
        /// <param name="targetTransform">目标的Transform</param>
        /// <returns>在就返回True，反之False</returns>
        private bool CheckInRange(Transform targetTransform)
        {
            Vector3 targetCenter = targetTransform.position;
            //rangeParent物体下挂载了该单位的攻击范围中每个方块的中点
            for (int i = 0; i < rangeParent.childCount; i++)
            {
                Vector3 rangeCenter = rangeParent.GetChild(i).position;
                if (targetCenter.x < rangeCenter.x + 0.5f && targetCenter.x > rangeCenter.x - 0.5f &&
                    targetCenter.z < rangeCenter.z + 0.5f && targetCenter.z > rangeCenter.z - 0.5f)
                {
                    return true;
                }
            }

            return false;
        }
    }
}