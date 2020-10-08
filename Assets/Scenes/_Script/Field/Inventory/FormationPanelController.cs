using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormationPanelController : MonoBehaviour
{
    private GameManager gameManager;
    private List<DataChar> characterData;

    public GameObject characterListParent;
    public GameObject[] characterList;
    public GameObject characterIconPrefab;
    public GameObject formation00;
    public GameObject formation01;
    public GameObject formation02;
    public GameObject formation10;
    public GameObject formation11;
    public GameObject formation12;
    public GameObject formation20;
    public GameObject formation21;
    public GameObject formation22;

    public Text hp;
    public Text mp;
    public Text phyAtk;
    public Text phyDef;
    public Text magAtk;
    public Text magDef;
    public Text critical;
    public Text speed;
    public GameObject selectedSkill0;
    public GameObject selectedSkill1;
    public GameObject selectedSkill2;
    public GameObject selectedSkill3;
    public List<GameObject> characterSkillSet;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstatnce();
        characterData = gameManager.GetCharacterList();
        characterList = new GameObject[10];

        characterSkillSet.Add(selectedSkill0);
        characterSkillSet.Add(selectedSkill1);
        characterSkillSet.Add(selectedSkill2);
        characterSkillSet.Add(selectedSkill3);

        ForamtionPanelUpdate();
        StartCoroutine(PanelSwapObserver());
    }

    IEnumerator PanelSwapObserver()
    {
        while (true)
        {
            int flag = PlayerPrefs.GetInt(PrefsEntity.InformationFlag, -1);
            //Debug.Log(flag);
            if (flag == 5)
            {
                ForamtionPanelUpdate();
                PlayerPrefs.SetInt(PrefsEntity.InformationFlag, -1);
            }
            //if (flag != startFlag)
            //{
            //    SetPanelActivate(flag);
            //}
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ForamtionPanelUpdate()
    {
        foreach (Transform child in characterListParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        int characterIndex;

        for(characterIndex = 0; characterIndex<characterData.Count; characterIndex++)
        {
            int i = characterIndex;
            characterList[characterIndex] = Instantiate(characterIconPrefab, characterListParent.transform);

            switch (characterData[characterIndex].cls)
            {
                case "Thief":
                    characterList[characterIndex].transform.Find("CharacterImage").GetComponent<Image>().sprite 
                        = Resources.Load<Sprite>(Resource.ThiefIcon) as Sprite;
                    characterList[characterIndex].transform.Find("CharacterImage").GetComponent<Image>().color
                        = new Color(1, 1, 1, 1);
                    break;
                case "Archer":
                    characterList[characterIndex].transform.Find("CharacterImage").GetComponent<Image>().sprite 
                        = Resources.Load<Sprite>(Resource.ArcherIcon) as Sprite;
                    characterList[characterIndex].transform.Find("CharacterImage").GetComponent<Image>().color
                        = new Color(1, 1, 1, 1);
                    break;
                case "Mage":
                    characterList[characterIndex].transform.Find("CharacterImage").GetComponent<Image>().sprite 
                        = Resources.Load<Sprite>(Resource.MagicianIcon) as Sprite;
                    characterList[characterIndex].transform.Find("CharacterImage").GetComponent<Image>().color
                        = new Color(1, 1, 1, 1);
                    break;
                case "Knight":
                    characterList[characterIndex].transform.Find("CharacterImage").GetComponent<Image>().sprite 
                        = Resources.Load<Sprite>(Resource.KnightIcon) as Sprite;
                    characterList[characterIndex].transform.Find("CharacterImage").GetComponent<Image>().color
                        = new Color(1, 1, 1, 1);
                    break;
                case "Priest":
                    characterList[characterIndex].transform.Find("CharacterImage").GetComponent<Image>().sprite 
                        = Resources.Load<Sprite>(Resource.PriestIcon) as Sprite;
                    characterList[characterIndex].transform.Find("CharacterImage").GetComponent<Image>().color
                        = new Color(1, 1, 1, 1);
                    break;
                default:
                    Debug.Log("Error : Class is " + characterData[characterIndex].cls);
                    break;
            }
            characterList[characterIndex].AddComponent<Button>()
                .onClick.AddListener(() =>
               {
                   OnClickCharacterIcon(i);
               });
        }

        for (int i = characterIndex; i<10; i++)
        {
            characterList[characterIndex] = Instantiate(characterIconPrefab, characterListParent.transform);
        }

        hp.text = "";
        mp.text = "";
        phyAtk.text = "";
        phyDef.text = "";
        magAtk.text = "";
        magDef.text = "";
        critical.text = "";
        speed.text = "";

        for (int i = 0; i < 4; i++)
        {
            characterSkillSet[i].transform.Find("SkillIcon").GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }

    private void GetFormation()
    {

    }

    public void OnClickFormationReset()
    {

    }

    public void OnClickFormationConfirm()
    {

    }

    private void OnClickCharacterIcon(int index)
    {
        Debug.Log("<>>" + index);
        hp.text = characterData[index].curHP.ToString() + " / " + characterData[index].getMaxHP().ToString();
        mp.text = characterData[index].curMP.ToString() + " / " + characterData[index].getMaxMP().ToString();
        phyAtk.text = characterData[index].getPhyATK().ToString();
        phyDef.text = characterData[index].getPhyDEF().ToString();
        magAtk.text = characterData[index].getMgcATK().ToString();
        magDef.text = characterData[index].getMgcDEF().ToString();
        critical.text = characterData[index].critical.ToString();
        speed.text = characterData[index].getSPD().ToString();

        for (int i = 0; i < 4; i++)
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
    }
}
