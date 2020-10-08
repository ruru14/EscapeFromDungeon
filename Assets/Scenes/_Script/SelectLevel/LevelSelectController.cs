using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{
    public Button btnConfirm;
    public Text levelExplain;
    public int selectLevel;
    // Start is called before the first frame update
    void Start()
    {
        btnConfirm.interactable = false;
    }

    public void OnClickNormal()
    {
        btnConfirm.interactable = true;
        selectLevel = 0;
        levelExplain.text = Utility.ScriptReader(ScriptFilePath.LevelSelectNormal);// "Normal";
        Debug.Log("Level : Normal");
    }
    public void OnClickHard()
    {
        btnConfirm.interactable = true;
        selectLevel = 1;
        levelExplain.text = Utility.ScriptReader(ScriptFilePath.LevelSelectHard);// "Hard";
        Debug.Log("Level : Hard");
    }
    public void OnClickHell()
    {
        btnConfirm.interactable = true;
        selectLevel = 2;
        levelExplain.text = Utility.ScriptReader(ScriptFilePath.LevelSelectHell); //"Hell";
        Debug.Log("Level : Hell");
    }
    public void OnClickConfirm()
    {
        PlayerPrefs.SetInt(PrefsEntity.CurrentLevel, selectLevel);
        SceneManager.LoadScene("SelectCharacter");
    }
}
