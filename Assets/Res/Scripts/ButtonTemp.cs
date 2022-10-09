using System;
using System.Collections;
using System.Collections.Generic;
using Res.Scripts.Attackers;
using Res.Scripts.Game_Managers;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTemp : MonoBehaviour
{
    private Button thisButton;
    public int spawnPointNum;
    public NodeLoopManager nodeLoopManager;
    public AttackerSummonData attackerSummonData;
    private void Awake()
    {
        thisButton = GetComponent<Button>();
        //thisButton.onClick.AddListener(CallSummon);
    }

    // private void CallSummon()
    // {
    //     EntitySummoner.Instance.SummonAttacker(attackerSummonData, nodeLoopManager);
    // }
}
