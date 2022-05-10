using UnityEngine;

namespace Attackers
{
    public class Attacker : MonoBehaviour
    {
        public float maxHealth;
        public float currentHealth;
        public float moveSpeed;
        public int id;

        public void Initialize()
        {
            currentHealth = maxHealth;
        }
    }
}
