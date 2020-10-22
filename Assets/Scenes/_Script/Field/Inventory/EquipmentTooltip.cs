using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentTooltip : MonoBehaviour, IPointerClickHandler
{
    public GameObject tooltipPanel;
    public GameObject tooltip;
    public Image equipmentIcon;
    public Text equipName;
    public Text equipGrade;
    public Text equipRequire;
    public Text hp;
    public Text mp;
    public Text ap;
    public Text phyAtk;
    public Text phyDef;
    public Text magAtk;
    public Text magDef;
    public Text critical;
    public Text healEf;

    // Start is called before the first frame update
    void Start()
    {
        Utility.ObjectVisibility(tooltipPanel, false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Tooltip(Vector3 position, Equip equip)
    {
        //Set Position
        tooltip.transform.localPosition = position + 
            new Vector3(-960.0f, -540.0f, 0) + 
            new Vector3(-355.0f, -222.5f, 0);
        if(tooltip.transform.localPosition.y < -540 + 222.5)
        {
            tooltip.transform.localPosition = 
                new Vector3(tooltip.transform.localPosition.x, -540 + 222.5f, tooltip.transform.localPosition.z);
        }
        if (tooltip.transform.localPosition.x < -960 + 355.0)
        {
            tooltip.transform.localPosition =
                new Vector3(-960 + 355.0f, tooltip.transform.localPosition.y, tooltip.transform.localPosition.z);
        }

        //TODO : Fill information

        /*
         * 
    public Image equipmentIcon;
    public Text hp;
    public Text mp;
    public Text ap;
    public Text phyAtk;
    public Text phyDef;
    public Text magAtk;
    public Text magDef;
    public Text critical;
    public Text healEf;
         */

        equipName.text = "+" + equip.level + " " + equip.name;
        string grade = "";
        Color gradeColor = new Color(0, 0, 0);
        switch (equip.grade)
        {
            case EquipGrade.Normal:
                grade = "노멀";
                gradeColor = new Color(0.83f, 0.746f, 0.661f);
                break;
            case EquipGrade.Rare:
                grade = "레어";
                gradeColor = new Color(0.589f, 0.934f, 1.0f);
                break;
            case EquipGrade.Unique:
                grade = "유니크";
                gradeColor = new Color(1.0f, 0.589f, 0.699f);
                break;
            case EquipGrade.Mystic:
                grade = "미스틱";
                gradeColor = new Color(1.0f, 0.849f, 0.0f);
                break;
        }
        equipGrade.text = "(" + grade + " 아이템)";
        equipGrade.color = gradeColor;
        string equipType = "";
        string equipReq = "";
        switch (equip.type)
        {
            case EquipType.Body:
                equipType = "갑옷";
                break;
            case EquipType.Foot:
                equipType = "신발";
                break;
            case EquipType.Head:
                equipType = "머리";
                break;
            case EquipType.SubWeapon:
                equipType = "보조무기";
                break;
            case EquipType.Weapon:
                equipType = "무기";
                break;
        }
        switch (equip.cls)
        {
            case EquipClass.Archer:
                equipReq = "궁수";
                break;
            case EquipClass.Knight:
                equipReq = "기사";
                break;
            case EquipClass.Mage:
                equipReq = "마법사";
                break;
            case EquipClass.Priest:
                equipReq = "사제";
                break;
            case EquipClass.Thief:
                equipReq = "도적";
                break;
        }
        equipmentIcon.sprite = Resources.Load<Sprite>(equip.img);
        equipRequire.text = equipType + "/" + equipReq + " 착용 가능";
        hp.text = "HP : " + equip.maxHP;
        mp.text = "MP : " + equip.maxMP;
        ap.text = "AP : " + equip.speed;
        phyAtk.text = "물리공격력 : " + equip.phyATK;
        phyDef.text = "물리방어력 : " + equip.phyDEF;
        magAtk.text = "마법공격력 : " + equip.macATK;
        magDef.text = "마법방어력 : " + equip.macDEF;
        critical.text = "치명타확률 : " + equip.critical + "%";
        healEf.text = "회복효율 : " + equip.healEFC + "%";

        //Set Panel Visible
        Utility.ObjectVisibility(tooltipPanel, true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Utility.ObjectVisibility(tooltipPanel, false);
    }
}
