using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public static FarmManager instance { get; private set; }
    public int farmsCount = 0;
    public List<Farm_Holder> farms = new List<Farm_Holder>();
    public List<GameObject> assets = new List<GameObject>();

    public GameObject currFarm = null;


    private void Start()
    {
        instance = this;
    }

}
