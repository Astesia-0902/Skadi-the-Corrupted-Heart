namespace Res.Scripts.Attackers.Skadi
{
    public class SkadiAm : AnimatorManagerAttacker
    {
        protected override void Update()
        {
            
        }

        public void OnDeath()
        {
            Destroy(attacker.gameObject);
        }
    }
}
