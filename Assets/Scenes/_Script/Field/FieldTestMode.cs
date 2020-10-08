using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FieldTestMode : MonoBehaviour
{
    public InputField inputField;
    public InputField inputFieldFloor;
    public static Camera mainCam;
    public static Camera viewCam;
    private List<Transform>[] fieldBridgeContainer;
    private Transform[,] fieldEventContainer;
    private bool camFlag = false;
    private GameManager gameManager;
    private void Awake()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        viewCam = GameObject.Find("View Camera").GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstatnce();
        Debug.Log(gameManager.GetCharacterList().ToArray());
        fieldBridgeContainer = MapGenerator.GetBridgeContainer();
        fieldEventContainer = MapGenerator.GetEventContainer();
        mainCam.enabled = !camFlag;
        viewCam.enabled = camFlag;
        inputFieldFloor.text = PlayerPrefs.GetInt(PrefsEntity.CurrentFloor, 100).ToString();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BtnReload()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        PlayerPrefs.SetInt(PrefsEntity.BridgeCount, int.Parse(inputField.text));
    }

    public void ShowAllBridges()
    {
        for (int i = 0; i < fieldBridgeContainer.Length; i++)
        {
            foreach (Transform bridge in fieldBridgeContainer[i])
                bridge.gameObject.SetActive(true);
        }
    }
    public void HideAllBridges()
    {
        for (int i = 0; i < fieldBridgeContainer.Length; i++)
        {
            foreach (Transform bridge in fieldBridgeContainer[i])
                bridge.gameObject.SetActive(false);
        }
    }

    public void ShowAllEvents()
    {
        foreach(Transform t in fieldEventContainer)
        {
            if (t != null)
                t.gameObject.SetActive(true);
        }
    }
    public void HideAllEvents()
    {
        foreach (Transform t in fieldEventContainer)
        {
            if (t != null)
                t.gameObject.SetActive(false);
        }
    }

    public void CameraTogle()
    {
        camFlag = !camFlag;
        mainCam.enabled = !camFlag;
        viewCam.enabled = camFlag;
    }

    public void ChangeFloor()
    {
        PlayerPrefs.SetInt(PrefsEntity.CurrentFloor, int.Parse(inputFieldFloor.text));
        PlayerPrefsX.SetBool(PrefsEntity.SaveFlag, false);
        Scene scene = SceneManager.GetActiveScene();
        LoadingManager.LoadSceneFieldToField();
    }
}
