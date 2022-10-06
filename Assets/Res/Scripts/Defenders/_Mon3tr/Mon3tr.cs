using System.Security.Cryptography;
using Res.Scripts.Defenders._Kalts;
using UnityEngine;

namespace Res.Scripts.Defenders._Mon3tr
{
    public class Mon3TR : Defender
    {
        private Kalts kalts;
        public float additionalAttack = 130;
        public bool isSkillOn;
        public float skillTimer;

        private GameObject mySkillEffect;
        public GameObject skillEffect;

        private void OnEnable()
        {
            kalts = FindObjectOfType<Kalts>();
            kalts.FindMons3TR();
        }

        protected override void Update()
        {
            base.Update();
            SkillUpdate();
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
                    PlayRandomSFX(attackSFX);
                    animatorManager.PlayTargetAnimation(isSkillOn ? "Skill_2" : "Attack", true, attackAnimationSpeed);
                    RefreshRotation();
                }
                else
                {
                    currentTarget = null;
                }
            }
        }

        public void CastSkill()
        {
            if (isSkillOn)
                return;

            if (mySkillEffect == null)
            {
                mySkillEffect = Instantiate(skillEffect, transform);
            }
            isSkillOn = true;
            armor += armor;
            additionalAttack = 1.3f * attackDamage;
            attackDamage += additionalAttack;
            realDamageToDeal = attackDamage;
            attackDamage = 0;
        }

        private void SkillUpdate()
        {
            if (!isSkillOn)
                return;
            skillTimer += Time.deltaTime;
            
            if (skillTimer >= 20f)
            {
                SkillEnd();
            }

            realDamageToDeal -= additionalAttack * (0.05f * Time.deltaTime);
        }

        private void SkillEnd()
        {
            if (mySkillEffect != null)
            {
                Destroy(mySkillEffect);
            }
            
            skillTimer = 0;
            attackDamage = realDamageToDeal;
            realDamageToDeal = 0;
            isSkillOn = false;
            kalts.SkillEnd();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            kalts.SkillEnd();
        }
    }
}