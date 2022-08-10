using System.Collections.Generic;
using Game_Managers;
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
        private float lighthouseActivateTimer = 5f;
        private float lighthouseActivateTimerBuffer;
        private bool startFlag;
        private bool lighthouseFlag;

        private Queue<DefenderSummonData> defenderSummonQueue;
        private Queue<DefenderWithdrawData> defenderWithdrawQueue;


        private void Awake()
        {
            defenderSummonQueue = new Queue<DefenderSummonData>();
            defenderWithdrawQueue = new Queue<DefenderWithdrawData>();
        }

        private void Start()
        {
            //���ļ��ж�ȡ��Ա�Ĳ�������
            DefenderSummonData[] allDefenderSummonData = Resources.LoadAll<DefenderSummonData>("Defenders");
            DefenderWithdrawData[] allDefenderWithdrawData =
                Resources.LoadAll<DefenderWithdrawData>("Defenders Withdraw");

            foreach (DefenderSummonData defenderSummonData in allDefenderSummonData)
            {
                Debug.Log("��¼��������Ϣ��" + defenderSummonData.name);
                defenderSummonQueue.Enqueue(defenderSummonData);
            }

            foreach (DefenderWithdrawData defenderWithdrawData in allDefenderWithdrawData)
            {
                Debug.Log("��¼������������Ϣ��" + defenderWithdrawData.name);
                defenderWithdrawQueue.Enqueue(defenderWithdrawData);
            }

            //startFlag = true;
        }

        private void Update()
        {
            LighthouseUpdate();
            
            if (startFlag)
            {
                //����ս���ļ�ʱ��
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

        /// <summary>
        /// ��鵱ǰʱ����Ƿ��и�Ա��Ҫ�³�
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
        /// �������
        /// </summary>
        private void ActivateLighthouse()
        {
            lighthouseTimer = 0;
            lighthouseFlag = true;
            Debug.Log("����������");
            foreach (Attacker attacker in EntitySummoner.Instance.attackersInGame)
            {
                attacker.GetStunned(3600f);
            }

            GameManager.Instance.skadi.GetStunned(20f);
        }

        private float damageTimer;
        /// <summary>
        /// ���������ĳ���Ч��
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
        /// ���º��õĲ���ť
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
}