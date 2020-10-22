using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DecomposePanelController : MonoBehaviour
{
    public GameObject itemListParent;
    public GameObject itemPrefab;
    public Text decomposeScroll;
    public Text equipName;
    public Text equipGrade;
    public Text decomposeReq;
    public Text decomposeRes;

    private GameManager gameManager;
    private List<Equip> itemList;
    private GameObject[] itemSlot;

    public GameObject selectedItem;
    private Equip selectedEquip;
    private int selectedEquipIndex;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstatnce();
        //To Test
        //gameManager.DataReset();
        //for (int i = 0; i < 2; i++)
        //{
        //    gameManager.AddEquipment(EquipManager.GetEquip(EquipmentFilePath.Archer.Body.mystic, 0));
        //    gameManager.AddEquipment(EquipManager.GetEquip(EquipmentFilePath.Knight.Body.mystic, 0));
        //    gameManager.AddEquipment(EquipManager.GetEquip(EquipmentFilePath.Thief.Body.rare, 0));
        //    gameManager.AddEquipment(EquipManager.GetEquip(EquipmentFilePath.Mage.Body.unique, 0));
        //    gameManager.AddEquipment(EquipManager.GetEquip(EquipmentFilePath.Priest.Body.normal, 0));
        //}
        //
        itemSlot = new GameObject[49];
        DecomposeListUpdate();
        StartCoroutine(PanelSwapObserver());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PanelSwapObserver()
    {
        while (true)
        {
            int flag = PlayerPrefs.GetInt(PrefsEntity.InformationFlag, -1);
            //Debug.Log(flag);
            if(flag == 4)
            {
                DecomposeListUpdate();
                PlayerPrefs.SetInt(PrefsEntity.InformationFlag, -1);
            }
            //if (flag != startFlag)
            //{
            //    SetPanelActivate(flag);
            //}
            yield return null;
        }
    }

    private void DecomposeListUpdate()
    {
        foreach(Transform child in itemListParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        itemList = gameManager.GetEquipment();
        for(int i=0; i<itemList.Count; i++)
        {
            int itemIndex = i;
            itemSlot[i] = Instantiate(itemPrefab, itemListParent.transform);
            itemSlot[i].transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite
                = Resources.Load<Sprite>(itemList[i].img) as Sprite;
            itemSlot[i].gameObject
                .AddComponent<TooltipManager>()
                .SetEquip(itemList[i], itemIndex, IconType.Equipment);
            //.transform.Find("ItemImage").gameObject
            itemSlot[i].gameObject.AddComponent<Button>().onClick.AddListener(() =>
            {
                OnClickItem(itemIndex);
            });
        }
        for (int i=itemList.Count; i<itemSlot.Length; i++)
        {
            itemSlot[i] = Instantiate(itemPrefab, itemListParent.transform);
            itemSlot[i].transform.Find("ItemImage").gameObject.GetComponent<Image>().color
                = Color.clear;
        }
        decomposeScroll.text = "보유한 장비 분해 주문서 : ";
        equipName.text = "";
        equipGrade.text = "";
        decomposeReq.text = "";
        decomposeRes.text = "";
        //TODO : EquipScroll Image Clear
    }

    public void OnClickItem(int index)
    {
        Debug.Log(index + "th Item : " + itemList[index].name);
        selectedEquipIndex = index;
        selectedItem.transform.Find("ItemImage").gameObject.GetComponent<Image>().color
            = new Color(255, 255, 255, 255);
        selectedItem.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite
            = Resources.Load<Sprite>(itemList[index].img) as Sprite;
        selectedEquip = itemList[index];
        equipName.text = "+" + selectedEquip.level + " " + selectedEquip.name;
        int reqScroll = 0;
        int resScroll = 0;
        string grade = "";
        Color gradeColor = new Color(0, 0, 0);
        switch (selectedEquip.grade)
        {
            case EquipGrade.Normal:
                grade = "노멀";
                reqScroll = 1;
                resScroll = 1;
                gradeColor = new Color(0.83f, 0.746f, 0.661f);
                break;
            case EquipGrade.Rare:
                grade = "레어";
                reqScroll = 3;
                resScroll = 2;
                gradeColor = new Color(0.589f, 0.934f, 1.0f);
                break;
            case EquipGrade.Unique:
                grade = "유니크";
                reqScroll = 6;
                resScroll = 3;
                gradeColor = new Color(1.0f, 0.589f, 0.699f);
                break;
            case EquipGrade.Mystic:
                grade = "미스틱";
                reqScroll = 10;
                resScroll = 5;
                gradeColor = new Color(1.0f, 0.849f, 0.0f);
                break;
        }
        equipGrade.text = "(" + grade + " 아이템)";
        equipGrade.color = gradeColor;
        decomposeReq.text = "요구 장비 분해 주문서 : " + reqScroll.ToString();
        decomposeRes.text = "획득하는 장비 강화 주문서 : " + resScroll.ToString();
        //TODO : EquipScroll Image Setting
    }

    public void OnClickDecompose()
    {
        if(selectedEquipIndex == -1)
        {
            return;
        }
        itemList.RemoveAt(selectedEquipIndex);
        gameManager.SetEquipment(itemList);
        selectedItem.transform.Find("ItemImage").gameObject.GetComponent<Image>().color
            = Color.clear;
        selectedEquip = null;
        selectedEquipIndex = -1;
        DecomposeListUpdate();
        Debug.Log("Decompose");
    }
}
