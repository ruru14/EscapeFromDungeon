using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpperTabController : MonoBehaviour
{
    public GameObject controlBlocker;
    public GameObject inventoryPanel;
    public GameObject characterPanel;
    public GameObject upgradePanel;
    public GameObject equipPanel;
    public GameObject skillPanel;
    public GameObject decomposePanel;
    public GameObject formationPanel;

    private static int startFlag;
    private int activePanel;

    // Start is called before the first frame update
    void Start()
    {
        Utility.ObjectVisibility(inventoryPanel, false);
        activePanel = -1;
        startFlag = -1;
        SetPanelActivate(startFlag);
        StartCoroutine(TabSetting());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator TabSetting()
    {
        while (true)
        {
            if (startFlag == 0 | startFlag == 5)
            {
                SetPanelActivate(startFlag);
                startFlag = -1;
            }
            yield return null;
        }
    }


    public void OnClickBack()
    {
        //Scene scene = SceneManager.GetActiveScene();
        //Debug.Log(scene.name);
        Utility.ObjectVisibility(inventoryPanel, false);
        Utility.ObjectVisibility(controlBlocker, false);
    }

    public void SetPanelActivate(int flag)
    {
        Utility.ObjectVisibility(characterPanel, false);
        Utility.ObjectVisibility(upgradePanel, false);
        Utility.ObjectVisibility(equipPanel, false);
        Utility.ObjectVisibility(skillPanel, false);
        Utility.ObjectVisibility(decomposePanel, false);
        Utility.ObjectVisibility(formationPanel, false);
        switch (flag)
        {
            case 0:
                Utility.ObjectVisibility(characterPanel, true);
                activePanel = 0;
                startFlag = 0;
                PlayerPrefs.SetInt(PrefsEntity.InformationFlag, 0);
                break;
            case 1:
                Utility.ObjectVisibility(equipPanel, true);
                activePanel = 1;
                startFlag = 1;
                PlayerPrefs.SetInt(PrefsEntity.InformationFlag, 1);
                break;
            case 2:
                Utility.ObjectVisibility(skillPanel, true);
                activePanel = 2;
                startFlag = 2;
                PlayerPrefs.SetInt(PrefsEntity.InformationFlag, 2);
                break;
            case 3:
                Utility.ObjectVisibility(upgradePanel, true);
                activePanel = 3;
                startFlag = 3;
                PlayerPrefs.SetInt(PrefsEntity.InformationFlag, 3);
                break;
            case 4:
                Utility.ObjectVisibility(decomposePanel, true);
                activePanel = 4;
                startFlag = 4;
                PlayerPrefs.SetInt(PrefsEntity.InformationFlag, 4);
                break;
            case 5:
                Utility.ObjectVisibility(formationPanel, true);
                activePanel = 5;
                startFlag = 5;
                PlayerPrefs.SetInt(PrefsEntity.InformationFlag, 5);
                break;
        }
    }
    public void OnClickCharacter()
    {
        SetPanelActivate(0);
    }
    public void OnClickEquip()
    {
        SetPanelActivate(1);
    }

    public void OnClickSkill()
    {
        SetPanelActivate(2);
    }

    public void OnClickScroll()
    {
        SetPanelActivate(3);
    }

    public void OnClickDecompose()
    {
        SetPanelActivate(4);
    }

    public void OnClickFormation()
    {
        SetPanelActivate(5);
    }

    public static void SetFlag(int i)
    {
        startFlag = i;
    }

    public int GetActivePanel()
    {
        return activePanel;
    }

}
