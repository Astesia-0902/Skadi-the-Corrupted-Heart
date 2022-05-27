using UnityEngine;

namespace Defenders
{
    /// <summary>
    /// 动画控制器以及Animation Event
    /// </summary>
    public class AnimatorManagerDefender : MonoBehaviour
    {
        protected Defender Defender;
        protected Animator Anim;

        protected virtual void Awake()
        {
            Anim = GetComponent<Animator>();
            Defender = GetComponentInParent<Defender>();
        }
        
        public virtual void PlayTargetAnimation(string animationName)
        {
            Anim.Play(animationName);
        }

        public virtual void OnAttack()
        {
            //TODO:攻击特效和敌人的受击特效
            if (Defender.currentTarget != null)
            {
                Defender.currentTarget.TakeDamage(Defender.attackDamage, 0);
            }
        }
    }
}
