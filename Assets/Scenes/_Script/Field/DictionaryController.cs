using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryController : MonoBehaviour
{
    public GameObject dictionaryPanel;
    public GameObject controlBlocker;
    // Start is called before the first frame update
    void Start()
    {
        Utility.ObjectVisibility(dictionaryPanel, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickDictionaryOpen()
    {
        Utility.ObjectVisibility(controlBlocker, true);
        Utility.ObjectVisibility(dictionaryPanel, true);
    }
    public void OnClickDictionaryClose()
    {
        Utility.ObjectVisibility(controlBlocker, false);
        Utility.ObjectVisibility(dictionaryPanel, false);
    }
}
