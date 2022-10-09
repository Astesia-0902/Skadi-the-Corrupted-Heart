using System;
using UnityEngine;

namespace Res.Scripts.UI
{
    public class QueueUpdater : MonoBehaviour
    {
        private QueueData[] queueDatas;

        private void Awake()
        {
            queueDatas = GetComponentsInChildren<QueueData>();
        }

        public void UpdateQueueManagers()
        {
            foreach (QueueData queueData in queueDatas)
            {
                queueData.UpdateMyModel();
            }
        }

        public void ClearQueueManagers()
        {
            foreach (QueueData queueData in queueDatas)
            {
                queueData.ClearMyModel();
            }
        }
    }
}
