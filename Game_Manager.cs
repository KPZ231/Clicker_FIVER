/*
 * Game_Manager.cs
 * 
 * Description: Manages the game state, saving and loading game data.
 */

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class ItemData
{
    public string itemName;
    public int itemID;

    public ItemData(Shop_Item item)
    {
        itemName = item._itemName;
    }

    public Shop_Item ToItem()
    {
        Shop_Item item = ScriptableObject.CreateInstance<Shop_Item>();
        item._itemName = itemName;
        return item;
    }
}

[Serializable]
public class DragonData
{
    public string dragonName;
    public int dragonID;
    public string dragonIconPath; // Path to the icon file
    public Rarity dragonRarity;
    public float[] dragonColor; // Array of floats for color

    public DragonData(Dragon dragon)
    {
        dragonName = dragon.dragon_name;
        dragonRarity = dragon.rarity;
        dragonColor = new float[4] { dragon.color.r, dragon.color.g, dragon.color.b, dragon.color.a }; // Convert Color to array

        // Set the path to the icon sprite in the "Resources" folder
        dragonIconPath = "Sprites/" + dragon.icon.name; // Assuming the icons are stored in a "dragon_icons" folder within "Resources"
    }

    public Dragon ToDragon()
    {
        Dragon dragon = ScriptableObject.CreateInstance<Dragon>();
        dragon.dragon_name = dragonName;
        dragon.rarity = dragonRarity;
        dragon.color = new Color(dragonColor[0], dragonColor[1], dragonColor[2], dragonColor[3]); // Convert array to Color

        // Load the sprite from the Resources folder
        dragon.icon = Resources.Load<Sprite>(dragonIconPath);

        return dragon;
    }
}


[Serializable]
public class AchievementData
{
    public string achievementName;
    public string achievementIconPath;

    public AchievementData(Achievement achievement)
    {
        achievementName = achievement.content;

        achievementIconPath = "Sprites/" + achievement.icon.name; // In runtime, use the name or another identifier
    }

    public Achievement ToAchievement()
    {
        Achievement achievement = ScriptableObject.CreateInstance<Achievement>();
        achievement.content = achievementName;
        // Load the sprite in runtime if necessary, e.g., from Resources folder
        achievement.icon = Resources.Load<Sprite>(achievementIconPath);
        return achievement;
    }
}

[Serializable]
public class GameData
{
    public List<ItemData> items;
    public int clickedTimes;
    public int fossilFragments;
    public int amberFragments;
    public int times = 1;
    public int achievementsCount;
    public bool someBoolean;
    public int resetTimes;

    public int timesToHatch;

