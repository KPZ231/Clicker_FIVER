using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Manager : MonoBehaviour
{
    public static Shop_Manager instance { get; private set; }
    [Header("Shop Inventory")]
    public List<Shop_Item> items = new List<Shop_Item>();
    [SerializeField] private GameObject shopButtonPrefab = null;

    public GameObject currentSelectedButton = null;

    private GameObject shopPlaceholder = null;


    private void Start()
    {
        instance = this;

        shopPlaceholder = GameObject.Find("Grid");

        GenerateItems();
    }

    private void GenerateItems()
    {
        int count = 0;
        foreach (Shop_Item item in items)
        {
            string tempButtonName = string.Empty;
            GameObject button = Instantiate(shopButtonPrefab);
            button.transform.SetParent(shopPlaceholder.transform, false);
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item._itemName;
            button.AddComponent<Item_Check>();
            Item item_ = button.AddComponent<Item>();
            tempButtonName = button.name;
            button.name = tempButtonName + " " + count;
            button.transform.GetComponent<Image>().sprite = item.icon;
            item_.shop_Item = items[count];
            count++;
        }
    }

    public void CheckItem()
    {
        if (currentSelectedButton.GetComponent<Item>() != null)
        {
            Item item = currentSelectedButton.GetComponent<Item>();

            if (Click_Manager.instance.clickedTimes >= item.shop_Item.cost)
            {
                Click_Manager.instance.clickedTimes -= item.shop_Item.cost;
                Click_Manager.instance.UpdateCounter();

                if (item.shop_Item.__itemType == ItemType.ClickerBooster && item.shop_Item.id == 1) //Double Click
                {
                    if (item.timesBought < 2)
                    {
                        item.timesBought++;
                        ClickerBooster(2);
                    }
                    else
                    {
                        item.transform.gameObject.GetComponent<Button>().interactable = false;
                        item.transform.gameObject.GetComponent<Item>().enabled = false;
                        item.transform.gameObject.GetComponent<Item_Check>().enabled = false;
                    }
                }
                else if (item.shop_Item.__itemType == ItemType.ClickerBooster && item.shop_Item.id == 2) //Triple Click
                {
                    if (item.timesBought < 2)
                    {
                        item.timesBought++;
                        ClickerBooster(3);
                    }
                    else
                    {
                        item.transform.gameObject.GetComponent<Button>().interactable = false;
                        item.transform.gameObject.GetComponent<Item>().enabled = false;
                        item.transform.gameObject.GetComponent<Item_Check>().enabled = false;
                    }
                }
            }
        }
    }

    private void ClickerBooster(int times)
    {
        Click_Manager.instance.times *= times;
    }
}
