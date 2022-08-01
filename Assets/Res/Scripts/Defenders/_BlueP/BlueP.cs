using System.Collections.Generic;
using Res.Scripts.Attackers;
using UnityEngine;

namespace Res.Scripts.Defenders._BlueP
{
    public class BlueP : Defender
    {
        public Attacker secondaryTarget;
        public Attacker secondaryTargetToDeal;

        protected override void AttackUpdate()
        {
            currentTarget = GetPriorityTarget(GetAllTargetsInRange());
            secondaryTarget = GetSecondaryTarget();

            if (currentTarget != null)
                targetToDeal = currentTarget;
            if (secondaryTarget != null)
                secondaryTargetToDeal = secondaryTarget;

            if (attackTimer > 0)
                return;

            if (targetToDeal != null && CanAttack())
            {
                if (!targetToDeal.isDead && CheckInRange(targetToDeal.transform))
                {
                    attackTimer = attackTimerStandard;
                    SkillPointOnAttack();
                    animatorManager.PlayTargetAnimation("Attack", true);
                    
                    targetRotation = transform.position.x - targetToDeal.transform.position.x < 0 ? Quaternion.Euler(-90, 180, 0) : Quaternion.identity;
                }
                else
                {
                    //目标死亡时切换目标
                    currentTarget = null;
                }
            }
        }

        private Attacker GetSecondaryTarget()
        {
            List<Attacker> attackers = GetAllTargetsInRange();

            foreach (Attacker attacker in attackers)
            {
                if (attacker != currentTarget)
                {
                    return attacker;
                }
            }

            return null;
        }
    }
}