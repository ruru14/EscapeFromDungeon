using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelController : MonoBehaviour
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

    public GameObject selectedSkill0;
    public GameObject selectedSkill1;
    public GameObject selectedSkill2;
    public GameObject selectedSkill3;
    public List<GameObject> characterSkillSet;

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
        characterSkillSet.Add(selectedSkill0);
        characterSkillSet.Add(selectedSkill1);
        characterSkillSet.Add(selectedSkill2);
        characterSkillSet.Add(selectedSkill3);
        //To Test
        //EquipManager.GetEquip(EquipmentFilePath.SampleArcher);
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
            if (flag == 0)
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
        foreach(GameObject i in characterList)
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
        for (int i = 0; i<characterList.Length; i++)
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
        hp.text = "";
        mp.text = "";
        phyAtk.text = "";
        phyDef.text = "";
        magAtk.text = "";
        magDef.text = "";
        critical.text = "";
        speed.text = "";
        //Debug.Log(characterSkillSet.Count);
        for(int i=0; i<4; i++)
        {
            //characterSkillSet[i].transform.GetComponent<Image>().sprite = Sp;
            characterSkillSet[i].transform.Find("SkillIcon").GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }

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
        hp.text = characterData[index].curHP.ToString() + " / " + characterData[index].getMaxHP().ToString();
        mp.text = characterData[index].curMP.ToString() + " / " + characterData[index].getMaxMP().ToString();
        phyAtk.text = characterData[index].getPhyATK().ToString();
        phyDef.text = characterData[index].getPhyDEF().ToString();
        magAtk.text = characterData[index].getMgcATK().ToString();
        magDef.text = characterData[index].getMgcDEF().ToString();
        critical.text = characterData[index].critical.ToString();
        speed.text = characterData[index].getSPD().ToString();


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

        //TODO : Show equip, skills
        for(int i=0; i<4; i++)
        {
            int skillNumber = i;
            characterSkillSet[skillNumber].transform.Find("SkillIcon").GetComponent<Image>().sprite
                = Resources.Load<Sprite>(characterData[index].charSkillSet.set[skillNumber].image);
            characterSkillSet[skillNumber].transform.Find("SkillIcon").GetComponent<Image>().color
                = new Color(1, 1, 1, 1);
            if (characterSkillSet[skillNumber].GetComponent<TooltipManager>() == null)
            {
                characterSkillSet[skillNumber].AddComponent<TooltipManager>();
            }
            characterSkillSet[skillNumber].GetComponent<TooltipManager>().SetSkill(
                characterData[index].charSkillSet.set[skillNumber], skillNumber, IconType.Skill,
                characterData[index].cls, characterData[index].charSkillSet.set[skillNumber].type);
        }

        ShowCharacterEquipment(index);

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
}
