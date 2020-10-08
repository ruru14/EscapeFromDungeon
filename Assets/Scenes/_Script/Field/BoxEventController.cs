using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxEventController : MonoBehaviour
{
    public GameObject boxPanel;
    public GameObject controlBlocker;
    public Button btnConfirm;
    public Button btnRouletteMove;
    public Text txtRouletteMove;
    public Text txtRouletteMessage;
    public Image eventImage;
    private int currentLevel;
    private bool rouletteStartFlag;
    private bool rouletteStopFlag;
    private int rouletteBtnCounter;
    private int eventFlag;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstatnce();
        currentLevel = PlayerPrefs.GetInt(PrefsEntity.CurrentLevel, 0);
        //rouletteBtnCounter = 0;
        //rouletteStartFlag = false;
        //rouletteStopFlag = false;
        //btnConfirm.interactable = false;
        //txtRouletteMessage.text = "";
        //txtRouletteMove.text = "START";
        //eventImage.sprite = Resources.Load<Sprite>(BoxEventImage.DefaultImage) as Sprite;
        RouletteInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (rouletteStartFlag)
        {
            eventFlag = Random.Range(1, 101);
            OccurRoulette();
        }
    }

    private void RouletteInitialize()
    {
        rouletteBtnCounter = 0;
        rouletteStartFlag = false;
        rouletteStopFlag = false;
        btnConfirm.interactable = false;
        txtRouletteMessage.text = "";
        txtRouletteMove.text = "START";
        eventImage.sprite = Resources.Load<Sprite>(BoxEventImage.DefaultImage) as Sprite;
    }

    private void OccurRoulette()
    {
        switch (currentLevel)
        {
            case 0:
                RouletteNormal(eventFlag);
                break;
            case 1:
                RouletteHard(eventFlag);
                break;
            case 2:
                RouletteHell(eventFlag);
                break;
        }
    }

    private void RouletteNormal(int eventFlag)
    {
        if (eventFlag >= 1 & eventFlag <= 10) // 10%, HP Heal (30 ~ 50%)
        {
            BoxEventHpHeal();
        }
        else if (eventFlag >= 11 & eventFlag <= 20) // 10%, MP Heal (30 ~ 50%)
        {
            BoxEventMpHeal();
        }
        else if (eventFlag >= 21 & eventFlag <= 30) // 10%, Map open
        {
            BoxEventMapOpen();
        }
        else if (eventFlag >= 31 & eventFlag <= 40) // 10%, Get Equipment
        {
            BoxEventGetEquipment();
        }
        else if (eventFlag >= 41 & eventFlag <= 50) // 10%, Get Scroll Equip
        {
            BoxEventGetScrollEquip();
        }
        else if (eventFlag >= 51 & eventFlag <= 65) // 15%, Get Scroll Skill
        {
            BoxEventGetScrollSkill();
        }
        else if (eventFlag >= 66 & eventFlag <= 80) // 15%, Get Scroll Stat
        {
            BoxEventGetScrollStat();
        }
        else if (eventFlag >= 81 & eventFlag <= 100) // 20%, Get 1 Key
        {
            BoxEventGetKey();
        }
    }
    private void RouletteHard(int eventFlag)
    {
        if (eventFlag >= 1 & eventFlag <= 15) // 15%, HP Heal (30 ~ 50%)
        {
            BoxEventHpHeal();
        }
        else if (eventFlag >= 16 & eventFlag <= 30) // 15%, MP Heal (30 ~ 50%)
        {
            BoxEventMpHeal();
        }
        else if (eventFlag >= 31 & eventFlag <= 40) // 10%, Map open
        {
            BoxEventMapOpen();
        }
        else if (eventFlag >= 41 & eventFlag <= 50) // 10%, Get Equipment
        {
            BoxEventGetEquipment();
        }
        else if (eventFlag >= 51 & eventFlag <= 60) // 10%, Get Scroll Equip
        {
            BoxEventGetScrollEquip();
        }
        else if (eventFlag >= 61 & eventFlag <= 70) // 10%, Get Scroll Skill
        {
            BoxEventGetScrollSkill();
        }
        else if (eventFlag >= 71 & eventFlag <= 80) // 10%, Get Scroll Stat
        {
            BoxEventGetScrollStat();
        }
        else if (eventFlag >= 81 & eventFlag <= 100) // 20%, Get 1 Key
        {
            BoxEventGetKey();
        }
    }
    private void RouletteHell(int eventFlag)
    {
        if (eventFlag >= 1 & eventFlag <= 20) // 20%, HP Heal (30 ~ 50%)
        {
            BoxEventHpHeal();
        }
        else if (eventFlag >= 21 & eventFlag <= 40) // 20%, MP Heal (30 ~ 50%)
        {
            BoxEventMpHeal();
        }
        else if (eventFlag >= 41 & eventFlag <= 50) // 10%, Map open
        {
            BoxEventMapOpen();
        }
        else if (eventFlag >= 51 & eventFlag <= 60) // 10%, Get Equipment
        {
            BoxEventGetEquipment();
        }
        else if (eventFlag >= 61 & eventFlag <= 70) // 10%, Get Scroll Equip
        {
            BoxEventGetScrollEquip();
        }
        else if (eventFlag >= 71 & eventFlag <= 75) // 5%, Get Scroll Skill
        {
            BoxEventGetScrollSkill();
        }
        else if (eventFlag >= 76 & eventFlag <= 80) // 5%, Get Scroll Stat
        {
            BoxEventGetScrollStat();
        }
        else if (eventFlag >= 81 & eventFlag <= 100) // 20%, Get 1 Key
        {
            BoxEventGetKey();
        }
    }

    private void BoxEventHpHeal()
    {
        eventImage.sprite = Resources.Load<Sprite>(BoxEventImage.HpHeal) as Sprite;
        txtRouletteMessage.text = BoxEventMessage.HpHeal;
        if (!rouletteStartFlag)
        {
            Debug.Log("HpHeal");
        }
    }

    private void BoxEventMpHeal()
    {
        eventImage.sprite = Resources.Load<Sprite>(BoxEventImage.MpHeal) as Sprite;
        txtRouletteMessage.text = BoxEventMessage.MpHeal;
        if (!rouletteStartFlag)
        {
            Debug.Log("MpHeal");
        }
    }

    private void BoxEventMapOpen()
    {
        eventImage.sprite = Resources.Load<Sprite>(BoxEventImage.MapOpen) as Sprite;
        txtRouletteMessage.text = BoxEventMessage.MapOpen;
        if (!rouletteStartFlag)
        {
            MapGenerator.FieldMapOpen();
            Debug.Log("Log by BoxEventController : MapOpen");
        }
    }

    private void BoxEventGetEquipment()
    {
        eventImage.sprite = Resources.Load<Sprite>(BoxEventImage.GetEquipment) as Sprite;
        txtRouletteMessage.text = BoxEventMessage.GetEquipment;
        if (!rouletteStartFlag)
        {
            Debug.Log("GetEquip");
        }
    }

    private void BoxEventGetScrollEquip()
    {
        eventImage.sprite = Resources.Load<Sprite>(BoxEventImage.GetScrollEquip) as Sprite;
        txtRouletteMessage.text = BoxEventMessage.GetScrollEquip;
        if (!rouletteStartFlag)
        {
            Debug.Log("GetScrollEquip");
        }
    }

    private void BoxEventGetScrollSkill()
    {
        eventImage.sprite = Resources.Load<Sprite>(BoxEventImage.GetScrollSkill) as Sprite;
        txtRouletteMessage.text = BoxEventMessage.GetScrollSkill;
        if (!rouletteStartFlag)
        {
            Debug.Log("GetScrollSkill");
        }
    }

    private void BoxEventGetScrollStat()
    {
        eventImage.sprite = Resources.Load<Sprite>(BoxEventImage.GetScrollStat) as Sprite;
        txtRouletteMessage.text = BoxEventMessage.GetScrollStat;
        if (!rouletteStartFlag)
        {
            Debug.Log("GetScrollStat");
        }
    }

    private void BoxEventGetKey()
    {
        eventImage.sprite = Resources.Load<Sprite>(BoxEventImage.GetKey) as Sprite;
        txtRouletteMessage.text = BoxEventMessage.GetKey;
        if (!rouletteStartFlag)
        {
            gameManager.AddKey(1);
            Debug.Log("GetKey");
        }
    }

    //Not used
    private void BoxEventUpgradeEquipment()
    {
        eventImage.sprite = Resources.Load<Sprite>(BoxEventImage.UpgradeEquipment) as Sprite;
        txtRouletteMessage.text = BoxEventMessage.UpgradeEquipment;
        if (!rouletteStartFlag)
        {
            Debug.Log("UpgEquip");
        }
    }

    //Not used
    private void BoxEventUpgradeSkill()
    {
        eventImage.sprite = Resources.Load<Sprite>(BoxEventImage.UpgradeSkill) as Sprite;
        txtRouletteMessage.text = BoxEventMessage.UpgradeSkill;
        if (!rouletteStartFlag)
        {
            Debug.Log("UpgSkill");
        }
    }

    public void OnClickRoulette()
    {
        if (!rouletteStartFlag) //Start
        {
            rouletteStartFlag = !rouletteStartFlag;
            txtRouletteMove.text = "STOP";
        }else if (rouletteStartFlag) //Stop
        {
            rouletteStartFlag = !rouletteStartFlag;
            Utility.ObjectVisibility(btnRouletteMove.gameObject, false);
            btnConfirm.interactable = true;
        }
    }

    public void OnClickConfirm()
    {
        Utility.ObjectVisibility(controlBlocker, false);
        Utility.ObjectVisibility(boxPanel, false);
        Utility.ObjectVisibility(btnRouletteMove.gameObject, true);
        txtRouletteMessage.text = "";
        txtRouletteMove.text = "START";
        eventImage.sprite = Resources.Load<Sprite>(BoxEventImage.DefaultImage) as Sprite;
        OccurRoulette();
        RouletteInitialize();
    }
}
