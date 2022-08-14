using System;
using Game_Managers;
using Res.Scripts.Attackers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Res.Scripts.UI
{
    public class DeployButton : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public AttackerSummonData attackerSummonData;
        private Image myImage;

        private void Awake()
        {
            myImage = GetComponent<Image>();
        }

        public void LoadNewAttackerData(AttackerSummonData newData)
        {
            attackerSummonData = newData;
            if (newData.uiImage != null)
            {
                myImage.sprite = newData.uiImage;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            GameManager.Instance.BeginDeploy(attackerSummonData);
        }
    
        public void OnDrag(PointerEventData eventData)
        {
            GameManager.Instance.DragDeploy();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            GameManager.Instance.EndDrag();
        }
    }
}
