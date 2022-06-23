using System.Collections.Generic;
using Attackers;
using Game_Managers;
using UnityEngine;

namespace Defenders._Specter_the_Unchained
{
    public class Specter : Defender
    {
        public float skillDps;
        
        private bool skillOn;
        private float skillTimer;
        private bool isStand;
        public Transform skillRange;
        public bool isHenshin;
        private static readonly int IsHenshin = Animator.StringToHash("isHenshin");

        protected override void Update()
        {
            base.Update();
            SkillUpdate();
            StandSkill();
            StandTimer();
            isHenshin = animatorManager.anim.GetBool(IsHenshin);
        }

        public override void TakeDamage(float physicDamage1, float magicDamage1, float realDamage1)
        {
            if (isHenshin)
                return;
            
            currentHealth -= physicDamage1 - armor > 0.05f * physicDamage1
                ? physicDamage1 - armor
                : 0.05f * physicDamage1;
            currentHealth -= magicDamage1 * (1 - magicResistance);
            currentHealth -= realDamage1;

            if (skillOn)
            {
                if (currentHealth <= 0)
                {
                    currentHealth = 1f;
                }
            }

            onHealthChanged.Invoke(currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                if (isStand)
                {
                    Die();
                }
                else
                {
                    isStand = true;
                    Unblock();
                    blockNumStandard = 0;
                    currentHealth = maxHealth;
                    animatorManager.PlayTargetAnimation("Die", true);
                    animatorManager.anim.SetBool(IsHenshin, true);
                }
            }
        }

        private float standTimer;
        private void StandTimer()
        {
            if (!isStand)
                return;

            standTimer += Time.deltaTime;
            if (standTimer >= 20f)
            {
                isStand = false;
                blockNumStandard = 2;
                animatorManager.anim.SetBool(IsHenshin, true);
                animatorManager.PlayTargetAnimation("Die_B", true);
            }
        }

        private void StandSkill()
        {
            if (!isStand)
                return;

            List<Attacker> attackers = new List<Attacker>();

            foreach (Attacker attacker in EntitySummoner.Instance.attackersInGame)
            {
                if (CheckInSkillRange(attacker.transform))
                {
                    attacker.TakeDamage(0, skillDps * Time.deltaTime, 0);
                    attacker.moveSpeed = attacker.standardMoveSpeed * 0.6f;
                }
                else
                {
                    attacker.moveSpeed = attacker.standardMoveSpeed;
                }
            }

        }

        private bool CheckInSkillRange(Transform targetTransform)
        {
            Vector3 targetCenter = targetTransform.position;
            //rangeParent物体下挂载了该单位的攻击范围中每个方块的中点
            for (int i = 0; i < skillRange.childCount; i++)
            {
                Vector3 rangeCenter = skillRange.GetChild(i).position;
                if (targetCenter.x < rangeCenter.x + 0.5f && targetCenter.x > rangeCenter.x - 0.5f &&
                    targetCenter.z < rangeCenter.z + 0.5f && targetCenter.z > rangeCenter.z - 0.5f)
                {
                    return true;
                }
            }
            return false;
        }

        public void CastSkill()
        {
            if (skillReady == false || isStand)
                return;

            skillOn = true;
            attackDamage *= 1.2f;
            attackTimerStandard /= 2f;

        }

        private void SkillUpdate()
        {
            if (!skillOn)
            {
                skillTimer = 0f;
                return;
            }

            skillTimer += Time.deltaTime;
            
            if (skillTimer >= 20f)
            {
                skillOn = false;
                attackDamage /= 1.2f;
                attackTimerStandard *= 2f;
            }

        }

        public override void SkillPointUpdate()
        {
            if (isStand)
            {
                skillPoint = 0;
                return;
            }
            
            base.SkillPointUpdate();
        }

        protected override void Die()
        {
            isDead = true;
            GameManager.Instance.RemoveDefender(this);
            animatorManager.PlayTargetAnimation("Die_B_2", true);
            Unblock();
        }

        protected override void NeuralDamageBurst()
        {
            if (isHenshin)
                return;
            
            isStunned = true;
            afterBurst = true;
            Unblock();
            animatorManager.SetAnimatorBool("isStunned", isStunned);
            TakeDamage(0, 0, 1000f);
            animatorManager.PlayTargetAnimation(isStand ? "Stun_B" : "Stun", true);
        }
    }
}
