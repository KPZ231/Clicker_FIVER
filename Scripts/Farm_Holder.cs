using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

[System.Serializable]
public class Farm_Holder : MonoBehaviour
{
    public Farm farm;
    int timesUpgraded = 0;

    private string json = string.Empty;

    public class FarmPrefab
    {
        public int id;
        public string farmName = string.Empty;
        public int eggsGeneratedPerMin = 1;
        public int eggsMultipler;
        public int currentFarmLevel = 0;
        public int farmCost = 0;
        public int upgradeCost = 0;
    }

    private void Start()
    {
        StartCoroutine(CHCECK_ANIVABLITY_TIMER());
        StartCoroutine(ADD_CLICKS_FARM());
        SetDefault();
    }

    private IEnumerator CHCECK_ANIVABLITY_TIMER(float time = 0.1f)
    {
        while (Game_Manager.instance.isPlaying)
        {
            yield return new WaitForSeconds(time);

            CheckAnivablity();
        }

    }

    private IEnumerator ADD_CLICKS_FARM(float time = 1f)
    {
        if (FarmManager.instance.farmsCount > 0)
        {
            while (Game_Manager.instance.isPlaying)
            {
                yield return new WaitForSeconds(time);

                Click_Manager.instance.clickedTimes += farm.eggsGeneratedPerMin;

                Click_Manager.instance.UpdateCounter();
            }
        }

    }


    public void SetDefault()
    {
        FarmPrefab farmPref = new FarmPrefab();
        farmPref.id = 0;
        farmPref.farmName = "Farm 1";
        farmPref.currentFarmLevel = 0;
        farmPref.farmCost = 0;
        farmPref.upgradeCost = 30;
        farmPref.eggsGeneratedPerMin = 2;
        farmPref.eggsMultipler = 0;

        json = JsonUtility.ToJson(farmPref);

        Debug.Log(json);
        
        GetDefault();
    }


    public void GetDefault()
    {
        FarmPrefab farmPref = new FarmPrefab();
        farmPref.id = 0;
        farmPref.farmName = "Farm 1";
        farmPref.currentFarmLevel = 0;
        farmPref.farmCost = 0;
        farmPref.upgradeCost = 30;
        farmPref.eggsGeneratedPerMin = 2;
        farmPref.eggsMultipler = 0;

        farmPref = JsonUtility.FromJson<FarmPrefab>(json);

        farm.id = farmPref.id;
        farm.currentFarmLevel = farmPref.currentFarmLevel;
        farm.eggsGeneratedPerMin  = farmPref.eggsGeneratedPerMin;
        farm.farmCost = farmPref.farmCost;
        farm.upgradeCost = farmPref.upgradeCost;
    }


    private void CheckAnivablity()
    {
        if (FarmManager.instance.currFarm != null)
        {

            //Farm farm = FarmManager.instance.currFarm.gameObject.GetComponent<Farm_Holder>().farm;


            FarmManager.instance.currFarm.transform.Find("Cost").GetComponent<TextMeshProUGUI>().text = farm.upgradeCost.ToString();


            if (Click_Manager.instance.clickedTimes >= FarmManager.instance.currFarm.gameObject.GetComponent<Farm_Holder>().farm.upgradeCost)
            {
                FarmManager.instance.currFarm.transform.Find("Add").GetComponent<Button>().interactable = true;
            }
            else
            {
                FarmManager.instance.currFarm.transform.Find("Add").GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            Debug.Log("No Farm Selected");
        }

    }

    public void Add(GameObject currentFarm)
    {
        if (timesUpgraded <= 10)
        {
            timesUpgraded++;
        }

        //Farm farm = currentFarm.gameObject.GetComponent<Farm_Holder>().farm;
        Farm_Holder holder = currentFarm.gameObject.GetComponent<Farm_Holder>();

        if (Click_Manager.instance.clickedTimes >= farm.upgradeCost && timesUpgraded <= 10)
        {

            currentFarm.transform.Find("Add").GetComponent<Button>().interactable = true;
            Click_Manager.instance.clickedTimes -= farm.upgradeCost;

            FarmManager.instance.farms.Add(holder.GetComponent<Farm_Holder>());
            GameObject pref = Instantiate(FarmManager.instance.assets[farm.currentFarmLevel],
             currentFarm.transform.Find("BG").position, Quaternion.identity);

            pref.transform.SetParent(currentFarm.transform.Find("BG").transform);
        }
        else
        {
            currentFarm.transform.Find("Add").GetComponent<Button>().interactable = false;
        }

        switch (timesUpgraded)
        {
            case 0:
                farm.upgradeCost = 30;
                farm.eggsGeneratedPerMin = 2;
                break;
            case 1:
                farm.upgradeCost = 60;
                farm.eggsGeneratedPerMin = 5;
                break;
            case 2:
                farm.upgradeCost = 100;
                farm.eggsGeneratedPerMin = 10;
                break;
            case 3:
                farm.upgradeCost = 160;
                farm.eggsGeneratedPerMin = 15;
                break;
            case 4:
                farm.upgradeCost = 250;
                farm.eggsGeneratedPerMin = 30;
                break;
            case 5:
                farm.upgradeCost = 340;
                farm.eggsGeneratedPerMin = 70;
                break;
            case 6:
                farm.upgradeCost = 430;
                farm.eggsGeneratedPerMin = 100;
                break;
            case 7:
                farm.upgradeCost = 550;
                farm.eggsGeneratedPerMin = 200;
                break;
            case 8:
                farm.upgradeCost = 700;
                farm.eggsGeneratedPerMin = 500;
                break;
            case 9:
                farm.upgradeCost = 1000;
                farm.eggsGeneratedPerMin = 2500;
                break;
            default:
                farm.upgradeCost = 30;
                farm.eggsGeneratedPerMin = 2;
                break;
        }

    }

    // public void Sell(GameObject currentFarm)
    // {
    //     currentFarm = this.gameObject.transform.parent.gameObject;
    //     FarmManager.instance.farms.Remove(currentFarm.GetComponent<Farm_Holder>());
    //     Destroy(currentFarm);

    // }


}

