using System;
using Res.Scripts.Game_Managers;
using Unity.Mathematics;
using UnityEngine;

namespace Res.Scripts.Attackers.Skadi
{
    /// <summary>
    /// 斯卡蒂的类
    /// </summary>
    public class Skadi : Attacker
    {
        public bool skillOn;
        public GameObject littleSeaborn;
        public GameObject skadiGreen;
        private static readonly int SkillOn = Animator.StringToHash("skillOn");
        private static readonly int Interacting = Animator.StringToHash("isInteracting");

        protected override void Awake()
        {
            currentHealth = maxHealth;
            hitPoint = transform.GetChild(2);
            animatorManager = GetComponentInChildren<AnimatorManagerAttacker>();
            targetRotation = Quaternion.Euler(71.6f, 0, 0);
            defaultRotation = Quaternion.Euler(71.6f, 0, 0);
        }

        private void Start()
        {
            animatorManager.PlayTargetAnimation("Start", true);
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
                SkillStart();
            }
            else if (wave == 3)
            {
                Henshin();
            }
            else if (wave == 4)
            {
                SkillStart();
            }
        }

        public void Henshin()
        {
            Skadi green = Instantiate(skadiGreen, transform.position, quaternion.identity).GetComponent<Skadi>();
            GameManager.Instance.skadi = green;
            //TODO:变身的特效
            Destroy(this.gameObject);
        }

        public void StunRecover()
        {
            stunTimer = 0f;
            isStunned = false;
            animatorManager.anim.SetBool(Interacting, false);
        }

        public override void GetStunned(float stunTime)
        {
            //斯卡蒂被眩晕时会召唤出小海嗣
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