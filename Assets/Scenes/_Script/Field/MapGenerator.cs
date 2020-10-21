using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FieldEventType : int
{
    Nothing = 0,
    Entrance = 1,
    Exit = 2,
    Enemy = 3,
    Trap = 4,
    Jail = 5,
    Box = 6
}
public class MapGenerator : MonoBehaviour
{

    public GameObject fieldTilePrefab;
    public Transform fieldBridgePrefab;
    public Transform fieldEntrance;
    public Transform fieldExit;
    public Transform fieldEnemy;
    public Transform fieldJail;
    public Transform fieldTrap;
    public Transform fieldBox;
    public Text txtCurrentFloor;
    public Image backgroundImage;
    public Image floorElementImage;
    public GameObject temp;
    private static Vector2 mapSize;
    private static List<int>[] adjList;
    private static List<Transform>[] fieldBridgeContainer;
    private static bool[,] fieldBridgeVisibility;
    private static GameObject[,] fieldTileContainer;
    private static Transform[,] fieldEventContainer;
    private static FieldEventType[,] fieldEventFlag; // 0 = No event, 1 = Entrance, 2 = Exit, 3 = Enemy, 4 = Trap, 5 = Event, 6 = Box
    private int currentFloor;
    private int loopCount = 0;

    private GameManager gameManager;

    [Range(0, 1)]
    public float outlinePercent;

    private void Awake()
    {
        //PlayerPrefs.DeleteKey("Current Floor");
        mapSize = new Vector2(6, 6);
        currentFloor = PlayerPrefs.GetInt(PrefsEntity.CurrentFloor, 100);
        if(currentFloor == 100)
        {
            PlayerPrefs.SetInt(PrefsEntity.CurrentFloor, 100);
        }
        //currentFloor = 45;
        if (currentFloor <= 100 & currentFloor >= 81)
            mapSize = new Vector2(4, 4);
        else if(currentFloor <= 80 & currentFloor >= 61)
            mapSize = new Vector2(5, 4);
        else if(currentFloor <= 60 & currentFloor >= 41)
            mapSize = new Vector2(5, 5);
        else if(currentFloor <= 40 & currentFloor >= 21)
            mapSize = new Vector2(6, 5);
        else if(currentFloor <= 20 & currentFloor >= 1)
            mapSize = new Vector2(6, 6);
        txtCurrentFloor.text = currentFloor.ToString() + "F";
        fieldEventFlag = new FieldEventType[(int)mapSize.x, (int)mapSize.y];
        fieldEventContainer = new Transform[(int)mapSize.x, (int)mapSize.y];
        fieldBridgeContainer = new List<Transform>[(int)mapSize.x * (int)mapSize.y];
        for (int i = 0; i < fieldBridgeContainer.Length; i++)
            fieldBridgeContainer[i] = new List<Transform>();
        fieldBridgeVisibility = DataManager.BinaryDeserialize<bool[,]>(DataFilePath.FieldBridgeVisibility);
        if(fieldBridgeVisibility == default)
        {
            fieldBridgeVisibility = new bool[(int)mapSize.x * (int)mapSize.y, (int)mapSize.x * (int)mapSize.y];
        }
        fieldTileContainer = new GameObject[(int)mapSize.x, (int)mapSize.y];
        adjList = new List<int>[(int)mapSize.x * (int)mapSize.y];
        for (int i = 0; i < adjList.Length; i++)
            adjList[i] = new List<int>();

        ChangeFloorElement();
        GenerateMap();
        while (!BridgeBFS(adjList))
        {
            loopCount++;
            GenerateBridge();
        }
        Debug.Log("Map Generated. Loop Count : " + loopCount);

        GenerateEntrance();
        GenerateExit();
        GenerateEvent(fieldEnemy, FieldEventType.Enemy, EventCount(3)); //3
        GenerateEvent(fieldTrap, FieldEventType.Trap, EventCount(4)); //4
        GenerateEvent(fieldJail, FieldEventType.Jail, EventCount(5)); //5
        GenerateEvent(fieldBox, FieldEventType.Box, EventCount(6)); //6
    }

    void Start()
    {
        //PrintAdjList(adjList);
        gameManager = GameManager.GetInstatnce();
        PlayerPrefsX.SetBool(PrefsEntity.SaveFlag, true);

    }

