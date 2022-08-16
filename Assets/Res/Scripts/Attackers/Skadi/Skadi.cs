using System;
using Game_Managers;
using UnityEngine;

namespace Res.Scripts.Attackers.Skadi
{
    public class Skadi : Attacker
    {
        public bool skillOn;
        public GameObject littleSeaborn;
        private static readonly int SkillOn = Animator.StringToHash("skillOn");
        private static readonly int Interacting = Animator.StringToHash("isInteracting");

        protected override void Awake()
        {
            currentHealth = maxHealth;
            hitPoint = transform.GetChild(2);
            animatorManager = GetComponentInChildren<AnimatorManagerAttacker>();
        }

        protected override void Update()
        {
            isInteracting = animatorManager.anim.GetBool(IsInteracting);
            StunTimerUpdate();
        }

        public void WaveCheck(int wave)
        {
            if (wave == 2)
            {
                skillOn = true;
                animatorManager.PlayTargetAnimation("Skill_2_Begin", true);
            }
            else if (wave == 3)
            {
                //TODO:变成绿的
            }
            else if (wave == 4)
            {
                //TODO:绿的开技能
            }
        }

        public void StunRecover()
        {
            stunTimer = 0f;
            isStunned = false;
            animatorManager.anim.SetBool(Interacting, false);
        }

        public override void GetStunned(float stunTime)
        {
            if (!isStunned)
            {
                Instantiate(littleSeaborn, new Vector3(4, 0.5003526f, -1.85f), Quaternion.identity);
                animatorManager.PlayTargetAnimation("Stun_Begin", true);
            }
            isStunned = true;
            stunTimer = stunTime;
        }

        protected override void StunTimerUpdate()
        {
            if (isStunned)
            {
                stunTimer -= Time.deltaTime;
                if (stunTimer <= 0f)
                {
                    stunTimer = 0f;
                    isStunned = false;
                    animatorManager.anim.SetBool(Interacting, false);
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

            //TODO:˹���ٵ�Ѫ���Ƕ�����

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }
}