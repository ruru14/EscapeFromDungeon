using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainTestMode : MonoBehaviour
{
    public int test;
    public int test2;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefsX.SetBool(PrefsEntity.StatUP, true);
        test = 5;
        //gameManager = GameManager.GetInstatnce();
        TextAsset data = Resources.Load("TextScript/test", typeof(TextAsset)) as TextAsset;
        StringReader sr = new StringReader(data.text);

        string line;
        line = sr.ReadLine();
        while (line != null)
        {
            Debug.Log(line);
            line = sr.ReadLine();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadField()
    {
        LoadingManager.LoadSceneContinue();
    }

    public void DataReset()
    {
        //Debug.Log((int)Random.Range(1, 3));
        //Debug.Log(test2);
        PlayerPrefs.DeleteAll();
        Debug.Log("Data Reset");
        //DataManager.BinarySerialize<TestData>(new TestData(8), DataFilePath.test);
        //TestData t = DataManager.BinaryDeserialize<TestData>(DataFilePath.test);
        //Debug.Log(t.a + " " + t.b);
    }
}
