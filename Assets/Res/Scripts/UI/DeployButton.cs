using Game_Managers;
using Res.Scripts.Attackers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    /// <summary>
    /// ����ť��UI
    /// </summary>
    public class DeployButton : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public AttackerSummonData attackerSummonData;//��ǰ��ť��Ӧ�ĺ�������

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
