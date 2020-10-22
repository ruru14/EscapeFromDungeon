using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Equip {

    public int level;
    public string img; // 장비 이미지 URL
    public string name; // 장비이름

    public EquipType type; // 장비종류 : 투구 - 갑옷 - 신발 - 무기 - 보조무기
    public EquipClass cls; // 직업제한 : 도적 - 궁수 - 법사 - 기사 - 사제
    public EquipGrade grade; // 장비등급 : 노말 - 레어 - 유니크 - 미스틱

    public float maxHP; // 체력
    public float maxMP; // 마나
    public float incMP; // 마나회복
    public float speed; //incAP; // 속도회복
    public float phyATK; // 물리공격력
    public float phyDEF; // 물리방어력
    public float macATK; // 마법공격력
    public float macDEF; // 마법방어력
    public float phyATKRate; // 물리공격력
    public float phyDEFRate; // 물리방어력
    public float macATKRate; // 마법공격력
    public float macDEFRate; // 마법방어력
    public float healEFC; // 회복효율
    public float critical; // 치명타확률

    public Equip() {

    }

    public bool isUpgrade() {
        if (level < 10) return true;


        return false;
    }

    //public void upgrade() {
    //    if (isUpgrade())
    //    {
    //        level++;
    //    }
    //}

    public float getHP() { return maxHP; }
    public float getMP() { return maxMP; }
    public float getIncMP() { return incMP; }
    public float getIncAP() { return speed; }
    public float getPhyATK() { return phyATK; }
    public float getPhyDEF() { return phyDEF; }
    public float getMgcATK() { return  macATK; }
    public float getMgcDEF() { return macDEF; }
    public float getPhyATKRate() { return  phyATKRate; }
    public float getPhyDEFRate() { return  phyDEFRate; }
    public float getMgcATKRate() { return  macATKRate; }
    public float getMgcDEFRate() { return  macDEFRate; }
    public float getHealEFC() { return healEFC; }
    public float getCritical() { return critical; }
}
//[System.Serializable]
//public class EquipHead : Equip
//{
//    public EquipHead() : base() { 
        
//    }
//}

//[System.Serializable]
//public class EquipBody : Equip
//{
//    public EquipBody() : base()
//    {

//    }
//}
//[System.Serializable]
//public class EquipFoot : Equip
//{
//    public EquipFoot() : base()
//    {

//    }
//}
//[System.Serializable]
//public class EquipWeapon : Equip
//{
//    public EquipWeapon() : base()
//    {

//    }
//}
//[System.Serializable]
//public class EquipSubWeapon : Equip
//{
//    public EquipSubWeapon() : base()
//    {

//    }
//}


[System.Serializable]
public class EquipSet {
    float total;

    //public EquipHead head;
    //public EquipBody body;
    //public EquipFoot foot;
    //public EquipWeapon weapon;
    //public EquipSubWeapon subWeapon;

    public Equip[] set;

    public EquipSet(int cls) {
        set = new Equip[5];

        switch (cls) {
            case 0:
                setThief();
                break;
            case 1:
                setArcher();
                break;
            case 2:
                setMage();
                break;
            case 3:
                setKnight();
                break;
            case 4:
                setPriest();
                break;
            default:
                set[0] = new Equip();
                set[1] = new Equip();
                set[2] = new Equip();
                set[3] = new Equip();
                set[4] = new Equip();

                break;
        }

        //set[0] = head;
        //set[1] = body;
        //set[2] = foot;
        //set[3] = weapon;
        //set[4] = subWeapon;
    }

    Equip setEquip(int idx, Equip e) {
        Equip tmp = set[idx];
        set[idx] = e;

        return tmp;
    }

    Equip resetEquip(int idx) {
        return setEquip(idx, new Equip());
    }

