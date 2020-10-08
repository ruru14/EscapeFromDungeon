using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public enum Class { KNIGHT, ARCHER, THIEF, MAGE, PRIEST }

[System.Serializable]
public class DataChar// : MonoBehaviour
{
    public string cls;

    public int level;

    // Start is called before the first frame update
    [Header("- Health Point")]
    float maxHP;//최대 체력
    public float curHP;

    [Header("- Mana Point")]
    float maxMP;//최대 마나
    public float curMP;

    [Header("- Action Point")]
    public float incAP;//AP 증가 => 속도

    [Header("- Physical Stat")]
    public float phyATK;//물리공 + 장비뎀
    public float phyDEF;//물리방 + 장비방

    [Header("- Magical Stat")]
    public float mgcATK;//마법공 + 장비뎀
    public float mgcDEF;//마법방 + 장비방

    [Header("- Speed, Critical")]
    public float spd;//속도
    public float critical;//크리티컬 확률

    //public float acc;//명중
    //public float buffAcc;

    //public float avoid;//회피
    //public float buffAvoid;

    public SkillSet charSkillSet;

    public EquipSet charEquipSet;

    public DataChar(string cls, float HP, float MP, float AP, float pATK, float pDEF, float mATK, float mDEF, float spd, float critical) {
        this.cls = cls;
        curHP = maxHP = HP;
        curMP = maxMP = MP;
        incAP = AP;
        phyATK = pATK;
        phyDEF = pDEF;
        mgcATK = mATK;
        mgcDEF = mDEF;
        this.spd = spd;
        this.critical = critical;

        charEquipSet = new EquipSet(cls);
    }

    public float getMaxHP() {
        return maxHP + charEquipSet.getHP();
    }

    public float getMaxMP() {
        return maxMP + charEquipSet.getMP();
    }

    public static DataChar getKnight() {
        DataChar tmp = new DataChar("Knight", 85, 30, 20.0f, 18, 15, 10, 15, 0, 0.05f);
        tmp.charSkillSet = new SkillKnight();
        return tmp;
    }

    public static DataChar getThief()
    {
        DataChar tmp = new DataChar("Thief", 65, 50, 28.0f, 20, 12, 17, 12, 0, 0.1f);
        tmp.charSkillSet = new SkillThief();
        return tmp;
    }

    public static DataChar getArcher()
    {
        DataChar tmp = new DataChar("Archer", 60, 45, 27.0f, 21, 11, 15, 12, 0, 0.1f);
        tmp.charSkillSet = new SkillArcher();
        return tmp;
    }

    public static DataChar getMage()
    {
        DataChar tmp =  new DataChar("Mage", 55, 70, 25.0f, 12, 10, 22, 16, 0, 0.1f);
        tmp.charSkillSet = new SkillMage();
        return tmp;
    }

    public static DataChar getPriest()
    {
        DataChar tmp =  new DataChar("Priest", 60, 65, 24.0f, 12, 12, 17, 16, 0, 0.05f);
        tmp.charSkillSet = new SkillPriest();
        return tmp;
    }

    public static DataChar getEnemyDC(string Name, SkillSet set, float HP, float MP, float AP, float pATK, float pDEF, float mATK, float mDEF, float spd, float critical)
    {
        DataChar tmp = new DataChar(Name, HP, MP, AP, pATK, pDEF, mATK, mDEF, spd, critical);
        tmp.charSkillSet = set;
        return tmp;
    }

    public float getPhyATK() { return phyATK + charEquipSet.getPhyATK(); }
    public float getPhyDEF() { return phyDEF + charEquipSet.getPhyDEF(); }
    public float getMgcATK() { return mgcATK + charEquipSet.getMgcATK(); }
    public float getMgcDEF() { return mgcDEF + charEquipSet.getMgcDEF(); }
    public float getIncAP() { return incAP + charEquipSet.getIncAP(); }
    public float getSPD() { return spd; }
}
