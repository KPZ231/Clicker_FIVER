using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Check : MonoBehaviour, IPointerClickHandler
{

    [HideInInspector] public Item currentItem = null;
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Shop_Manager.instance.currentSelectedButton = this.gameObject;
        Shop_Manager.instance.CheckItem();

        currentItem = Shop_Manager.instance.currentSelectedButton.GetComponent<Item>();
    }
}