    void setKnight()
    {
        set[0] = EquipManager.GetEquip(EquipmentFilePath.Knight.Head.normal, 0);
        set[1] = EquipManager.GetEquip(EquipmentFilePath.Knight.Body.normal, 0);
        set[2] = EquipManager.GetEquip(EquipmentFilePath.Knight.Foot.normal, 0);
        set[3] = EquipManager.GetEquip(EquipmentFilePath.Knight.Weapon.normal, 0);
        set[4] = EquipManager.GetEquip(EquipmentFilePath.Knight.SubWeapon.normal, 0);
    }
    void setArcher()
    {
        set[0] = EquipManager.GetEquip(EquipmentFilePath.Archer.Head.normal, 0);
        set[1] = EquipManager.GetEquip(EquipmentFilePath.Archer.Body.normal, 0);
        set[2] = EquipManager.GetEquip(EquipmentFilePath.Archer.Foot.normal, 0);
        set[3] = EquipManager.GetEquip(EquipmentFilePath.Archer.Weapon.normal, 0);
        set[4] = EquipManager.GetEquip(EquipmentFilePath.Archer.SubWeapon.normal, 0);
    }
    void setThief()
    {
        set[0] = EquipManager.GetEquip(EquipmentFilePath.Thief.Head.normal, 0);
        set[1] = EquipManager.GetEquip(EquipmentFilePath.Thief.Body.normal, 0);
        set[2] = EquipManager.GetEquip(EquipmentFilePath.Thief.Foot.normal, 0);
        set[3] = EquipManager.GetEquip(EquipmentFilePath.Thief.Weapon.normal, 0);
        set[4] = EquipManager.GetEquip(EquipmentFilePath.Thief.Weapon.normal, 0);
    }
    void setMage()
    {
        set[0] = EquipManager.GetEquip(EquipmentFilePath.Mage.Head.normal, 0);
        set[1] = EquipManager.GetEquip(EquipmentFilePath.Mage.Body.normal, 0);
        set[2] = EquipManager.GetEquip(EquipmentFilePath.Mage.Foot.normal, 0);
        set[3] = EquipManager.GetEquip(EquipmentFilePath.Mage.Weapon.normal, 0);
        set[4] = EquipManager.GetEquip(EquipmentFilePath.Mage.SubWeapon.normal, 0);
    }
    void setPriest()
    {
        set[0] = EquipManager.GetEquip(EquipmentFilePath.Priest.Head.normal, 0);
        set[1] = EquipManager.GetEquip(EquipmentFilePath.Priest.Body.normal, 0);
        set[2] = EquipManager.GetEquip(EquipmentFilePath.Priest.Foot.normal, 0);
        set[3] = EquipManager.GetEquip(EquipmentFilePath.Priest.Weapon.normal, 0);
        set[4] = EquipManager.GetEquip(EquipmentFilePath.Priest.SubWeapon.normal, 0);
    }

    public float getHP() {
        total = 1;
        foreach (Equip e in set) {
            total += e.getHP();
        }

        return total; 
    }

    public float getMP()
    {
        total = 1;
        foreach (Equip e in set)
        {
            total += e.getMP();
        }

        return total;
    }
    public float getIncMP()
    {
        total = 1;
        foreach (Equip e in set)
        {
            total += e.getIncMP();
        }

        return total;
    }
    public float getIncAP()
    {
        total = 1;
        foreach (Equip e in set)
        {
            total += e.getIncAP();
        }

        return total;
    }
    public float getPhyATK()
    {
        total = 0;
        foreach (Equip e in set)
        {
            total += e.getPhyATK();
        }

        return total;
    }
    public float getPhyDEF()
    {
        total = 0;
        foreach (Equip e in set)
        {
            total += e.getPhyDEF();
        }

        return total;
    }
    public float getMgcATK()
    {
        total = 0;
        foreach (Equip e in set)
        {
            total += e.getMgcATK();
        }

        return total;
    }
    public float getMgcDEF()
    {
        total = 0;
        foreach (Equip e in set)
        {
            total += e.getMgcDEF();
        }

        return total;
    }
    public float getPhyATKRate()
    {
        total = 1;
        foreach (Equip e in set)
        {
            total += e.getPhyATKRate();
        }

        return total;
    }
    public float getPhyDEFRate()
    {
        total = 1;
        foreach (Equip e in set)
        {
            total += e.getPhyDEFRate();
        }

        return total;
    }
    public float getMgcATKRate()
    {
        total = 1;
        foreach (Equip e in set)
        {
            total += e.getMgcATKRate();
        }

        return total;
    }
    public float getMgcDEFRate()
    {
        total = 1;
        foreach (Equip e in set)
        {
            total += e.getMgcDEFRate();
        }

        return total;
    }
    public float getHealEFC()
    {
        total = 1;
        foreach (Equip e in set)
        {
            total += e.getHealEFC();
        }

        return total;
    }
    public float getCritical()
    {
        total = 1;
        foreach (Equip e in set)
        {
            total += e.getCritical();
        }

        return total;
    }

}