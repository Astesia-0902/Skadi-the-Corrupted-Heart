using UnityEngine;

namespace Res.Scripts.Attackers
{
    /// <summary>
    /// 动画控制器以及Animation Event
    /// </summary>
    public class AnimatorManagerAttacker : MonoBehaviour
    {
        public Animator anim;
        protected Attacker attacker;
        private static readonly int IsInteracting = Animator.StringToHash("isInteracting");
        private static readonly int CanMove = Animator.StringToHash("CanMove");

        protected virtual void Awake()
        {
            attacker = GetComponentInParent<Attacker>();
            anim = GetComponent<Animator>();
        }

        protected virtual void Update()
        {
            anim.SetBool(CanMove,attacker.CanMove());
        }

        public virtual void PlayTargetAnimation(string animationName, bool isInteract)
        {
            anim.Play(animationName);
            anim.SetBool(AnimatorManagerAttacker.IsInteracting,isInteract);
        }

        public virtual void OnAttack()
        {
            //TODO:攻击特效和敌人的受击特效
            if (attacker.currentAttackTarget != null)
            {
                attacker.currentAttackTarget.TakeDamage(attacker.attackDamage, attacker.magicDamage, attacker.realDamage);
                attacker.currentAttackTarget.TakeNeuralDamage(attacker.sanityDamage);
            }
        }

        public virtual void RemoveThis()
        {
            attacker.nodeLoopManager.EnqueueAttackerToRemove(attacker);
        }
    }
}
