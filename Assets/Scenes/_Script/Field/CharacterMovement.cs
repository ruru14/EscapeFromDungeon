using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{

    public GameObject fieldCharacter;
    public Camera mainCam;
    public Camera viewCam;
    public Button btnUp;
    public Button btnDown;
    public Button btnLeft;
    public Button btnRight;
    private GameObject[] fieldTileArr;
    private Vector2 mapSize;
    private static int currentX;
    private static int currentY;
    private static int startX;
    private static int startY;
    private List<int>[] adjList;
    private List<Transform>[] fieldBridgeContainer;
    private bool[, ] fieldBridgeVisibility;
    private GameObject[,] fieldTileContainer;
    private static Transform[,] fieldEventContainer;
    private bool[] fieldEventVisibility;
    private float viewCamYIndicator, viewCamZIndicator;
    private Vector3 characterTemp; // 이름미정, 위치조절벡터
    private Animator fieldCharacterMotion;

    private Renderer flickUp;
    private Renderer flickDown;
    private Renderer flickLeft;
    private Renderer flickRight;
    private Color originColor;
    private float flicker;

    private void Awake()
    {
        characterTemp = new Vector3(0, 0.1f, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        mapSize = MapGenerator.GetMapsize();
        currentX = PlayerPrefs.GetInt(PrefsEntity.CurrentX);
        currentY = PlayerPrefs.GetInt(PrefsEntity.CurrentY);
        startX = PlayerPrefs.GetInt(PrefsEntity.StartX);
        startY = PlayerPrefs.GetInt(PrefsEntity.StartY);

        viewCamYIndicator = 2f;
        viewCamZIndicator = -3.5f;
        SetViewCamYIndicator();
        SetViewCamZIndicator();

        fieldTileArr = GameObject.FindGameObjectsWithTag("FieldTile");
        adjList = MapGenerator.GetAdjList();
        fieldBridgeContainer = MapGenerator.GetBridgeContainer();
        fieldBridgeVisibility = MapGenerator.GetBridgeVisibility();
        fieldTileContainer = MapGenerator.GetTileContainer();
        fieldEventContainer = MapGenerator.GetEventContainer();
        fieldEventVisibility = PlayerPrefsX.GetBoolArray(PrefsEntity.FieldEventVisibility);
        if(fieldEventContainer.Length != fieldEventVisibility.Length)
        {
            fieldEventVisibility = new bool[fieldEventContainer.Length];
        }

        fieldCharacter.transform.position = fieldTileArr[startX * (int)mapSize.y + startY].transform.position + characterTemp;
        viewCam.transform.position = Vector3.right * fieldTileArr[startX * (int)mapSize.y + startY].transform.position.x
            + Vector3.up * viewCamYIndicator
            + Vector3.forward * fieldTileArr[startX * (int)mapSize.y + startY].transform.position.z + Vector3.forward * viewCamZIndicator;
        SetEventInvisible();
        SetEventVisible(currentX + 1, currentY);
        SetEventVisible(currentX - 1, currentY);
        SetEventVisible(currentX, currentY + 1);
        SetEventVisible(currentX, currentY - 1);
        SetBridgeVisibile();
        btnUp.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        btnDown.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        btnLeft.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        btnRight.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;

        originColor = fieldTileContainer[0, 0].GetComponent<Renderer>().material.color;

        //Debug.Log("Y : " + viewCamYIndicator + ", Z : " + viewCamZIndicator);
        //Debug.Log("X : " + currentX + ", Y : " + currentY);

        //Vector2 t = new Vector2(2, 2);
        //Debug.Log(PlayerPrefs.GetString(t.ToString()));

        fieldCharacterMotion = fieldCharacter.GetComponent<Animator>();
    }

    private void SetEventInvisible()
    {
        for(int i=0; i<(int)mapSize.x; i++)
        {
            for(int j=0; j<(int)mapSize.y; j++)
            {
                if(fieldEventContainer[i, j] != null)
                {
                    fieldEventContainer[i, j].gameObject.SetActive(
                        fieldEventVisibility[i * (int)mapSize.y + j]);
                }
            }
        }
    }

    private void SetEventVisible(int x, int y)
    {
        try
        {
            //fieldEventVisibility = PlayerPrefsX.GetBoolArray(PrefsEntity.FieldEventVisibility);
            fieldEventContainer[x, y].gameObject.SetActive(true);
            fieldEventVisibility[x * (int)mapSize.y + y] = true;
            PlayerPrefsX.SetBoolArray(PrefsEntity.FieldEventVisibility, fieldEventVisibility);
        }
        catch (System.NullReferenceException)
        {
            return;
        }
        catch (System.IndexOutOfRangeException)
        {
            return;
        }
        catch (MissingReferenceException)
        {
            return;
        }
    }

    private void Test()
    {
        foreach(Transform t in fieldEventContainer)
        {
            Debug.Log(t.position);
        }
    }

    private void SetViewCamYIndicator()
    {
        if ((int)mapSize.y == 4)
            viewCamYIndicator = 2.9f;
        else if ((int)mapSize.y == 5)
            viewCamYIndicator = 2.2f;
        else if ((int)mapSize.y == 6)
            viewCamYIndicator = 1.9f;
    }

    private void SetViewCamZIndicator()
    {
        if ((int)mapSize.y == 4)
            viewCamZIndicator = -2.5f;
        else if ((int)mapSize.y == 5)
            viewCamZIndicator = -1.92f;
        else if ((int)mapSize.y == 6)
            viewCamZIndicator = -1.6f;
    }

    private void SetBridgeVisibile()
    {
        foreach (Transform obj in fieldBridgeContainer[currentX * (int)mapSize.y + currentY])
        {
            obj.gameObject.SetActive(true);
        }
        try
        {
            //fieldBridgeVisibility[from, to]
            fieldBridgeVisibility[currentX * (int)mapSize.y + currentY, currentX * (int)mapSize.y + currentY + 1] = true; //Up
            fieldBridgeVisibility[currentX * (int)mapSize.y + currentY + 1, currentX * (int)mapSize.y + currentY] = true; //Up
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
        try
        {
            fieldBridgeVisibility[currentX * (int)mapSize.y + currentY, currentX * (int)mapSize.y + currentY - 1] = true; //Down
            fieldBridgeVisibility[currentX * (int)mapSize.y + currentY - 1, currentX * (int)mapSize.y + currentY] = true; //Down
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
        try
        {
            fieldBridgeVisibility[currentX * (int)mapSize.y + currentY, (currentX - 1) * (int)mapSize.y + currentY] = true; //Left
            fieldBridgeVisibility[(currentX - 1) * (int)mapSize.y + currentY, currentX * (int)mapSize.y + currentY] = true; //Left
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
        try
        {
            fieldBridgeVisibility[currentX * (int)mapSize.y + currentY, (currentX + 1) * (int)mapSize.y + currentY] = true; //Right
            fieldBridgeVisibility[(currentX + 1) * (int)mapSize.y + currentY, currentX * (int)mapSize.y + currentY] = true; //Right
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
        //Debug.Log("Up : " + fieldBridgeVisibility[currentX * (int)mapSize.y + currentY, currentX * (int)mapSize.y + currentY + 1]);
        //Debug.Log("Down : " + fieldBridgeVisibility[currentX * (int)mapSize.y + currentY, currentX * (int)mapSize.y + currentY - 1]);
        //Debug.Log("Left : " + fieldBridgeVisibility[currentX * (int)mapSize.y + currentY, (currentX + 1) * (int)mapSize.y + currentY]);
        //Debug.Log("Right : " + fieldBridgeVisibility[currentX * (int)mapSize.y + currentY, (currentX - 1) * (int)mapSize.y + currentY]);
        DataManager.BinarySerialize<bool[,]>(fieldBridgeVisibility, DataFilePath.FieldBridgeVisibility);
    }

    // Update is called once per frame
    void Update()
    {
        fieldCharacter.transform.position = Vector3.MoveTowards(fieldCharacter.transform.position,
            fieldTileArr[currentX * (int)mapSize.y + currentY].transform.position + characterTemp, 0.05f);
        //Debug.Log(fieldCharacter.transform.position + ", " + (fieldTileArr[currentX * (int)mapSize.x + currentY].transform.position + characterTemp));
        viewCam.transform.position = Vector3.MoveTowards(viewCam.transform.position,
            Vector3.right * fieldTileArr[currentX * (int)mapSize.y + currentY].transform.position.x
            + Vector3.up * viewCamYIndicator
            + Vector3.forward * fieldTileArr[currentX * (int)mapSize.y + currentY].transform.position.z + Vector3.forward * viewCamZIndicator, 
            0.05f);
        ButtonInteractionMove(fieldCharacter.transform.position, fieldTileArr[currentX * (int)mapSize.y + currentY].transform.position + characterTemp);
        if(fieldCharacter.transform.position == fieldTileArr[currentX * (int)mapSize.y + currentY].transform.position + characterTemp)
        {
            fieldCharacterMotion.SetBool("move", false);
            fieldCharacter.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        flicker = Mathf.Abs(Mathf.Sin(Time.time * 4)) + 0.5f;
        if (btnUp.interactable)
        {
            flickUp.material.color = originColor * flicker;
        }
        if (btnDown.interactable)
        {
            flickDown.material.color = originColor * flicker;
        }
        if (btnLeft.interactable)
        {
            flickLeft.material.color = originColor * flicker;
        }
        if (btnRight.interactable)
        {
            flickRight.material.color = originColor * flicker;
        }
    }

    public void PrintPosition()
    {
        Debug.Log("X : " + currentX + ", Y : " + currentY);
    }

    public void ColorInitialization()
    {
        foreach(GameObject i in fieldTileContainer)
        {
            i.GetComponent<Renderer>().material.color = originColor;
        }
    }

    private void ButtonInteractionMove(Vector3 curPosition, Vector3 directPosition)
    {
        if (curPosition != directPosition)
        {
            btnUp.interactable = false;
            btnDown.interactable = false;
            btnLeft.interactable = false;
            btnRight.interactable = false;
        }
        else
        {
            //btnUp.interactable = true;
            //btnDown.interactable = true;
            //btnLeft.interactable = true;
            //btnRight.interactable = true;
            ButtonInteractionStop();
        }

    }

    private void ButtonInteractionStop()
    {
        // Can go to up check
        //(currentX * (int)mapSize.y + currentY) % (int)mapSize.y == (int)mapSize.y - 1
        if (currentY == (int)mapSize.y - 1 |
            !adjList[currentX * (int)mapSize.y + currentY].Contains(currentX * (int)mapSize.y + currentY + 1))
        {
            btnUp.interactable = false;
        }
        else
        {
            btnUp.interactable = true;
            flickUp = fieldTileContainer[currentX, currentY + 1].GetComponent<Renderer>();
        }

        // Can go to down check
        //(currentX * (int)mapSize.y + currentY) % (int)mapSize.y == 0
        if (currentY == 0 |
            !adjList[currentX * (int)mapSize.y + currentY].Contains(currentX * (int)mapSize.y + currentY - 1))
        {
            btnDown.interactable = false;
        }
        else
        {
            btnDown.interactable = true;
            flickDown = fieldTileContainer[currentX, currentY - 1].GetComponent<Renderer>();
        }

        // Can go to left check
        //(currentX * (int)mapSize.y + currentY) / (int)mapSize.y == 0
        if (currentX == 0 |
            !adjList[currentX * (int)mapSize.y + currentY].Contains((currentX - 1) * (int)mapSize.y + currentY))
        {
            btnLeft.interactable = false;
        }
        else
        {
            btnLeft.interactable = true;
            flickLeft = fieldTileContainer[currentX -1, currentY].GetComponent<Renderer>();
        }

        // Can go to right check
        //(currentX * (int)mapSize.y + currentY) / (int)mapSize.y == (int)mapSize.y - 1
        if (currentX == (int)mapSize.x - 1 |
            !adjList[currentX * (int)mapSize.y + currentY].Contains((currentX + 1) * (int)mapSize.y + currentY))
        {
            btnRight.interactable = false;
        }
        else
        {
            btnRight.interactable = true;
            flickRight = fieldTileContainer[currentX + 1, currentY].GetComponent<Renderer>();
        }
    }
    
    /* Move method's if conditions
     * 1st : Move direction is end of map?
     * 2nd : Move direction has bridge?
     */
    public void MoveUp()
    {
        //(currentX * (int)mapSize.y + currentY) % (int)mapSize.y == (int)mapSize.y - 1
        if (currentY == (int)mapSize.y - 1 |
            !adjList[currentX * (int)mapSize.y + currentY].Contains(currentX * (int)mapSize.y + currentY + 1))
        {
            Debug.Log("Can't Up");
            return;
        }
        //fieldCharacter.transform.position = fieldTileArr[currentX * (int)mapSize.x + ++currentY].transform.position;
        PlayerPrefs.SetInt(PrefsEntity.CurrentY, ++currentY);
        PlayerPrefs.SetInt(PrefsEntity.StartY, PlayerPrefs.GetInt(PrefsEntity.CurrentY));
        PrintPosition();
        ColorInitialization();
        SetEventVisible(currentX + 1, currentY);
        SetEventVisible(currentX - 1, currentY);
        SetEventVisible(currentX, currentY + 1);
        SetEventVisible(currentX, currentY - 1);
        SetBridgeVisibile();
        fieldCharacter.transform.rotation = Quaternion.Euler(0, 0, 0);
        fieldCharacterMotion.SetBool("move", true);
    }

    public void MoveDown()
    {
        //(currentX * (int)mapSize.y + currentY) % (int)mapSize.y == 0
        if (currentY == 0 |
            !adjList[currentX * (int)mapSize.y + currentY].Contains(currentX * (int)mapSize.y + currentY - 1))
        {
            Debug.Log("Can't Down");
            return;
        }
        //fieldCharacter.transform.position = fieldTileArr[currentX * (int)mapSize.x + --currentY].transform.position;
        PlayerPrefs.SetInt(PrefsEntity.CurrentY, --currentY);
        PlayerPrefs.SetInt(PrefsEntity.StartY, PlayerPrefs.GetInt(PrefsEntity.CurrentY));
        PrintPosition();
        ColorInitialization();
        SetEventVisible(currentX + 1, currentY);
        SetEventVisible(currentX - 1, currentY);
        SetEventVisible(currentX, currentY + 1);
        SetEventVisible(currentX, currentY - 1);
        SetBridgeVisibile();
        fieldCharacter.transform.rotation = Quaternion.Euler(0, 180, 0);
        fieldCharacterMotion.SetBool("move", true);
    }

    public void MoveLeft()
    {
        //(currentX * (int)mapSize.y + currentY) / (int)mapSize.y == 0
        if (currentX == 0 |
            !adjList[currentX * (int)mapSize.y + currentY].Contains((currentX - 1) * (int)mapSize.y + currentY))
        {
            Debug.Log("Can't Left");
            return;
        }
        //fieldCharacter.transform.position = fieldTileArr[--currentX * (int)mapSize.x + currentY].transform.position;
        PlayerPrefs.SetInt(PrefsEntity.CurrentX, --currentX);
        PlayerPrefs.SetInt(PrefsEntity.StartX, PlayerPrefs.GetInt(PrefsEntity.CurrentX));
        PrintPosition();
        ColorInitialization();
        SetEventVisible(currentX + 1, currentY);
        SetEventVisible(currentX - 1, currentY);
        SetEventVisible(currentX, currentY + 1);
        SetEventVisible(currentX, currentY - 1);
        SetBridgeVisibile();
        fieldCharacter.transform.rotation = Quaternion.Euler(0, -90, 0);
        fieldCharacterMotion.SetBool("move", true);
    }

    public void MoveRight()
    {
        //(currentX * (int)mapSize.y + currentY) / (int)mapSize.y == (int)mapSize.y - 1
        if (currentX == (int)mapSize.x - 1 |
            !adjList[currentX * (int)mapSize.y + currentY].Contains((currentX + 1) * (int)mapSize.y + currentY))
        {
            Debug.Log("Can't Right");
            return;
        }
        //fieldCharacter.transform.position = fieldTileArr[++currentX * (int)mapSize.x + currentY].transform.position;
        PlayerPrefs.SetInt(PrefsEntity.CurrentX, ++currentX);
        PlayerPrefs.SetInt(PrefsEntity.StartX, PlayerPrefs.GetInt(PrefsEntity.CurrentX));
        PrintPosition();
        ColorInitialization();
        SetEventVisible(currentX + 1, currentY);
        SetEventVisible(currentX - 1, currentY);
        SetEventVisible(currentX, currentY + 1);
        SetEventVisible(currentX, currentY - 1);
        SetBridgeVisibile();
        fieldCharacter.transform.rotation = Quaternion.Euler(0, 90, 0);
        fieldCharacterMotion.SetBool("move",true);
    }
}
