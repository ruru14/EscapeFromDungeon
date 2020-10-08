[System.Serializable]

public enum EQUIPTYPE {SWORD, SHIELD};//장비 종류 분류
public enum EQUIPPOS {HEAD, BODY, HAND,  HAND_R, HAND_L, FOOT,  FOOT_R, FOOT_L }; // 장비 위치

[System.Serializable]
public class Equipment
{
    public EQUIPTYPE type = EQUIPTYPE.SWORD;

    public EQUIPPOS pos = EQUIPPOS.HAND_L;

    public Buff stat = new Buff(); //장비가 부여하는 스탯

    public void UpgradeEquip() { //장비 강화시 사용할 함수
        
    }

}
