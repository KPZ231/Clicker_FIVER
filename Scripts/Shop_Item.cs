using System.ComponentModel;
using UnityEngine;

public enum ItemType
{
    ClickerBooster,
    ClicksBooster,
    Farm
}

[CreateAssetMenu(fileName = "Shop_Item", menuName = "Shop_Item", order = 0)]
public class Shop_Item : ScriptableObject
{
    public int id;
    public string _itemName = string.Empty;
    public Sprite icon;
    public int cost = 10;
    public ItemType itemType;
    public int clickerBoosterMultipler = 0; //Levave null if the other type is selected
    public int clickBoosterMultipler = 0;//Levave null if the other type is selected
    public Farm farm = null; //Levave null if the other type is selected

}