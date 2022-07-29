using UnityEngine;

namespace Res.Scripts.Attackers
{
    [CreateAssetMenu(fileName = "New AttackerSummonData",menuName = "Attacker Summon Data")]
    public class AttackerSummonData : ScriptableObject
    {
        public GameObject attackerPrefeb;
        public int attackerID;
        public int cost;
    }
}
