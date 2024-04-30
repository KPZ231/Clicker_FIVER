using UnityEngine;
using TMPro;

public class Game_Manager : MonoBehaviour
{

    public static Game_Manager instance { get; private set; }
    [HideInInspector] public bool isPlaying = false;


    private void Start()
    {
        // Load();
        instance = this;
        isPlaying = true;
    }

    private void OnApplicationQuit()
    {
        Save();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
    }
    // public void Load()
    // {
    //     int tempTimesClick = 0;
    //     if (PlayerPrefs.HasKey("times_clicked"))
    //     {
    //         tempTimesClick = PlayerPrefs.GetInt("times_clicked");
    //         Debug.Log(tempTimesClick);
    //         Debug.Log(Click_Manager.instance.counter.name);
    //         Click_Manager.instance.counter.text = tempTimesClick.ToString();
    //         Click_Manager.instance.clickedTimes = tempTimesClick;

    //         PlayerPrefs.Save();
    //     }
    //     else
    //     {
    //         Debug.LogWarning("No Key");
    //         return;
    //     }
    // }

    public void Save()
    {
        if (!PlayerPrefs.HasKey("times_clicked"))
        {
            PlayerPrefs.SetInt("times_clicked", Click_Manager.instance.clickedTimes);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.DeleteKey("times_clicked");
            PlayerPrefs.SetInt("times_clicked", Click_Manager.instance.clickedTimes);
            PlayerPrefs.Save();
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting...");
    }

}