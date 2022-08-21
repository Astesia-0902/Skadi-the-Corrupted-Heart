using System.Collections.Generic;
using Res.Scripts.Attackers;
using Res.Scripts.Game_Managers;
using UnityEngine;

namespace Res.Scripts.UI
{
    public class QueueData : MonoBehaviour
    {
        private GameObject currentModel;
        public int mySeabornID;
        public int myQueueID;
        public int is34Or2;
        public NodeLoopManager nodeLoopManager;

        public void OnModelChanged(AttackerSummonData attackerSummonData)
        {
            if (attackerSummonData.attackerID == mySeabornID)
                return;

            bool res = false;
            if (is34Or2 == 34)
            {
                int queueIndex = myQueueID * 2;
                res = EntitySummoner.Instance.ReplaceAttacker34(queueIndex, attackerSummonData);
            }
            else if (is34Or2 == 2)
            {
                res = EntitySummoner.Instance.ReplaceAttacker2(myQueueID, attackerSummonData, nodeLoopManager);
            }

            if (!res)
                return;

            if (currentModel != null)
                Destroy(currentModel);

            mySeabornID = attackerSummonData.attackerID;
            currentModel = Instantiate(GameManager.Instance.unitsToSelect[mySeabornID], transform);
            currentModel.transform.position = transform.position;
            currentModel.SetActive(true);
        }

        public void UpdateMyModel()
        {
            int queueIndex = is34Or2 == 34 ? myQueueID * 2 : myQueueID;
            List<AttackerSummonData> myList = is34Or2 == 34
                ? EntitySummoner.Instance.attackerList34
                : EntitySummoner.Instance.attackerList2;

            if (queueIndex >= myList.Count)
                return;

            int currentSeabornID = myList[queueIndex].attackerID;
            if (currentSeabornID == mySeabornID)
                return;

            mySeabornID = currentSeabornID;

            if (currentModel != null)
                Destroy(currentModel);

            currentModel = Instantiate(GameManager.Instance.unitsToSelect[mySeabornID], transform);
            currentModel.transform.position = transform.position;
            currentModel.SetActive(true);
        }
    }
}