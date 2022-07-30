using System;
using System.Collections.Generic;
using Defenders;
using Game_Managers;
using Res.Scripts.Defenders;
using Res.Scripts.Defenders._Grani;
using UnityEngine;

namespace Res.Scripts.Game_Managers
{
    public class TimelineManager : MonoBehaviour
    {
        private float timerAccumulator;
        private int timer;
        private bool startFlag;

        private Queue<DefenderSummonData> defenderSummonQueue;
        private Queue<DefenderWithdrawData> defenderWithdrawQueue;


        private void Awake()
        {
            defenderSummonQueue = new Queue<DefenderSummonData>();
            defenderWithdrawQueue = new Queue<DefenderWithdrawData>();
        }

        private void Start()
        {
            DefenderSummonData[] allDefenderSummonData = Resources.LoadAll<DefenderSummonData>("Defenders");
            DefenderWithdrawData[] allDefenderWithdrawData =
                Resources.LoadAll<DefenderWithdrawData>("Defenders Withdraw");

            foreach (DefenderSummonData defenderSummonData in allDefenderSummonData)
            {
                Debug.Log("记录防御方信息：" + defenderSummonData.name);
                defenderSummonQueue.Enqueue(defenderSummonData);
            }

            foreach (DefenderWithdrawData defenderWithdrawData in allDefenderWithdrawData)
            {
                Debug.Log("记录防御方撤退信息：" + defenderWithdrawData.name);
                defenderWithdrawQueue.Enqueue(defenderWithdrawData);
            }

            //startFlag = true;
        }

        private void Update()
        {
            if (startFlag)
            {
                timerAccumulator += Time.deltaTime;
                if (timerAccumulator >= 1f)
                {
                    timer++;
                    timerAccumulator = 0f;
                }
                
                TimelineChecker();
            }
        }

        public void ActivateTimer()
        {
            startFlag = true;
        }

        public void DeactivateTimer()
        {
            startFlag = false;
        }

        public void TimelineChecker()
        {
            if (defenderSummonQueue.Count != 0 && timer == defenderSummonQueue.Peek().deployTime)
            {
                DefenderSummonData defenderSummonData = defenderSummonQueue.Dequeue();
                Instantiate(defenderSummonData.defenderPrefeb, defenderSummonData.position, Quaternion.identity);
            }

            if (defenderWithdrawQueue.Count != 0 && timer == defenderWithdrawQueue.Peek().withdrawTime)
            {
                GameObject defenderToWithdraw = GameObject.Find(defenderWithdrawQueue.Dequeue().defenderToWithdraw);
                Destroy(defenderToWithdraw);
            }
        }
    }
}