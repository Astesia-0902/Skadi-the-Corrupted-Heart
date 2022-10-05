using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Res.Scripts.Attackers;
using Res.Scripts.Defenders;
using Res.Scripts.Defenders._Elysium;
using Res.Scripts.Defenders._Mon3tr;
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
        public int lighthouseTimer;
        public bool startFlag;
        public bool lighthouseFlag;

        public DeployButton[] deployButtons;
        public Material[] material;

        private Queue<DefenderSummonData> defenderSummonQueue;
        private Queue<DefenderWithdrawData> defenderWithdrawQueue;

        public Transform sunLight;
        public Vector3 sunLightRotation;

        protected override void Awake()
        {
            base.Awake();
            defenderSummonQueue = new Queue<DefenderSummonData>();
            defenderWithdrawQueue = new Queue<DefenderWithdrawData>();
        }

        private void Start()
        {
            sunLightRotation = new Vector3(250f, -17.56f, 243f);
            material = Resources.LoadAll<Material>("Mat");
            dissolveValue = 0f;
            material[0].SetFloat(Clip, dissolveValue);
            DefenderSummonData[] allDefenderSummonData = Resources.LoadAll<DefenderSummonData>("Defenders");
            DefenderWithdrawData[] allDefenderWithdrawData =
                Resources.LoadAll<DefenderWithdrawData>("Defenders Withdraw");

            Array.Sort(allDefenderSummonData, new DefenderSummonDataComp());
            Array.Sort(allDefenderWithdrawData, new DefenderWithdrawDataComp());

            foreach (DefenderSummonData defenderSummonData in allDefenderSummonData)
            {
                defenderSummonQueue.Enqueue(defenderSummonData);
            }

            foreach (DefenderWithdrawData defenderWithdrawData in allDefenderWithdrawData)
            {
                defenderWithdrawQueue.Enqueue(defenderWithdrawData);
            }

            startFlag = true;
            LoadNewAttackersData(waveCount.ToString());
        }

        private void Update()
        {
            sunLightRotation.x -= 0.4f * Time.deltaTime;
            sunLight.rotation = Quaternion.Euler(sunLightRotation);
            LighthouseUpdate();

            if (startFlag)
            {
                timerAccumulator += Time.deltaTime;
                
                if (timerAccumulator >= 1f)
                {
                    timer++;
                    TimelineChecker();

                    if (!lighthouseFlag)
                    {
                        lighthouseTimer++;
                    }

                    if (lighthouseTimer >= 60)
                    {
                        ActivateLighthouse();
                        waveCount++;
                    }

                    timerAccumulator = 0f;
                }
            }
        }

        public void SkillChecker()
        {
            if (timer == 12)
            {
                FindObjectOfType<Elysium>().CastSkill();
            }

            if (timer == 69)
            {
                FindObjectOfType<Mon3TR>().CastSkill();
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

        public void ActivateLighthouse()
        {
            lighthouseTimer = 0;
            lighthouseFlag = true;
            CostManager.Instance.DrainCost(CostManager.Instance.currentCost);
            foreach (Attacker attacker in EntitySummoner.Instance.attackersInGame)
            {
                attacker.GetStunned(3600f);
            }

            EntitySummoner.Instance.ClearQueue();
            StartCoroutine(EmptyAttackerSummonDataIE());

            //眩晕斯卡蒂
            GameManager.Instance.skadi.GetStunned(9999f);
            CostManager.Instance.DisableCostRecovery(10f);
        }

        private float damageTimer;

        private void LighthouseUpdate()
        {
            if (!lighthouseFlag)
                return;

            damageTimer += Time.deltaTime;
            if (damageTimer >= 1f)
            {
                damageTimer = 0f;
                foreach (Attacker attacker in EntitySummoner.Instance.attackersInGame)
                {
                    attacker.TakeDamage(0, 0, 9000f);
                }
            }
        }

        public void NextWave()
        {
            lighthouseFlag = false;
            GameManager.Instance.skadi.TakeDamage(0, 0, 0.25f * GameManager.Instance.skadi.maxHealth);
            GameManager.Instance.skadi.StunRecover();
            LoadNewAttackersData(waveCount.ToString());
            GameManager.Instance.skadi.WaveCheck(waveCount);
            CostManager.Instance.AddCost(60 * (waveCount - 1));
        }

        private float dissolveValue;
        private static readonly int Clip = Shader.PropertyToID("_Clip");

        private void LoadNewAttackersData(string wave)
        {
            EntitySummoner.Instance.ClearQueue();
            AttackerSummonData[] attackerSummonDatas =
                Resources.LoadAll<AttackerSummonData>("Attacker Summon Data/" + wave);

            StartCoroutine(LoadAttackerSummonDataIE(attackerSummonDatas));
        }

        private IEnumerator EmptyAttackerSummonDataIE()
        {
            while (dissolveValue < 1f)
            {
                dissolveValue += Time.deltaTime;
                material[0].SetFloat(Clip, dissolveValue);
                yield return null;
            }

            foreach (DeployButton deployButton in deployButtons)
            {
                deployButton.SetEmpty();
            }
        }

        private IEnumerator LoadAttackerSummonDataIE(AttackerSummonData[] attackerSummonDatas)
        {
            for (int i = 0; i < attackerSummonDatas.Length; i++)
            {
                deployButtons[i].LoadNewAttackerData(attackerSummonDatas[i]);
            }

            while (dissolveValue > 0f)
            {
                dissolveValue -= Time.deltaTime;
                material[0].SetFloat(Clip, dissolveValue);
                yield return null;
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