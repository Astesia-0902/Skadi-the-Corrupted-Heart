using Game_Managers;
using Res.Scripts.Attackers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    /// <summary>
    /// 部署按钮的UI
    /// </summary>
    public class DeployButton : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public AttackerSummonData attackerSummonData;//当前按钮对应的海嗣种类

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
