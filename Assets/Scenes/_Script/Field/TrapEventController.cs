using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEventController : MonoBehaviour
{
    public GameObject trapPanel;
    public GameObject controlBlocker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void TrapNormal(int eventFlag)
    {
        if (eventFlag >= 1 & eventFlag <= 10) // 10%, Friend HP Decrease (10 ~ 20%)
        {
            FriendHpDecrease();
        }
        else if (eventFlag >= 11 & eventFlag <= 20) // 10%, Friend MP Decrease (10 ~ 20%)
        {
            FriendMpDecrease();
        }
        else if (eventFlag >= 21 & eventFlag <= 35) // 15%, Enemy Stat Up
        {
            EnemyStatUp();
        }
        else if (eventFlag >= 36 & eventFlag <= 50) // 15%, Friend Stat Down
        {
            FriendStatDown();
        }
        else if (eventFlag >= 51 & eventFlag <= 60) // 10%, Enemy Regen
        {
            EnemyRegen();
        }
        else if (eventFlag >= 61 & eventFlag <= 70) // 10%, Battle Start
        {
            BattleStart();
        }
        else if (eventFlag >= 71 & eventFlag <= 100) // 30%, Nothing Happen
        {
            NothingHappen();
        }
    }

    private void TrapHard(int eventFlag)
    {
        if (eventFlag >= 1 & eventFlag <= 15) // 15%, Friend HP Decrease (10 ~ 20%)
        {
            FriendHpDecrease();
        }
        else if (eventFlag >= 16 & eventFlag <= 30) // 15%, Friend MP Decrease (10 ~ 20%)
        {
            FriendMpDecrease();
        }
        else if (eventFlag >= 31 & eventFlag <= 45) // 15%, Enemy Stat Up
        {
            EnemyStatUp();
        }
        else if (eventFlag >= 46 & eventFlag <= 60) // 15%, Friend Stat Down
        {
            FriendStatDown();
        }
        else if (eventFlag >= 61 & eventFlag <= 70) // 10%, Enemy Regen
        {
            EnemyRegen();
        }
        else if (eventFlag >= 71 & eventFlag <= 80) // 10%, Battle Start
        {
            BattleStart();
        }
        else if (eventFlag >= 81 & eventFlag <= 100) // 20%, Nothing Happen
        {
            NothingHappen();
        }
    }   

    private void TrapHell(int eventFlag)
    {

        if (eventFlag >= 1 & eventFlag <= 20) // 20%, Friend HP Decrease (10 ~ 20%)
        {
            FriendHpDecrease();
        }
        else if (eventFlag >= 21 & eventFlag <= 40) // 20%, Friend MP Decrease (10 ~ 20%)
        {
            FriendMpDecrease();
        }
        else if (eventFlag >= 41 & eventFlag <= 55) // 15%, Enemy Stat Up
        {
            EnemyStatUp();
        }
        else if (eventFlag >= 56 & eventFlag <= 70) // 15%, Friend Stat Down
        {
            FriendStatDown();
        }
        else if (eventFlag >= 71 & eventFlag <= 80) // 10%, Enemy Regen
        {
            EnemyRegen();
        }
        else if (eventFlag >= 81 & eventFlag <= 90) // 10%, Battle Start
        {
            BattleStart();
        }
        else if (eventFlag >= 91 & eventFlag <= 100) // 10%, Nothing Happen
        {
            NothingHappen();
        }
    }
    
    private void FriendHpDecrease()
    {

    }

    private void FriendMpDecrease()
    {

    }

    private void EnemyStatUp()
    {

    }

    private void FriendStatDown()
    {

    }

    private void EnemyRegen()
    {

    }

    private void BattleStart()
    {

    }

    private void NothingHappen()
    {

    }

    public void OnClickConfirm()
    {
        Utility.ObjectVisibility(controlBlocker, false);
        Utility.ObjectVisibility(trapPanel, false);
    }
}
