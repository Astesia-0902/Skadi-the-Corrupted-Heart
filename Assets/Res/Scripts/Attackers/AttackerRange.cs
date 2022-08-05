using Game_Managers;
using Res.Scripts.Defenders;
using Res.Scripts.Game_Managers;
using Unity.Mathematics;
using UnityEngine;

namespace Res.Scripts.Attackers
{
    public class AttackerRange : Attacker
    {
        [Header("?????????")]
        public float range;

        public override void Initialize(NodeLoopManager node)
        {
            base.Initialize(node);
        }

        protected override Defender GetPriorityTarget()
        {
            int count = GameManager.Instance.defendersInGame.Count;
            
            for (int i = count - 1; i >= 0; i--)
            {
                if (CheckRange(GameManager.Instance.defendersInGame[i].transform))
                {
                    return GameManager.Instance.defendersInGame[i];
                }
            }

            return null;
        }

        protected virtual bool CheckRange(Transform targetTransform)
        {
            Vector3 myPosition = transform.position;
            Vector3 targetPosition = targetTransform.position;

            float distance = Mathf.Sqrt((myPosition.x - targetPosition.x) * (myPosition.x - targetPosition.x) +
                                        (myPosition.z - targetPosition.z) * (myPosition.z - targetPosition.z));

            if (distance <= range)
            {
                return true;
            }

            return false;
        }
    }
}