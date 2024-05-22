using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Click_Manager : MonoBehaviour
{
    public static Click_Manager instance { get; private set; }

    [Header("Click")]
    public int clickedTimes = 0; //~Shell Fragments 
    public int fossilFragments = 0;
    public TextMeshProUGUI counter = null;
    public TextMeshProUGUI fossilFragmentsCounter = null;
    public TextMeshProUGUI amberFragmentsCounter = null;
    public Slider hatchSlider = null;
    public int clicksToHatch = 0;
    [SerializeField] private List<Sprite> dragonImages = new List<Sprite>();
    public GameObject clickButton = null;
    public int times = 1;

    private bool hatched = false;
    private bool getDragonClicks = false;
    private int millionsCounted = 0; // Zmienna do śledzenia liczby już uwzględnionych milionów

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        ChooseHatchImage();
    }

    public void Click()
    {
        IncrementClicks();
        ChooseHatchCliks();
        StartCoroutine(PLAY_CLICK_ANIMATION(0.1f));

        Audio_Manager.instance.Play("Tap");
    }

    public IEnumerator PLAY_CLICK_ANIMATION(float time)
    {
        clickButton.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

        yield return new WaitForSeconds(time);

        clickButton.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void UpdateCounter()
    {
        counter.text = clickedTimes.ToString();
        hatchSlider.maxValue = clicksToHatch;
        hatchSlider.value = clickedTimes;

        if (Game_Manager.instance.gameData.resetTimes >= 1)
        {
            fossilFragmentsCounter.text = fossilFragments.ToString();
        }
    }

    public void IncrementClicks()
    {
        clickedTimes += times;

        if (Game_Manager.instance.gameData.resetTimes >= 1)
        {
            // Sprawdzenie, czy przekroczyliśmy kolejny milion kliknięć
            int newMillionsCounted = clickedTimes / 1000000;
            if (newMillionsCounted > millionsCounted)
            {
                fossilFragments += 100;
                millionsCounted = newMillionsCounted;
            }
        }
        UpdateCounter();

    }

    public void ChooseHatchImage()
    {
        int max = dragonImages.Count;
        int randomSelection = Random.Range(0, max);

        clickButton.GetComponent<Image>().sprite = dragonImages[randomSelection];
    }

    private void ChooseHatchCliks()
    {
        if (!getDragonClicks)
        {
            clicksToHatch = clickedTimes + clickedTimes;

            getDragonClicks = true;
        }

        if (!hatched)
        {
            if (clickedTimes >= clicksToHatch)
            {
                Dragon_Manager.instance.Hatch();

                getDragonClicks = false;
            }
        }
    }
}
