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

    public void OnClickTestCharacter()
    {
        List<DataChar> tempList = new List<DataChar>();
        tempList.Add(DataChar.getArcher());
        tempList.Add(DataChar.getArcher());
        tempList.Add(DataChar.getKnight());
        tempList.Add(DataChar.getKnight());
        tempList.Add(DataChar.getMage());
        tempList.Add(DataChar.getMage());
        tempList.Add(DataChar.getPriest());
        tempList.Add(DataChar.getPriest());
        tempList.Add(DataChar.getThief());
        tempList.Add(DataChar.getThief());
        gameManager.UpdateCharacterList(tempList);
    }

    public void OnClickTestEquipment()
    {
        List<Equip> tempEquipment = new List<Equip>();
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Knight.Body.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Knight.Foot.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Knight.Head.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Knight.Weapon.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Knight.SubWeapon.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Thief.Body.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Thief.Foot.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Thief.Head.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Thief.Weapon.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Priest.Body.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Priest.Foot.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Priest.Head.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Priest.Weapon.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Priest.SubWeapon.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Archer.Body.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Archer.Foot.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Archer.Head.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Archer.Weapon.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Archer.SubWeapon.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Mage.Body.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Mage.Foot.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Mage.Head.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Mage.Weapon.mystic, 0));
        tempEquipment.Add(EquipManager.GetEquip(EquipmentFilePath.Mage.SubWeapon.mystic, 0));
        gameManager.SetEquipment(tempEquipment);
    }
}
