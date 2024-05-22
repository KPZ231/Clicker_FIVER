using UnityEngine;

public enum Rarity
{
    Legendary,
    Rare,
    Common
}

[CreateAssetMenu(fileName = "Dragon", menuName = "Dragon", order = 0)]


public class Dragon : ScriptableObject
{
    public int id;
    public string dragon_name = string.Empty;
    public Sprite icon;
    public Dragon_Egg egg;
    public Color color;

    public string dragonIconPath_;
    public Rarity rarity;
}
