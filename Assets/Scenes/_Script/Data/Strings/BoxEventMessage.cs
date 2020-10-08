using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxEventMessage
{
    private BoxEventMessage() { }

    public static readonly string HpHeal = "HP Heal";
    public static readonly string MpHeal = "MP Heal";
    public static readonly string MapOpen = "Map Open";
    public static readonly string GetEquipment = "Get Equipment";
    public static readonly string GetScrollDecomp = "Get Scroll Decomposition";
    public static readonly string GetScrollEquip = "Get Scroll Equipment";
    public static readonly string GetScrollSkill = "Get Scroll Skill";
    public static readonly string GetScrollStat = "Get Scroll Status";
    public static readonly string GetKey = "Get Key";
    public static readonly string UpgradeEquipment = "Upgrade Equipment";
    public static readonly string UpgradeSkill = "Upgrade Skill";
}
