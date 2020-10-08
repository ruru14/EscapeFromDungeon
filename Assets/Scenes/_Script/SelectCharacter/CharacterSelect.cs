using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    public GameObject explainPanel;
    public Button btnThief;
    public Button btnArcher;
    public Button btnMagician;
    public Button btnKnight;
    public Button btnPriest;
    public Text flag;
    private Vector3 btnPosition;
    public TextMeshProUGUI characterExplain;
    private static bool selectFlag;
    private GameManager gameManager;

    private void Awake()
    {
        selectFlag = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstatnce();
        Utility.ObjectVisibility(explainPanel, false);
        characterExplain.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Position Anchor = Top / Left
    public void OnClickThiefSelect()
    {
        if (!selectFlag)
        {
            btnArcher.gameObject.SetActive(false);
            btnMagician.gameObject.SetActive(false);
            btnKnight.gameObject.SetActive(false);
            btnPriest.gameObject.SetActive(false);

            selectFlag = true;
            Debug.Log("Selected : Thief");
            Utility.ObjectVisibility(explainPanel, true);
            btnPosition = btnThief.transform.position;
            btnPosition.x = 192f;
            btnThief.transform.position = btnPosition;
            ExplainCharacter(0);
            flag.text = "0";
        }
    }
    public void OnClickArcherSelect()
    {
        if (!selectFlag)
        {
            btnThief.gameObject.SetActive(false);
            btnMagician.gameObject.SetActive(false);
            btnKnight.gameObject.SetActive(false);
            btnPriest.gameObject.SetActive(false);

            selectFlag = true;
            Debug.Log("Selected : Archer");
            Utility.ObjectVisibility(explainPanel, true);
            btnPosition = btnArcher.transform.position;
            btnPosition.x = 192f;
            btnArcher.transform.position = btnPosition;
            ExplainCharacter(1);
            flag.text = "1";
        }
    }
    public void OnClickMagicianSelect()
    {
        if (!selectFlag)
        {
            btnThief.gameObject.SetActive(false);
            btnArcher.gameObject.SetActive(false);
            btnKnight.gameObject.SetActive(false);
            btnPriest.gameObject.SetActive(false);

            selectFlag = true;
            Debug.Log("Selected : Magician");
            Utility.ObjectVisibility(explainPanel, true);
            btnPosition = btnMagician.transform.position;
            btnPosition.x = 192f;
            btnMagician.transform.position = btnPosition;
            ExplainCharacter(2);
            flag.text = "2";
        }
    }
    public void OnClickKnightSelect()
    {
        if (!selectFlag)
        {
            btnThief.gameObject.SetActive(false);
            btnArcher.gameObject.SetActive(false);
            btnMagician.gameObject.SetActive(false);
            btnPriest.gameObject.SetActive(false);

            selectFlag = true;
            Debug.Log("Selected : Knight");
            Utility.ObjectVisibility(explainPanel, true);
            btnPosition = btnKnight.transform.position;
            btnPosition.x = 192f;
            btnKnight.transform.position = btnPosition;
            ExplainCharacter(3);
            flag.text = "3";
        }
    }
    public void OnClickPriestSelect()
    {
        if (!selectFlag)
        {
            btnThief.gameObject.SetActive(false);
            btnArcher.gameObject.SetActive(false);
            btnMagician.gameObject.SetActive(false);
            btnKnight.gameObject.SetActive(false);

            selectFlag = true;
            Debug.Log("Selected : Priest");
            Utility.ObjectVisibility(explainPanel, true);
            btnPosition = btnPriest.transform.position;
            btnPosition.x = 192f;
            btnPriest.transform.position = btnPosition;
            ExplainCharacter(4);
            flag.text = "4";
        }
    }

    public void OnClickCancel()
    {
        btnThief.gameObject.SetActive(true);
        btnArcher.gameObject.SetActive(true);
        btnMagician.gameObject.SetActive(true);
        btnKnight.gameObject.SetActive(true);
        btnPriest.gameObject.SetActive(true);
        Debug.Log("Clicked : Cancel");
        Utility.ObjectVisibility(explainPanel, false);
        btnPosition.x = 192f;
        btnPosition.y = 540f;
        Vector3 buttonGap = new Vector3(384f, 0, 0);
        btnThief.transform.position = btnPosition + buttonGap * 0;
        btnArcher.transform.position = btnPosition + buttonGap * 1;
        btnMagician.transform.position = btnPosition + buttonGap * 2;
        btnKnight.transform.position = btnPosition + buttonGap * 3;
        btnPriest.transform.position = btnPosition + buttonGap * 4;
        selectFlag = false;
    }

    public void OnClickConfirm()
    {
        Debug.Log("Clicked : Confirm. " + flag.text);
        // 0 = Thief, 1 = Archer, 2 = Magician, 3 = Knight, 4 = Priest
        switch (int.Parse(flag.text))
        {
            case 0:
                gameManager.AddCharacter(DataChar.getThief());
                break;
            case 1:
                gameManager.AddCharacter(DataChar.getArcher());
                break;
            case 2:
                gameManager.AddCharacter(DataChar.getMage());
                break;
            case 3:
                gameManager.AddCharacter(DataChar.getKnight());
                break;
            case 4:
                gameManager.AddCharacter(DataChar.getPriest());
                break;
        }
        SceneManager.LoadScene("Intro");
    }

    private void ExplainCharacter(int flag)
    {
        switch (flag)
        {
            case 0:
                characterExplain.text = "Thief Explain";
                break;
            case 1:
                characterExplain.text = "Archer Explain";
                break;
            case 2:
                characterExplain.text = "Magician Explain";
                break;
            case 3:
                characterExplain.text = "Knight Explain";
                break;
            case 4:
                characterExplain.text = "Priest Explain";
                break;
        }
    }

    //Not used
    private void ExplainPanelHide()
    {
        explainPanel.GetComponent<CanvasGroup>().alpha = 0f;
        explainPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //Not used
    private void ExplainPanelShow()
    {
        explainPanel.GetComponent<CanvasGroup>().alpha = 1f;
        explainPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
