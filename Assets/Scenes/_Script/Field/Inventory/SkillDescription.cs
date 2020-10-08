using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillDescription : MonoBehaviour, IPointerClickHandler
{
    public GameObject skillDescriptionPrefab;
    public GameObject skillDescription;
    public GameObject parent;
    public GameObject content;
    private SkillExplain skillInformation;
    private Skill skill;
    private SKILLTYPE type;
    private string cls;
    private int skillIndex;
    private int detailInfoSize;
    private bool descriptionFlag = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setter(GameObject skillDescriptionPrefab, GameObject parent, GameObject content, string cls, int skillIndex, Skill skill, SKILLTYPE type)
    {
        this.skillDescriptionPrefab = skillDescriptionPrefab;
        this.parent = parent;
        this.content = content;
        this.skillIndex = skillIndex;
        this.cls = cls;
        this.skill = skill;
        this.type = type;
        //characterImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(Resource.PriestIdle) as Sprite;

        //Debug.Log(skill.name);
        skillInformation = SkillParser.GetSkill(SkillName.GetSkillNameFile(skill.name, cls, type), skill.level-1);

        transform.Find("SkillOverview").Find("SkillName").GetComponent<Text>().text = skillInformation.skillName + " Lv." + skill.level;
        Color nameColor = new Color(0, 0, 0);
        if (type == SKILLTYPE.PAS)
        {
            nameColor = new Color(0.7f, 0.8f, 1f);
        }
        else
        {
            nameColor = new Color(1f, 0.8f, 0.8f);
        }
        transform.Find("SkillOverview").Find("SkillName").GetComponent<Text>().color = nameColor;

        transform.Find("SkillOverview").Find("SkillSummary").GetComponent<Text>().text = skillInformation.explain;
        transform.Find("SkillOverview").Find("SkillIcon").GetComponent<Image>().sprite =
            Resources.Load<Sprite>(skill.image);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (descriptionFlag)
        {
            //skillDescription.transform.localScale = new Vector3(0, 0, 0);
            skillDescription.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
            Utility.ObjectVisibility(skillDescription, false);
            //Destroy(skillDescription);
            descriptionFlag = !descriptionFlag;
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)content.GetComponent<VerticalLayoutGroup>().transform);
            //LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parent.GetComponent<ContentSizeFitter>().transform);

        }
        else
        {
            if(skillDescription == null)
            {
                skillDescription = Instantiate(skillDescriptionPrefab, parent.transform);
                SetSkillInformation();
            }
            else
            {
                Utility.ObjectVisibility(skillDescription, true);
            }
            skillDescription.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, detailInfoSize);
            skillDescription.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
            {
                SkillDeploy();
            });
            descriptionFlag = !descriptionFlag;
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)content.GetComponent<VerticalLayoutGroup>().transform);
            //LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parent.GetComponent<ContentSizeFitter>().transform);

        }
        //LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)content.GetComponent<ContentSizeFitter>().transform);
        //content.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
    }

    private void SetSkillInformation()
    {
        //Debug.Log(skillInformation.ability.Length);
        detailInfoSize = 90 + (skillInformation.ability.Length / 18) * 35;
        skillDescription.transform.Find("SkillAbility").GetComponent<Text>().text = skillInformation.ability;
        Color abilityColor = new Color(0, 0, 0);
        if (type == SKILLTYPE.PAS)
        {
            abilityColor = new Color(0.51f, 0.75f, 0.76f);
        }
        else
        {
            abilityColor = new Color(0.76f, 0.59f, 0.51f);
        }
        skillDescription.transform.Find("SkillAbility").GetComponent<Text>().color = abilityColor;
    }

    public void SkillDeploy()
    {
        transform.parent.parent.parent.parent.parent.parent.
            Find("SkillPanelController").GetComponent<SkillPanelController>().ChangeSkillSelect(skill);
        //Debug.Log("Selected Skill : " + skill.name);
    }
}
