using UnityEngine;



[CreateAssetMenu(fileName = "Farm", menuName = "Farm", order = 0)]
public class Farm : ScriptableObject
{
    public int id;
    public string farmName = string.Empty;
    public int eggsGeneratedPerMin = 1;
    public int eggsMultipler;
    public int currentFarmLevel = 0;
}