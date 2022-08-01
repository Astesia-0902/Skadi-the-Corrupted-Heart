using System.Collections.Generic;
using Attackers;
using Game_Managers;
using Res.Scripts.Attackers;
using Res.Scripts.Defenders;
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

        protected override void AttackUpdate()
        {
            currentTarget = GetPriorityTarget(GetAllTargetsInRange());

            if (currentTarget != null)
                targetToDeal = currentTarget;

            if (attackTimer > 0)
                return;

            if (targetToDeal != null && CanAttack())
            {
                if (!targetToDeal.isDead && CheckInRange(targetToDeal.transform))
                {
                    attackTimer = attackTimerStandard;

                    float attackAnimationSpeed = attackTimerStandard < 1f ? 1 / attackTimerStandard : 1f;

                    animatorManager.PlayTargetAnimation(
                        Mathf.Abs(targetToDeal.transform.position.z - transform.position.z) > 0.2f
                            ? "Attack_Down"
                            : "Attack", true, attackAnimationSpeed);
                }
                else
                {
                    //目标死亡时切换目标
                    currentTarget = null;
                }
            }
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
            
            onHealthChanged.Invoke(currentHealth, maxHealth);
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
                standTimer = 0f;

                foreach (Attacker attacker in EntitySummoner.Instance.attackersInGame)
                {
                    attacker.moveSpeed = attacker.standardMoveSpeed;
                }
            }
        }

        private void StandSkill()
        {
            if (!isStand || isHenshin)
                return;

            foreach (Attacker attacker in EntitySummoner.Instance.attackersInGame)
            {
                if (CheckInSkillRange(attacker.transform))
                {
                    attacker.TakeDamage(0, skillDps * Time.deltaTime, 0);
                    attacker.moveSpeed = attacker.standardMoveSpeed * 0.3f;
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
            skillPoint = 0;
            skillReady = false;
            attackDamage *= 1.5f;
            attackTimerStandard /= 3f;

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
                attackDamage /= 1.5f;
                attackTimerStandard *= 3f;
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

        public override bool CanAttack()
        {
            return !(isStunned || isDead || isInteracting || isStand || isHenshin);
        }

        public override bool CanBlock()
        {
            return !(isStunned || isDead || isStand || isHenshin);
        }
    }
}
