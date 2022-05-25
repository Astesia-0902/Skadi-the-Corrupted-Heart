using UnityEngine;

namespace Defenders
{
    /// <summary>
    /// 动画控制器以及Animation Event
    /// </summary>
    public class AnimatorManagerDefender : MonoBehaviour
    {
        private Defender defender;
        private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            defender = GetComponentInParent<Defender>();
        }
        
        public virtual void PlayTargetAnimation(string animationName)
        {
            anim.Play(animationName);
        }

        public void OnAttack()
        {
            //TODO:攻击特效和敌人的受击特效
            if (defender.currentTarget != null)
            {
                defender.currentTarget.TakeDamage(defender.attackDamage, 0);
            }
        }
    }
}
