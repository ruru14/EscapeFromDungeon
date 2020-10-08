using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillName
{
    private SkillName() { }


    public static readonly string SkillDirectory = "./Assets/Scenes/Resources/Script/Skill/characterSkill";


    public static string GetSkillNameFile(string skillName, string cls, SKILLTYPE skillType)
    {
        skillName = skillName.Replace(" ", "");
        skillName = char.ToLower(skillName[0]) + skillName.Substring(1);
        cls = char.ToLower(cls[0]) + cls.Substring(1);
        string type;
        if(skillType == SKILLTYPE.ACT)
        {
            type = "active";
        }
        else
        {
            type = "passive";
        }

        return "/" + cls + "Skill/" + type + "/" + skillName + ".json";
    }

    public class Thief
    {
        public class Active
        {
            public static readonly string Assassination = "/assassination.json";
            //
            public static readonly string BackStab = "/backStab.json";
            //Throwing이 필요할까?
            public static readonly string ThrowingPoisonDart = "/throwingPoisonDart.json";
            //갈증 -> Thirsty
            public static readonly string Bloodthirsthy = "/bloodthirsthy.json";
            public static readonly string Vengeance = "/vengeance.json";
        }

        public class Passive
        {
            public static readonly string PrecedingMovement = "/precedingMovement.json";
            //날카로운? -> KeenEyes
            public static readonly string SharpEye = "/keenEye.json";
            public static readonly string Confusion = "/confusion.json";
            public static readonly string ExplosiveTrap = "/explosiveTrap.json";
            public static readonly string FinalBlow = "/finalBlow.json";
        }
    }

    public class Archer
    {
        public class Active
        {
            public static readonly string DoubleShot = "/doubleShot.json";
            public static readonly string PowerShot = "/powerShot.json";
            public static readonly string MultiShot = "/multiShot.json";
            public static readonly string BleedingShot = "/bleedingShot.json";
            public static readonly string MagicShot = "/magicShot.json";
        }

        public class Passive
        {
            public static readonly string SharpEye = "/sharpEye.json";
            public static readonly string CapturingWeakness = "/capturingWeakness.json";
            public static readonly string HeadStart = "/headStart.json";
            public static readonly string FinishAttack = "/finishAttack.json";
            //집중? -> ConcentratedShot
            public static readonly string ConcentratedFire = "/concentratedFire.json";
        }
    }
    public class Mage
    {
        public class Active
        {
            public static readonly string FireBall = "/fireBall.json";
            public static readonly string MagicArrow = "/magicArrow.json";
            //ManaBomb?
            public static readonly string ManaBombing = "/manaBombing.json";
            //ManaCharge?
            public static readonly string ManaCharging = "/manaCharging.json";
            public static readonly string Dispel = "/dispel.json";
        }

        public class Passive
        {
            public static readonly string ManaDrain = "/manaDrain.json";
            public static readonly string Enlightenment = "/enlightment.json";
            //유리대포...?
            public static readonly string GlassCannon = "/glassCannon.json";
            public static readonly string ManaCyclone = "/manaCyclone.json";
            public static readonly string EfficientCasting = "/efficientCasting.json";
        }
    }
    public class Knight
    {
        public class Active
        {
            public static readonly string PowerBash = "/powerBash.json";
            public static readonly string DefenseMode = "/defenseMode.json";
            //도발? -> proboke
            public static readonly string Taunt = "/taunt.json";
            //분노? -> rage / enrage
            public static readonly string Fury = "/fury.json";
            public static readonly string CounterAttack = "/counterAttack.json";
        }

        public class Passive
        {
            public static readonly string Enduring = "/enduring.json";
            public static readonly string FastGuard = "/fastGuard.json";
            public static readonly string SkillfulGuard = "/skillfulGuard.json";
            //광전사? -> berserk
            public static readonly string MadWarrior = "/madWarrior.json";
            public static readonly string EfficientDefense = "/efficientDefense.json";
            //InnerStrength
        }
    }
    public class Priest
    {
        public class Active
        {
            public static readonly string Healing = "/healing.json";
            public static readonly string TheGraceOfProtection = "/theGraceOfProtection.json";
            public static readonly string TheGraceOfCourage = "/theGraceOfCourage.json";
            public static readonly string Sanctuary = "/sanctuary.json";
            public static readonly string Resurrection = "/resurrection.json";
        }

        public class Passive
        {
            public static readonly string Faithful = "/faithful.json";
            public static readonly string CallingOfAngel = "/callingOfAngel.json";
            public static readonly string AgentOfAngel = "/agentOfAngel.json";
            public static readonly string SacredLight = "/sacredLight.json";
            public static readonly string Concentration = "/concentration.json";
            //Benediction(기도)
        }
    }
}
