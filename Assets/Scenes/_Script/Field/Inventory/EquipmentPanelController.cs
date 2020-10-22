using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EquipmentPanelController : MonoBehaviour
{
    public GameObject characterListParent;
    public GameObject characterIcon;
    public GameObject itemListParent;
    public GameObject itemPrefab;
    public GameObject characterImage;
    public GameObject equipHead;
    public GameObject equipBody;
    public GameObject equipFoot;
    public GameObject equipWeapon;
    public GameObject equipSubweapon;

    public GameObject[] characterList;
    private List<Equip> itemList;
    private GameManager gameManager;
    private List<DataChar> characterData;
    private List<Equip> equipmentList;
    private GameObject[] itemSlot;
    private Equip selectedEquip;
    private DataChar selectedCharacter;

    public GameObject itemSwapPanel;
    private Color originColor;
    private float flicker;
    private bool itemSwapFlag;
    private int selectedCharacterIndex;
    private int selectedEquipIndex;

    private enum ItemSort : int
    {
        Thief = 0,
        Archer = 1,
        Mage = 2,
        Knight = 3,
        Priest = 4,
        All = 5
    }

    void Awake()
    {
        gameManager = GameManager.GetInstatnce();
        //characterData = gameManager.GetCharacterList();
        characterData = new List<DataChar>();
        //Debug.Log(EquipManager.GetEquip(EquipmentFilePath.Knight.Body.normal).name);
        //gameManager.AddEquipment(EquipManager.GetEquip(EquipmentFilePath.Knight.Body.mystic, 0));
        //gameManager.AddEquipment(EquipManager.GetEquip(EquipmentFilePath.Knight.Weapon.mystic, 0));
        //gameManager.AddEquipment(EquipManager.GetEquip(EquipmentFilePath.Mage.Body.mystic, 0));
        //gameManager.AddEquipment(EquipManager.GetEquip(EquipmentFilePath.Archer.Foot.mystic, 0));
        //gameManager.AddEquipment(EquipManager.GetEquip(EquipmentFilePath.Thief.Weapon.mystic, 0));
        //gameManager.DataReset();
        //gameManager.AddCharacter(DataChar.getArcher());
        //gameManager.AddCharacter(DataChar.getKnight());
        //gameManager.AddCharacter(DataChar.getMage());
        //gameManager.AddCharacter(DataChar.getArcher());
        //gameManager.AddCharacter(DataChar.getKnight());
        //gameManager.AddCharacter(DataChar.getThief());
        //gameManager.AddCharacter(DataChar.getPriest());
        //gameManager.AddCharacter(DataChar.getPriest());
        //gameManager.AddCharacter(DataChar.getMage());
        //gameManager.AddCharacter(DataChar.getArcher());
    }

    // Start is called before the first frame update
    void Start()
    {
        //gameManager = GameManager.GetInstatnce();
        //To Test

        //EquipManager.GetEquip(EquipmentFilePath.SampleArcher);

        //
        //CharacterListUpdate();
        Utility.ObjectVisibility(itemSwapPanel, false);
        itemSlot = new GameObject[49];
        StartCoroutine(PanelSwapObserver());
        originColor = new Color(1, 1, 1, 1);
        itemSwapFlag = false;

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
            if (flag == 1)
            {
                CharacterListUpdate();
                EquipmentListUpdate(EquipClass.All);
                PlayerPrefs.SetInt(PrefsEntity.InformationFlag, -1);
            }
            //if (flag != startFlag)
            //{
            //    SetPanelActivate(flag);
            //}
            yield return null;
        }
    }

    IEnumerator FlickHead()
    {
        itemSwapFlag = true;
        while (itemSwapFlag)
        {
            flicker = Mathf.Abs(Mathf.Sin(Time.time * 3)) + 0.3f;
            equipHead.transform.Find("ItemImage").GetComponent<Image>().color = originColor * flicker;
            yield return null;
        }
        FlickSetOriginal();
    }
    IEnumerator FlickBody()
    {
        itemSwapFlag = true;
        while (itemSwapFlag)
        {
            flicker = Mathf.Abs(Mathf.Sin(Time.time * 3)) + 0.3f;
            equipBody.transform.Find("ItemImage").GetComponent<Image>().color = originColor * flicker;
            yield return null;
        }
        FlickSetOriginal();
    }
    IEnumerator FlickFoot()
    {
        itemSwapFlag = true;
        while (itemSwapFlag)
        {
            flicker = Mathf.Abs(Mathf.Sin(Time.time * 3)) + 0.3f;
            equipFoot.transform.Find("ItemImage").GetComponent<Image>().color = originColor * flicker;
            yield return null;
        }
        FlickSetOriginal();
    }
    IEnumerator FlickWeapon()
    {
        itemSwapFlag = true;
        while (itemSwapFlag)
        {
            flicker = Mathf.Abs(Mathf.Sin(Time.time * 3)) + 0.3f;
            equipWeapon.transform.Find("ItemImage").GetComponent<Image>().color = originColor * flicker;
            yield return null;
        }
        FlickSetOriginal();
    }
    IEnumerator FlickSubweapon()
    {
        itemSwapFlag = true;
        while (itemSwapFlag)
        {
            flicker = Mathf.Abs(Mathf.Sin(Time.time * 3)) + 0.3f;
            equipSubweapon.transform.Find("ItemImage").GetComponent<Image>().color = originColor * flicker;
            yield return null;
        }
        FlickSetOriginal();
    }

    private void FlickSetOriginal()
    {
        equipHead.transform.Find("ItemImage").GetComponent<Image>().color = originColor;
        equipBody.transform.Find("ItemImage").GetComponent<Image>().color = originColor;
        equipFoot.transform.Find("ItemImage").GetComponent<Image>().color = originColor;
        equipWeapon.transform.Find("ItemImage").GetComponent<Image>().color = originColor;
        equipSubweapon.transform.Find("ItemImage").GetComponent<Image>().color = originColor;
    }

    private void CharacterListUpdate()
    {
        foreach (GameObject i in characterList)
        {
            Destroy(i);
        }
        characterData = gameManager.GetCharacterList();
        //Test
        //characterData.Add(DataChar.getArcher());
        //characterData.Add(DataChar.getArcher());
        //characterData.Add(DataChar.getArcher());
        //characterData.Add(DataChar.getArcher());
        //characterData.Add(DataChar.getArcher());
        //characterData.Add(DataChar.getArcher());
        //characterData.Add(DataChar.getArcher());

        //
        //characterList = new GameObject[gameManager.NumOfCharacter()];
        characterList = new GameObject[characterData.Count];
        for (int i = 0; i < characterList.Length; i++)
        {
            int iconIndex = i;
            characterList[i] = Instantiate(characterIcon, characterListParent.transform);
            //characterList[i].transform.localPosition = new Vector3(-260f + 115*i, 0f, 0f);
            //Debug.Log(characterList[i].transform.position);
            //Debug.Log(characterList[i].transform.localPosition);
            switch (characterData[i].cls)
            {
                case "Thief":
                    characterList[i].transform.Find("CharacterImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.ThiefIcon) as Sprite;
                    characterList[i].GetComponent<Button>().onClick.AddListener(() => {
                        CharacterSelect(iconIndex);
                    });
                    break;
                case "Archer":
                    characterList[i].transform.Find("CharacterImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.ArcherIcon) as Sprite;
                    characterList[i].GetComponent<Button>().onClick.AddListener(() => {
                        CharacterSelect(iconIndex);
                    });
                    break;
                case "Mage":
                    characterList[i].transform.Find("CharacterImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.MagicianIcon) as Sprite;
                    characterList[i].GetComponent<Button>().onClick.AddListener(() => {
                        CharacterSelect(iconIndex);
                    });
                    break;
                case "Knight":
                    characterList[i].transform.Find("CharacterImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.KnightIcon) as Sprite;
                    characterList[i].GetComponent<Button>().onClick.AddListener(() => {
                        CharacterSelect(iconIndex);
                    });
                    break;
                case "Priest":
                    characterList[i].transform.Find("CharacterImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.PriestIcon) as Sprite;
                    characterList[i].GetComponent<Button>().onClick.AddListener(() => {
                        CharacterSelect(iconIndex);
                    });
                    break;
                default:
                    Debug.Log("Error : Class is " + characterData[i].cls);
                    break;
            }
        }
        characterImage.GetComponent<Image>().color = new Color(0, 0, 0, 0);

        equipHead.transform.Find("ItemImage").gameObject.GetComponent<Image>().color
            = new Color(0, 0, 0, 0);
        equipBody.transform.Find("ItemImage").gameObject.GetComponent<Image>().color
            = new Color(0, 0, 0, 0);
        equipFoot.transform.Find("ItemImage").gameObject.GetComponent<Image>().color
            = new Color(0, 0, 0, 0);
        equipWeapon.transform.Find("ItemImage").gameObject.GetComponent<Image>().color
            = new Color(0, 0, 0, 0);
        equipSubweapon.transform.Find("ItemImage").gameObject.GetComponent<Image>().color
            = new Color(0, 0, 0, 0);
    }

    private void EquipmentListUpdate(EquipClass sort)
    {
        foreach (Transform child in itemListParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        int additionalSpace = 0;
        itemList = gameManager.GetEquipment();
        for (int i = 0; i < itemList.Count; i++)
        {
            if(sort == itemList[i].cls | sort == EquipClass.All)
            {
                int itemIndex = i;
                itemSlot[i] = Instantiate(itemPrefab, itemListParent.transform);
                itemSlot[i].transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite
                    = Resources.Load<Sprite>(itemList[i].img) as Sprite;
                itemSlot[i].gameObject
                    .AddComponent<TooltipManager>()
                    .SetEquip(itemList[i], itemIndex, IconType.Equipment);
                itemSlot[i].gameObject.AddComponent<Button>().onClick.AddListener(() =>
                {
                    OnClickItem(itemIndex);
                });
            }
            else
            {
                additionalSpace++;
            }
        }
        for (int i = itemList.Count - additionalSpace; i < itemSlot.Length; i++)
        {
            itemSlot[i] = Instantiate(itemPrefab, itemListParent.transform);
            itemSlot[i].transform.Find("ItemImage").gameObject.GetComponent<Image>().color
                = Color.clear;
        }
    }

    public void CharacterSelect(int index)
    {
        selectedCharacterIndex = index;
        selectedCharacter = characterData[index];
        switch (characterData[index].cls)
        {
            case "Thief":
                characterImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.ThiefIdle) as Sprite;
                characterImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                OnClickSortThief();
                break;
            case "Archer":
                characterImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.ArcherIdle) as Sprite;
                characterImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                OnClickSortArhcer();
                break;
            case "Mage":
                characterImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.MagicianIdle) as Sprite;
                characterImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                OnClickSortMage();
                break;
            case "Knight":
                characterImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.KnightIdle) as Sprite;
                characterImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                OnClickSortKnight();
                break;
            case "Priest":
                characterImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.PriestIdle) as Sprite;
                characterImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                OnClickSortPriest();
                break;
            default:
                Debug.Log("Error : Class is " + characterData[index].cls);
                break;

        }
        ShowCharacterEquipment(index);
        equipHead.gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            EquipmentSwap(EquipType.Head, 0);
        });
        equipBody.gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            EquipmentSwap(EquipType.Body, 1);
        });
        equipFoot.gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            EquipmentSwap(EquipType.Foot, 2);
        });
        equipWeapon.gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            EquipmentSwap(EquipType.Weapon, 3);
        });
        equipSubweapon.gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            if(characterData[index].cls == "Thief")
            {
                EquipmentSwap(EquipType.Weapon, 4);
            }
            else
            {
                EquipmentSwap(EquipType.SubWeapon, 4);
            }
        });
        //TODO : Show equip, skills
    }

    private void ShowCharacterEquipment(int index)
    {
        equipHead.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite
                = Resources.Load<Sprite>(characterData[index].charEquipSet.set[0].img) as Sprite;
        equipHead.transform.Find("ItemImage").gameObject.GetComponent<Image>().color
            = new Color(1, 1, 1, 1);
        if (equipHead.gameObject.GetComponent<TooltipManager>() == null)
            equipHead.gameObject.AddComponent<TooltipManager>();
        equipHead.gameObject
            .GetComponent<TooltipManager>()
            .SetEquip(characterData[index].charEquipSet.set[0], index, IconType.Equipment);

        equipBody.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite
                = Resources.Load<Sprite>(characterData[index].charEquipSet.set[1].img) as Sprite;
        equipBody.transform.Find("ItemImage").gameObject.GetComponent<Image>().color
            = new Color(1, 1, 1, 1);
        if (equipBody.gameObject.GetComponent<TooltipManager>() == null)
            equipBody.gameObject.AddComponent<TooltipManager>();
        equipBody.gameObject
            .GetComponent<TooltipManager>()
            .SetEquip(characterData[index].charEquipSet.set[1], index, IconType.Equipment);

        equipFoot.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite
                = Resources.Load<Sprite>(characterData[index].charEquipSet.set[2].img) as Sprite;
        equipFoot.transform.Find("ItemImage").gameObject.GetComponent<Image>().color
            = new Color(1, 1, 1, 1);
        if (equipFoot.gameObject.GetComponent<TooltipManager>() == null)
            equipFoot.gameObject.AddComponent<TooltipManager>();
        equipFoot.gameObject
            .GetComponent<TooltipManager>()
            .SetEquip(characterData[index].charEquipSet.set[2], index, IconType.Equipment);

        equipWeapon.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite
                = Resources.Load<Sprite>(characterData[index].charEquipSet.set[3].img) as Sprite;
        equipWeapon.transform.Find("ItemImage").gameObject.GetComponent<Image>().color
            = new Color(1, 1, 1, 1);
        if (equipWeapon.gameObject.GetComponent<TooltipManager>() == null)
            equipWeapon.gameObject.AddComponent<TooltipManager>();
        equipWeapon.gameObject
            .GetComponent<TooltipManager>()
            .SetEquip(characterData[index].charEquipSet.set[3], index, IconType.Equipment);

        equipSubweapon.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite
                = Resources.Load<Sprite>(characterData[index].charEquipSet.set[4].img) as Sprite;
        equipSubweapon.transform.Find("ItemImage").gameObject.GetComponent<Image>().color
            = new Color(1, 1, 1, 1);
        if (equipSubweapon.gameObject.GetComponent<TooltipManager>() == null)
            equipSubweapon.gameObject.AddComponent<TooltipManager>();
        equipSubweapon.gameObject
            .GetComponent<TooltipManager>()
            .SetEquip(characterData[index].charEquipSet.set[4], index, IconType.Equipment);

    }

    public void OnClickItem(int index)
    {
        Debug.Log(index + "th Item : " + itemList[index].name);
        selectedEquipIndex = index;
        selectedEquip = itemList[index];
        Utility.ObjectVisibility(itemSwapPanel, true);
        switch (itemList[index].type)
        {
            case EquipType.Body:
                StartCoroutine(FlickBody());
                break;
            case EquipType.Foot:
                StartCoroutine(FlickFoot());
                break;
            case EquipType.Head:
                StartCoroutine(FlickHead());
                break;
            case EquipType.Weapon:
                StartCoroutine(FlickWeapon());
                if (itemList[index].cls == EquipClass.Thief)
                    StartCoroutine(FlickSubweapon());
                break;
            case EquipType.SubWeapon:
                StartCoroutine(FlickSubweapon());
                break;
        }

        //Debug.Log(itemList[i].name);
    }

    private void EquipmentSwap(EquipType type, int slot)
    {
        //Debug.Log(type + ", " + selectedEquip.type + ", " + itemSwapFlag);
        if(type == selectedEquip.type & itemSwapFlag)
        {
            Equip temp = selectedCharacter.charEquipSet.set[slot];
            selectedCharacter.charEquipSet.set[slot] = selectedEquip;
            itemList[selectedEquipIndex] = temp;
            gameManager.SetEquipment(itemList);
            gameManager.UpdateCharacterList(characterData);
            CharacterSelect(selectedCharacterIndex);
            itemSwapFlag = false;
        }
    }

    public void OnClickSortThief()
    {
        EquipmentListUpdate(EquipClass.Thief);
    }
    public void OnClickSortArhcer()
    {
        EquipmentListUpdate(EquipClass.Archer);
    }
    public void OnClickSortMage()
    {

        EquipmentListUpdate(EquipClass.Mage);
    }
    public void OnClickSortKnight()
    {

        EquipmentListUpdate(EquipClass.Knight);
    }
    public void OnClickSortPriest()
    {

        EquipmentListUpdate(EquipClass.Priest);
    }
    public void OnClickSortAll()
    {
        EquipmentListUpdate(EquipClass.All);

    }

    public void ItemSwapStop()
    {
        itemSwapFlag = false;
        selectedEquip = null;
        Utility.ObjectVisibility(itemSwapPanel, false);
    }
}
