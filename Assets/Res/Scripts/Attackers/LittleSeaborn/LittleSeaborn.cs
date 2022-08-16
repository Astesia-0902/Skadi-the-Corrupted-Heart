using System;
using Game_Managers;
using Res.Scripts.Game_Managers;

namespace Res.Scripts.Attackers.LittleSeaborn
{
    public class LittleSeaborn : Attacker
    {
        private void Start()
        {
            EntitySummoner.Instance.attackerStationaryInGame.Add(this);
            currentHealth = maxHealth;
            animatorManager.PlayTargetAnimation("Start", true);
        }

        protected override void Update()
        {
            
        }

        public override void GetStunned(float stunTime)
        {
            
        }

        public override void TakeDamage(float physicDamage, float magicDamage1, float realDamage1)
        {
            base.TakeDamage(physicDamage, magicDamage1, realDamage1);
        }

        protected override void Die()
        {
            isDead = true;
            GameManager.Instance.skadi.TakeDamage(0, 0, 0.25f * GameManager.Instance.skadi.maxHealth);
            GameManager.Instance.skadi.StunRecover();
            EntitySummoner.Instance.attackerStationaryInGame.Remove(this);
            Destroy(this.gameObject);
        }
    }
}
