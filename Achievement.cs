using UnityEngine;

[CreateAssetMenu(fileName = "Achivement", menuName = "Achivement", order = 0)]
public class Achievement : ScriptableObject {
    public string content = string.Empty;
    public Sprite icon;
    public string path;
}