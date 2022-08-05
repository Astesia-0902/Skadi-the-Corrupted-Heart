using System.Collections.Generic;
using Res.Scripts.Attackers;
using Res.Scripts.Defenders;
using Res.Scripts.UI;
using UnityEngine;

namespace Res.Scripts.Game_Managers
{
    public class TimelineManager : MonoBehaviour
    {
        private float timerAccumulator;
        private int timer;
        private int lighthouseTimer;
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

            startFlag = true;
        }

        private void Update()
        {
            if (startFlag)
            {
                timerAccumulator += Time.deltaTime;
                if (timerAccumulator >= 1f)
                {
                    timer++;
                    lighthouseTimer++;
                    if (lighthouseTimer >= 60)
                    {
                        ActivateLighthouse();
                    }
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

        private void TimelineChecker()
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

        private void ActivateLighthouse()
        {
            lighthouseTimer = 0;
            Debug.Log("灯塔启动！");
        }

        private void LoadNewAttackersData(string wave)
        {
            AttackerSummonData[] attackerSummonDatas = Resources.LoadAll<AttackerSummonData>("Attacker Summon Data/"+wave);
            DeployButton[] deployButtons = FindObjectsOfType<DeployButton>();
            
            for (int i = 0; i < attackerSummonDatas.Length; i++)
            {
                deployButtons[i].LoadNewAttackerData(attackerSummonDatas[i]);
            }
            
        }
    }
}