using Attackers;
using Defenders;
using Res.Scripts.Attackers;
using UnityEngine;

namespace Res.Scripts.Defenders._Grani
{
    public class GraniAm : AnimatorManagerDefender
    {
        private Grani grani;
        private static readonly int Multiplier = Animator.StringToHash("Multiplier");
        private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

        protected override void Awake()
        {
            base.Awake();
            grani = GetComponentInParent<Grani>();
        }

        private void Update()
        {
            //���ڸ�����ʹ�õ���ѭ�������������ڲ���ÿ֡���趨�����ٶȣ����Թ����ٶȵĲ���Ҫʵʱ����
            anim.SetFloat(Multiplier, grani.attackAnimationSpeed);
            grani.isAttacking = anim.GetBool(IsAttacking);
        }

        public override void OnAttack()
        {
            if (grani.isSkillOn)
            {
                foreach (Attacker attacker in defender.GetAllTargetsInRange())
                {
                    if (attacker != null && attacker.isActiveAndEnabled)
                    {
                        attacker.TakeDamage(defender.attackDamage, 0f, 0f);
                    }
                }
            }
            else
            {
                defender.targetToDeal.TakeDamage(defender.attackDamage, defender.magicDamage,
                    defender.realDamageToDeal);
            }
        }
    }
}
