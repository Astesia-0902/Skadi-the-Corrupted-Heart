using UnityEngine;

namespace Res.Scripts.Defenders
{
    [CreateAssetMenu(fileName = "New DefenderSummonData",menuName = "DefenderSummonData")]
    public class DefenderSummonData : ScriptableObject
    {
        public GameObject defenderPrefeb;
        public Vector3 position;
        public int deployTime;
    }
}
