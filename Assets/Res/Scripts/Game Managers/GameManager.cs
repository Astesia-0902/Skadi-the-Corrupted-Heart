using System.Collections.Generic;
using Defenders;
using Res.Scripts.Attackers;
using Tool_Scripts;
using UnityEngine;

namespace Game_Managers
{
    /// <summary>
    /// ����λ�б��Լ���λ�Ĳ���
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        public bool deployFlag;
        private bool lockedOnFlag;
        
        public List<GameObject> unitsToSelect;
        private GameObject currentSelectedUnit;
        private NodeLoopManager nodeToDeploy;
        private AttackerSummonData attackerToDeploy;

        public List<Defender> defendersInGame;

        protected override void Awake()
        {
            base.Awake();
            defendersInGame = new List<Defender>();
        }

        public void AddDefender(Defender defender)
        {
            defendersInGame.Add(defender);
        }

        public void RemoveDefender(Defender defender)
        {
            if (defendersInGame.Contains(defender))
                defendersInGame.Remove(defender);
        }

        #region Place Attacker

        public void BeginDeploy(AttackerSummonData attackerSummonData)
        {
            deployFlag = true;
            attackerToDeploy = attackerSummonData;
            //�����϶�ʱ�������ĵ�λģ��
            currentSelectedUnit = unitsToSelect[attackerSummonData.attackerID - 1];
            currentSelectedUnit.SetActive(true);
            currentSelectedUnit.transform.position = Input.mousePosition;
            Time.timeScale = 0.1f;
        }

        public void DragDeploy()
        {
            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    //�����ڿɲ���ĸ�����
                    if (hit.collider.CompareTag("Spawn Point"))
                    {
                        Vector3 position = hit.collider.transform.position;
                        currentSelectedUnit.transform.position = position;
                        nodeToDeploy = hit.collider.GetComponent<NodeLoopManager>();
                        lockedOnFlag = true;    //��ǵ�ǰ����Ĺ����Ѿ�׼������
                        return;
                    }

                    currentSelectedUnit.transform.position = hit.point;
                }
            }

            lockedOnFlag = false;
        }

        public void EndDrag()
        {
            //����Ѿ����Բ���
            if (lockedOnFlag)
            {
                Debug.Log(attackerToDeploy, nodeToDeploy);
                EntitySummoner.Instance.AddAttacker(attackerToDeploy, nodeToDeploy);
                lockedOnFlag = false;
            }

            deployFlag = false;
            currentSelectedUnit.SetActive(false);
            attackerToDeploy = null;
            nodeToDeploy = null;
            Time.timeScale = 1f;
        }

        #endregion
    }
}
