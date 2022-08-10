using System.Collections;
using System.Collections.Generic;
using Res.Scripts.Attackers;
using Tool_Scripts;
using UnityEngine;

namespace Res.Scripts.Game_Managers
{
    public class EntitySummoner : Singleton<EntitySummoner>
    {
        public List<Attacker> attackersInGame;
        public List<Attacker> attackerStationaryInGame;
        public List<Transform> attackerTransformsInGame; //这个“多余”的List是给Jobs System用的
        private Dictionary<int, GameObject> attackerPrefebs;
        private Dictionary<int, Queue<Attacker>> attackerSpawnPool;

        public List<AttackerSummonData> attackerList;
        private List<NodeLoopManager> nodeList;

        private Queue<AttackerSummonData> attackerListSingle;
        private Queue<NodeLoopManager> nodeListSingle;

        private static bool _isInitialized;

        public List<NodeLoopManager> nodeLoopManagers;

        protected override void Awake()
        {
            base.Awake();

            if (!_isInitialized)
            {
                attackerListSingle = new Queue<AttackerSummonData>();
                nodeListSingle = new Queue<NodeLoopManager>();
                attackerList = new List<AttackerSummonData>();
                nodeList = new List<NodeLoopManager>();
                attackersInGame = new List<Attacker>();
                attackerStationaryInGame = new List<Attacker>();
                attackerPrefebs = new Dictionary<int, GameObject>();
                attackerSpawnPool = new Dictionary<int, Queue<Attacker>>();
                attackerTransformsInGame = new List<Transform>();
                _isInitialized = true;
            }
            else
            {
                Debug.Log("Entity Summoner: Initialized.");
            }
        }

        private float summonTimer;

        private void Update()
        {
            summonTimer += Time.deltaTime;
            if (summonTimer >= 10f)
            {
                SummonAttackerAllInOnce();
                summonTimer = 0f;
            }
        }

        //与生成敌人相关的方法

        #region Summon Seaborn

        /// <summary>
        /// 将海嗣拖入出兵点后，将其加入出击列表
        /// </summary>
        /// <param name="attackerSummonData"></param>
        /// <param name="nodeLoopManager"></param>
        public void AddAttacker(AttackerSummonData attackerSummonData, NodeLoopManager nodeLoopManager)
        {
            //一号和五号出兵点是单次出兵点（二号也是，但是还没挂脚本）
            if (nodeLoopManager.spawnPointID == 1 || nodeLoopManager.spawnPointID == 5)
            {
                if (CostManager.Instance.DrainCost(attackerSummonData.cost))
                {
                    CostManager.Instance.StoreCost(attackerSummonData.cost);
                    attackerListSingle.Enqueue(attackerSummonData);
                    nodeListSingle.Enqueue(nodeLoopManager);
                    return;
                }
                else
                {
                    return;
                }
            }

            if (CostManager.Instance.DrainCost(attackerSummonData.cost))
            {
                //三号和四号是多次出兵点（将怪物在这两个点部署一次后，每波都会出）
                if (nodeLoopManager.spawnPointID == 3 || nodeLoopManager.spawnPointID == 4)
                {
                    attackerList.Add(attackerSummonData);
                    nodeList.Add(nodeLoopManagers[0]);
                    attackerList.Add(attackerSummonData);
                    nodeList.Add(nodeLoopManagers[1]);
                    return;
                }
                
                attackerList.Add(attackerSummonData);
                nodeList.Add(nodeLoopManager);
            }
            //TODO:更新UI
        }

        /// <summary>
        /// 一次性弹射所有待命的海嗣
        /// </summary>
        public void SummonAttackerAllInOnce()
        {
            StartCoroutine(SummonAllAttackerMultiIE());
            StartCoroutine(SummonAllAttackerSingleIE());
        }

        private IEnumerator SummonAllAttackerMultiIE()
        {
            for (int i = 0; i < attackerList.Count; i++)
            {
                SummonAttacker(attackerList[i], nodeList[i]);
                yield return new WaitForSeconds(0.3f);
            }
        }

        /// <summary>
        /// 从单次出怪点召唤进攻方
        /// </summary>
        /// <returns></returns>
        private IEnumerator SummonAllAttackerSingleIE()
        {
            while (attackerListSingle.Count > 0 && nodeListSingle.Count > 0)
            {
                CostManager.Instance.RegainCost();
                SummonAttacker(attackerListSingle.Dequeue(), nodeListSingle.Dequeue());
                yield return new WaitForSeconds(0.3f);
            }
        }

        private void SummonAttacker(AttackerSummonData attackerSummonData, NodeLoopManager node)
        {
            AddAttackerData(attackerSummonData);
            SpawnAttacker(attackerSummonData.attackerID, node);
        }

        /// <summary>
        /// 对需要生成的敌人进行记录
        /// </summary>
        /// <param name="attackerSummonData"></param>
        private void AddAttackerData(AttackerSummonData attackerSummonData)
        {
            //保存敌人prefeb的列表
            if (!attackerPrefebs.ContainsKey(attackerSummonData.attackerID))
            {
                attackerPrefebs.Add(attackerSummonData.attackerID, attackerSummonData.attackerPrefeb);
            }

            //记录敌人种类的dictionary
            if (!attackerSpawnPool.ContainsKey(attackerSummonData.attackerID))
            {
                attackerSpawnPool.Add(attackerSummonData.attackerID, new Queue<Attacker>());
            }
        }

        /// <summary>
        /// 生成进攻单位
        /// </summary>
        /// <param name="attackerID"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private void SpawnAttacker(int attackerID, NodeLoopManager node)
        {
            Attacker spawnedAttacker;

            //检查生成单位的ID是否合法
            if (attackerPrefebs.ContainsKey(attackerID))
            {
                //这个队列暂存了之前嗝屁的同类型单位，如果这个队列不为空，就优先使用这个队列里的资源，节省性能
                Queue<Attacker> referencedQueue = attackerSpawnPool[attackerID];

                if (referencedQueue.Count > 0)
                {
                    spawnedAttacker = referencedQueue.Dequeue();
                    spawnedAttacker.Initialize(node);
                }
                else
                {
                    //如果队列中没有现成的资源，就生成一个新的
                    GameObject newAttacker =
                        Instantiate(attackerPrefebs[attackerID], node.nodesPosition[0], Quaternion.identity);
                    spawnedAttacker = newAttacker.GetComponent<Attacker>();
                    spawnedAttacker.Initialize(node);
                }
            }
            else
            {
                Debug.Log("Designated Attacker doesn't exist.");
                return;
            }

            //更新当前场景内单位的列表
            attackersInGame.Add(spawnedAttacker);
            attackerTransformsInGame.Add(spawnedAttacker.transform);
            spawnedAttacker.id = attackerID;
        }

        #endregion

        /// <summary>
        /// 由于塔防游戏后期敌人大量刷新和死亡会占用大量的性能，我们将被击杀的敌人保留下来，之后生成同类型敌人时重复使用
        /// </summary>
        /// <param name="attackerToBeRemoved"></param>
        public void RemoveAttacker(Attacker attackerToBeRemoved)
        {
            if (attackerToBeRemoved != null)
            {
                attackerSpawnPool[attackerToBeRemoved.id].Enqueue(attackerToBeRemoved);
                attackerToBeRemoved.uiForUnits.DestroyBar();
                attackersInGame.Remove(attackerToBeRemoved);
                attackerTransformsInGame.Remove(attackerToBeRemoved.transform);
                attackerToBeRemoved.gameObject.SetActive(false);
            }
        }
    }
}