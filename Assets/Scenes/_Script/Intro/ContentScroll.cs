using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ContentScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Text startMessage;
    public Text introStory;
    private float scrollSpeed = 4f; // default 2
    //private float watingTime = 5f;
    private float maxScroll;
    private RectTransform contenRectTransform;
    private bool routineCalled;
    private bool contentReaderFlag;

    // Start is called before the first frame update
    void Start()
    {
        contentReaderFlag = true;
        introStory.text = Utility.ScriptReader(ScriptFilePath.IntroStory);
        routineCalled = true;
        Utility.ObjectVisibility(startMessage.gameObject, false);
        contenRectTransform = scrollRect.content;
        //Debug.Log(introStory);
        //Debug.Log(contenRectTransform.rect.yMax);
        maxScroll = 3900f;
        //Debug.Log(contenRectTransform.position.y);
        //Debug.Log(maxScroll);

    }

    // Update is called once per frame
    void Update()
    {
        bool hasScrolled = contenRectTransform.position.y < maxScroll;
        if (hasScrolled & contentReaderFlag)
        {
            Move();
        }
        if (!hasScrolled & routineCalled)
        {
            contentReaderFlag = false;
            routineCalled = false;
            StartCoroutine(ShowStart());
        }

    }

    private void Move()
    {
        Vector3 contentPosition = contenRectTransform.position;
        float newPositionY = contentPosition.y + scrollSpeed;
        Vector3 newPosition = new Vector3(contentPosition.x, newPositionY, contentPosition.z);
        contenRectTransform.position = newPosition;
        //Debug.Log(contenRectTransform.position.y);
    }

    IEnumerator ShowStart()
    {
        while (true)
        {
            Utility.ObjectVisibility(startMessage.gameObject, true);
            yield return new WaitForSeconds(0.5f);
            Utility.ObjectVisibility(startMessage.gameObject, false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ClickedStartMessage()
    {
        if (!routineCalled)
        {
            LoadingManager.LoadSceneFirstStart();
        }
    }
}
