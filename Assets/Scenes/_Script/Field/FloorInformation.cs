using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorInformation : MonoBehaviour
{
    public GameObject informationPanel;
    public GameObject controlblocker;
    public Text elementMessage;


    // Start is called before the first frame update
    void Start()
    {
        Utility.ObjectVisibility(informationPanel, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickInformationImage()
    {
        SetElementInformation();
        Utility.ObjectVisibility(controlblocker, true);
        Utility.ObjectVisibility(informationPanel, true);
    }

    public void OnClickConfirm()
    {
        Utility.ObjectVisibility(controlblocker, false);
        Utility.ObjectVisibility(informationPanel, false);
    }

    private void SetElementInformation()
    {
        int element = PlayerPrefs.GetInt(PrefsEntity.FieldElement, 0);
        //0 = Soil, 1 = Water, 2 = Fire, 3 = Wood, 4 = Steal
        //Soil : 707 584 430
        //Water : 431 664 705
        //Fire : 726 537 556
        //Wood : 537 725 544
        //Steal : 725 720 537
        int selectedLevel = PlayerPrefs.GetInt(PrefsEntity.CurrentLevel, 0);
        //0 = Normal, 1 = Hard, 2 = Hell
        Color elementColor;
        switch (element)
        {
            case 0:
                elementColor = new Color(0.707f, 0.584f, 0.430f, 1);
                switch (selectedLevel)
                {
                    case 0:
                        elementMessage.text = Utility.ScriptReader(ScriptFilePath.InfoSoilNormal);
                        elementMessage.color = elementColor;
                        break;
                    case 1:
                        elementMessage.text = Utility.ScriptReader(ScriptFilePath.InfoSoilHard);
                        elementMessage.color = elementColor;
                        break;
                    case 2:
                        elementMessage.text = Utility.ScriptReader(ScriptFilePath.InfoSoilHell);
                        elementMessage.color = elementColor;
                        break;
                }
                break;
            case 1:
                elementColor = new Color(0.431f, 0.664f, 0.705f, 1);
                switch (selectedLevel)
                {
                    case 0:
                        elementMessage.text = Utility.ScriptReader(ScriptFilePath.InfoWaterNormal);
                        elementMessage.color = elementColor;
                        break;
                    case 1:
                        elementMessage.text = Utility.ScriptReader(ScriptFilePath.InfoWaterHard);
                        elementMessage.color = elementColor;
                        break;
                    case 2:
                        elementMessage.text = Utility.ScriptReader(ScriptFilePath.InfoWaterHell);
                        elementMessage.color = elementColor;
                        break;
                }
                break;
            case 2:
                elementColor = new Color(0.726f, 0.537f, 0.556f, 1);
                switch (selectedLevel)
                {
                    case 0:
                        elementMessage.text = Utility.ScriptReader(ScriptFilePath.InfoFireNormal);
                        elementMessage.color = elementColor;
                        break;
                    case 1:
                        elementMessage.text = Utility.ScriptReader(ScriptFilePath.InfoFireHard);
                        elementMessage.color = elementColor;
                        break;
                    case 2:
                        elementMessage.text = Utility.ScriptReader(ScriptFilePath.InfoFireHell);
                        elementMessage.color = elementColor;
                        break;
                }
                break;
            case 3:
                elementColor = new Color(0.537f, 0.725f, 0.544f, 1);
                switch (selectedLevel)
                {
                    case 0:
                        elementMessage.text = Utility.ScriptReader(ScriptFilePath.InfoWoodNormal);
                        elementMessage.color = elementColor;
                        break;
                    case 1:
                        elementMessage.text = Utility.ScriptReader(ScriptFilePath.InfoWoodHard);
                        elementMessage.color = elementColor;
                        break;
                    case 2:
                        elementMessage.text = Utility.ScriptReader(ScriptFilePath.InfoWoodHell);
                        elementMessage.color = elementColor;
                        break;
                }
                break;
            case 4:
                elementColor = new Color(0.725f, 0.720f, 0.537f, 1);
                switch (selectedLevel)
                {
                    case 0:
                        elementMessage.text = Utility.ScriptReader(ScriptFilePath.InfoStealNormal);
                        elementMessage.color = elementColor;
                        break;
                    case 1:
                        elementMessage.text = Utility.ScriptReader(ScriptFilePath.InfoStealHard);
                        elementMessage.color = elementColor;
                        break;
                    case 2:
                        elementMessage.text = Utility.ScriptReader(ScriptFilePath.InfoStealHell);
                        elementMessage.color = elementColor;
                        break;
                }
                break;
        }
    }

}
