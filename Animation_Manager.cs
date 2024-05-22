using UnityEngine;

public class Animation_Manager : MonoBehaviour
{
    public static Animation_Manager instance { get; private set; }


    //Booleans
    #region Booleans

    private static bool isShopOn = false;
    private static bool isSideBarOn = false;
    #endregion

    public void ShopAnim()
    {
        Animator shopAnim = GameObject.Find("Shop").GetComponent<Animator>();


        if (!isShopOn)
        {
            shopAnim.Play("ShopIn");
            isShopOn = true; ;
            shopAnim.transform.GetChild(1).GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, -90f);
        }
        else
        {
            shopAnim.Play("ShopOut");
            isShopOn = false;
            shopAnim.transform.GetChild(1).GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 90f);
        }
    }

    public void SidebarAnim()
    {

        Animator sideBar = GameObject.Find("SideBar").GetComponent<Animator>();
        if (!isSideBarOn)
        {
            sideBar.Play("SideBarIn");
            isSideBarOn = true; ;
            //shopAnim.transform.GetChild(1).GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,-90f);
        }
        else
        {
            sideBar.Play("SideBarOut");
            isSideBarOn = false;
            //shopAnim.transform.GetChild(1).GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,90f);
        }
    }
}
