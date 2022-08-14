using System;
using System.Collections.Generic;
using Game_Managers;
using Res.Scripts.Attackers;
using Res.Scripts.Defenders;
using Res.Scripts.UI;
using Tool_Scripts;
using UnityEngine;

namespace Res.Scripts.Game_Managers
{
    public class TimelineManager : Singleton<TimelineManager>
    {
        public int waveCount = 1;
        private float timerAccumulator;
        private int timer;
        private int lighthouseTimer;
        private float lighthouseActivateTimer = 10f;
        private float lighthouseActivateTimerBuffer;
        public bool startFlag;
        private bool lighthouseFlag;

        private Queue<DefenderSummonData> defenderSummonQueue;
        private Queue<DefenderWithdrawData> defenderWithdrawQueue;


        protected override void Awake()
        {
            base.Awake();
            defenderSummonQueue = new Queue<DefenderSummonData>();
            defenderWithdrawQueue = new Queue<DefenderWithdrawData>();
        }

        private void Start()
        {
            //从文件中读取干员的部署数据
            DefenderSummonData[] allDefenderSummonData = Resources.LoadAll<DefenderSummonData>("Defenders");
            DefenderWithdrawData[] allDefenderWithdrawData =
                Resources.LoadAll<DefenderWithdrawData>("Defenders Withdraw");

            Array.Sort(allDefenderSummonData, new DefenderSummonDataComp());
            Array.Sort(allDefenderWithdrawData, new DefenderWithdrawDataComp());

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
            LighthouseUpdate();

            if (startFlag)
            {
                //整场战斗的计时器
                if (!lighthouseFlag)
                {
                    timerAccumulator += Time.deltaTime;
                }

                if (timerAccumulator >= 1f)
                {
                    timer++;
                    TimelineChecker();
                    lighthouseTimer++;
                    if (lighthouseTimer >= 60)
                    {
                        ActivateLighthouse();
                        waveCount++;
                    }

                    timerAccumulator = 0f;
                }
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

        /// <summary>
        /// 检查当前时间戳是否有干员需要下场
        /// </summary>
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

        /// <summary>
        /// 激活灯塔
        /// </summary>
        public void ActivateLighthouse()
        {
            lighthouseTimer = 0;
            lighthouseFlag = true;
            Debug.Log("灯塔启动！");
            foreach (Attacker attacker in EntitySummoner.Instance.attackersInGame)
            {
                attacker.GetStunned(3600f);
            }

            GameManager.Instance.skadi.GetStunned(10f);
            CostManager.Instance.DisableCostRecovery(10f);
        }

        private float damageTimer;

        /// <summary>
        /// 灯塔激活后的持续效果
        /// </summary>
        private void LighthouseUpdate()
        {
            if (!lighthouseFlag)
                return;

            lighthouseActivateTimerBuffer += Time.deltaTime;
            if (lighthouseActivateTimerBuffer >= lighthouseActivateTimer)
            {
                lighthouseActivateTimerBuffer = 0f;
                lighthouseFlag = false;
                LoadNewAttackersData(waveCount.ToString());
                //TODO:生成四个喷溅者
            }

            damageTimer += Time.deltaTime;
            if (damageTimer >= 1f)
            {
                damageTimer = 0f;
                foreach (Attacker attacker in EntitySummoner.Instance.attackersInGame)
                {
                    attacker.TakeDamage(0, 0, 3000f);
                }
            }
        }

        /// <summary>
        /// 更新海嗣的部署按钮
        /// </summary>
        /// <param name="wave"></param>
        private void LoadNewAttackersData(string wave)
        {
            AttackerSummonData[] attackerSummonDatas =
                Resources.LoadAll<AttackerSummonData>("Attacker Summon Data/" + wave);
            DeployButton[] deployButtons = FindObjectsOfType<DeployButton>();

            for (int i = 0; i < attackerSummonDatas.Length; i++)
            {
                deployButtons[i].LoadNewAttackerData(attackerSummonDatas[i]);
            }
        }
    }

    public class DefenderSummonDataComp : IComparer<DefenderSummonData>
    {
        public int Compare(DefenderSummonData x, DefenderSummonData y)
        {
            if (x == null && y != null)
            {
                return -1;
            }
            else if (y == null && x != null)
            {
                return 1;
            }
            else if (x == null && y == null)
            {
                return 0;
            }

            if (x.deployTime > y.deployTime)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }

    public class DefenderWithdrawDataComp : IComparer<DefenderWithdrawData>
    {
        public int Compare(DefenderWithdrawData x, DefenderWithdrawData y)
        {
            if (x == null && y != null)
            {
                return -1;
            }
            else if (y == null && x != null)
            {
                return 1;
            }
            else if (x == null && y == null)
            {
                return 0;
            }

            if (x.withdrawTime > y.withdrawTime)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}