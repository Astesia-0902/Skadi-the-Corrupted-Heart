using System.Collections.Generic;
using Res.Scripts.Attackers;
using Res.Scripts.Attackers.Skadi;
using Res.Scripts.Defenders;
using Res.Scripts.UI;
using Tool_Scripts;
using UnityEngine;

namespace Res.Scripts.Game_Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public bool deployFlag;
        private bool lockedOnFlag;
        private bool lockedOnQueueFlag;

        public List<GameObject> unitsToSelect;
        private GameObject currentSelectedUnit;
        private NodeLoopManager nodeToDeploy;
        private QueueData slotToReplace;
        private AttackerSummonData attackerToDeploy;

        public List<Defender> defendersInGame;

        public float gameSpeed;

        //当前场上斯卡蒂的引用
        public Skadi skadi;

        protected override void Awake()
        {
            base.Awake();

            gameSpeed = 1f;
            defendersInGame = new List<Defender>();
        }

        private void Start()
        {
            skadi = FindObjectOfType<Skadi>();
        }

        public void AddDefender(Defender defender)
        {
            defendersInGame.Add(defender);
        }

        public void RemoveDefender(Defender defender)
        {
            if (defendersInGame.Contains(defender))
            {
                defendersInGame.Remove(defender);
            }
        }

        #region Place Attacker

        public void BeginDeploy(AttackerSummonData attackerSummonData)
        {
            if (attackerSummonData == null)
            {
                return;
            }

            deployFlag = true;
            attackerToDeploy = attackerSummonData;
            currentSelectedUnit = unitsToSelect[attackerSummonData.attackerID];
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
                    if (hit.collider.CompareTag("Spawn Point"))
                    {
                        lockedOnQueueFlag = false;
                        Vector3 position = hit.collider.transform.position;
                        position.y += 0.2f;
                        currentSelectedUnit.transform.position = position;
                        nodeToDeploy = hit.collider.GetComponent<NodeLoopManager>();
                        lockedOnFlag = true;
                        return;
                    }
                    else if (hit.collider.CompareTag("Queue"))
                    {
                        lockedOnFlag = false;
                        Vector3 position = hit.collider.transform.position;
                        currentSelectedUnit.transform.position = position;
                        slotToReplace = hit.collider.GetComponent<QueueData>();
                        lockedOnQueueFlag = true;
                        return;
                    }

                    if (hit.point != null)
                    {
                        currentSelectedUnit.transform.position = hit.point;
                    }
                }
            }

            lockedOnQueueFlag = false;
            lockedOnFlag = false;
        }

        public void EndDrag()
        {
            if (lockedOnFlag)
            {
                Debug.Log(attackerToDeploy, nodeToDeploy);
                EntitySummoner.Instance.AddAttacker(attackerToDeploy, nodeToDeploy);
                lockedOnFlag = false;
            }
            else if (lockedOnQueueFlag)
            {
                lockedOnQueueFlag = false;
                slotToReplace.OnModelChanged(attackerToDeploy);
            }

            deployFlag = false;
            currentSelectedUnit.SetActive(false);
            attackerToDeploy = null;
            slotToReplace = null;
            nodeToDeploy = null;
            Time.timeScale = gameSpeed;
        }

        #endregion
    }
}