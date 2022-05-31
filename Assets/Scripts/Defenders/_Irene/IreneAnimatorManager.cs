using Attackers;
using Game_Managers;
using UnityEngine;

namespace Defenders._Irene
{
    public class IreneAnimatorManager : AnimatorManagerDefender
    {
        [SerializeField] public Transform skillRangeParent;
        private float blowUpDamage;
        [SerializeField] private float blowUpCoeffi;
        private float handCannonDamage;
        [SerializeField] private float handCannonCoeffi;
        private Attacker skillTarget;
        

        protected override void Awake()
        {
            base.Awake();
            blowUpDamage = Defender.attackDamage * blowUpCoeffi;
            handCannonDamage = Defender.attackDamage * handCannonCoeffi;
        }

        public void PlaySkill()
        {
            PlayTargetAnimation("Skill_1", true);
        }

        public void BlowUp()
        {
            skillTarget = Defender.currentTarget;
            
            if (!skillTarget.isDead)
            {
                skillTarget.BlowUpStart(1.5f);
                skillTarget.TakeDamage(blowUpDamage, 0f, 0f);
            }
        }

        public void HandCannon()
        {
            if (!skillTarget.isDead)
            {
                skillTarget.TakeDamage(handCannonDamage, 0f, 0f);
            }
        }

        private bool CheckInRangeJudgement(Transform targetTransform)
        {
            Vector3 targetCenter = targetTransform.position;
            //rangeParent物体下挂载了该单位的攻击范围中每个方块的中点
            for (int i = 0; i < skillRangeParent.childCount; i++)
            {
                Vector3 rangeCenter = skillRangeParent.GetChild(i).position;
                if (targetCenter.x < rangeCenter.x + 0.5f && targetCenter.x > rangeCenter.x - 0.5f &&
                    targetCenter.z < rangeCenter.z + 0.5f && targetCenter.z > rangeCenter.z - 0.5f)
                {
                    return true;
                }
            }

            return false;
        }
    }
}