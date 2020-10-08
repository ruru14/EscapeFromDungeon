using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum IconType : int
{
    Equipment = 0,
    Skill = 1
}

public class TooltipManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public int index;
    public GameObject test;
    private GameObject frame;
    private Equip equip;
    private Skill skill;
    private bool btnDown = false;
    private float durationThreshold = 0.7f;
    private float timePressStarted;
    private bool longPressTriger = false;
    private IconType iconType;

    private string cls;
    private SKILLTYPE skillType;

    // Start is called before the first frame update
    void Start()
    {
        //try
        //{
        //    frame = transform.parent.Find("Frame").gameObject;
        //    frame.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        //}catch(System.NullReferenceException e)
        //{

        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (btnDown && !longPressTriger)
        {
            if(Time.time - timePressStarted > durationThreshold)
            {
                longPressTriger = true;
                if(iconType == IconType.Equipment)
                {
                    Debug.Log("Equiptooltip");
                    GameObject.FindGameObjectWithTag("TooltipEquipment")
                        .GetComponent<EquipmentTooltip>()
                        .Tooltip(Input.mousePosition, equip);
                }
                else
                {
                    Debug.Log("Skilltooltip");
                    GameObject.FindGameObjectWithTag("TooltipSkill")
                        .GetComponent<SkillTooltip>()
                        .Tooltip(Input.mousePosition, skill, cls, skillType);
                }
                //Debug.Log(Input.mousePosition + " : " + index + " : " + equip.name);
            }
        }
    }

    public void SetEquip(Equip equip, int index, IconType iconType)
    {
        this.equip = equip;
        this.index = index;
        this.iconType = iconType;
    }

    public void SetSkill(Skill skill, int index, IconType iconType, string cls, SKILLTYPE skillType)
    {
        this.skill = skill;
        this.index = index;
        this.iconType = iconType;
        this.cls = cls;
        this.skillType = skillType;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        timePressStarted = Time.time;
        btnDown = true;
        longPressTriger = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        btnDown = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        btnDown = false;
    }

}
