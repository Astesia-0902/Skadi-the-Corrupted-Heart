using System;
using Game_Managers;
using UnityEngine;

namespace Res.Scripts.Attackers.Skadi
{
    public class Skadi : Attacker
    {
        public bool skillOn;
        private static readonly int SkillOn = Animator.StringToHash("skillOn");
        private static readonly int Interacting = Animator.StringToHash("isInteracting");

        protected override void Awake()
        {
            currentHealth = maxHealth;
            hitPoint = transform.GetChild(2);
            animatorManager = GetComponentInChildren<AnimatorManagerAttacker>();
        }

        private void Start()
        {
            
        }

        protected override void Update()
        {
            isInteracting = animatorManager.anim.GetBool(IsInteracting);
            StunTimerUpdate();
        }

        public override void GetStunned(float stunTime)
        {
            isStunned = true;
            animatorManager.PlayTargetAnimation("Stun_Begin", true);
            stunTimer = stunTime;
        }

        protected override void StunTimerUpdate()
        {
            if (isStunned)
            {
                stunTimer -= Time.deltaTime;
                if (stunTimer <= 0f)
                {
                    isStunned = false;
                    animatorManager.anim.SetBool(Interacting, false);
                    animatorManager.anim.speed = 1;
                }
            }
        }

        public void SkillStart()
        {
            skillOn = true;
            animatorManager.anim.SetBool(SkillOn, skillOn);
            animatorManager.PlayTargetAnimation("Skill_2_Begin", true);
        }

        public override void TakeDamage(float physicDamage, float magicDamage1, float realDamage1)
        {
            currentHealth -= physicDamage - armor > 0.05f * physicDamage
                ? physicDamage - armor
                : 0.05f * physicDamage;
            currentHealth -= magicDamage1 * (1 - magicResistance);
            currentHealth -= realDamage1;
            
            //TODO:斯卡蒂的血条是独立的
            
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }
}
