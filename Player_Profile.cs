using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class Player_Profile : MonoBehaviour
{

    public static Player_Profile instance { get; private set; }

    public string playerName = "KPZ";
    public List<Item> itemsBought = new List<Item>();

    [HideInInspector] public bool isToggled = false;

    [Header("UI")]
    public GameObject[] uiAssets = { }; // 0 - Icon, 1 - Name, 2 - Eggs Hatched, 3 - Times Clicked, 4 - Legendaries Got, 5 - Achivements Got
    private void Start()
    {
        instance = this;
    }
    //

    public void Toggled()
    {
        if (!isToggled)
        {
            isToggled = true;
        }
        else
        {
            isToggled = false;
        }
    }


    private void Update()
    {
        if (isToggled)
        {
            LoadStats();
        }
    }

    private void LoadStats()
    {
        //uiAssets[0].gameObject.GetComponent<Image>().sprite = 
        uiAssets[2].gameObject.GetComponent<TextMeshProUGUI>().text = "Hatched Eggs: " + Dragon_Manager.instance.hatchedDragons.Count.ToString();
        uiAssets[3].gameObject.GetComponent<TextMeshProUGUI>().text = "Times Clicked: " + Click_Manager.instance.clickedTimes.ToString();
        uiAssets[4].gameObject.GetComponent<TextMeshProUGUI>().text = "Clicks Per Second:  " + Click_Manager.instance.times;
        uiAssets[5].gameObject.GetComponent<TextMeshProUGUI>().text = "Achivements Got: " + Achivements_Manager.instance.achivementCounter;
       
    }

}



