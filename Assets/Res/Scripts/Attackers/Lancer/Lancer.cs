using System;
using System.Collections.Generic;
using Res.Scripts.Defenders;
using Res.Scripts.Game_Managers;
using UnityEngine;

namespace Res.Scripts.Attackers.Lancer
{
    public class Lancer : AttackerRange
    {
        protected override void Awake()
        {
            targetRotation = Quaternion.Euler(71.6f, 0, 0);
            defaultRotation = Quaternion.Euler(71.6f, 0, 0);
        }

        private void OnEnable()
        {
            EntitySummoner.Instance.attackerStationaryInGame.Add(this);
        }

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

        private void OnDisable()
        {
            EntitySummoner.Instance.attackerStationaryInGame.Remove(this);
        }
    }
}