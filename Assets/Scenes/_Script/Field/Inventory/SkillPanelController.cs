using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanelController : MonoBehaviour
{
    private GameManager gameManager;
    private List<DataChar> characterData;
    public GameObject characterListParent;
    public GameObject[] characterList;
    public GameObject characterIcon;
    public GameObject activeSkillParent;
    public GameObject passiveSkillParent;
    public GameObject[] activeSkillList;
    public GameObject[] passiveSkillList;
    public GameObject skillPrefab;
    public GameObject skillDescriptionPrefab;
    public GameObject characterImage;
    public GameObject selectedSkill0;
    public GameObject selectedSkill1;
    public GameObject selectedSkill2;
    public GameObject selectedSkill3;
    public GameObject skillChangeNotice;
    public GameObject tabManager;
    public GameObject skillChangePanel;
    private Skill changeStandBySkill;
    private List<GameObject> selectedSkillSet;
    private int selectedCharacter;
    private int temp = -7;
    //public GameObject passiveSkillParent;

    private Color originColor;
    private float flicker;
    private bool skillSelectFlag;


    void Awake()
    {
        gameManager = GameManager.GetInstatnce();
        //characterData = gameManager.GetCharacterList();
        characterData = new List<DataChar>();
        selectedSkillSet = new List<GameObject>();
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
        Utility.ObjectVisibility(skillChangeNotice, false);
    }

    // Start is called before the first frame update
    void Start()
    {
        selectedSkillSet.Add(selectedSkill0);
        selectedSkillSet.Add(selectedSkill1);
        selectedSkillSet.Add(selectedSkill2);
        selectedSkillSet.Add(selectedSkill3);
        CharacterListUpdate();
        //To Test
        //EquipManager.GetEquip(EquipmentFilePath.SampleArcher);
        StartCoroutine(PanelSwapObserver());

        originColor = selectedSkill0.GetComponent<Renderer>().material.color;
        skillSelectFlag = false;
        /*
         * 
        
        if (btnUp.interactable)
        {
            flickUp.material.color = originColor * flicker;
        }
         */
        Utility.ObjectVisibility(skillChangePanel, false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0) & tabManager.GetComponent<UpperTabController>().GetActivePanel() == 2)
        //{
        //    Debug.Log("11");
        //    skillSelectFlag = false;
        //    Debug.Log("22");
        //}
    }

    IEnumerator SelectSkillFlicker()
    {
        Utility.ObjectVisibility(skillChangeNotice, true);
        while (skillSelectFlag)
        {
            flicker = Mathf.Abs(Mathf.Sin(Time.time * 3)) + 0.3f;
            selectedSkill0.transform.Find("SkillIcon").GetComponent<Image>().color = originColor * flicker;
            selectedSkill1.transform.Find("SkillIcon").GetComponent<Image>().color = originColor * flicker;
            selectedSkill2.transform.Find("SkillIcon").GetComponent<Image>().color = originColor * flicker;
            selectedSkill3.transform.Find("SkillIcon").GetComponent<Image>().color = originColor * flicker;
            skillChangeNotice.GetComponent<Image>().color = originColor * flicker;
            skillChangeNotice.transform.Find("Image").GetComponent<Image>().color = originColor * flicker;
            skillChangeNotice.transform.Find("Text").GetComponent<Text>().color = originColor * flicker;
            yield return null;
        }
        FlickSetOriginal();
    }

    private void FlickSetOriginal()
    {
        skillChangeNotice.GetComponent<Image>().color = originColor;
        skillChangeNotice.transform.Find("Image").GetComponent<Image>().color = originColor;
        skillChangeNotice.transform.Find("Text").GetComponent<Text>().color = originColor;
        Utility.ObjectVisibility(skillChangeNotice, false);
        selectedSkill0.transform.Find("SkillIcon").GetComponent<Image>().color = originColor;
        selectedSkill1.transform.Find("SkillIcon").GetComponent<Image>().color = originColor;
        selectedSkill2.transform.Find("SkillIcon").GetComponent<Image>().color = originColor;
        selectedSkill3.transform.Find("SkillIcon").GetComponent<Image>().color = originColor;
        changeStandBySkill = null;
    }

    IEnumerator PanelSwapObserver()
    {
        while (true)
        {
            int flag = PlayerPrefs.GetInt(PrefsEntity.InformationFlag, -1);
            //Debug.Log(flag);
            if (flag == 2)
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
                        CharacterInformation(iconIndex);
                    });
                    break;
                case "Archer":
                    characterList[i].transform.Find("CharacterImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.ArcherIcon) as Sprite;
                    characterList[i].GetComponent<Button>().onClick.AddListener(() => {
                        CharacterInformation(iconIndex);
                    });
                    break;
                case "Mage":
                    characterList[i].transform.Find("CharacterImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.MagicianIcon) as Sprite;
                    characterList[i].GetComponent<Button>().onClick.AddListener(() => {
                        CharacterInformation(iconIndex);
                    });
                    break;
                case "Knight":
                    characterList[i].transform.Find("CharacterImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.KnightIcon) as Sprite;
                    characterList[i].GetComponent<Button>().onClick.AddListener(() => {
                        CharacterInformation(iconIndex);
                    });
                    break;
                case "Priest":
                    characterList[i].transform.Find("CharacterImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.PriestIcon) as Sprite;
                    characterList[i].GetComponent<Button>().onClick.AddListener(() => {
                        CharacterInformation(iconIndex);
                    });
                    break;
                default:
                    Debug.Log("Error : Class is " + characterData[i].cls);
                    break;
            }
        }

        characterImage.GetComponent<Image>().color = new Color(0, 0, 0, 0);

        for (int i = 0; i < 4; i++)
        {
            selectedSkillSet[i].transform.Find("SkillIcon").GetComponent<Image>().sprite = null;
            selectedSkillSet[i].transform.Find("SkillIcon").GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
        foreach (GameObject actSkill in activeSkillList)
        {
            Destroy(actSkill);
        }
        foreach (GameObject pasSkill in passiveSkillList)
        {
            Destroy(pasSkill);
        }
    }

    public void CharacterInformation(int index)
    {
        //Debug.Log("Test : " + index);
        selectedCharacter = index;
        foreach (GameObject actSkill in activeSkillList)
        {
            Destroy(actSkill);
        }

        activeSkillList = new GameObject[5];
        for(int i=0; i<5; i++)
        {
            int skillIndex = i;
            activeSkillList[i] = Instantiate(skillPrefab, activeSkillParent.transform);
            activeSkillList[i].AddComponent<SkillDescription>().Setter(skillDescriptionPrefab, 
                activeSkillList[i], 
                activeSkillParent, 
                characterData[index].cls, 
                i, 
                characterData[index].charSkillSet.act[i],
                SKILLTYPE.ACT);
            //Debug.Log(characterData[index].charSkillSet.act[i]) ;
        }

        foreach (GameObject pasSkill in passiveSkillList)
        {
            Destroy(pasSkill);
        }

        passiveSkillList = new GameObject[5];
        for (int i = 0; i < 5; i++)
        {
            int skillIndex = i;
            passiveSkillList[i] = Instantiate(skillPrefab, passiveSkillParent.transform);
            passiveSkillList[i].AddComponent<SkillDescription>().Setter(skillDescriptionPrefab, 
                passiveSkillList[i], 
                passiveSkillParent, 
                characterData[index].cls, 
                i, 
                characterData[index].charSkillSet.pas[i],
                SKILLTYPE.PAS);

        }
        //TODO : Show equip, skills

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

        for(int i=0; i<4; i++)
        {
            int skillNumber = i;
            selectedSkillSet[skillNumber].transform.Find("SkillIcon").GetComponent<Image>().sprite
                = Resources.Load<Sprite>(characterData[index].charSkillSet.set[i].image);
            selectedSkillSet[skillNumber].transform.Find("SkillIcon").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            if(selectedSkillSet[skillNumber].GetComponent<Button>() == null)
            {
                selectedSkillSet[skillNumber].AddComponent<Button>();
                selectedSkillSet[skillNumber].GetComponent<Button>().onClick.AddListener(() =>
                {
                    SkillChange(skillNumber);
                });
            }
            if(selectedSkillSet[skillNumber].GetComponent<TooltipManager>() == null)
            {
                selectedSkillSet[skillNumber].AddComponent<TooltipManager>();
            }
            selectedSkillSet[skillNumber].GetComponent<TooltipManager>()
                .SetSkill(characterData[index].charSkillSet.set[i], skillNumber, IconType.Skill,
                    characterData[index].cls, characterData[index].charSkillSet.set[i].type);
        }
    }

    public void ChangeSkillSelect(Skill skill)
    {
        Debug.Log("Change Skill Setted");
        changeStandBySkill = skill;
        skillSelectFlag = true;
        Utility.ObjectVisibility(skillChangePanel, true);
        StartCoroutine(SelectSkillFlicker());
    }

    public void SkillChange(int index)
    {
        //Debug.Log("Select Skill : " + index);
        bool skillExist = false;
        if(skillSelectFlag)
        {
            //Debug.Log(" : " + changeStandBySkill.name);
            foreach(Skill i in characterData[selectedCharacter].charSkillSet.set)
            {
                if(i.name == changeStandBySkill.name)
                {
                    skillExist = true;
                }
            }
            if (!skillExist)
            {
                characterData[selectedCharacter].charSkillSet.set.RemoveAt(index);
                characterData[selectedCharacter].charSkillSet.set.Insert(index, changeStandBySkill);
            }
        }
        CharacterInformation(selectedCharacter);
        gameManager.UpdateCharacterList(characterData);
        CancelSkillChange();
    }

    public void CancelSkillChange()
    {
        Utility.ObjectVisibility(skillChangePanel, false);
        skillSelectFlag = false;
    }
}
