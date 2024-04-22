using UnityEngine;

[CreateAssetMenu(fileName = "Dragon", menuName = "Dragon", order = 0)]
public class Dragon : ScriptableObject {
    public int id;
    public string dragon_name = string.Empty;
    public Sprite icon;
    public Dragon_Egg egg;
}
