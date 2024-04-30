using UnityEngine;
using UnityEngine.EventSystems;

public class Farm_Selection : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        FarmManager.instance.currFarm = this.gameObject.transform.Find("Farm").gameObject;
    }

}