using Game_Managers;
using Res.Scripts.Defenders;
using Res.Scripts.Game_Managers;

namespace Res.Scripts.Attackers.Wind_Chime
{
    public class WindChime : AttackerRange
    {
        protected override void AttackUpdate()
        {
            if (attackTimer > 0)
                return;

            attackTimer = attackTimerStandard;
            foreach (Defender defender in GameManager.Instance.defendersInGame)
            {
                if (CheckRange(defender.transform))
                {
                    defender.TakeDamage(0, magicDamage, 0);
                    defender.TakeNeuralDamage((int) (0.05f * magicDamage));
                }
            }
        }

        public override void GetStunned(float stunTime)
        {
            stunTime *= 0.5f;
            base.GetStunned(stunTime);
        }
    }
}