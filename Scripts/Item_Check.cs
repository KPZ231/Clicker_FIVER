using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Check : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Shop_Manager.instance.currentSelectedButton = this.gameObject;
    }

}