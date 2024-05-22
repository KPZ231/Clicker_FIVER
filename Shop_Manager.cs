using System.Collections;
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

    public bool eggBought = true;


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


            if (item.isForFossil && Game_Manager.instance.gameData.resetTimes == 0)
            {
                GameObject button = Instantiate(shopButtonPrefab);
                button.transform.SetParent(shopPlaceholder.transform, false);
                button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item._itemName;
                button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.cost.ToString() + "$";
                button.AddComponent<Item_Check>();
                Item item_ = button.AddComponent<Item>();
                tempButtonName = button.name;
                button.name = tempButtonName + " " + count;
                button.transform.GetComponent<Image>().sprite = item.icon;
                item_.shop_Item = items[count];
                button.GetComponent<Button>().interactable = false;
            }
            else if (!item.isForFossil)
            {
                GameObject button = Instantiate(shopButtonPrefab);
                button.transform.SetParent(shopPlaceholder.transform, false);
                button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item._itemName;
                button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.cost.ToString() + "$";
                button.AddComponent<Item_Check>();
                Item item_ = button.AddComponent<Item>();
                tempButtonName = button.name;
                button.name = tempButtonName + " " + count;
                button.transform.GetComponent<Image>().sprite = item.icon;
                item_.shop_Item = items[count];
            }
            else if (item.isForFossil && Game_Manager.instance.gameData.resetTimes >= 1)
            {
                GameObject button = Instantiate(shopButtonPrefab);
                button.transform.SetParent(shopPlaceholder.transform, false);
                button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item._itemName;
                button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.cost.ToString() + "$";
                button.AddComponent<Item_Check>();
                Item item_ = button.AddComponent<Item>();
                tempButtonName = button.name;
                button.name = tempButtonName + " " + count;
                button.transform.GetComponent<Image>().sprite = item.icon;
                item_.shop_Item = items[count];
            }

            count++;

        }
    }

    public void CheckItem()
    {
        if (currentSelectedButton.GetComponent<Item>() != null)
        {
            Item item = currentSelectedButton.GetComponent<Item>();

            if (Click_Manager.instance.clickedTimes >= item.shop_Item.cost && !item.shop_Item.isForFossil)
            {
                Game_Manager.instance.AddElement(currentSelectedButton.GetComponent<Item>().shop_Item);

                Click_Manager.instance.clickedTimes -= item.shop_Item.cost;
                Click_Manager.instance.UpdateCounter();
                Audio_Manager.instance.Play("Success_Bell");

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
                else if (item.shop_Item.__itemType == ItemType.AutomaticClicker && item.shop_Item.id == 3)//Auto Clicker
                {
                    StartCoroutine(GAIN_CLICKS(0.3f));
                }
                else if (item.shop_Item.__itemType == ItemType.CommonEgg && item.shop_Item.id == 4)
                {
                    GuarenteeEggs(Rarity.Common);
                }
                else if (item.shop_Item.__itemType == ItemType.RareEgg && item.shop_Item.id == 5)
                {
                    GuarenteeEggs(Rarity.Rare);
                }

            }
            else if (Game_Manager.instance.gameData.resetTimes >= 1 && item.shop_Item.isForFossil)
            {
                if (Click_Manager.instance.fossilFragments >= item.shop_Item.fossilCost)
                {
                    Game_Manager.instance.AddElement(currentSelectedButton.GetComponent<Item>().shop_Item);

                    Click_Manager.instance.fossilFragments -= item.shop_Item.cost;
                    Click_Manager.instance.UpdateCounter();
                    Audio_Manager.instance.Play("Success_Bell");

                    if (item.shop_Item.__itemType == ItemType.LegendaryEgg && item.shop_Item.id == 6)
                    {
                        GuarenteeEggs(Rarity.Legendary);
                    }
                    else if (item.shop_Item.__itemType == ItemType.ClickerBooster && item.shop_Item.id == 7)
                    {
                        ClickerBooster(5);
                    }
                    else if (item.shop_Item.__itemType == ItemType.ClickerBooster && item.shop_Item.id == 8)
                    {
                        ClickerBooster(8);
                    }
                    else if (item.shop_Item.__itemType == ItemType.ClickerBooster && item.shop_Item.id == 9)
                    {
                        ClickerBooster(10);
                    }
                    else if (item.shop_Item.__itemType == ItemType.ClickerBooster && item.shop_Item.id == 10)
                    {
                        ClickerBooster(15);
                    }
                }
            }
        }
    }

    private void ClickerBooster(int times)
    {
        Click_Manager.instance.times *= times;
    }

    public IEnumerator GAIN_CLICKS(float time)
    {
        while (Game_Manager.instance.isPlaying)
        {
            yield return new WaitForSeconds(time);

            AutoCLicker();

            Click_Manager.instance.UpdateCounter();
        }
    }

    private void AutoCLicker()
    {
        Click_Manager.instance.clickedTimes += Game_Manager.instance.gameData.resetTimes + 1;
    }

    public void GuarenteeEggs(Rarity rarity)
    {
        if (rarity == Rarity.Legendary)
        {
            Dragon_Manager.instance.choosedDragon_ = Dragon_Manager.instance.dragons[Random.Range(7, 8)];
            eggBought = true;
        }
        else if (rarity == Rarity.Rare)
        {
            Dragon_Manager.instance.choosedDragon_ = Dragon_Manager.instance.dragons[Random.Range(5, 6)];
            eggBought = true;
        }
        else
        {
            Dragon_Manager.instance.choosedDragon_ = Dragon_Manager.instance.dragons[Random.Range(0, 4)];
            eggBought = true;
        }
    }
}
