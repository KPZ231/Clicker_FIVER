using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Reset_System : MonoBehaviour
{
    public static Reset_System instance { get; private set; }

    public int RESET_CAP = 20000;
    public int timesReseted = 0;

    [HideInInspector] public bool canReset = false;
    [SerializeField] private GameObject cannotResetNotification = null;
    [SerializeField] private GameObject resetPanel = null;


    void Start()
    {
        instance = this;
        ResetBehaviour();
    }

    private void ResetBehaviour()
    {
        Debug.Log(canReset);

        canReset = true;
    }

    public void CheckIfCanReset()
    {
        if (Click_Manager.instance.clickedTimes >= RESET_CAP)
        {
            canReset = true;
            resetPanel.SetActive(true);
        }
        else
        {
            StartCoroutine(RESET_NOTIFICATION());
        }
    }

    private IEnumerator RESET_NOTIFICATION()
    {
        GameObject reset = GameObject.Find("Reset_Notification");

        reset.GetComponent<Animator>().Play("Reset_Notification");

        yield return new WaitForSeconds(7f);

    }

    public void Reset()
    {
        timesReseted++;
        Game_Manager.instance.SaveResetTimes(timesReseted);

        Game_Manager.instance.ClearGameDataExceptEssential();
        Game_Manager.instance.SaveBoolean(canReset);


        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
