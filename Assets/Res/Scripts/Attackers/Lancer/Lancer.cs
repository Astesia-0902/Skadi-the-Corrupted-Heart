using System.Collections.Generic;
using Game_Managers;
using Res.Scripts.Defenders;

namespace Res.Scripts.Attackers.Lancer
{
    public class Lancer : AttackerRange
    {
        protected override Defender GetPriorityTarget()
        {
            List<Defender> defenders = new List<Defender>(GameManager.Instance.defendersInGame);
            defenders.Sort(new DefenderHealthComp());

            for (int i = 0; i < defenders.Count; i++)
            {
                if (CheckRange(defenders[i].transform))
                {
                    return defenders[i];
                }
            }

            return null;
        }
    }
}