using System;
using System.Collections.Generic;
using Game_Managers;
using Res.Scripts.Defenders;
using Res.Scripts.Game_Managers;

namespace Res.Scripts.Attackers.Launcher
{
    public class Launcher : AttackerRange
    {
        public List<Defender> targetsToDeal;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            EntitySummoner.Instance.attackerStationaryInGame.Add(this);
        }

        private void OnDisable()
        {
            if (EntitySummoner.Instance != null)
                EntitySummoner.Instance.attackerStationaryInGame.Remove(this);
        }

        protected override void AttackUpdate()
        {
            if (!CanAttack())
                return;

            targetsToDeal = GetPriorityTargets();

            if (attackTimer > 0)
                return;

            if (targetsToDeal != null)
            {
                attackTimer = attackTimerStandard;
                animatorManager.PlayTargetAnimation("Attack", true);
                RefreshRotation();
            }
        }

        protected List<Defender> GetPriorityTargets()
        {
            List<Defender> res = new List<Defender>(3);

            int count = GameManager.Instance.defendersInGame.Count;

            for (int i = count - 1; i >= 0; i--)
            {
                if (CheckRange(GameManager.Instance.defendersInGame[i].transform))
                {
                    if (res.Count < 3)
                    {
                        res.Add(GameManager.Instance.defendersInGame[i]);
                    }
                    else
                    {
                        return res;
                    }
                }
                else
                {
                    res.Remove(GameManager.Instance.defendersInGame[i]);
                }
            }

            return null;
        }

        public override bool CanMove()
        {
            return false;
        }
    }
}