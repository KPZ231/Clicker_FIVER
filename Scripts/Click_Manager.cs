using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Click_Manager : MonoBehaviour
{
    public static Click_Manager instance { get; private set; }

    [Header("Click")]
    public int clickedTimes = 0;
    public TextMeshProUGUI counter = null;
    public Slider hatchSlider = null;
    [SerializeField] private int clicksToHatch = 0;


    private bool hatched = false;
    private bool getDragonClicks = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void Click(int times)
    {
        clickedTimes += times;
        UpdateCounter();
        ChooseHatchCliks();
        //Shop_Manager.instance.Check();
    }

    public void UpdateCounter()
    {
        counter.text = clickedTimes.ToString();
        hatchSlider.maxValue = clicksToHatch;
        hatchSlider.value = clickedTimes;
    }


    private void ChooseHatchCliks()
    {
        if (!getDragonClicks)
        {
            clicksToHatch = clickedTimes + Random.Range(100, 500);
            getDragonClicks = true;
        }

        if (!hatched)
        {
            if (clickedTimes == clicksToHatch)
            {
                Dragon_Manager.instance.Hatch();

                getDragonClicks = false;
            }
        }
    }

}