    public void GenerateMap()
    {
        string holderName = "Generated Map";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        //for (int x = 0; x < mapSize.x; x++)
        //{
        //    for (int y = 0; y < mapSize.y; y++)
        //    {
        //        Vector3 tilePosition = new Vector3(
        //            -2.5f + x * (5 / (mapSize.x - 1)) + (-x + mapSize.x / 2) * -(mapSize.x - 6) / 15,
        //            -0.0f,
        //            -2.5f + y * (5 / (mapSize.y - 1)) + (-y + mapSize.y / 2) * -(mapSize.y - 6) / 15
        //            );
        //        GameObject newTile = Instantiate(fieldTilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90));
        //        newTile.transform.localScale = new Vector3(
        //            (5 / (mapSize.x - 1)) * (1 - outlinePercent),
        //            (5 / (mapSize.y - 1)) * (1 - outlinePercent),
        //            0.15f);
        //    }
        //}

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                //Vector3 tilePosition = new Vector3(
                //    -2.5f + x * (5 / (mapSize.x - 1)) + (-x + mapSize.x / 2) * -(mapSize.x - 6) / 15,
                //    -0.0f,
                //    -2.5f + y * (5 / (mapSize.y - 1)) + (-y + mapSize.y / 2) * -(mapSize.y - 6) / 15
                //    );
                Vector3 tilePosition = new Vector3(
                    -2.5f + x * (5 / (mapSize.x - 1)) + (-x + mapSize.x / 2) * -(mapSize.x - 6) / 20,
                    -0.0f,
                    -2.5f + y * (5 / (mapSize.y - 1)) + (-y + mapSize.y / 2) * -(mapSize.y - 6) / 20
                    );

                int rotationAngle = Random.Range(0, 3);
                float rotationFactor = Random.Range(-4.0f, 4.0f);
                Quaternion tileRotation = Quaternion.Euler(new Vector3(0, (rotationAngle * 90) + rotationFactor, 0));
                GameObject newTile = Instantiate(fieldTilePrefab, tilePosition, tileRotation);
                newTile.transform.localScale = new Vector3(
                    (5 / (mapSize.x - 1)) * (1 - outlinePercent),
                    (5 / (mapSize.y - 1)) * (1 - outlinePercent),
                    1.0f);

                newTile.GetComponent<Renderer>().material.mainTextureOffset
                    = new Vector2(
                        1 / mapSize.x * Random.Range(0, (int)mapSize.x),
                        1 / mapSize.y * Random.Range(0, (int)mapSize.y));
                newTile.GetComponent<Renderer>().material.mainTextureScale
                    = new Vector2(Random.Range(0.1f, 1 / mapSize.x), Random.Range(0.1f, 1 / mapSize.y));
                newTile.transform.parent = mapHolder;
                //Debug.Log(newTile.GetComponent<MeshRenderer>().material.color);
                fieldTileContainer[x, y] = newTile;
            }
        }

    }

    public void GenerateBridge()
    {
        string holderName = "Generated Bridge";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }
        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        GameObject[] fieldTileArr = GameObject.FindGameObjectsWithTag("FieldTile");


        for (int i = 0; i < adjList.Length; i++)
        {
            adjList[i].Clear();
            fieldBridgeContainer[i].Clear();
        }

        List<string> fieldBridgeFlag = new List<string>();
        Vector3 bridgeCoordination;

        //Bridge generate
        //For test
        int debugBridge = PlayerPrefs.GetInt(PrefsEntity.BridgeCount, 35);

        //Original : fieldTileArr.Length + Random.Range(-2, 4)
        for (int i=0; i< GenerateBridgeCount(); i++)
        {
            Vector2 tile = GetBridgeCoordinate(i, fieldTileArr);

            if (fieldBridgeFlag.Contains(((int)tile.x).ToString() + ((int)tile.y).ToString()) |
                fieldBridgeFlag.Contains(((int)tile.y).ToString() + ((int)tile.x).ToString()))
            {
                i--;
                continue;
            }

            PlayerPrefsX.SetVector2(PrefsEntity.Bridge + i.ToString(), tile);

            adjList[(int)tile.x].Add((int)tile.y);
            adjList[(int)tile.y].Add((int)tile.x);

            bridgeCoordination = (fieldTileArr[(int)tile.x].transform.position + fieldTileArr[(int)tile.y].transform.position) / 2;
            int bridgeRotationFactor = (Mathf.Abs((int)tile.x - (int)tile.y) == 1) ? 0 : 1;
            Quaternion bridgeRotation = Quaternion.Euler(new Vector3(0, bridgeRotationFactor * 90, 0));
            Transform bridgeTile = Instantiate(fieldBridgePrefab, bridgeCoordination, bridgeRotation) as Transform;
            bridgeTile.localScale = new Vector3(
                    (11 / (mapSize.x - 1)) * (1 - outlinePercent * 2),
                    (11 / (mapSize.y - 1)) * (1 - outlinePercent * 2),
                    1.8f);

            try
            {
                bridgeTile.gameObject.SetActive(fieldBridgeVisibility[(int)tile.x, (int)tile.y]);
                bridgeTile.gameObject.SetActive(fieldBridgeVisibility[(int)tile.y, (int)tile.x]);
            }
            catch(System.Exception e)
            {
                Debug.Log(e.Message);
            }
            //Debug.Log((int)tile.x + ", " + (int)tile.y + " : " + fieldBridgeVisibility[(int)tile.x, (int)tile.y]);

            fieldBridgeContainer[(int)tile.x].Add(bridgeTile);
            fieldBridgeContainer[(int)tile.y].Add(bridgeTile);

            bridgeTile.parent = mapHolder;

            fieldBridgeFlag.Add(((int)tile.x).ToString() + ((int)tile.y).ToString());
        }
    }

    private Vector2 GetBridgeCoordinate(int flag, GameObject[] fieldTileArr)
    {
        Vector2 tile;
        tile.x = (int)Random.Range(0, fieldTileArr.Length - 1);
        tile.y = -1;
        int tileFlag = 0;
        while ((int)tile.y < 0 |
            (int)tile.y > fieldTileArr.Length - 1 |
            (tile.x % (int)mapSize.y == 0 & tileFlag == 0) |
            (tile.x % (int)mapSize.y == (int)mapSize.y - 1 & tileFlag == 1))
        {
            tileFlag = Random.Range(0, 3);
            // 0 = up : -1
            // 1 = down : +1
            // 2 = left : -y
            // 3 = right : +y
            switch (tileFlag)
            {
                case 0:
                    tile.y = (int)tile.x - 1;
                    continue;
                case 1:
                    tile.y = (int)tile.x + 1;
                    continue;
                case 2:
                    tile.y = (int)tile.x - (int)mapSize.y;
                    continue;
                case 3:
                    tile.y = (int)tile.x + (int)mapSize.y;
                    continue;
            }
        }
        if (PlayerPrefsX.GetBool(PrefsEntity.SaveFlag))
        {
            return PlayerPrefsX.GetVector2(PrefsEntity.Bridge + flag.ToString(), tile);
        }
        return tile;
    }

    private void PrintAdjList(List<int>[] adjList)
    {
        string t;

        for (int i = 0; i < (int)mapSize.x * (int)mapSize.y; i++)
        {
            t = "";
            foreach (int j in adjList[i])
                t = t + " " + j.ToString();
            Debug.Log(t);
        }
    }

    private void PrintBridgeContainer(List<Transform>[] bridgeContainer)
    {
        string t;

        for (int i = 0; i < (int)mapSize.x * (int)mapSize.y; i++)
        {
            t = "";
            foreach (Transform j in bridgeContainer[i])
                t = t + " " + j.position;
            Debug.Log(i + " : " + t);
        }
    }

    private bool BridgeBFS(List<int>[] adjList)
    {
        Queue<int> queue = new Queue<int>();
        bool[] visited = new bool[adjList.Length];

        queue.Enqueue(0);

        while(queue.Count != 0)
        {
            int current = queue.Dequeue();
            if (!visited[current])
            {
                visited[current] = true;

                foreach(int i in adjList[current])
                {
                    if (!visited[i])
                        queue.Enqueue(i);
                }
            }
        }

        for(int i=0; i<adjList.Length; i++)
        {
            if (!visited[i])
                return false;
        }

        return true;
    }


    //TODO : Seperate entrance and character
    public void GenerateEntrance()
    {
        for (int i = 0; i < 1; i++)
        {
            Vector2 entrance = GetEntranceCoordination();
            //Debug.Log((int)mapSize.x + ", " + (int)mapSize.y);
            //Debug.Log(x + ",, " + y);
            if (fieldEventFlag[(int)entrance.x, (int)entrance.y] == 0)
            {
                Vector3 eventPosition = new Vector3(
                        -2.5f + (int)entrance.x * (5 / (mapSize.x - 1)) + (-(int)entrance.x + mapSize.x / 2) * -(mapSize.x - 6) / 15,
                        0.15f,
                        -2.5f + (int)entrance.y * (5 / (mapSize.y - 1)) + (-(int)entrance.y + mapSize.y / 2) * -(mapSize.y - 6) / 15
                        );
                Transform newTile = Instantiate(fieldEntrance, eventPosition, Quaternion.Euler(Vector3.right * 0)) as Transform;
                //newTile.localScale = Vector3.one * 0.3f;
                if (!PlayerPrefsX.GetBool(PrefsEntity.SaveFlag)) // Not saved : Entrance = Start Position
                {
                    PlayerPrefs.SetInt(PrefsEntity.StartX, (int)entrance.x);
                    PlayerPrefs.SetInt(PrefsEntity.StartY, (int)entrance.y);
                    PlayerPrefs.SetInt(PrefsEntity.CurrentX, (int)entrance.x);
                    PlayerPrefs.SetInt(PrefsEntity.CurrentY, (int)entrance.y);
                }
                PlayerPrefsX.SetVector2(PrefsEntity.Entrance, entrance);
                fieldEventContainer[(int)entrance.x, (int)entrance.y] = newTile;

                fieldEventFlag[(int)entrance.x, (int)entrance.y] = FieldEventType.Entrance;
            }
            else
            {
                i--;
            }
        }
    }

    private Vector2 GetEntranceCoordination()
    {
        Vector2 entrance;
        entrance.x = Random.Range(0, (int)mapSize.x);
        entrance.y = Random.Range(0, (int)mapSize.y);
        if (currentFloor != 100)
        {
            entrance.x = PlayerPrefs.GetInt(PrefsEntity.CurrentX);
            entrance.y = PlayerPrefs.GetInt(PrefsEntity.CurrentY);
        }
        if (PlayerPrefsX.GetBool(PrefsEntity.SaveFlag))
        {
            return PlayerPrefsX.GetVector2(PrefsEntity.Entrance, entrance);
        }
        return entrance;
    }

    public void GenerateExit()
    {
        for (int i = 0; i < 1; i++)
        {
            Vector2 exit = GetExitCoordination();
            if (fieldEventFlag[(int)exit.x, (int)exit.y] == 0)
            {
                Vector3 eventPosition = new Vector3(
                        -2.5f + (int)exit.x * (5 / (mapSize.x - 1)) + (-(int)exit.x + mapSize.x / 2) * -(mapSize.x - 6) / 15,
                        0.15f,
                        -2.5f + (int)exit.y * (5 / (mapSize.y - 1)) + (-(int)exit.y + mapSize.y / 2) * -(mapSize.y - 6) / 15
                        );
                eventPosition.y = 0.16f;
                Transform newTile = Instantiate(fieldExit, eventPosition, Quaternion.Euler(Vector3.right * 0)) as Transform;
                //newTile.localScale = Vector3.one * 0.3f;
                newTile.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                fieldEventContainer[(int)exit.x, (int)exit.y] = newTile;

                fieldEventFlag[(int)exit.x, (int)exit.y] = FieldEventType.Exit;
                PlayerPrefsX.SetVector2(PrefsEntity.Exit, exit);
            }
            else
            {
                i--;
            }
        }
    }

    private Vector2 GetExitCoordination()
    {
        Vector2 exit;
        exit.x = Random.Range(0, (int)mapSize.x);
        exit.y = Random.Range(0, (int)mapSize.y);

        if (PlayerPrefsX.GetBool(PrefsEntity.SaveFlag))
        {
            return PlayerPrefsX.GetVector2(PrefsEntity.Exit, exit);
        }
        return exit;
    }

    public static void GenerateEvent(Transform eventType, FieldEventType eventFlag, int numOfEvent)
    {
        for (int i=0; i<numOfEvent; i++)
        {
            Vector2 eventCoordination = GetEventCoordination(i, eventType);
            if(eventCoordination == new Vector2(-1, -1))
            {
                continue;
            }
            if (fieldEventFlag[(int)eventCoordination.x, (int)eventCoordination.y] == 0
                & eventCoordination != new Vector2(PlayerPrefs.GetInt(PrefsEntity.CurrentX), PlayerPrefs.GetInt(PrefsEntity.CurrentY)))
            {
                Vector3 eventPosition = new Vector3(
                        -2.5f + (int)eventCoordination.x * (5 / (mapSize.x - 1)) + (-(int)eventCoordination.x + mapSize.x / 2) * -(mapSize.x - 6) / 15,
                        0.15f,
                        -2.5f + (int)eventCoordination.y * (5 / (mapSize.y - 1)) + (-(int)eventCoordination.y + mapSize.y / 2) * -(mapSize.y - 6) / 15
                        );
                Quaternion eventRotation = Quaternion.Euler(new Vector3(0,0,0));
                /* Object Offset 
                 * Box : 0.07
                */
                /* Object Angle
                 * Box : -45
                 */
                switch (eventFlag)
                {
                    case FieldEventType.Box:
                        eventPosition.y = 0.07f;
                        eventRotation = Quaternion.Euler(new Vector3(0, -45, 0));
                        break;
                    case FieldEventType.Trap:
                        eventPosition.y = 0.07f;
                        eventRotation = Quaternion.Euler(new Vector3(0, -45, 0));
                        break;
                    case FieldEventType.Enemy:
                        eventPosition.y = -0.7f;
                        break;
                    default:
                        break;
                }
                Transform newTile = Instantiate(eventType, eventPosition, eventRotation) as Transform;
                //newTile.localScale = Vector3.one * 0.3f;
                //TODO : Set Rotation  v
                fieldEventContainer[(int)eventCoordination.x, (int)eventCoordination.y] = newTile;

                fieldEventFlag[(int)eventCoordination.x, (int)eventCoordination.y] = eventFlag;
                PlayerPrefsX.SetVector2(eventType.name + i.ToString(), eventCoordination);
                //Debug.Log(eventCoordination.ToString());
                PlayerPrefs.SetString(eventCoordination.ToString(), eventType.name + i.ToString());
            }
            else
            {
                i--;
            }
        }
    }
    
    private static Vector2 GetEventCoordination(int flag, Transform eventType)
    {
        Vector2 eventCoordination;
        eventCoordination.x = Random.Range(0, (int)mapSize.x);
        eventCoordination.y = Random.Range(0, (int)mapSize.y);

        if (PlayerPrefsX.GetBool(PrefsEntity.SaveFlag))
        {
            return PlayerPrefsX.GetVector2(eventType.name + flag.ToString(), eventCoordination);
        }
        return eventCoordination;
    }

    private void ChangeFloorElement()
    {
        int bgFlag = PlayerPrefs.GetInt(PrefsEntity.FieldElement, 0);
        //0 = Soil, 1 = Water, 2 = Fire, 3 = Wood, 4 = Steal
        if (currentFloor % 20 == 0)
        {
            bgFlag = Random.Range(0, 5);
            if (PlayerPrefsX.GetBool(PrefsEntity.SaveFlag))
            {
                bgFlag = PlayerPrefs.GetInt(PrefsEntity.FieldElement, 0);
            }
        }
        switch (bgFlag)
        {
            case 0:
                backgroundImage.sprite = Resources.Load<Sprite>(Resource.FieldBackgroundSoil) as Sprite;
                floorElementImage.sprite = Resources.Load<Sprite>(Resource.FieldElementSoil) as Sprite;
                fieldTilePrefab.GetComponent<MeshRenderer>().sharedMaterial.SetTexture(
                    "_MainTex",
                    Resources.Load(Resource.FieldTileSoil) as Texture);
                fieldBridgePrefab.GetComponent<MeshRenderer>().sharedMaterial.SetTexture(
                    "_MainTex",
                    Resources.Load(Resource.FieldTileSoil) as Texture);
                //fieldTilePrefab.GetComponent<MeshRenderer>().sharedMaterial.mainTexture
                //    = Resources.Load(Resource.FieldTileSoil) as Texture;
                //fieldBridgePrefab.GetComponent<MeshRenderer>().sharedMaterial.mainTexture
                //    = Resources.Load(Resource.FieldTileSoil) as Texture;
                break;
            case 1:
                backgroundImage.sprite = Resources.Load<Sprite>(Resource.FieldBackgroundWater) as Sprite;
                floorElementImage.sprite = Resources.Load<Sprite>(Resource.FieldElementWater) as Sprite;
                fieldTilePrefab.GetComponent<MeshRenderer>().sharedMaterial.SetTexture(
                    "_MainTex",
                    Resources.Load(Resource.FieldTileWater) as Texture);
                fieldBridgePrefab.GetComponent<MeshRenderer>().sharedMaterial.SetTexture(
                    "_MainTex",
                    Resources.Load(Resource.FieldTileWater) as Texture);
                //fieldTilePrefab.GetComponent<MeshRenderer>().sharedMaterial.mainTexture
                //    = Resources.Load(Resource.FieldTileWater) as Texture;
                //fieldBridgePrefab.GetComponent<MeshRenderer>().sharedMaterial.mainTexture
                //    = Resources.Load(Resource.FieldTileWater) as Texture;
                break;
            case 2:
                backgroundImage.sprite = Resources.Load<Sprite>(Resource.FieldBackgroundFire) as Sprite;
                floorElementImage.sprite = Resources.Load<Sprite>(Resource.FieldElementFire) as Sprite;
                fieldTilePrefab.GetComponent<MeshRenderer>().sharedMaterial.SetTexture(
                    "_MainTex",
                    Resources.Load(Resource.FieldTileFire) as Texture);
                fieldBridgePrefab.GetComponent<MeshRenderer>().sharedMaterial.SetTexture(
                    "_MainTex",
                    Resources.Load(Resource.FieldTileFire) as Texture);
                //fieldTilePrefab.GetComponent<MeshRenderer>().sharedMaterial.mainTexture
                //    = Resources.Load(Resource.FieldTileFire) as Texture;
                //fieldBridgePrefab.GetComponent<MeshRenderer>().sharedMaterial.mainTexture
                //    = Resources.Load(Resource.FieldTileFire) as Texture;
                break;
            case 3:
                backgroundImage.sprite = Resources.Load<Sprite>(Resource.FieldBackgroundWood) as Sprite;
                floorElementImage.sprite = Resources.Load<Sprite>(Resource.FieldElementWood) as Sprite;
                fieldTilePrefab.GetComponent<MeshRenderer>().sharedMaterial.SetTexture(
                    "_MainTex",
                    Resources.Load(Resource.FieldTileWood) as Texture);
                fieldBridgePrefab.GetComponent<MeshRenderer>().sharedMaterial.SetTexture(
                    "_MainTex",
                    Resources.Load(Resource.FieldTileWood) as Texture);
                //renderer.material.SetTexture("_MainTex", Resources.LoadAssetAtPath("Assets/Images/mom/b_mom0" + 2 + ".png", typeof(Texture)) as Texture);

                //fieldTilePrefab.GetComponent<MeshRenderer>().sharedMaterial.mainTexture
                //    = Resources.Load(Resource.FieldTileWood) as Texture;
                //fieldBridgePrefab.GetComponent<MeshRenderer>().sharedMaterial.mainTexture
                //    = Resources.Load(Resource.FieldTileWood) as Texture;
                break;
            case 4:
                backgroundImage.sprite = Resources.Load<Sprite>(Resource.FieldBackgroundSteal) as Sprite;
                floorElementImage.sprite = Resources.Load<Sprite>(Resource.FieldElementSteal) as Sprite;
                fieldTilePrefab.GetComponent<MeshRenderer>().sharedMaterial.SetTexture(
                    "_MainTex",
                    Resources.Load(Resource.FieldTileSteal) as Texture);
                fieldBridgePrefab.GetComponent<MeshRenderer>().sharedMaterial.SetTexture(
                    "_MainTex",
                    Resources.Load(Resource.FieldTileSteal) as Texture);
                //fieldTilePrefab.GetComponent<MeshRenderer>().sharedMaterial.mainTexture
                //    = Resources.Load(Resource.FieldTileSteal) as Texture;
                //fieldBridgePrefab.GetComponent<MeshRenderer>().sharedMaterial.mainTexture
                //    = Resources.Load(Resource.FieldTileSteal) as Texture;
                break;
        }
        PlayerPrefs.SetInt(PrefsEntity.FieldElement, bgFlag);

    }

    private int GenerateBridgeCount()
    {
        int maxBrige = ((int)mapSize.x - 1) * (int)mapSize.y + ((int)mapSize.y - 1) * (int)mapSize.x;
        return (int)(maxBrige * 0.75);
    }


    //0 = No event, 1 = Entrance, 2 = Exit, 3 = Enemy, 4 = Trap, 5 = Jail, 6 = Box
    private static int EventCount(int flag)
    {
        int currentFloor = PlayerPrefs.GetInt(PrefsEntity.CurrentFloor, 100);
        if (flag == 3) // Enemy : 3
        {
            if (PlayerPrefsX.GetBool(PrefsEntity.SaveFlag))
            {
                return PlayerPrefs.GetInt(PrefsEntity.NumOfEnemy);
            }
            if (currentFloor > 80) // 100~81
            {
                PlayerPrefs.SetInt(PrefsEntity.NumOfEnemy, 2);
                return 2;
            }
            else if (currentFloor <= 80 & currentFloor > 60) // 80~61
            {
                PlayerPrefs.SetInt(PrefsEntity.NumOfEnemy, 3);
                return 3;
            }
            else if (currentFloor <= 60 & currentFloor > 40) // 60~41
            {
                PlayerPrefs.SetInt(PrefsEntity.NumOfEnemy, 4);
                return 4;
            }
            else if (currentFloor <= 40 & currentFloor > 20) // 40~21
            {
                PlayerPrefs.SetInt(PrefsEntity.NumOfEnemy, 5);
                return 5;
            }
            else if (currentFloor <= 20 & currentFloor > 0) // 20~1
            {
                PlayerPrefs.SetInt(PrefsEntity.NumOfEnemy, 6);
                return 6;
            }
        }

        if (flag == 4) // Trap : 4
        {
            if (PlayerPrefsX.GetBool(PrefsEntity.SaveFlag))
            {
                return PlayerPrefs.GetInt(PrefsEntity.NumOfTrap);
            }
            if (currentFloor > 80) // 100~81
            {
                int numOfTrap = Random.Range(1, 3);
                PlayerPrefs.SetInt(PrefsEntity.NumOfTrap, numOfTrap);
                return numOfTrap;
            }
            else if (currentFloor <= 80 & currentFloor > 60) // 80~61
            {
                int numOfTrap = Random.Range(1, 3);
                PlayerPrefs.SetInt(PrefsEntity.NumOfTrap, numOfTrap);
                return numOfTrap;
            }
            else if (currentFloor <= 60 & currentFloor > 40) // 60~41
            {
                int numOfTrap = Random.Range(2, 4);
                PlayerPrefs.SetInt(PrefsEntity.NumOfTrap, numOfTrap);
                return numOfTrap;
            }
            else if (currentFloor <= 40 & currentFloor > 30) // 40~31
            {
                int numOfTrap = Random.Range(2, 4);
                PlayerPrefs.SetInt(PrefsEntity.NumOfTrap, numOfTrap);
                return numOfTrap;
            }
            else if (currentFloor <= 30 & currentFloor > 20) // 30~21
            {
                int numOfTrap = Random.Range(3, 5);
                PlayerPrefs.SetInt(PrefsEntity.NumOfTrap, numOfTrap);
                return numOfTrap;
            }
            else if (currentFloor <= 20 & currentFloor > 0) // 20~1
            {
                int numOfTrap = Random.Range(3, 5);
                PlayerPrefs.SetInt(PrefsEntity.NumOfTrap, numOfTrap);
                return numOfTrap;
            }
        }

        if (flag == 5) // Jail : 5
        {
            if (PlayerPrefsX.GetBool(PrefsEntity.SaveFlag))
            {
                return PlayerPrefs.GetInt(PrefsEntity.NumOfJail);
            }
            if (currentFloor > 80) // 100~81
            {
                int numOfJail = Random.Range(0, 2);
                PlayerPrefs.SetInt(PrefsEntity.NumOfJail, numOfJail);
                return numOfJail;
            }
            else if (currentFloor <= 80 & currentFloor > 60) // 80~61
            {
                int numOfJail = Random.Range(0, 2);
                PlayerPrefs.SetInt(PrefsEntity.NumOfJail, numOfJail);
                return numOfJail;
            }
            else if (currentFloor <= 60 & currentFloor > 40) // 60~41
            {
                int numOfJail = Random.Range(0, 2);
                PlayerPrefs.SetInt(PrefsEntity.NumOfJail, numOfJail);
                return numOfJail;
            }
            else if (currentFloor <= 40 & currentFloor > 20) // 40~21
            {
                int numOfJail = Random.Range(0, 2);
                PlayerPrefs.SetInt(PrefsEntity.NumOfJail, numOfJail);
                return numOfJail;
            }
            else if (currentFloor <= 20 & currentFloor > 0) // 20~1
            {
                int numOfJail = Random.Range(0, 2);
                PlayerPrefs.SetInt(PrefsEntity.NumOfJail, numOfJail);
                return numOfJail;
            }
        }
        if (flag == 6) // Box : 6
        {
            if (PlayerPrefsX.GetBool(PrefsEntity.SaveFlag))
            {
                return PlayerPrefs.GetInt(PrefsEntity.NumOfBox);
            }
            if (currentFloor > 80) // 100~81
            {
                int numOfBox = Random.Range(1, 3);
                PlayerPrefs.SetInt(PrefsEntity.NumOfBox, numOfBox);
                return numOfBox;
            }
            else if (currentFloor <= 80 & currentFloor > 60) // 80~61
            {
                int numOfBox = Random.Range(1, 3);
                PlayerPrefs.SetInt(PrefsEntity.NumOfBox, numOfBox);
                return numOfBox;
            }
            else if (currentFloor <= 60 & currentFloor > 40) // 60~41
            {
                int numOfBox = Random.Range(2, 4);
                PlayerPrefs.SetInt(PrefsEntity.NumOfBox, numOfBox);
                return numOfBox;
            }
            else if (currentFloor <= 40 & currentFloor > 30) // 40~31
            {
                int numOfBox = Random.Range(2, 4);
                PlayerPrefs.SetInt(PrefsEntity.NumOfBox, numOfBox);
                return numOfBox;
            }
            else if (currentFloor <= 30 & currentFloor > 20) // 30~21
            {
                int numOfBox = Random.Range(3, 5);
                PlayerPrefs.SetInt(PrefsEntity.NumOfBox, numOfBox);
                return numOfBox;
            }
            else if (currentFloor <= 20 & currentFloor > 0) // 20~1
            {
                int numOfBox = Random.Range(3, 5);
                PlayerPrefs.SetInt(PrefsEntity.NumOfBox, numOfBox);
                return numOfBox;
            }
        }

        return 1;
    }


    public static List<int>[] GetAdjList()
    {
        return adjList;
    }

    public static List<Transform>[] GetBridgeContainer()
    {
        return fieldBridgeContainer;
    }

    public static bool[,] GetBridgeVisibility()
    {
        return fieldBridgeVisibility;
    }

    public static Transform[,] GetEventContainer()
    {
        return fieldEventContainer;
    }

    public static GameObject[,] GetTileContainer()
    {
        return fieldTileContainer;
    }

    public static Vector2 GetMapsize()
    {
        return mapSize;
    }

    public static FieldEventType[,] GetEventFlag()
    {
        return fieldEventFlag;
    }


    public static void FieldMapOpen()
    {
        for (int i = 0; i < fieldBridgeContainer.Length; i++)
        {
            foreach (Transform bridge in fieldBridgeContainer[i])
                bridge.gameObject.SetActive(true);
        }
        for(int i=0; i< (int)mapSize.x * (int)mapSize.y; i++)
        {
            for(int j=0; j< (int)mapSize.x * (int)mapSize.y; j++)
            {
                fieldBridgeVisibility[i, j] = true;
            }
        }
        DataManager.BinarySerialize<bool[,]>(fieldBridgeVisibility, DataFilePath.FieldBridgeVisibility);
        bool[] fieldEvnetVisibility = PlayerPrefsX.GetBoolArray(PrefsEntity.FieldEventVisibility);
        foreach(Transform e in fieldEventContainer){
            if(e != null)
            {
                e.gameObject.SetActive(true);
            }
        }
        for(int i=0; i< fieldEvnetVisibility.Length; i++)
        {
            fieldEvnetVisibility[i] = true;
        }
        PlayerPrefsX.SetBoolArray(PrefsEntity.FieldEventVisibility, fieldEvnetVisibility);
        Debug.Log("Map Open");
    }
}
