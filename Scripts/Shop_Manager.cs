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

    public GameObject currentSelectedButton;
    
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
            count++;
            GameObject button = Instantiate(shopButtonPrefab);
            button.transform.SetParent(shopPlaceholder.transform, false);
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item._itemName;
            button.AddComponent<Item_Check>();
            tempButtonName = button.name;
            button.name = tempButtonName + " " + count;
            button.transform.GetComponent<Image>().sprite = item.icon;
        }
    }
}
