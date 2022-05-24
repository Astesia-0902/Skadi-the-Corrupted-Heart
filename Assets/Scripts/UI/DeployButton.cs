using Game_Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeployButton : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public AttackerSummonData attackerSummonData;

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Mouse Down");
        GameManager.Instance.BeginDeploy(attackerSummonData);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Mouse Drag");
        GameManager.Instance.DragDeploy();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Mouse Up");
        GameManager.Instance.EndDrag();
    }
}
