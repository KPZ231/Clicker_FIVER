using System.ComponentModel;
using UnityEngine;

public enum ItemType
{
    ClickerBooster,
    ClicksBooster,
    AutomaticClicker,
    LegendaryEgg,
    RareEgg,
    CommonEgg,
    ShellFragments,
    FossilFragments,
    AmberFragments
}

[CreateAssetMenu(fileName = "Shop_Item", menuName = "Shop_Item", order = 0)]
public class Shop_Item : ScriptableObject
{
    public int id;
    public string _itemName = string.Empty;
    public Sprite icon;
    public int cost = 10;
    public int fossilCost = 100;
    public int amberCost = 1;
    public int shellGain = 100;
    public int fossilGain = 100;
    public int amberGain = 10;
    public ItemType __itemType;
    public int clickerBoosterMultipler = 0; //Levave null if the other type is selected
    public int clickBoosterMultipler = 0;//Levave null if the other type is selected
    public bool isForFossil = false;

}