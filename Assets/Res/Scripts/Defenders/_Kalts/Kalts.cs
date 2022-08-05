using System.Collections.Generic;
using Defenders;
using Res.Scripts.Defenders._Mon3tr;

namespace Res.Scripts.Defenders._Kalts
{
    public class Kalts : DefenderHealer
    {
        private Mon3TR mon3TR;

        public void FindMons3TR()
        {
            mon3TR = FindObjectOfType<Mon3TR>();
        }

        protected override Defender GetPriorityHealTarget(List<Defender> defenders)
        {
            if (mon3TR != null)
            {
                return mon3TR;
            }
            
            return base.GetPriorityHealTarget(defenders);
        }
    }
}
