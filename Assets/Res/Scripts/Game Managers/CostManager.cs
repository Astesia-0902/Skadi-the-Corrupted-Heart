using System;
using System.Collections.Generic;
using Tool_Scripts;
using UnityEngine;

namespace Res.Scripts.Game_Managers
{
    /// <summary>
    /// 管理进攻方的部署费用，功能详见策划文档
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
            if (!TimelineManager.Instance.startFlag)
                return;
            
            CostUpdate();
            CostDisableUpdate();
        }

        private void CostUpdate()
        {
            if (!costFlag || TimelineManager.Instance.lighthouseFlag)
                return;
            
            costRecoveryTimer += Time.deltaTime;

            if (costRecoveryTimer >= 1f && costFlag)
            {
                AddCost(1);
                costRecoveryTimer = 0f;
            }
        }

        public void AddCost(int amount)
        {
            currentCost += amount;
            if (currentCost > 99)
            {
                currentCost = 99;
            }
            onCostChange.Invoke();
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
        /// 单次出怪的部署位是会返还费用的，所以需要记录一下
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
                AddCost(costToAddQueue.Dequeue());
            }
        }

        private bool costDisableFlag;
        private float costDisableTimer;
        public void DisableCostRecovery(float time)
        {
            costDisableFlag = true;
            costDisableTimer = time;
            costFlag = false;
        }

        public void CostDisableUpdate()
        {
            if (!costDisableFlag)
                return;

            costDisableTimer -= Time.deltaTime;
            if (costDisableTimer <= 0)
            {
                costDisableFlag = false;
                costFlag = true;
            }
        }
    }
}
