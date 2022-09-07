using System;
using Res.Scripts.Game_Managers;
using UnityEngine;

namespace Res.Scripts.Attackers.Reaper
{
    public class Reaper : Attacker
    {
        public bool isTriggered;

        private void OnEnable()
        {
            isTriggered = false;
        }

        protected override void Update()
        {
            base.Update();
            CheckRampage();
            RampageUpdate();
        }

        private void CheckRampage()
        {
            if (isTriggered)
                return;

            if (currentHealth < maxHealth)
            {
                isTriggered = true;
                animatorManager.PlayTargetAnimation("Skill_Begin", true);
                moveSpeed *= 5;
            }
        }

        private float damageTimer;

        private void RampageUpdate()
        {
            if (!isTriggered)
                return;

            damageTimer += Time.deltaTime;
            if (damageTimer >= 1f)
            {
                damageTimer = 0f;
                RampageDamage();
                currentHealth -= 0.04f * maxHealth;
            }
        }

        private void RampageDamage()
        {
            int count = GameManager.Instance.defendersInGame.Count;
            for (int i = 0; i < count; i++)
            {
                if (CheckRange(GameManager.Instance.defendersInGame[i].transform))
                {
                    GameManager.Instance.defendersInGame[i].TakeNeuralDamage((int) (0.2 * attackDamage));
                }
            }
        }

        private bool CheckRange(Transform targetTransform)
        {
            Vector3 myPosition = transform.position;
            Vector3 targetPosition = targetTransform.position;

            float distance = Mathf.Sqrt((myPosition.x - targetPosition.x) * (myPosition.x - targetPosition.x) +
                                        (myPosition.z - targetPosition.z) * (myPosition.z - targetPosition.z));

            if (distance <= 2.5f)
            {
                return true;
            }

            return false;
        }

        protected override void AttackUpdate()
        {
            if (!isTriggered)
                return;
            
            base.AttackUpdate();
        }
    }
}