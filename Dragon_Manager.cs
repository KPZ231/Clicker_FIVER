using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Dragon_Manager : MonoBehaviour
{
    public static Dragon_Manager instance { get; private set; }

    [Header("Dragon")]
    public List<Dragon> dragons = new List<Dragon>();
    public List<Dragon> hatchedDragons = new List<Dragon>();
    public Dragon choosedDragon_ = null;

    [Header("UI")]
    [SerializeField] private GameObject dragonsUI = null;
    [SerializeField] private GameObject dragonsUIScroll = null;
    [SerializeField] private GameObject dragonPrefabUI = null;
    [SerializeField] private GameObject _particleSystem = null;

    [SerializeField] private GameObject grid = null;

    [HideInInspector] public bool isToggled = false;

    private void Start()
    {
        instance = this;
    }

    public void Toggle()
    {
        if (!isToggled)
        {
            isToggled = true;
            LoadDragons();
        }
        else
        {
            isToggled = false;
            UnloadDragons();
        }
    }

    public void LoadDragons()
    {       
        if (Game_Manager.instance != null && Game_Manager.instance.dragonIcons != null)
        {
            // Sprawdź, czy liczba smoków jest równa liczbie ikon
            if (Game_Manager.instance.hatchedDragons.Count == Game_Manager.instance.dragonIcons.Count)
            {
                Dictionary<string, (GameObject uiElement, int count)> dragonNameCounts = new Dictionary<string, (GameObject, int)>();

                for (int i = 0; i < Game_Manager.instance.hatchedDragons.Count; i++)
                {
                    Dragon dragon = Game_Manager.instance.hatchedDragons[i];
                    Sprite dragonIcon = Game_Manager.instance.dragonIcons[i];

                    if (dragonNameCounts.ContainsKey(dragon.dragon_name))
                    {
                        // Increment count and update UI text
                        var entry = dragonNameCounts[dragon.dragon_name];
                        entry.count++;
                        dragonNameCounts[dragon.dragon_name] = (entry.uiElement, entry.count);
                        entry.uiElement.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{dragon.dragon_name} {entry.count}";
                    }
                    else
                    {
                        // Create new UI element
                        GameObject prefab = Instantiate(dragonPrefabUI);
                        prefab.transform.SetParent(grid.transform, false);
                        prefab.GetComponent<Image>().color = dragon.color;
                        prefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{dragon.dragon_name} 1";
                        prefab.transform.GetChild(0).GetComponent<Image>().sprite = dragonIcon;
                        prefab.name = dragon.dragon_name;

                        dragonNameCounts[dragon.dragon_name] = (prefab, 1);
                    }
                }
            }
            else
            {
                Debug.LogError("Number of dragons does not match the number of dragon icons!");
            }
        }
        else
        {
            Debug.LogError("Game_Manager instance or dragonIcons array is missing!");
        }
    }



    private void UnloadDragons()
    {
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            Destroy(grid.transform.GetChild(i).gameObject);
        }
    }

    public void Hatch()
    {
        int choosedDragon;
        int prct;

        prct = Random.Range(0, 100);

        if (Shop_Manager.instance.eggBought == false)
        {
            if (prct >= 0 && prct <= 50)
            {
                choosedDragon = Random.Range(0, 4);
                choosedDragon_ = dragons[choosedDragon];
            }
            else if (prct > 50 && prct <= 90)
            {
                choosedDragon = Random.Range(5, 6);
                choosedDragon_ = dragons[choosedDragon];
            }
            else if (prct > 90 && prct <= 100)
            {
                choosedDragon = Random.Range(7, 8);
                choosedDragon_ = dragons[choosedDragon];
            }
        }

        hatchedDragons.Add(choosedDragon_);
        Game_Manager.instance.dragonIcons.Add(choosedDragon_.icon);
        StartCoroutine(PARTICLE_SYSTEM_PLAY_ANIMATION());
        Click_Manager.instance.ChooseHatchImage();
        Shop_Manager.instance.eggBought = false;
        Game_Manager.instance.AddDino(choosedDragon_);
    }

    IEnumerator PARTICLE_SYSTEM_PLAY_ANIMATION()
    {
        _particleSystem.SetActive(true);
        _particleSystem.GetComponent<Animator>().Play("Shatter");
        yield return new WaitForSeconds(0.15f);
        _particleSystem.SetActive(false);
    }

    public void ShowDragon()
    {
        Debug.Log("Hatched" + choosedDragon_.dragon_name);
        Audio_Manager.instance.Play("Cartoon_Wink");

        dragonsUI.SetActive(true);
        isToggled = false;
        Toggle();
    }

    private Sprite LoadSprite(string path)
    {
        // Ładuj sprite'a bezpośrednio z zasobów
        Sprite sprite = Resources.Load<Sprite>(path);

        if (sprite == null)
        {
            Debug.LogError($"Failed to load sprite from Resources path: {path}");
        }

        return sprite;
    }
}
