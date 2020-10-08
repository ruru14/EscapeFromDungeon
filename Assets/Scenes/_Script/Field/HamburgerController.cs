using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HamburgerController : MonoBehaviour
{
    public GameObject hamburgerPanel;
    public GameObject inventoryPanel;
    public GameObject controlBlocker;

    private bool hamburgerOnOff = false;

    // Start is called before the first frame update
    void Start()
    {
        Utility.ObjectVisibility(hamburgerPanel, hamburgerOnOff);
    }

    public void OnClickHamburger()
    {
        hamburgerOnOff = !hamburgerOnOff;
        Utility.ObjectVisibility(hamburgerPanel, hamburgerOnOff);
    }

    public void OnClickOption()
    {
        SceneManager.LoadScene("Option", LoadSceneMode.Additive);
    }

    public void OnClickInventory()
    {
        PlayerPrefs.SetInt(PrefsEntity.InformationFlag, 0);
        UpperTabController.SetFlag(0);
        Utility.ObjectVisibility(inventoryPanel, true);
        Utility.ObjectVisibility(controlBlocker, true);
        //SceneManager.LoadScene("Information", LoadSceneMode.Additive);

    }

    public void OnClickFormation()
    {
        PlayerPrefs.SetInt(PrefsEntity.InformationFlag, 5);
        UpperTabController.SetFlag(5);
        Utility.ObjectVisibility(inventoryPanel, true);
        Utility.ObjectVisibility(controlBlocker, true);
        //SceneManager.LoadScene("Information", LoadSceneMode.Additive);

    }
}
