using System;
using System.Collections.Generic;
using System.Linq;
using Attackers;
using Defenders;
using Game_Managers;
using Res.Scripts.Attackers;
using Res.Scripts.Game_Managers;
using UI;
using UnityEngine;

namespace Res.Scripts.Defenders
{
    /// <summary>
    /// 防守方行为的基类，根据不同干员的特性，需要在之类中重写特定的方法，具体请查阅明日方舟Wiki
    /// </summary>
    public class Defender : MonoBehaviour
    {
        [Header("Health Status")] public float maxHealth;
        public float currentHealth;

        [Header("Defence Paras")] public float armor;
        public float magicResistance;
        public int blockNumStandard; //阻挡数

        [Header("Status Flag")] public bool isDead;
        public bool isInteracting;
        public bool isRange;
        public bool isStunned;

        [Header("Effects")] public Transform hitPoint;

        protected Transform rangeParent;
        public Attacker currentTarget;
        protected AnimatorManagerDefender animatorManager;

        public List<Attacker> attackersBlocked;

        private UiForUnits uiForUnits;

        //更新生命值/神经损伤的UI事件
        public Action<float, float> onHealthChanged;
        public Action<float> onSanityChanged;
        
        public Quaternion defaultRotation = Quaternion.Euler(45, 0, 0);
        public Quaternion targetRotation = Quaternion.Euler(45, 0, 0);
        
        public GameObject skillEffectPrefeb1;
        public GameObject skillEffectPrefeb2;
        protected GameObject skillEffectPointer;

        protected virtual void Awake()
        {
            isDead = false;
            currentHealth = maxHealth;
            sanity = 1000f;
            animatorManager = GetComponentInChildren<AnimatorManagerDefender>();
            uiForUnits = GetComponentInChildren<UiForUnits>();

            attackersBlocked = new List<Attacker>();
            attackTimer = attackTimerStandard;
            rangeParent = transform.GetChild(1);
            hitPoint = transform.GetChild(3);

            magicDamageToDeal = magicDamage;
            physicalDamageToDeal = attackDamage;
            realDamageToDeal = 0;
        }

        protected virtual void Start()
        {
            GameManager.Instance.AddDefender(this);
            animatorManager.PlayTargetAnimation("Start", true);
        }

        protected virtual void Update()
        {
            if (isDead)
                return;

            isInteracting = animatorManager.anim.GetBool(IsInteracting);
            NeuralDamageUpdate();
            UpdateAttackTimer();
            AttackUpdate();
            SkillPointUpdate();
            StunUpdate();
            Rotate();
        }

        protected virtual void OnDisable()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.RemoveDefender(this);
        }

        #region Take Damage

        [Header("Sanity Damage")] public float sanity;
        public float sanityRecoveryTimer;
        public float sanityRecoveryThreshold = 10f;
        public float resistance;
        public bool afterBurst;

        public float stunTimer;

