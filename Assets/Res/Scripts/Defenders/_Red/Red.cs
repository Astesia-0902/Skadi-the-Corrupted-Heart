using Game_Managers;
using Res.Scripts.Attackers;

namespace Res.Scripts.Defenders._Red
{
    public class Red : Defender
    {
        public void CastSkill()
        {
            for (int i = 2; i < rangeParent.childCount; i++)
            {
                rangeParent.GetChild(i).gameObject.SetActive(true);
            }

            foreach (Attacker attacker in EntitySummoner.Instance.attackersInGame)
            {
                if (CheckInRange(attacker.transform))
                {
                    attacker.GetStunned(2f);
                    attacker.TakeDamage(attackDamage * 2f, 0, 0);
                }
            }
            
            for (int i = 2; i < rangeParent.childCount; i++)
            {
                rangeParent.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
