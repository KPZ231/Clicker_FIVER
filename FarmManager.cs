using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FarmManager : MonoBehaviour
{
    public static FarmManager instance { get; private set; }
    public int farmsCount = -1;
    public List<Farm_Holder> farms = new List<Farm_Holder>();
    public List<GameObject> assets = new List<GameObject>();
    public List<Farm> anivableFarms = new List<Farm>();

    public GameObject farmPrefab = null;
    public GameObject currFarm = null;


    [SerializeField] private Button addFarmButton = null;
    [SerializeField] private TextMeshProUGUI costText = null;

    public bool isToggled = false;
    private int cost = 200;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (isToggled)
        {
            if (farmsCount == 3)
            {
                addFarmButton.interactable = false;
            }
            else
            {
                addFarmButton.interactable = true;
            }

            costText.text = cost.ToString() + "$";
        }

    }

    public void ToggleUI()
    {
        if (isToggled)
        {
            GameObject par = GameObject.Find("FarmPanel");

            foreach (Image img in par.GetComponentsInChildren<Image>())
            {
                img.enabled = false;
                foreach (TextMeshProUGUI text in par.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    text.enabled = false;
                }
            }

            isToggled = false;
        }
        else
        {
            GameObject par = GameObject.Find("FarmPanel");

            foreach (Image img in par.GetComponentsInChildren<Image>())
            {
                img.enabled = true;
                foreach (TextMeshProUGUI text in par.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    text.enabled = true;
                }
            }


            isToggled = true;
        }
    }


    public void AddFarm()
    {
        switch (farmsCount)
        {
            default:
                cost = 200;
                break;
            case 0:
                cost = 300;
                break;
            case 1:
                cost = 500;
                break;
            case 2:
                cost = 1000;
                break;
            case 3:
                cost = 2000;
                break;
        }

        if (Click_Manager.instance.clickedTimes >= cost || Click_Manager.instance.fossilFragments >= cost)
        {
            Click_Manager.instance.clickedTimes -= cost;
            if (farmsCount <= 2)
            {
                GameObject parent = GameObject.Find("FarmLayout");
                GameObject farmPref = Instantiate(farmPrefab);
                farmPref.transform.SetParent(parent.transform);
                farmPref.transform.localScale = new Vector3(1, 1, 1);

                farmsCount++;

                if (farmPref.transform.GetChild(0).GetComponent<Farm_Holder>() != null)
                {
                    farms.Add(farmPref.GetComponent<Farm_Holder>());
                    Audio_Manager.instance.Play("Success_Bell");
                    farmPref.transform.GetChild(0).GetComponent<Farm_Holder>().farm = anivableFarms[farmsCount];
                }
            }
            else
            {
                Debug.LogWarning("Cannot add more farms");
            }
        }


    }

}
