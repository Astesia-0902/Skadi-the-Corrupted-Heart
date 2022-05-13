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

        public void Initialize()
        {
            gameObject.SetActive(true);
            currentHealth = maxHealth;
            nodeIndex = 0;
            //TODO:由于有多个出生点，这里可能需要修改
            transform.position = GameLoopManager.NodesPosition[0];
        }
    }
}