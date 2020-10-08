using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;


[System.Serializable]
public class SkillExplain
{
    public string skillName;
    public string img;
    public string ability;
    public string explain;
    public int cost;
    public int upgrade;
}


public class SkillParser
{
    private SkillParser()
    {

    }

    [System.Serializable]
    private class Data
    {
        public SkillExplain[] skill;
    }

    public static SkillExplain GetSkill(string skillName, int skillLevel)
    {
        Data skillData = new Data();

        try
        {
            FileStream stream = new FileStream(SkillName.SkillDirectory + skillName, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            stream.Close();
            skillData = JsonUtility.FromJson<Data>(Encoding.UTF8.GetString(data));
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        

        return skillData.skill[skillLevel];
    }
}
