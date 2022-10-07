using Defenders;

namespace Res.Scripts.Defenders._Red
{
    public class RedAm : AnimatorManagerDefender
    {
        private Red red;
        protected override void Awake()
        {
            base.Awake();
            red = GetComponentInParent<Red>();
        }

        public void OnStart()
        {
            red.CastSkill();
            red.PlayRandomSFX(red.spawnSFX);
        }
    }
}
