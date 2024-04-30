using UnityEngine;
using System.Collections.Generic;

public class Player_Profile : MonoBehaviour
{

    public static Player_Profile instance {get; private set;}

    public string playerName = string.Empty;
    public int timesClicked = 0;
    public int currentFarms = 0;
    public List<Item> itemsBought = new List<Item>();

    private void Start(){
        instance = this;
    }

    private void Update() {
        timesClicked = Click_Manager.instance.clickedTimes;
    }

    // public void UpdateItems(Item item){
    //     if()
    //     item = Shop_Manager.instance.currentSelectedButton.GetComponent<Item>();
    //     itemsBought.Add(item);
    // }

}



