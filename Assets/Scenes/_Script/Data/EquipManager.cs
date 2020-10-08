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

    private class EquipStatus
    {
        public string img; // 장비 이미지 URL
        public string name; // 장비이름
        public EquipType type; // 장비종류 : 투구 - 갑옷 - 신발 - 무기 - 보조무기
        public EquipClass cls; // 직업제한 : 도적 - 궁수 - 법사 - 기사 - 사제
        public EquipGrade grade; // 장비등급 : 노말 - 레어 - 유니크 - 미스틱
        public float maxHP; // 체력
        public float maxMP; // 마나
        public float incMP; // 마나회복
        public float incAP; // 속도회복
        public float phyATK; // 물리공격력
        public float phyDEF; // 물리방어력
        public float macATK; // 마법공격력
        public float macDEF; // 마법방어력
        public float healEFC; // 회복효율
        public float critical; // 치명타확률
    }

    public static Equip GetEquip(string equipName)
    {
        EquipStatus equipStatus = new EquipStatus();

        try
        {
            FileStream stream = new FileStream(EquipmentFilePath.DataDirectory + equipName, FileMode.Open, FileAccess.Read, FileShare.Read);
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
}
