using System.Collections.Generic;
using UnityEngine;

public class Dragon_Manager : MonoBehaviour
{
    public static Dragon_Manager instance { get; private set; }

    [Header("Dragon")]
    public List<Dragon> dragons = new List<Dragon>();
    public Dragon choosedDragon_ = null;

    private void Start()
    {
        instance = this;
    }

    public void Hatch()
    {
        int choosedDragon;
        int prct;

        prct = Random.Range(0, 100);

        if (prct >= 0 && prct <= 20)
        {
            choosedDragon = Random.Range(0, 5);
            choosedDragon_ = dragons[choosedDragon];
        }
        else if (prct > 21 && prct <= 50)
        {
            choosedDragon = Random.Range(6, 12);
            choosedDragon_ = dragons[choosedDragon];
        }
        else if (prct > 50 && prct <= 75)
        {
            choosedDragon = Random.Range(13, 18);
            choosedDragon_ = dragons[choosedDragon];
        }
        else if (prct > 76 && prct <= 100)
        {
            choosedDragon = Random.Range(19, 22);
            choosedDragon_ = dragons[choosedDragon];
        }

        ShowDragon();
    }

    public void ShowDragon()
    {
        Debug.Log("Hatched" + choosedDragon_.dragon_name);
    }
}