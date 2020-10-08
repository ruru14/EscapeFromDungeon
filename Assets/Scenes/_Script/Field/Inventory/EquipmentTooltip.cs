using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentTooltip : MonoBehaviour, IPointerClickHandler
{
    public GameObject tooltipPanel;
    public GameObject tooltip;

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

        //Set Panel Visible
        Utility.ObjectVisibility(tooltipPanel, true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Utility.ObjectVisibility(tooltipPanel, false);
    }
}
