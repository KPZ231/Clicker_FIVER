using UnityEngine;

public class Animation_Manager : MonoBehaviour
{
    public static Animation_Manager instance { get; private set; }


    //Booleans
    #region Booleans

    private static bool isShopOn = false;

    #endregion

    public void ShopAnim()
    {
        Animator shopAnim = GameObject.Find("Shop").GetComponent<Animator>();

        if (!isShopOn)
        {
            shopAnim.Play("ShopIn");
            isShopOn = true;;
            shopAnim.transform.GetChild(1).GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,-90f);
        }
        else
        {
            shopAnim.Play("ShopOut");
            isShopOn = false;
            shopAnim.transform.GetChild(1).GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,90f);
        }

    }
}
