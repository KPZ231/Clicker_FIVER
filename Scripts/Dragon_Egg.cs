using UnityEngine;

public enum Dragon_Rarity
{
    Legendary,
    Mythic,
    Very_Rare,
    Rare,
    Uncommon,
    Common
}

[CreateAssetMenu(fileName = "Dragon_Egg", menuName = "Dragon_Egg", order = 0)]
public class Dragon_Egg : ScriptableObject
{
    public int id;
    public string dragon_name = string.Empty;
    public Sprite icon;
    public Dragon_Rarity rarity;
}