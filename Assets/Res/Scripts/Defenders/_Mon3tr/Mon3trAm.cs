using UnityEngine;

namespace Defenders._Mon3tr
{
    public class Mon3trAm : AnimatorManagerDefender
    {
        public GameObject skillAttackEffect;

        public void AttackEffect()
        {
            Instantiate(skillAttackEffect, transform);
        }
    }
}
