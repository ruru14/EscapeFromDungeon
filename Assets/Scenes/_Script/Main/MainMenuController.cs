using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Button btnContinue;
    public GameObject startPanel;

    // Start is called before the first frame update
    void Start()
    {
        Utility.ObjectVisibility(startPanel, false);
        if (!PlayerPrefsX.GetBool(PrefsEntity.SaveFlag, false))
        {
            btnContinue.interactable = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClickStart()
    {
        //No saved data
        if (PlayerPrefsX.GetBool(PrefsEntity.SaveFlag, false))
        {
            Utility.ObjectVisibility(startPanel, true);
        }
        else
        {
            Debug.Log("BtnClick : Start");
            PlayerPrefs.DeleteAll();
            GameManager.GetInstatnce().DataReset();
            SceneManager.LoadScene("SelectLevel");
        }
    }
    public void OnClickContinue()
    {
        Debug.Log("BtnClick : Continue");
        LoadingManager.LoadSceneContinue();
        GameManager.GetInstatnce();
        //TODO
    }
    public void OnClickOption()
    {
        Debug.Log("BtnClick : Option");
        SceneManager.LoadScene("Option", LoadSceneMode.Additive);
    }
    public void OnClickExit()
    {
        Debug.Log("BtnClick : Exit");
        Application.Quit();
    }
    public void OnClickReStartCancel()
    {
        Utility.ObjectVisibility(startPanel, false);
    }
    public void OnClickReStartConfirm()
    {
        Debug.Log("BtnClick : Start");
        PlayerPrefs.DeleteAll();
        GameManager.GetInstatnce().DataReset();
        SceneManager.LoadScene("SelectLevel");
    }
}
