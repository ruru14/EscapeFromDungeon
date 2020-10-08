using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TabManager : MonoBehaviour
{
    public GameObject characterPanel;
    public GameObject scrollPanel;
    public GameObject equipPanel;
    public GameObject skillPanel;
    public GameObject formationPanel;


    // Start is called before the first frame update
    void Start()
    {
        int startFlag = PlayerPrefs.GetInt(PrefsEntity.InformationFlag, 0);
        SetPanelActivate(startFlag);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClickBack()
    {
        //Scene scene = SceneManager.GetActiveScene();
        //Debug.Log(scene.name);
        SceneManager.UnloadSceneAsync("Information");
    }

    public void SetPanelActivate(int flag)
    {
        Utility.ObjectVisibility(characterPanel, false);
        Utility.ObjectVisibility(scrollPanel, false);
        Utility.ObjectVisibility(equipPanel, false);
        Utility.ObjectVisibility(skillPanel, false);
        Utility.ObjectVisibility(formationPanel, false);
        switch (flag)
        {
            case 0:
                Utility.ObjectVisibility(characterPanel, true);
                break;
            case 1:
                Utility.ObjectVisibility(scrollPanel, true);
                break;
            case 2:
                Utility.ObjectVisibility(equipPanel, true);
                break;
            case 3:
                Utility.ObjectVisibility(skillPanel, true);
                break;
            case 4:
                Utility.ObjectVisibility(formationPanel, true);
                break;
        }
    }
    public void OnClickCharacter()
    {
        SetPanelActivate(0);
    }

    public void OnClickScroll()
    {
        SetPanelActivate(1);
    }

    public void OnClickEquip()
    {
        SetPanelActivate(2);
    }

    public void OnClickSkill()
    {
        SetPanelActivate(3);
    }

    public void OnClickFormation()
    {
        SetPanelActivate(4);
    }
}
