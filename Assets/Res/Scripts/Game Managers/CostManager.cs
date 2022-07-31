using System;
using System.Collections.Generic;
using Tool_Scripts;
using UnityEngine;

namespace Res.Scripts.Game_Managers
{
    /// <summary>
    /// ����������Ĳ�����ã���������߻��ĵ�
    /// </summary>
    public class CostManager : Singleton<CostManager>
    {
        public int currentCost;
        public bool costFlag;

        private float costRecoveryTimer;
        private Queue<int> costToAddQueue;

        public Action onCostChange;

        protected override void Awake()
        {
            base.Awake();
            costFlag = true;
            costToAddQueue = new Queue<int>();
        }

        private void Update()
        {
            costRecoveryTimer += Time.deltaTime;

            if (costRecoveryTimer >= 1f && costFlag)
            {
                currentCost++;
                if (currentCost >= 99)
                {
                    currentCost = 99;
                }
                onCostChange.Invoke();
                costRecoveryTimer = 0f;
            }
        }

        public void AddCost(int amount)
        {
            currentCost += amount;
        }

        public bool DrainCost(int amount)
        {
            if (currentCost < amount)
            {
                return false;
            }
            else
            {
                currentCost -= amount;
                onCostChange.Invoke();
                return true;
            }
            
        }

        /// <summary>
        /// ���γ��ֵĲ���λ�ǻ᷵�����õģ�������Ҫ��¼һ��
        /// </summary>
        /// <param name="cost"></param>
        public void StoreCost(int cost)
        {
            costToAddQueue.Enqueue(cost);
        }

        public void RegainCost()
        {
            if (costToAddQueue.Count > 0)
            {
                currentCost += costToAddQueue.Dequeue();
                onCostChange.Invoke();
            }
        }
    }
}
