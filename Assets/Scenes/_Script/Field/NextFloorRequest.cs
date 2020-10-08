using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextFloorRequest : MonoBehaviour
{
    public GameObject requestNextFloorPanel;
    public GameObject controlBlocker;


    public void OnClickConfirm()
    {
        int currentFloor = PlayerPrefs.GetInt(PrefsEntity.CurrentFloor, 100);
        PlayerPrefsX.SetBool(PrefsEntity.SaveFlag, false);
        PlayerPrefs.SetInt(PrefsEntity.CurrentFloor, --currentFloor);
        Scene scene = SceneManager.GetActiveScene();
        LoadingManager.LoadSceneFieldToField();
    }

    public void OnClickCancel()
    {
        Utility.ObjectVisibility(controlBlocker, false);
        Utility.ObjectVisibility(requestNextFloorPanel, false);
    }
}
