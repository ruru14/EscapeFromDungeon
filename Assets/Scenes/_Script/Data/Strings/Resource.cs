using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
    private Resource() { }


    public static readonly string FieldBackgroundSoil = "Field/Background/fieldBgSoil";
    public static readonly string FieldBackgroundWater = "Field/Background/fieldBgWater";
    public static readonly string FieldBackgroundFire = "Field/Background/fieldBgFire";
    public static readonly string FieldBackgroundWood = "Field/Background/fieldBgWood";
    public static readonly string FieldBackgroundSteal = "Field/Background/fieldBgSteal";
    public static readonly string FieldElementSoil = "Field/ElementSymbol/fieldElementSoil";
    public static readonly string FieldElementWater = "Field/ElementSymbol/fieldElementWater";
    public static readonly string FieldElementFire = "Field/ElementSymbol/fieldElementFire";
    public static readonly string FieldElementWood = "Field/ElementSymbol/fieldElementWood";
    public static readonly string FieldElementSteal = "Field/ElementSymbol/fieldElementSteal";
    public static readonly string FieldTileSoil = "Field/Tile/fieldTileSoil";
    public static readonly string FieldTileWater = "Field/Tile/fieldTileWater";
    public static readonly string FieldTileFire = "Field/Tile/fieldTileFire";
    public static readonly string FieldTileWood = "Field/Tile/fieldTileWood";
    public static readonly string FieldTileSteal = "Field/Tile/fieldTileSteal";
    public static readonly string ThiefIcon = "Field/Icon/iconThief";
    public static readonly string ArcherIcon = "Field/Icon/iconArcher";
    public static readonly string MagicianIcon = "Field/Icon/iconMagician";
    public static readonly string KnightIcon = "Field/Icon/iconKnight";
    public static readonly string PriestIcon = "Field/Icon/iconPriest";
    public static readonly string ThiefIdle = "battle/Thief/sprite/idle/대기자세1";
    public static readonly string ArcherIdle = "battle/Archer/sprite/idle/궁수대기자세1";
    public static readonly string MagicianIdle = "battle/Mage/sprite/idle/법사대기자세1";
    public static readonly string KnightIdle = "battle/Knight/sprite/idle/대기1";
    public static readonly string PriestIdle = "battle/Priest/sprite/idle/사제대기자세1";

    public static string GetClassImage(string cls)
    {
        string returnValue = "";
        switch (cls)
        {
            case "Knight":
                returnValue = KnightIdle;
                break;
            case "Thief":
                returnValue = ThiefIdle;
                break;
            case "Mage":
                returnValue = MagicianIdle;
                break;
            case "Archer":
                returnValue = ArcherIdle;
                break;
            case "Priest":
                returnValue = PriestIdle;
                break;

        }
        return returnValue;
    }
}
