using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;

/*
 * {
 *   "name" : "Weapon";
 *   "type" : "Weapon";
 *   "phyAtk" : 10;
 *   "magAtk" : 0;
 * }
 */
 public enum EquipClass : int
{
    Thief = 0,
    Archer = 1,
    Mage = 2,
    Knight = 3,
    Priest = 4,
    All = 5
}

public enum EquipType : int
{
    Head = 0,
    Body = 1,
    Foot = 2,
    Weapon = 3,
    SubWeapon = 4
}

public enum EquipGrade : int
{
    Normal = 0,
    Rare = 1,
    Unique = 2,
    Mystic = 3
}

public class EquipManager
{
    private EquipManager()
    {

    }

    [System.Serializable]
    private class EquipStatus
    {
        public string img; // 장비 이미지 URL
        public string name; // 장비이름
        public EquipType type; // 장비종류 : 투구 - 갑옷 - 신발 - 무기 - 보조무기
        public EquipClass cls; // 직업제한 : 도적 - 궁수 - 법사 - 기사 - 사제
        public EquipGrade grade; // 장비등급 : 노말 - 레어 - 유니크 - 미스틱
        public float maxHP; // 체력
        public float maxHP_Rate; // 체력(비율)
        public float maxMP; // 마나
        public float maxMP_Rate; // 마나(비율)
        public float incMP; // 마나회복
        public float incAP; // 속도회복
        public float phyATK; // 물리공격력
        public float phyATK_Rate; // 물리공격력(비율)
        public float phyDEF; // 물리방어력
        public float phyDEF_Rate; // 물리방어력(비율)
        public float macATK; // 마법공격력
        public float macATK_Rate; // 마법공격력(비율)
        public float macDEF; // 마법방어력
        public float macDEF_Rate; // 마법방어력(비율)
        public float healEFC; // 회복효율
        public float critical; // 치명타확률
        public float speed;
        public int upgrade;
    }
    
    [System.Serializable]
    private class Data
    {
        public EquipStatus[] equipment;
    }

    public static Equip GetEquip(string equipName, int equipLevel)
    {
        Data equipData = new Data();
        try
        {
            FileStream stream = new FileStream(equipName, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            stream.Close();
            equipData = JsonUtility.FromJson<Data>(Encoding.UTF8.GetString(data));
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        Equip equip = new Equip();
        equip.img = equipData.equipment[equipLevel].img;
        equip.name = equipData.equipment[equipLevel].name;
        equip.type = equipData.equipment[equipLevel].type;
        equip.cls = equipData.equipment[equipLevel].cls;
        equip.maxHP = equipData.equipment[equipLevel].maxHP;
        equip.maxMP = equipData.equipment[equipLevel].maxMP;
        equip.incMP = equipData.equipment[equipLevel].incMP;
        equip.speed = equipData.equipment[equipLevel].speed;
        equip.phyATK = equipData.equipment[equipLevel].phyATK;
        equip.phyDEF = equipData.equipment[equipLevel].phyDEF;
        equip.macATK = equipData.equipment[equipLevel].macATK;
        equip.macDEF = equipData.equipment[equipLevel].macDEF;
        equip.healEFC = equipData.equipment[equipLevel].healEFC;
        equip.critical = equipData.equipment[equipLevel].critical;
        equip.level = equipData.equipment[equipLevel].upgrade;

        return equip;

    }

    public static Equip GetEquip(string equipName)
    {
        return GetEquip(equipName, 0);

        EquipStatus equipStatus = new EquipStatus();

        try
        {
            FileStream stream = new FileStream(equipName, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            stream.Close();
            equipStatus = JsonUtility.FromJson<EquipStatus>(Encoding.UTF8.GetString(data));
        }catch(Exception e)
        {
            Debug.Log(e);
        }

        //Debug.Log(JsonUtility.ToJson(t));
        //JsonUtility.FromJson<EquipStatus>("");
        //FileStream stream = new FileStream(EquipmentFilePath.DataDirectory + EquipmentFilePath.Test, FileMode.Create);
        //byte[] data = Encoding.UTF8.GetBytes(JsonUtility.ToJson(t));
        //stream.Write(data, 0, data.Length);
        //stream.Close();
        //Debug.Log("TTTT");

        //FileStream readStream = new FileStream(EquipmentFilePath.DataDirectory + EquipmentFilePath.Test, FileMode.Open);
        //byte[] readData = new byte[readStream.Length];
        //readStream.Read(readData, 0, readData.Length);
        //readStream.Close();
        //EquipStatus tt = JsonUtility.FromJson<EquipStatus>(Encoding.UTF8.GetString(readData));
        //Debug.Log(tt.cls);
        //Debug.Log(tt.type);
        //Debug.Log(tt.cls == EquipClass.Archer);

        Equip equip = new Equip();
        equip.name = equipStatus.name;
        equip.img = equipStatus.img;
        return equip;
    }

    public static Equip UpgradeEquip(Equip equip)
    {
        
        return GetEquip(equip.img + ".json", equip.level+1);
    }
}
