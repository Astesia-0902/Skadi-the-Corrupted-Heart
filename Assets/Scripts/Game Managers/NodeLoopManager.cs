using System;
using System.Collections;
using System.Collections.Generic;
using Attackers;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Game_Managers
{
    public class NodeLoopManager : MonoBehaviour
    {
        //TODO：目前实现多个进攻路线的思路是将Game Loop Manager中控制进攻方移动的部分分离出来，多挂载几个物体，每个物体赋予各自的NodeParent
        private Queue<Attacker> attackersToRemove;
        private Queue<int> attackersIDToSummon;

        public bool loopFlag;
        public Transform nodesParent;
        public Vector3[] NodesPosition;

        public int spawnPointID;

        private void Awake()
        {
            nodesParent = transform.GetChild(0);
        }

        private void Start()
        {
            attackersToRemove = new Queue<Attacker>();
            attackersIDToSummon = new Queue<int>();
            
            NodesPosition = new Vector3[nodesParent.childCount];
            for (int i = 0; i < NodesPosition.Length; i++)
            {
                NodesPosition[i] = nodesParent.GetChild(i).position;
            }

            loopFlag = true;
            StartCoroutine(GameLoop());
        }

        IEnumerator GameLoop()
        {
            while (loopFlag)
            {
                MoveAttackers();
                
                //Remove Defenders
                if (attackersToRemove.Count > 0)
                {
                    for (int i = 0; i < attackersToRemove.Count; i++)
                    {
                        EntitySummoner.RemoveAttacker(attackersToRemove.Dequeue());
                    }
                }

                yield return null;
            }
        }

        private void MoveAttackers()
        {
            //为什么Unity要使用NativeArray而不是List，应该有以下几点好处
            //不需要GC，速度快，生成和销毁代价低，适合作为ECS中的临时数据结构，Native可以直接调用。缺点是功能不如List多。
            //顺便一提，NativeArray包含在Unity.Collection这个命名空间，而不是System.Collection
            NativeArray<int> nodeIndex =
                new NativeArray<int>(EntitySummoner.AttackersInGame.Count, Allocator.TempJob); //临时作业分配
            NativeArray<float> attackerSpeed =
                new NativeArray<float>(EntitySummoner.AttackersInGame.Count, Allocator.TempJob);
            NativeArray<Vector3> nodesToUse = new NativeArray<Vector3>(NodesPosition, Allocator.TempJob);
            //同样地，这个数据类型也是Jobs System下的，可以大量并行地处理Transform
            TransformAccessArray attackerAccess = //desiredJobCount:需要同时并行处理数据的数量
                new TransformAccessArray(EntitySummoner.AttackerTransformsInGame.ToArray(), 2);

            for (int i = 0; i < EntitySummoner.AttackersInGame.Count; i++)
            {
                if (EntitySummoner.AttackersInGame[i].spawnPoint == spawnPointID)
                {
                    nodeIndex[i] = EntitySummoner.AttackersInGame[i].nodeIndex;
                    attackerSpeed[i] = EntitySummoner.AttackersInGame[i].moveSpeed;
                }
            }

            //初始化Job
            MoveAttackersJob moveJob = new MoveAttackersJob
            {
                NodesPositions = nodesToUse,
                AttackerSpeeds = attackerSpeed,
                NodesIndex = nodeIndex,
                DeltaTime = Time.deltaTime
            };

            //JobHandle 用于标识已调度作业的句柄。可用作后续作业的依赖项，也可确保在主线程中执行完成。
            JobHandle moveJobHandle = moveJob.Schedule(attackerAccess); //参数则是JobData
            //确保该作业已完成
            moveJobHandle.Complete();

            //在一轮调度完成以后，检查进攻单位的路径点是否是终点
            for (int i = 0; i < EntitySummoner.AttackersInGame.Count; i++)
            {
                //确保数组下标和AttackersInGame对齐
                if (nodeIndex[i] == 0)
                    continue;
                
                EntitySummoner.AttackersInGame[i].nodeIndex = nodeIndex[i];

                //进攻方的目标节点为最后一个时，视为到达终点。
                if (EntitySummoner.AttackersInGame[i].nodeIndex == NodesPosition.Length)
                {
                    //TODO：进攻方到达了目标点
                    EnqueueAttackerToRemove(EntitySummoner.AttackersInGame[i]);
                }
            }

            //NativeArray不使用GC，需要手动释放内存
            nodeIndex.Dispose();
            attackerSpeed.Dispose();
            nodesToUse.Dispose();
            attackerAccess.Dispose();
        }

        public void EnqueueAttackerIDToSummon(int id)
        {
            attackersIDToSummon.Enqueue(id);
        }

        public void EnqueueAttackerToRemove(Attacker attacker)
        {
            attackersToRemove.Enqueue(attacker);
        }
    }
    
    public struct MoveAttackersJob : IJobParallelForTransform
    {
        //允许多个线程同时读写一个容器
        [NativeDisableParallelForRestriction] public NativeArray<Vector3> NodesPositions;

        [NativeDisableParallelForRestriction] public NativeArray<float> AttackerSpeeds;

        [NativeDisableParallelForRestriction] public NativeArray<int> NodesIndex;

        public float DeltaTime;

        public void Execute(int index, TransformAccess transform)
        {
            if (NodesIndex[index] < NodesPositions.Length)
            {
                Vector3 positionMoveTo = NodesPositions[NodesIndex[index]];
                transform.position =
                    Vector3.MoveTowards(transform.position, positionMoveTo, AttackerSpeeds[index] * DeltaTime);

                if (transform.position == positionMoveTo)
                {
                    NodesIndex[index]++;
                }
            }
        }
    }
}