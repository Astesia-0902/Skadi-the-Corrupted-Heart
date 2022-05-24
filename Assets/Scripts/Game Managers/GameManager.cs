using System.Collections.Generic;
using Tool_Scripts;
using UnityEngine;

namespace Game_Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public bool deployFlag;
        private bool lockedOnFlag;
        public List<GameObject> unitsToSelect;
        private GameObject currentSelectedUnit;
        private NodeLoopManager nodeToDeploy;
        private AttackerSummonData attackerToDeploy;

        public void BeginDeploy(AttackerSummonData attackerSummonData)
        {
            deployFlag = true;
            attackerToDeploy = attackerSummonData;
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
                    if (hit.collider.CompareTag("Spawn Point"))
                    {
                        Vector3 position = hit.collider.transform.position;
                        position.y -= 1f;
                        currentSelectedUnit.transform.position = position;
                        nodeToDeploy = hit.collider.GetComponent<NodeLoopManager>();
                        lockedOnFlag = true;
                        return;
                    }

                    Vector3 positionTemp = hit.point;
                    positionTemp.y -= 0.5f; 
                    currentSelectedUnit.transform.position = positionTemp;
                }
            }

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

            deployFlag = false;
            currentSelectedUnit.SetActive(false);
            attackerToDeploy = null;
            nodeToDeploy = null;
            Time.timeScale = 1f;
        }
    }
}
