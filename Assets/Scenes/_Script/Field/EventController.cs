using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventController : MonoBehaviour
{
    private static int currentX, currentY, currentLevel;
    private static FieldEventType[,] fieldEventFlag;
    private static Transform[,] fieldEventContainer;
    public GameObject playerCharacter;
    public GameObject boxPanel;
    public GameObject trapPanel;
    public GameObject requestNextFloorPanel;
    public GameObject controlBlocker;
    private bool exitFlag;


    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        Utility.ObjectVisibility(controlBlocker, false);
        Utility.ObjectVisibility(boxPanel, false);
        Utility.ObjectVisibility(trapPanel, false);
        Utility.ObjectVisibility(requestNextFloorPanel, false);
        currentLevel = PlayerPrefs.GetInt(PrefsEntity.CurrentLevel, 0); // 0 = Normal, 1 = Hard, 2 = Hell
        exitFlag = true;
        fieldEventFlag = MapGenerator.GetEventFlag();
        fieldEventContainer = MapGenerator.GetEventContainer();
    }

    // Update is called once per frame
    void Update()
    {
        currentX = PlayerPrefs.GetInt(PrefsEntity.CurrentX, 0);
        currentY = PlayerPrefs.GetInt(PrefsEntity.CurrentY, 0);
        // 0 = No event, 1 = Entrance, 2 = Exit, 3 = Enemy, 4 = Trap, 5 = Jail, 6 = Box
        switch (fieldEventFlag[currentX, currentY])
        {
            case FieldEventType.Nothing: //No Event
                exitFlag = true;
                break;
            case FieldEventType.Entrance: //Entrance
                exitFlag = true;
                break;
            case FieldEventType.Exit: //Exit
                if (exitFlag &
                    CoordinationEquals(playerCharacter.transform.position, fieldEventContainer[currentX, currentY].position))
                {
                    exitFlag = false;
                        OccurExit(ref fieldEventFlag[currentX, currentY], ref fieldEventContainer[currentX, currentY]);
                }
                break;
            case FieldEventType.Enemy: //Enemy
                exitFlag = true;
                if (CoordinationEquals(playerCharacter.transform.position, fieldEventContainer[currentX, currentY].position))
                    OccurEnemy(ref fieldEventFlag[currentX, currentY], ref fieldEventContainer[currentX, currentY]);
                break;
            case FieldEventType.Trap: //Trap
                exitFlag = true;
                if (CoordinationEquals(playerCharacter.transform.position, fieldEventContainer[currentX, currentY].position))
                    OccurTrap(ref fieldEventFlag[currentX, currentY], ref fieldEventContainer[currentX, currentY]);
                break;
            case FieldEventType.Jail: //Jail
                exitFlag = true;
                if (CoordinationEquals(playerCharacter.transform.position, fieldEventContainer[currentX, currentY].position))
                    OccurJail(ref fieldEventFlag[currentX, currentY], ref fieldEventContainer[currentX, currentY]);
                break;
            case FieldEventType.Box: //Box
                exitFlag = true;
                if (CoordinationEquals(playerCharacter.transform.position, fieldEventContainer[currentX, currentY].position))
                    OccurBox(ref fieldEventFlag[currentX, currentY], ref fieldEventContainer[currentX, currentY]);
                break;
        }
    }


    private void OccurExit(ref FieldEventType eventFlag, ref Transform eventObject)
    {
        //Debug.Log("Exit");
        Utility.ObjectVisibility(controlBlocker, true);
        Utility.ObjectVisibility(requestNextFloorPanel, true);
        //eventObject.gameObject.SetActive(false);
        //PlayerPrefs.SetInt("Current Floor", --currentFloor);
        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name);
        //DestroyImmediate(eventObject.gameObject);
        //eventFlag = 0;
    }

    // 0 = No event, 1 = Entrance, 2 = Exit, 3 = Enemy, 4 = Trap, 5 = Event, 6 = Box
    private void OccurEnemy(ref FieldEventType eventFlag, ref Transform eventObject)
    {
        Debug.Log("Enemy");
        Vector2 eventCoordination = new Vector2(currentX, currentY);
        PlayerPrefsX.SetVector2(PlayerPrefs.GetString(eventCoordination.ToString()), new Vector2(-1, -1));
        //Debug.Log(PlayerPrefs.GetString(eventCoordination.ToString()));
        //Debug.Log(fieldEventContainer[currentX, currentY].name);
        DestroyImmediate(eventObject.gameObject);
        eventFlag = FieldEventType.Nothing;
        //Debug.Log("St X : " + PlayerPrefs.GetInt(PrefsEntity.StartX) + " St Y : " + PlayerPrefs.GetInt(PrefsEntity.StartY));
        LoadingManager.LoadSceneFieldToBattle();
    }

    private void OccurTrap(ref FieldEventType eventFlag, ref Transform eventObject)
    {
        Debug.Log("Trap");
        Vector2 eventCoordination = new Vector2(currentX, currentY);
        PlayerPrefsX.SetVector2(PlayerPrefs.GetString(eventCoordination.ToString()), new Vector2(-1, -1));
        DestroyImmediate(eventObject.gameObject);
        switch (currentLevel)
        {
            case 0:
                TrapNormal();
                break;
            case 1:
                TrapHard();
                break;
            case 2:
                TrapHell();
                break;
        }
        Utility.ObjectVisibility(controlBlocker, true);
        Utility.ObjectVisibility(trapPanel, true);
        eventFlag = FieldEventType.Nothing;
    }

    private void TrapNormal()
    {
        int eventFlag = Random.Range(1, 100);
        if(eventFlag >= 1 & eventFlag <=10) // 10%, HP Down
        {

        }else if(eventFlag >= 11 & eventFlag <= 20) // 10%, Enemy Stat UP
        {

        }
        else if (eventFlag >= 21 & eventFlag <= 30) // 10%, Friend Stat Down
        {

        }
        else if (eventFlag >= 31 & eventFlag <= 50) // 20%, Field Enemy Spawn
        {

        }
        else if (eventFlag >= 51 & eventFlag <= 55) // 5%, Field Boss Spawn
        {

        }
        else if (eventFlag >= 56 & eventFlag <= 75) // 20%, Nothing Occur
        {

        }
        else if (eventFlag >= 76 & eventFlag <= 85) // 10%, MP Down
        {

        }
        else if (eventFlag >= 86 & eventFlag <= 100) // 15%, Battle Occur
        {

        }
    }

    private void TrapHard()
    {
        int eventFlag = Random.Range(1, 100);
        if (eventFlag >= 1 & eventFlag <= 10) // 10%, HP Down
        {

        }
        else if (eventFlag >= 11 & eventFlag <= 20) // 10%, Enemy Stat UP
        {

        }
        else if (eventFlag >= 21 & eventFlag <= 30) // 10%, Friend Stat Down
        {

        }
        else if (eventFlag >= 31 & eventFlag <= 50) // 20%, Field Enemy Spawn
        {

        }
        else if (eventFlag >= 51 & eventFlag <= 60) // 10%, Field Boss Spawn
        {

        }
        else if (eventFlag >= 61 & eventFlag <= 75) // 15%, Nothing Occur
        {

        }
        else if (eventFlag >= 76 & eventFlag <= 85) // 10%, MP Down
        {

        }
        else if (eventFlag >= 86 & eventFlag <= 100) // 15%, Battle Occur
        {

        }

    }

    private void TrapHell()
    {
        int eventFlag = Random.Range(1, 100);
        if (eventFlag >= 1 & eventFlag <= 10) // 10%, HP Down
        {

        }
        else if (eventFlag >= 11 & eventFlag <= 20) // 10%, Enemy Stat UP
        {

        }
        else if (eventFlag >= 21 & eventFlag <= 30) // 10%, Friend Stat Down
        {

        }
        else if (eventFlag >= 31 & eventFlag <= 50) // 20%, Field Enemy Spawn
        {

        }
        else if (eventFlag >= 51 & eventFlag <= 65) // 15%, Field Boss Spawn
        {

        }
        else if (eventFlag >= 66 & eventFlag <= 75) // 10%, Nothing Occur
        {

        }
        else if (eventFlag >= 76 & eventFlag <= 85) // 10%, MP Down
        {

        }
        else if (eventFlag >= 86 & eventFlag <= 100) // 15%, Battle Occur
        {

        }

    }

    private void OccurJail(ref FieldEventType eventFlag, ref Transform eventObject)
    {
        Debug.Log("Jail");
        Vector2 eventCoordination = new Vector2(currentX, currentY);
        PlayerPrefsX.SetVector2(PlayerPrefs.GetString(eventCoordination.ToString()), new Vector2(-1, -1));
        DestroyImmediate(eventObject.gameObject);
        switch (currentLevel)
        {
            case 0:
                EventNormal();
                break;
            case 1:
                EventHard();
                break;
            case 2:
                EventHell();
                break;
        }
        eventFlag = FieldEventType.Nothing;
    }

    private void EventNormal()
    {
        int eventFlag = Random.Range(1, 100);
        if (eventFlag >= 1 & eventFlag <= 50) // 50%, Positive Event
        {
            eventFlag = Random.Range(1, 100);
            if (eventFlag >= 1 & eventFlag <= 20) // 20%, HP Up
            {

            }
            else if (eventFlag >= 21 & eventFlag <= 40) // 20%, Floor map open
            {

            }
            else if (eventFlag >= 41 & eventFlag <= 50) // 10%, Equipment get
            {

            }
            else if (eventFlag >= 51 & eventFlag <= 60) // 10%, Scroll get
            {

            }
            else if (eventFlag >= 61 & eventFlag <= 100) // 40%, Friend get
            {

            }
        }
        else if (eventFlag >= 51 & eventFlag <= 80) // 30%, Negative Event
        {
            TrapNormal();
        }
        else if (eventFlag >= 81 & eventFlag <= 100) // 20%, Trade or Treasure
        {
            eventFlag = Random.Range(1, 100);
            if (eventFlag >= 1 & eventFlag <= 25) // 25%, Equipment get
            {

            }
            else if (eventFlag >= 26 & eventFlag <= 50) // 25%, Scroll get
            {

            }
            else if (eventFlag >= 51 & eventFlag <= 75) // 25%, Equipment level up
            {

            }
            else if (eventFlag >= 76 & eventFlag <= 100) // 25%, Skill level up
            {

            }
        }
    }
    private void EventHard()
    {
        int eventFlag = Random.Range(1, 100);
        if (eventFlag >= 1 & eventFlag <= 35) // 35%, Positive Event
        {
            eventFlag = Random.Range(1, 100);
            if (eventFlag >= 1 & eventFlag <= 20) // 20%, HP Up
            {

            }
            else if (eventFlag >= 21 & eventFlag <= 40) // 20%, Floor map open
            {

            }
            else if (eventFlag >= 41 & eventFlag <= 50) // 10%, Equipment get
            {

            }
            else if (eventFlag >= 51 & eventFlag <= 60) // 10%, Scroll get
            {

            }
            else if (eventFlag >= 61 & eventFlag <= 100) // 40%, Friend get
            {

            }
        }
        else if (eventFlag >= 36 & eventFlag <= 85) // 50%, Negative Event
        {
            TrapHard();
        }
        else if (eventFlag >= 86 & eventFlag <= 100) // 15%, Trade or Treasure
        {
            eventFlag = Random.Range(1, 100);
            if (eventFlag >= 1 & eventFlag <= 25) // 25%, Equipment get
            {

            }
            else if (eventFlag >= 26 & eventFlag <= 50) // 25%, Scroll get
            {

            }
            else if (eventFlag >= 51 & eventFlag <= 75) // 25%, Equipment level up
            {

            }
            else if (eventFlag >= 76 & eventFlag <= 100) // 25%, Skill level up
            {

            }
        }
    }
    private void EventHell()
    {
        int eventFlag = Random.Range(1, 100);
        if (eventFlag >= 1 & eventFlag <= 30) // 30%, Positive Event
        {
            eventFlag = Random.Range(1, 100);
            if (eventFlag >= 1 & eventFlag <= 20) // 20%, HP Up
            {

            }
            else if (eventFlag >= 21 & eventFlag <= 40) // 20%, Floor map open
            {

            }
            else if (eventFlag >= 41 & eventFlag <= 50) // 10%, Equipment get
            {

            }
            else if (eventFlag >= 51 & eventFlag <= 60) // 10%, Scroll get
            {

            }
            else if (eventFlag >= 61 & eventFlag <= 100) // 40%, Friend get
            {

            }
        }
        else if (eventFlag >= 31 & eventFlag <= 91) // 60%, Negative Event
        {
            TrapHell();
        }
        else if (eventFlag >= 91 & eventFlag <= 100) // 10%, Trade or Treasure
        {
            eventFlag = Random.Range(1, 100);
            if (eventFlag >= 1 & eventFlag <= 25) // 25%, Equipment get
            {

            }
            else if (eventFlag >= 26 & eventFlag <= 50) // 25%, Scroll get
            {

            }
            else if (eventFlag >= 51 & eventFlag <= 75) // 25%, Equipment level up
            {

            }
            else if (eventFlag >= 76 & eventFlag <= 100) // 25%, Skill level up
            {

            }
        }
    }

    private void OccurBox(ref FieldEventType eventFlag, ref Transform eventObject)
    {
        Debug.Log("Box");
        Vector2 eventCoordination = new Vector2(currentX, currentY);
        PlayerPrefsX.SetVector2(PlayerPrefs.GetString(eventCoordination.ToString()), new Vector2(-1, -1));
        DestroyImmediate(eventObject.gameObject);
        Utility.ObjectVisibility(controlBlocker, true);
        Utility.ObjectVisibility(boxPanel, true);
        eventFlag = FieldEventType.Nothing;
    }

    private void BoxEvent()
    {
        int eventFlag = Random.Range(1, 100);
        if (eventFlag >= 1 & eventFlag <= 80) // 80%, Positive box
        {
            eventFlag = Random.Range(1, 100);
            if (eventFlag >= 1 & eventFlag <= 25) // 25%, Equipment get
            {

            }
            else if (eventFlag >= 26 & eventFlag <= 50) // 25%, Stat scroll get
            {

            }
            else if (eventFlag >= 51 & eventFlag <= 75) // 25%, Skill scroll get
            {

            }
            else if (eventFlag >= 76 & eventFlag <= 100) // 25%, Equipment scroll get
            {

            }
        }
        else if (eventFlag >= 81 & eventFlag <= 90) // 10%, Nothing happen
        {

        }
        else if (eventFlag >= 91 & eventFlag <= 100) // 10%, Negative box
        {
            eventFlag = Random.Range(1, 100);
            if (eventFlag >= 1 & eventFlag <= 25) // 25%, Mimic occur
            {

            }
            else if (eventFlag >= 26 & eventFlag <= 50) // 25%, Field monster spawn
            {

            }
            else if (eventFlag >= 51 & eventFlag <= 75) // 25%, HP down (Trap)
            {

            }
            else if (eventFlag >= 76 & eventFlag <= 100) // 25%, Nothing get
            {

            }
        }
    }

    private bool CoordinationEquals(Vector3 a, Vector3 b)
    {
        if (Mathf.Ceil(a.x).Equals(Mathf.Ceil(b.x)) &
            Mathf.Ceil(a.z).Equals(Mathf.Ceil(b.z)))
            return true;

        return false;
    }
}
