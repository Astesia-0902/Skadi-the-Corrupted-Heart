using System.Collections.Generic;
using Attackers;
using UnityEngine;

namespace Game_Managers
{
    public class EntitySummoner : MonoBehaviour
    {
        public static List<Attacker> AttackersInGame;
        public static List<Transform> AttackerTransformsInGame; //这个“多余”的List是给Jobs System用的
        public static Dictionary<int, GameObject> AttackerPrefebs;
        public static Dictionary<int, Queue<Attacker>> AttackerSpawnPool;

        private static bool _isInitialized;

        public static void Initialize()
        {
            if (!_isInitialized)
            {
                AttackersInGame = new List<Attacker>();
                AttackerPrefebs = new Dictionary<int, GameObject>();
                AttackerSpawnPool = new Dictionary<int, Queue<Attacker>>();
                AttackerTransformsInGame = new List<Transform>();
                AttackerSummonData[] attackerSummonData = Resources.LoadAll<AttackerSummonData>("Attackers");

                foreach (AttackerSummonData item in attackerSummonData)
                {
                    AttackerPrefebs.Add(item.attackerID, item.attackerPrefeb);
                    AttackerSpawnPool.Add(item.attackerID, new Queue<Attacker>());
                }

                _isInitialized = true;
            }
            else
            {
                Debug.Log("Entity Summoner: Initialized.");
            }
        }

        /// <summary>
        /// 生成进攻单位
        /// </summary>
        /// <param name="attackerID"></param>
        /// <returns></returns>
        public static Attacker SpawnAttacker(int attackerID)
        {
            Attacker spawnedAttacker;

            //检查生成单位的ID是否合法
            if (AttackerPrefebs.ContainsKey(attackerID))
            {
                //这个队列暂存了之前嗝屁的同类型单位，如果这个队列不为空，就优先使用这个队列里的资源，节省性能
                Queue<Attacker> referencedQueue = AttackerSpawnPool[attackerID];

                if (referencedQueue.Count > 0)
                {
                    spawnedAttacker = referencedQueue.Dequeue();
                    spawnedAttacker.Initialize();
                }
                else
                {
                    //如果队列中没有现成的资源，就生成一个新的
                    //TODO:修改敌人出生的位置
                    GameObject newAttacker =
                        Instantiate(AttackerPrefebs[attackerID], GameLoopManager.NodesPosition[0], Quaternion.identity);
                    spawnedAttacker = newAttacker.GetComponent<Attacker>();
                    spawnedAttacker.Initialize();
                }
            }
            else
            {
                Debug.Log("Designated Attacker doesn't exist.");
                return null;
            }

            //更新当前场景内单位的列表
            AttackersInGame.Add(spawnedAttacker);
            AttackerTransformsInGame.Add(spawnedAttacker.transform);
            spawnedAttacker.id = attackerID;
            return spawnedAttacker;
        }

        /// <summary>
        /// 由于塔防游戏后期敌人大量刷新和死亡会占用大量的性能，我们将被击杀的敌人保留下来，之后生成同类型敌人时重复使用
        /// </summary>
        /// <param name="attackerToBeRemoved"></param>
        public static void RemoveAttacker(Attacker attackerToBeRemoved)
        {
            AttackerSpawnPool[attackerToBeRemoved.id].Enqueue(attackerToBeRemoved);
            attackerToBeRemoved.gameObject.SetActive(false);
            AttackersInGame.Remove(attackerToBeRemoved);
            AttackerTransformsInGame.Remove(attackerToBeRemoved.transform);
        }
    }
}