using Defenders.Extension;
using Game_Managers;
using Res.Scripts.Defenders;
using Res.Scripts.Defenders.Extension;
using UnityEngine;

namespace Defenders
{
    /// <summary>
    /// 动画控制器以及Animation Event
    /// </summary>
    public class AnimatorManagerDefender : MonoBehaviour
    {
        [Header("Tracer Effect")] public GameObject tracerFXPrefeb;
        public GameObject hitFXPrefeb;
        public Transform tracerPivot;
        

        public RangeAttackTracerDefender rangeAttackTracerDefender;
        
        protected Defender defender;
        public Animator anim;
        private static readonly int IsInteracting = Animator.StringToHash("isInteracting");
        private static readonly int Multiplier = Animator.StringToHash("Multiplier");

        protected virtual void Awake()
        {
            anim = GetComponent<Animator>();
            defender = GetComponentInParent<Defender>();
        }
        
        
        public virtual void PlayTargetAnimation(string animationName,bool isInteract,float multiplier)
        {
            anim.SetFloat(Multiplier, multiplier);
            anim.Play(animationName);
            anim.SetBool(IsInteracting,isInteract);
        }

        public virtual void PlayTargetAnimation(string animationName, bool isInteract)
        {
            anim.Play(animationName);
            anim.SetBool(IsInteracting,isInteract);
        }

        public virtual void SetAnimatorBool(string variableName,bool value)
        {
            anim.SetBool(variableName, value);
        }

        public virtual bool GetAnimatorBool(string variableName)
        {
            return anim.GetBool(variableName);
        }

        public virtual void OnAttack()
        {
            //TODO:攻击特效和敌人的受击特效
            if (defender.targetToDeal != null)
            {
                if (defender.isRange)
                {
                    rangeAttackTracerDefender = Instantiate(tracerFXPrefeb, tracerPivot).GetComponent<RangeAttackTracerDefender>();
                    if (rangeAttackTracerDefender != null)
                    {
                        rangeAttackTracerDefender.target = defender.targetToDeal;
                        rangeAttackTracerDefender.magicDamage = defender.magicDamage;
                        rangeAttackTracerDefender.physicDamage = defender.attackDamage;
                        rangeAttackTracerDefender.realDamage = defender.realDamageToDeal;
                        rangeAttackTracerDefender.hitFXPrefeb = hitFXPrefeb;
                    }
                }
                else
                {
                    if (hitFXPrefeb != null)
                    {
                        Instantiate(hitFXPrefeb, defender.targetToDeal.hitPoint);
                    }

                    defender.targetToDeal.TakeDamage(defender.attackDamage, defender.magicDamage,
                        defender.realDamageToDeal);
                }
            }
        }

        public virtual void OnDeath()
        {
            Destroy(defender.gameObject);
        }
    }
}
