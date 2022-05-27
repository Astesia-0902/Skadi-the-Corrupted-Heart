namespace Defenders
{
    public class DefenderHealerAnimatorManager : AnimatorManagerDefender
    {
        protected DefenderHealer DefenderHealer;

        protected override void Awake()
        {
            base.Awake();
            DefenderHealer = GetComponentInParent<DefenderHealer>();
        }

        public override void OnAttack()
        {
            DefenderHealer.currentHealTarget.GetHeal(DefenderHealer.attackDamage);
        }
    }
}