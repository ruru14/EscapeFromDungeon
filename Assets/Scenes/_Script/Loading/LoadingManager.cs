using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    Image loadingBar;
    Text testText;
    private static string sceneName = "Field";

    // Start is called before the first frame update
    void Start()
    {
        loadingBar.fillAmount = 0;
        Debug.Log(PlayerPrefsX.GetBool(PrefsEntity.SaveFlag));
        StartCoroutine(LoadAsyncScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void LoadScene(string loadSceneName)
    { 
        sceneName = loadSceneName;
        SceneManager.LoadScene("Loading");
    }

    public static void LoadSceneFirstStart()
    {
        sceneName = "Field";
        SceneManager.LoadScene("Loading");
    }

    public static void LoadSceneFieldToField()
    {
        sceneName = "Field";
        ResetVisibility();
        SceneManager.LoadScene("Loading");
    }

    public static void LoadSceneFieldToBattle()
    {
        sceneName = "Battle";
        SceneManager.LoadScene("Loading");
    }

    public static void LoadSceneBattleToField()
    {
        sceneName = "Field";
        SceneManager.LoadScene("Loading");
    }

    public static void LoadSceneContinue()
    {
        sceneName = "Field";
        SceneManager.LoadScene("Loading");
    }

    private static void ResetVisibility()
    {
        PlayerPrefs.DeleteKey(PrefsEntity.FieldEventVisibility);
        bool[,] temp = new bool[36, 36];
        DataManager.BinarySerialize<bool[,]>(temp, DataFilePath.FieldBridgeVisibility);
    }

    IEnumerator LoadAsyncScene()
    {
        yield return null;
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(sceneName);
        asyncScene.allowSceneActivation = false;
        float timeC = 0;
        while (!asyncScene.isDone)
        {
            yield return null;
            timeC += Time.deltaTime;
            if(asyncScene.progress >= 0.9f)
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1, timeC);
                if(loadingBar.fillAmount == 1.0f)
                {
                    asyncScene.allowSceneActivation = true;
                }
            }
            else
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, asyncScene.progress, timeC);
                if(loadingBar.fillAmount >= asyncScene.progress)
                {
                    timeC = 0f;
                }
            }
        }
    }
}
