using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Achivements_Manager : MonoBehaviour
{
    public static Achivements_Manager instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject achivementAnimation = null;
    public int achivementCounter = 0;
    public bool isToggled = false;

    [SerializeField] private GameObject grid = null;
    [SerializeField] private GameObject prefab = null;

    [Header("Achivements")]
    [SerializeField] public List<Achievement> achivements = new List<Achievement>();

    private Dictionary<string, bool> conditionMet = new Dictionary<string, bool>();
    public List<Achievement> completedAchivements = new List<Achievement>();

    private void Start()
    {
        instance = this;
        InitializeConditions();
        LoadCompletedAchivements();
    }

    private void InitializeConditions()
    {
        foreach (Achievement achivement in achivements)
        {
            conditionMet[achivement.content] = false;
        }
    }

    private void LoadCompletedAchivements()
    {
        foreach (Achievement achivement in Game_Manager.instance.completedAchievements)
        {
            if (!conditionMet[achivement.content])
            {
                LoadAchivement(achivement);
            }
        }
    }

    public void SaveAchivements()
    {
        // Implementacja zapisywania osiągnięć
    }

    public void CheckAchivements()
    {
        int clicks = Click_Manager.instance.clickedTimes;

        foreach (Achievement achivement in achivements)
        {
            if (!conditionMet[achivement.content])
            {
                if ((achivement.content == "First 100!!!" && clicks >= 100) ||
                    (achivement.content == "Wow 200!?" && clicks >= 200) ||
                    (achivement.content == "1000 CLICK WOOW!" && clicks >= 1000) ||
                    (achivement.content == "Build a farm" && FarmManager.instance.farmsCount == 1) ||
                    (achivement.content == "Build four farms?!!" && FarmManager.instance.farmsCount == 4) ||
                    (achivement.content == "1 Hatched Egg!" && Dragon_Manager.instance.hatchedDragons.Count == 1) ||
                    (achivement.content == "10 Hatched Eggs!" && Dragon_Manager.instance.hatchedDragons.Count == 10) ||
                    (achivement.content == "100 Hatched Eggs!" && Dragon_Manager.instance.hatchedDragons.Count == 100))
                {
                    LoadAchivement(achivement);
                }
            }
        }
    }

    public void LoadAchivement(Achievement achivement)
    {
        //Audio_Manager.instance.Play("Game_Bonus");
        Game_Manager.instance.AddAchievement(achivement);
        achivementCounter++;
        Debug.Log(achivement.content);
        conditionMet[achivement.content] = true;
        completedAchivements.Add(achivement);

        GameObject obj = Instantiate(prefab);
        obj.transform.SetParent(grid.transform);

        obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = achivement.content;
        obj.transform.GetComponent<Image>().sprite = achivement.icon;

        achivementAnimation.GetComponentInChildren<TextMeshProUGUI>().text = achivement.content;

        StartCoroutine(LOAD_ACHIVEMENT(2f));
    }

    public IEnumerator LOAD_ACHIVEMENT(float time)
    {
        achivementAnimation.GetComponent<Animator>().Play("Achivement_In");

        yield return new WaitForSeconds(time);
        achivementAnimation.GetComponent<Animator>().Play("Achivement_Out");

        achivementAnimation.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
    }

    public List<Achievement> GetCompletedAchivements()
    {
        return completedAchivements;
    }

    void Update()
    {
        CheckAchivements();
    }
}