    public List<DragonData> hatchedDragons;
    public List<AchievementData> completedAchievements;
}

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance { get; private set; }

    [HideInInspector] public bool isPlaying = false;
    public List<Shop_Item> items;
    public List<Dragon> hatchedDragons;
    public List<Achievement> completedAchievements;
    public List<Sprite> dragonIcons; // Array to hold all dragon icons
    [HideInInspector] public GameData gameData;
    private string saveFilePath;

    private void Awake()
    {
        instance = this;
        saveFilePath = Path.Combine(Application.persistentDataPath, "gameData.json");
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Start()
    {
        Load();
        PopulateIconsArray();
        isPlaying = true;
    }

    public void PopulateIconsArray()
    {

        // Clear existing icons from the array
        dragonIcons = new List<Sprite>();

        // Populate the array with loaded dragon icons
        for (int i = 0; i < gameData.hatchedDragons.Count; i++)
        {
            dragonIcons.Add(gameData.hatchedDragons[i].ToDragon().icon);
        }
    }
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayVFX(ParticleSystem system, GameObject parent)
    {
        float duration = system.main.duration;

        ParticleSystem system_ = Instantiate(system);
        system_.gameObject.transform.SetParent(parent.transform);
        system_.Play();
        system_.gameObject.transform.localScale = new Vector3(1, 1, 1);
        system_.gameObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DeleteSaveData();
        }
    }

    public void Save()
    {
        if (gameData == null)
        {
            gameData = new GameData();
        }

        gameData.clickedTimes = Click_Manager.instance.clickedTimes;
        gameData.times = Click_Manager.instance.times;

        gameData.achievementsCount = Achivements_Manager.instance.achivementCounter;
        gameData.completedAchievements = new List<AchievementData>();
        gameData.fossilFragments = Click_Manager.instance.fossilFragments;
        gameData.timesToHatch = Click_Manager.instance.clicksToHatch;

        foreach (Achievement achievement in completedAchievements)
        {
            gameData.completedAchievements.Add(new AchievementData(achievement));
        }

        gameData.hatchedDragons = new List<DragonData>();
        foreach (Dragon dragon in hatchedDragons)
        {
            gameData.hatchedDragons.Add(new DragonData(dragon));
        }

        gameData.items = new List<ItemData>();
        foreach (Shop_Item item in items)
        {
            gameData.items.Add(new ItemData(item));
        }

        string jsonData = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(saveFilePath, jsonData);

        Debug.Log("Game data saved to " + saveFilePath);
    }

    public void Load()
    {
        if (File.Exists(saveFilePath))
        {
            string jsonData = File.ReadAllText(saveFilePath);
            gameData = JsonUtility.FromJson<GameData>(jsonData);

            Click_Manager.instance.clickedTimes = gameData.clickedTimes;
            Click_Manager.instance.times = gameData.times;
            Click_Manager.instance.counter.text = gameData.clickedTimes.ToString();
            Click_Manager.instance.fossilFragments = gameData.fossilFragments;
            Click_Manager.instance.clicksToHatch = gameData.timesToHatch;
            // Reset_System.instance.timesReseted = gameData.resetTimes;
            completedAchievements = new List<Achievement>();
            foreach (AchievementData achievementData in gameData.completedAchievements)
            {
                completedAchievements.Add(achievementData.ToAchievement());
            }

            hatchedDragons = new List<Dragon>();
            foreach (DragonData dragonData in gameData.hatchedDragons)
            {
                hatchedDragons.Add(dragonData.ToDragon());
            }

            items = new List<Shop_Item>();
            foreach (ItemData itemData in gameData.items)
            {
                items.Add(itemData.ToItem());
            }

            LoadBoolean();

            Debug.Log("Game data loaded from " + saveFilePath);
            Click_Manager.instance.UpdateCounter();
        }
        else
        {
            Debug.LogWarning("No save file found at " + saveFilePath);
            gameData = new GameData();
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting...");
    }

    public void DeleteSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save data deleted from " + saveFilePath);

            gameData = new GameData();
            items = new List<Shop_Item>();
            hatchedDragons = new List<Dragon>();
            completedAchievements = new List<Achievement>();
            gameData.someBoolean = true;
            Reset_System.instance.canReset = false;
            gameData.timesToHatch = 0;

            if (Click_Manager.instance != null)
            {
                Click_Manager.instance.clickedTimes = 0;
                Click_Manager.instance.times = 1;
                Click_Manager.instance.counter.text = "0";
                Click_Manager.instance.fossilFragments = 0;
            }
            if (Achivements_Manager.instance != null)
            {
                Achivements_Manager.instance.achivementCounter = 0;
            }

            Debug.Log("Game data reset.");
        }
        else
        {
            Debug.LogWarning("No save file to delete at " + saveFilePath);
        }
    }

    public void SaveBoolean(bool value)
    {
        if (gameData == null)
        {
            gameData = new GameData();
        }

        gameData.someBoolean = value;
        Save();
    }

    public bool LoadBoolean()
    {
        if (gameData == null)
        {
            Load();
        }
        return gameData.someBoolean;
    }

    public void ClearGameDataExceptEssential()
    {
        if (gameData == null)
        {
            gameData = new GameData();
        }

        // Preserve the data that needs to remain
        int savedFossilFragments = gameData.fossilFragments;
        int savedAmberFragments = gameData.amberFragments;
        int savedResetTimes = gameData.resetTimes;
        List<AchievementData> savedCompletedAchievements = new List<AchievementData>(gameData.completedAchievements);
        List<DragonData> savedHatchedDragons = new List<DragonData>(gameData.hatchedDragons);

        // Reset other fields
        gameData = new GameData
        {
            fossilFragments = savedFossilFragments,
            amberFragments = savedAmberFragments,
            completedAchievements = savedCompletedAchievements,
            hatchedDragons = savedHatchedDragons,
            resetTimes = savedResetTimes
        };

        items = new List<Shop_Item>();
        hatchedDragons = new List<Dragon>();
        completedAchievements = new List<Achievement>();

        if (Click_Manager.instance != null)
        {
            Click_Manager.instance.clickedTimes = 0;
            Click_Manager.instance.times = 1;
            Click_Manager.instance.counter.text = "0";
        }

        if (Achivements_Manager.instance != null)
        {
            Achivements_Manager.instance.achivementCounter = 0;
        }

        Debug.Log("Game data cleared except for essential fields.");
    }

    public void AddElement(Shop_Item item)
    {
        items.Add(item);
    }

    public void AddDino(Dragon dragon)
    {
        hatchedDragons.Add(dragon);
    }

    public void AddAchievement(Achievement achievement)
    {
        completedAchievements.Add(achievement);
    }

    public void SaveResetTimes(int timesReset)
    {
        gameData.resetTimes = timesReset;
    }
}