        public virtual void TakeDamage(float physicDamage1, float magicDamage1, float realDamage1)
        {
            currentHealth -= physicDamage1 - armor > 0.05f * physicDamage1
                ? physicDamage1 - armor
                : 0.05f * physicDamage1;
            currentHealth -= magicDamage1 * (1 - magicResistance);
            currentHealth -= realDamage1;

            //更新血条UI
            onHealthChanged.Invoke(currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            isDead = true;
            animatorManager.PlayTargetAnimation("Die", true);
            Unblock();
        }

        /// <summary>
        /// 受到神经损伤
        /// </summary>
        /// <param name="neuralDamageToTake"></param>
        public virtual void TakeNeuralDamage(int neuralDamageToTake)
        {
            if (isDead || afterBurst)
                return;
            
            sanity -= neuralDamageToTake;
            onSanityChanged.Invoke(sanity);

            if (sanity <= 0)
            {
                sanity = 0;
                NeuralDamageBurst();
            }
        }

        /// <summary>
        /// 受到神经损伤的治疗
        /// </summary>
        /// <param name="healAmount"></param>
        public virtual void TakeNeuralHeal(float healAmount)
        {
            sanity += healAmount;
            if (sanity >= 1000f)
            {
                sanity = 1000f;
            }

            onSanityChanged.Invoke(sanity);
        }

        /// <summary>
        /// 神经损伤爆发
        /// </summary>
        protected virtual void NeuralDamageBurst()
        {
            isStunned = true;
            afterBurst = true;
            Unblock();
            TakeDamage(0, 0, 1000f);
            GetStunned(10f);
        }

        /// <summary>
        /// 持续恢复神经损伤
        /// </summary>
        public virtual void NeuralDamageUpdate()
        {
            if (isDead)
                return;

            //是否是爆发后的恢复期
            if (afterBurst)
            {
                sanity += Time.deltaTime * 100f;
            }
            else
            {
                sanity += Time.deltaTime * 50f;
            }

            onSanityChanged.Invoke(sanity);

            if (sanity >= 1000f)
            {
                sanity = 1000f;
                if (afterBurst)
                    afterBurst = !afterBurst;
            }
        }

        public void GetStunned(float stunTime)
        {
            isStunned = true;
            animatorManager.SetAnimatorBool("isStunned", isStunned);
            animatorManager.PlayTargetAnimation("Stun", true);
            stunTimer = stunTime * (1f - resistance);
        }

        public void StunUpdate()
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0)
            {
                stunTimer = 0;
                if (isStunned)
                {
                    isStunned = false;
                    animatorManager.SetAnimatorBool("isStunned", isStunned);
                }
            }
        }

        /// <summary>
        /// 释放被阻挡的目标
        /// </summary>
        protected virtual void Unblock()
        {
            if (attackersBlocked.Count == 0)
                return;

            foreach (Attacker attacker in attackersBlocked)
            {
                if (attacker != null)
                {
                    attacker.Unblocked();
                }
            }

            attackersBlocked.Clear();
        }

        #endregion

        #region Get Heal

        public virtual void GetHeal(float heal)
        {
            if (isDead)
                return;

            currentHealth += heal;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            onHealthChanged.Invoke(currentHealth, maxHealth);
        }

        #endregion

        #region Attack

        [Header("Attack Status")] public float attackDamage;
        public float magicDamage;
        public float attackTimerStandard;
        protected float attackTimer;

        [Header("Damage To Deal")] public float physicalDamageToDeal;
        public float magicDamageToDeal;
        public float realDamageToDeal;

        public Attacker targetToDeal;

        /// <summary>
        /// 持续检测攻击状态
        /// </summary>
        protected virtual void AttackUpdate()
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
                    animatorManager.PlayTargetAnimation("Attack", true, attackAnimationSpeed);
                    RefreshRotation();
                }
                else
                {
                    //目标死亡时切换目标
                    currentTarget = null;
                }
            }
        }

        protected virtual void RefreshRotation()
        {
            targetRotation = transform.position.x - targetToDeal.transform.position.x > 0 ? Quaternion.Euler(-45, 180, 0) : defaultRotation;
        }

        /// <summary>
        /// 获取攻击范围内，以及被阻挡的所有的敌人
        /// </summary>
        /// <returns></returns>
        public virtual List<Attacker> GetAllTargetsInRange()
        {
            List<Attacker> targetsInRange = new List<Attacker>();
            foreach (Attacker attacker in EntitySummoner.Instance.attackersInGame)
            {
                if (attacker == null || !attacker.isActiveAndEnabled || attacker.isDead)
                    continue;

                //搜索在攻击范围的敌人
                if (CheckInRange(attacker.transform))
                    targetsInRange.Add(attacker);

                if (isRange)
                    continue;

                //统计被阻挡的敌人
                Vector3 attackerPosition = attacker.transform.position;
                Vector3 myPosition = transform.position;
                if (GetBlockNum() > 0 && CanBlock())
                {
                    //检查是否在所在的格子内
                    if (attackerPosition.x + 0.5f > myPosition.x && attackerPosition.x - 0.5f < myPosition.x &&
                        attackerPosition.z + 0.5f > myPosition.z && attackerPosition.z - 0.5f < myPosition.z)
                    {
                        if (!attackersBlocked.Contains(attacker))
                        {
                            attackersBlocked.Add(attacker);
                            //将当前敌人标记为已被阻挡
                            attacker.GetBlocked(this);
                        }
                    }
                }
            }

            foreach (Attacker attacker in EntitySummoner.Instance.attackerStationaryInGame)
            {
                if (attacker == null || !attacker.isActiveAndEnabled || attacker.isDead)
                    continue;
                
                if (CheckInRange(attacker.transform))
                    targetsInRange.Add(attacker);
            }

            //如果是远程干员，则不考虑阻挡的问题
            if (isRange)
            {
                return targetsInRange;
            }

            //优先攻击被阻挡的敌人
            if (attackersBlocked.Count > 0)
            {
                return attackersBlocked.Concat(targetsInRange).ToList();
            }

            return targetsInRange;
        }

        /// <summary>
        /// 获取当前干员剩余的阻挡数
        /// </summary>
        /// <returns></returns>
        public virtual int GetBlockNum()
        {
            int blockSum = 0;
            foreach (Attacker attacker in attackersBlocked)
            {
                blockSum += attacker.blockPara;
            }

            return blockNumStandard - blockSum;
        }

        /// <summary>
        /// 获取距离其目标点路程最近的敌人
        /// </summary>
        /// <param name="attackers"></param>
        /// <returns></returns>
        protected virtual Attacker GetPriorityTarget(List<Attacker> attackers)
        {
            if (GameManager.Instance.skadi != null && CheckInRange(GameManager.Instance.skadi.transform))
                return GameManager.Instance.skadi;
            
            if (attackers.Count == 0)
                return null;

            float min = float.MaxValue;
            int index = 0;
            for (int i = 0; i < attackers.Count; i++)
            {
                float current = attackers[i].GetDistanceFromDestination();
                if (current < min)
                {
                    min = current;
                    index = i;
                }
            }

            return attackers[index];
        }

        /// <summary>
        /// 检查目标是否在攻击范围内
        /// </summary>
        /// <param name="targetTransform">目标的Transform</param>
        /// <returns>在就返回True，反之False</returns>
        protected virtual bool CheckInRange(Transform targetTransform)
        {
            Vector3 targetCenter = targetTransform.position;
            //rangeParent物体下挂载了该单位的攻击范围中每个方块的中点
            for (int i = 0; i < rangeParent.childCount; i++)
            {
                if (!rangeParent.GetChild(i).gameObject.activeSelf)
                    continue;

                Vector3 rangeCenter = rangeParent.GetChild(i).position;
                if (targetCenter.x < rangeCenter.x + 0.5f && targetCenter.x > rangeCenter.x - 0.5f &&
                    targetCenter.z < rangeCenter.z + 0.5f && targetCenter.z > rangeCenter.z - 0.5f)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 移除阻挡的敌人
        /// </summary>
        /// <param name="attacker"></param>
        public virtual void RemoveBlockedEnemy(Attacker attacker)
        {
            if (attackersBlocked.Contains(attacker))
                attackersBlocked.Remove(attacker);
        }

        /// <summary>
        /// 控制攻击频率
        /// </summary>
        private void UpdateAttackTimer()
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer < 0)
                attackTimer = 0;
        }
        
        /// <summary>
        /// 控制单位攻击时面朝目标
        /// </summary>
        private void Rotate()
        {
            if (!isInteracting)
            {
                animatorManager.transform.rotation = Quaternion.Slerp(animatorManager.transform.rotation, defaultRotation, 20f * Time.deltaTime);
            }
            else
            {
                animatorManager.transform.rotation = Quaternion.Slerp(animatorManager.transform.rotation, targetRotation, 20f * Time.deltaTime);
            }
        }

        #endregion

        /// <summary>
        /// 与技能相关的方法
        /// </summary>

        #region Skill

        [Header("Skills")] public int skillPoint;

        public int maxSkillPoint;

        public bool isAttackRecovering;

        private float skillPointTimer;
        public bool skillReady;
        private static readonly int IsInteracting = Animator.StringToHash("isInteracting");

        /// <summary>
        /// 回复技能点
        /// </summary>
        public virtual void SkillPointUpdate()
        {
            if (isAttackRecovering)
                return;

            skillPointTimer += Time.deltaTime;
            if (skillPointTimer >= 1f)
            {
                skillPoint++;
                skillPointTimer = 0f;
            }

            if (skillPoint >= maxSkillPoint)
            {
                skillPoint = maxSkillPoint;
                skillReady = true;
            }
            else
            {
                skillReady = false;
            }
        }

        /// <summary>
        /// 攻击回复技力
        /// </summary>
        public virtual void SkillPointOnAttack()
        {
            if (!isAttackRecovering)
                return;

            skillPoint++;

            if (skillPoint >= maxSkillPoint)
            {
                skillPoint = maxSkillPoint;
                skillReady = true;
            }
            else
            {
                skillReady = false;
            }
        }
        
        protected virtual void DestroySkillEffect()
        {
            if (skillEffectPointer != null)
            {
                Destroy(skillEffectPointer);
                skillEffectPointer = null;
            }
        }

        #endregion

        #region Combat Status

        /// <summary>
        /// 当前干员是否可以阻挡敌人
        /// </summary>
        /// <returns></returns>
        public virtual bool CanBlock()
        {
            return !(isStunned || isDead);
        }

        /// <summary>
        /// 当前干员是否可以攻击
        /// </summary>
        /// <returns></returns>
        public virtual bool CanAttack()
        {
            return !(isStunned || isDead || isInteracting);
        }

        #endregion

        private void OnDestroy()
        {
            uiForUnits.DestroyBar();

            //animatorManager.PlayTargetAnimation("Die", true);
        }
    }

    /// <summary>
    /// 生命值升序比较器(考虑眩晕)
    /// </summary>
    public class DefenderHealthStunComp : IComparer<Defender>
    {
        public int Compare(Defender x, Defender y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            //被眩晕的排前面
            if (x.isStunned && !y.isStunned)
            {
                return -1;
            }
            else if (!x.isStunned && y.isStunned)
            {
                return 1;
            }

            //x大->返回1->x往后排->升序排列
            if (x.currentHealth / x.maxHealth > y.currentHealth / y.maxHealth)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }

    /// <summary>
    /// 生命值升序排序（不考虑眩晕）
    /// </summary>
    public class DefenderHealthComp : IComparer<Defender>
    {
        public int Compare(Defender x, Defender y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            //x大->返回1->x往后排->升序排列
            if (x.currentHealth / x.maxHealth > y.currentHealth / y.maxHealth)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }

    /// <summary>
    /// 按照神经损伤升序排序
    /// </summary>
    public class DefenderNeuralComp : IComparer<Defender>
    {
        public int Compare(Defender x, Defender y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            //x大->返回1->x往后排->升序排列
            if (x.sanity / 1000 > y.sanity / 1000)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}