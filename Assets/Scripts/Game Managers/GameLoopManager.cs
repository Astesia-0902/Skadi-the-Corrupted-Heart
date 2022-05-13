using System.Collections;
using System.Collections.Generic;
using Attackers;
using Tool_Scripts;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Game_Managers
{
    public class GameLoopManager : Singleton<GameLoopManager>
    {
        private static Queue<Attacker> _attackersToRemove;
        private static Queue<int> _attackersIDToSummon;

        public bool loopFlag;
        public Transform nodesParent;
        public static Vector3[] NodesPosition;

        private void Start()
        {
            _attackersToRemove = new Queue<Attacker>();
            _attackersIDToSummon = new Queue<int>();

            //TODO:未来将会有多条进攻路线，且由玩家自由选择
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
                //TODO:Spawn Attackers 由于由玩家手动选择出生点，这里需要修改

                //TODO:Spawn Defenders

                //Move Attackers 进攻方的移动
                MoveAttackers();

                //TODO:Tick Defenders

                //TODO:Apply Effects

                //TODO:Damage Attackers

                //TODO:Damage Defenders

                //Remove Defenders
                if (_attackersToRemove.Count > 0)
                {
                    for (int i = 0; i < _attackersToRemove.Count; i++)
                    {
                        EntitySummoner.RemoveAttacker(_attackersToRemove.Dequeue());
                    }
                }

                //TODO:Remove Attackers

                yield return null;
            }
        }

        private static void MoveAttackers()
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
                nodeIndex[i] = EntitySummoner.AttackersInGame[i].nodeIndex;
                attackerSpeed[i] = EntitySummoner.AttackersInGame[i].moveSpeed;
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

        public static void EnqueueAttackerIDToSummon(int id)
        {
            _attackersIDToSummon.Enqueue(id);
        }

        public static void EnqueueAttackerToRemove(Attacker attacker)
        {
            _attackersToRemove.Enqueue(attacker);
        }
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