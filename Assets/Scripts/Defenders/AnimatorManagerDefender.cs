using UnityEngine;

namespace Defenders
{
    /// <summary>
    /// 动画控制器以及Animation Event
    /// </summary>
    public class AnimatorManagerDefender : MonoBehaviour
    {
        protected Defender Defender;
        public Animator anim;
        private static readonly int IsInteracting = Animator.StringToHash("isInteracting");

        protected virtual void Awake()
        {
            anim = GetComponent<Animator>();
            Defender = GetComponentInParent<Defender>();
        }
        
        public virtual void PlayTargetAnimation(string animationName,bool isInteract)
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
            if (Defender.currentTarget != null)
            {
                Defender.currentTarget.TakeDamage(Defender.attackDamage, 0, 0);
            }
        }
    }
}
