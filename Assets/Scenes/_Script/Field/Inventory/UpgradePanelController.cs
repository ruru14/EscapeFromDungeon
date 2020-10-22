using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelController : MonoBehaviour
{
    private GameManager gameManager;
    private List<DataChar> characterData;
    public GameObject characterListParent;
    public GameObject[] characterList;
    public GameObject characterIcon;
    public GameObject characterImage;

    public GameObject equipHead;
    public GameObject equipBody;
    public GameObject equipFoot;
    public GameObject equipWeapon;
    public GameObject equipSubweapon;

    public GameObject itemListParent;
    public GameObject itemPrefab;
    private List<Equip> itemList;
    private GameObject[] itemSlot;

    public GameObject panelStatusUp;
    public GameObject panelSkillUp;
    public GameObject panelEquipUp;

    public Text hp;
    public Text mp;
    public Text phyAtk;
    public Text phyDef;
    public Text magAtk;
    public Text magDef;
    public Text critical;
    public Text speed;

    void Awake()
    {
        gameManager = GameManager.GetInstatnce();
        //characterData = gameManager.GetCharacterList();
        characterData = new List<DataChar>();
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
        StartCoroutine(PanelSwapObserver());
        //To Test
        //EquipManager.GetEquip(EquipmentFilePath.SampleArcher);
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
            if (flag == 3)
            {
                CharacterListUpdate();
                PlayerPrefs.SetInt(PrefsEntity.InformationFlag, -1);
            }
            //if (flag != startFlag)
            //{
            //    SetPanelActivate(flag);
            //}
            yield return null;
        }
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
        characterList = new GameObject[gameManager.NumOfCharacter()];
        //characterList = new GameObject[7];
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

    public void CharacterSelect(int index)
    {
        Debug.Log("Test : " + index);

        switch (characterData[index].cls)
        {
            case "Thief":
                characterImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.ThiefIdle) as Sprite;
                characterImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;
            case "Archer":
                characterImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.ArcherIdle) as Sprite;
                characterImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;
            case "Mage":
                characterImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.MagicianIdle) as Sprite;
                characterImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;
            case "Knight":
                characterImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.KnightIdle) as Sprite;
                characterImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;
            case "Priest":
                characterImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.PriestIdle) as Sprite;
                characterImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;
            default:
                Debug.Log("Error : Class is " + characterData[index].cls);
                break;

        }
        ShowCharacterEquipment(index);

        //TODO : Show equip, skills
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
            if (sort == itemList[i].cls | sort == EquipClass.All)
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

    public void OnClickItem(int index)
    {
        Debug.Log(index + "th Item : " + itemList[index].name);
        //selectedEquipIndex = index;
        //selectedEquip = itemList[index];
        //Utility.ObjectVisibility(itemSwapPanel, true);
        //switch (itemList[index].type)
        //{
        //    case EquipType.Body:
        //        StartCoroutine(FlickBody());
        //        break;
        //    case EquipType.Foot:
        //        StartCoroutine(FlickFoot());
        //        break;
        //    case EquipType.Head:
        //        StartCoroutine(FlickHead());
        //        break;
        //    case EquipType.Weapon:
        //        StartCoroutine(FlickWeapon());
        //        if (itemList[index].cls == EquipClass.Thief)
        //            StartCoroutine(FlickSubweapon());
        //        break;
        //    case EquipType.SubWeapon:
        //        StartCoroutine(FlickSubweapon());
        //        break;
        //}

        //Debug.Log(itemList[i].name);
    }

    private void ShowCharacterEquipment(int index)
    {
        equipHead.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite
                = Resources.Load<Sprite>(characterData[index].charEquipSet.set[0].img) as Sprite;
        equipHead.transform.Find("ItemImage").gameObject.GetComponent<Image>().color
            = new Color(1, 1, 1, 1);
        equipHead.gameObject
            .AddComponent<TooltipManager>()
            .SetEquip(characterData[index].charEquipSet.set[0], index, IconType.Equipment);

        equipBody.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite
                = Resources.Load<Sprite>(characterData[index].charEquipSet.set[1].img) as Sprite;
        equipBody.transform.Find("ItemImage").gameObject.GetComponent<Image>().color
            = new Color(1, 1, 1, 1);
        equipBody.gameObject
            .AddComponent<TooltipManager>()
            .SetEquip(characterData[index].charEquipSet.set[1], index, IconType.Equipment);

        equipFoot.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite
                = Resources.Load<Sprite>(characterData[index].charEquipSet.set[2].img) as Sprite;
        equipFoot.transform.Find("ItemImage").gameObject.GetComponent<Image>().color
            = new Color(1, 1, 1, 1);
        equipFoot.gameObject
            .AddComponent<TooltipManager>()
            .SetEquip(characterData[index].charEquipSet.set[2], index, IconType.Equipment);

        equipWeapon.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite
                = Resources.Load<Sprite>(characterData[index].charEquipSet.set[3].img) as Sprite;
        equipWeapon.transform.Find("ItemImage").gameObject.GetComponent<Image>().color
            = new Color(1, 1, 1, 1);
        equipWeapon.gameObject
            .AddComponent<TooltipManager>()
            .SetEquip(characterData[index].charEquipSet.set[3], index, IconType.Equipment);

        equipSubweapon.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite
                = Resources.Load<Sprite>(characterData[index].charEquipSet.set[4].img) as Sprite;
        equipSubweapon.transform.Find("ItemImage").gameObject.GetComponent<Image>().color
            = new Color(1, 1, 1, 1);
        equipSubweapon.gameObject
            .AddComponent<TooltipManager>()
            .SetEquip(characterData[index].charEquipSet.set[4], index, IconType.Equipment);

    }

    public void OnClickStatusUpgrade()
    {
        Utility.ObjectVisibility(panelStatusUp, true);
        Utility.ObjectVisibility(panelSkillUp, false);
        Utility.ObjectVisibility(panelEquipUp, false);
    }

    public void OnClickEquipUpgrade()
    {

        Utility.ObjectVisibility(panelStatusUp, false);
        Utility.ObjectVisibility(panelSkillUp, false);
        Utility.ObjectVisibility(panelEquipUp, true);
    }

    public void OnClickSkillUpgrade()
    {

        Utility.ObjectVisibility(panelStatusUp, false);
        Utility.ObjectVisibility(panelSkillUp, true);
        Utility.ObjectVisibility(panelEquipUp, false);
    }
}
