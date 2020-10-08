using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorInformation : MonoBehaviour
{
    public GameObject informationPanel;
    public GameObject controlblocker;


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

    }

}
