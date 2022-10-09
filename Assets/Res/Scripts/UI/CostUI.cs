using System;
using Res.Scripts.Game_Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Res.Scripts.UI
{
    public class CostUI : MonoBehaviour
    {
        private Text costText;

        private void Awake()
        {
            costText = GetComponentInChildren<Text>();
        }

        private void Start()
        {
            CostManager.Instance.onCostChange += UpdateCostText;
        }

        private void UpdateCostText()
        {
            costText.text = CostManager.Instance.currentCost.ToString();
        }

        private void OnDisable()
        {
            if (CostManager.Instance != null)
                if (CostManager.Instance.onCostChange != null)
                    CostManager.Instance.onCostChange -= UpdateCostText;
        }
    }
}