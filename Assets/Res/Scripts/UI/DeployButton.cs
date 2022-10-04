using Res.Scripts.Attackers;
using Res.Scripts.Game_Managers;
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
            if (newData == null)
            {
                SetEmpty();
            }
            else
            {
                attackerSummonData = newData;
                myImage.color = new Color(1f, 1f, 1f, 0.6f);
            }
            
            myImage.sprite = newData.uiImage != null ? newData.uiImage : null;
        }

        public void SetEmpty()
        {
            attackerSummonData = null;
            if(myImage != null)
            {
                myImage.color = new Color(0, 0, 0, 0f);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            GameManager.Instance.BeginDeploy(attackerSummonData);
        }
    
        public void OnDrag(PointerEventData eventData)
        {
            if (attackerSummonData == null)
            {
                return;
            }
            
            GameManager.Instance.DragDeploy();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (attackerSummonData == null)
            {
                return;
            }
            
            GameManager.Instance.EndDrag();
        }
    }
}
