using Defenders;
using UnityEngine;

namespace Res.Scripts.Defenders
{
    [CreateAssetMenu(fileName = "New DefenderWithdrawData",menuName = "DefenderWithdrawData")]
    public class DefenderWithdrawData : ScriptableObject
    {
        public string defenderToWithdraw;
        public int withdrawTime;
    }
}
