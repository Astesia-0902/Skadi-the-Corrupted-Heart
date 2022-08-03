using Defenders;
using Game_Managers;
using UnityEngine;

namespace Res.Scripts.Defenders._Grani
{
    public class Grani : Defender
    {
        public bool isAttacking;
        private bool endAttack;
        private float endAttackTimer;

        public bool isSkillOn;
        public float skillTimer;

        public float attackAnimationSpeed;
        private static readonly int SkillTrigger = Animator.StringToHash("Skill Trigger");
        private static readonly int EndAttack = Animator.StringToHash("endAttack");

        protected override void Awake()
        {
            base.Awake();
            attackAnimationSpeed = attackTimerStandard < 1f ? 1 / attackTimerStandard : 1f;
        }

        protected override void Update()
        {
            base.Update();
            SkillUpdate();

            if (endAttack)
            {
                endAttackTimer += Time.deltaTime;
            }
        }

        private void SkillUpdate()
        {
            //技能发动时，进行倒计时
            if (isSkillOn)
            {
                skillTimer += Time.deltaTime;

                if (skillTimer >= 30f)
                {
                    isSkillOn = false;
                    skillTimer = 0f;
                    attackDamage /= 1.6f;
                    armor /= 1.6f;
                    blockNumStandard -= 1;
                    rangeParent.transform.GetChild(1).gameObject.SetActive(true);

                    //如果技能结束时正在攻击，切换成普攻动画
                    if (isAttacking)
                    {
                        animatorManager.PlayTargetAnimation(
                            Mathf.Abs(targetToDeal.transform.position.z - transform.position.z) > 0.2f
                                ? "Attack_Down"
                                : "Attack", true);
                    }
                }
            }

            //判断技能是否可以开启
            if (!skillReady || isStunned)
                return;

            skillPoint = 0;
            isSkillOn = true;
            skillReady = false;
            blockNumStandard += 1;
            attackDamage *= 1.6f;
            armor *= 1.6f;
            rangeParent.transform.GetChild(1).gameObject.SetActive(false);

            //如果技能开始时正在攻击，切换成技能攻击动画
            if (isAttacking)
            {
                animatorManager.PlayTargetAnimation("Skill_Loop", true);
            }
        }

        protected override void AttackUpdate()
        {
            currentTarget = GetPriorityTarget(GetAllTargetsInRange());
            if (CheckInRange(GameManager.Instance.skadi.transform))
                currentTarget = GameManager.Instance.skadi;

            if (currentTarget != null)
                targetToDeal = currentTarget;

            if (attackTimer > 0)
                return;

            if (targetToDeal != null && CanAttack())
            {
                if (!isAttacking)
                {
                    if (!targetToDeal.isDead && CheckInRange(targetToDeal.transform))
                    {
                        isAttacking = true;
                        attackTimer = attackTimerStandard;

                        //每次攻击开始时播放起手式
                        animatorManager.PlayTargetAnimation("Skill_Begin", true);
                        
                        if (transform.position.x - targetToDeal.transform.position.x > 0)
                        {
                            targetRotation = Quaternion.Euler(-90, 180, 0);
                        }
                        else
                        {
                            targetRotation = Quaternion.identity;
                        }

                        //根据当前状态，决定进入哪个攻击动画的循环
                        if (isSkillOn)
                        {
                            animatorManager.anim.SetTrigger(SkillTrigger);
                        }
                        else
                        {
                            animatorManager.anim.SetTrigger(
                                Mathf.Abs(targetToDeal.transform.position.z - transform.position.z) > 0.2f
                                    ? "AttackDown Trigger"
                                    : "Attack Trigger");
                        }
                    }
                    else
                    {
                        //目标死亡时切换目标
                        currentTarget = null;
                    }
                }
            }

            //如果发现目标丢失，立刻停止攻击
            if (targetToDeal == null || targetToDeal.isDead || !CheckInRange(targetToDeal.transform))
            {
                if (isStunned)
                    return;
                //如果正在攻击，收起武器回到idle状态
                if (isAttacking)
                {
                    animatorManager.anim.SetBool(EndAttack, true);
                }
            }
        }
    }
}