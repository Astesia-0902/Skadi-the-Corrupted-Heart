using Defenders;
using UnityEngine;

namespace Res.Scripts.Defenders._Mon3tr
{
    public class Mon3TRAm : AnimatorManagerDefender
    {
        public GameObject skillAttackEffect;

        public void AttackEffect()
        {
            Instantiate(skillAttackEffect, transform);
        }
        
    }
}