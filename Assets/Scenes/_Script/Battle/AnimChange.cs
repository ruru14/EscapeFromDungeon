using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimChange
{
    public Animator animator;
    AnimatorOverrideController overrideController = new AnimatorOverrideController();

    public List<AnimationClipOverride> GetNormalEnemyClips(string shape, string type) {
        List<AnimationClipOverride> tmp = new List<AnimationClipOverride>();

        AnimationClipOverride clip = new AnimationClipOverride();
        clip.clipNamed = "idle";
        clip.findClip(shape, type);
        tmp.Add(clip);

        clip = new AnimationClipOverride();
        clip.clipNamed = "normalAttack";
        clip.findClip(shape, type);
        tmp.Add(clip);

        clip = new AnimationClipOverride();
        clip.clipNamed = "act1";
        clip.findClip(shape, type);
        tmp.Add(clip);

        clip = new AnimationClipOverride();
        clip.clipNamed = "act2";
        clip.findClip(shape, type);
        tmp.Add(clip);

        clip = new AnimationClipOverride();
        clip.clipNamed = "act4";
        clip.findClip(shape, type);
        tmp.Add(clip);

        clip = new AnimationClipOverride();
        clip.clipNamed = "act5";
        clip.findClip(shape, type);
        tmp.Add(clip);

        clip = new AnimationClipOverride();
        clip.clipNamed = "act6";
        clip.findClip(shape, type);
        tmp.Add(clip);

        clip = new AnimationClipOverride();
        clip.clipNamed = "act7";
        clip.findClip(shape, type);
        tmp.Add(clip);


        return tmp;
    }

    public List<AnimationClipOverride> GetEliteClips(int floor, string type)
    {
        List<AnimationClipOverride> tmp = new List<AnimationClipOverride>();

        AnimationClipOverride clip = new AnimationClipOverride();
        clip.clipNamed = "idle";
        clip.findClip(floor, type);
        tmp.Add(clip);

        clip = new AnimationClipOverride();
        clip.clipNamed = "normalAttack";
        clip.findClip(floor, type);
        tmp.Add(clip);

        clip = new AnimationClipOverride();
        clip.clipNamed = "act1";
        clip.findClip(floor, type);
        tmp.Add(clip);

        clip = new AnimationClipOverride();
        clip.clipNamed = "act2";
        clip.findClip(floor, type);
        tmp.Add(clip);

        if (floor%20 == 1)
        {
            clip = new AnimationClipOverride();
            clip.clipNamed = "act3";
            clip.findClip(floor, type);
            tmp.Add(clip);
        }

        return tmp;
    }

    public List<AnimationClipOverride> GetSubEnemyClips(string boss, int num, string type)
    {
        List<AnimationClipOverride> tmp = new List<AnimationClipOverride>();

        AnimationClipOverride clip = new AnimationClipOverride();
        clip.clipNamed = "idle";
        clip.findClip(boss, num, type);
        tmp.Add(clip);

        clip = new AnimationClipOverride();
        clip.clipNamed = "normalAttack";
        clip.findClip(boss, num, type);
        tmp.Add(clip);

        clip = new AnimationClipOverride();
        clip.clipNamed = "act1";
        clip.findClip(boss, num, type);
        tmp.Add(clip);

        clip = new AnimationClipOverride();
        clip.clipNamed = "act2";
        clip.findClip(boss, num, type);
        tmp.Add(clip);

        return tmp;
    }

    // Use this for initialization
    public void Init(List<AnimationClipOverride> clipOverrides)
    {
        overrideController.runtimeAnimatorController = animator.runtimeAnimatorController;

        foreach (AnimationClipOverride clipOverride in clipOverrides)
        {
            overrideController[clipOverride.clipNamed] = clipOverride.overrideWith;
        }

        animator.runtimeAnimatorController = overrideController;
    }

    public void Init(AnimationClipOverride clipOverride)
    {
        overrideController.runtimeAnimatorController = animator.runtimeAnimatorController;

        overrideController[clipOverride.clipNamed] = clipOverride.overrideWith;

        animator.runtimeAnimatorController = overrideController;
    }
}

public class AnimationClipOverride
{
    public string clipNamed;
    public AnimationClip overrideWith;

    public void findClip(string path)
    {
        overrideWith = Resources.Load<AnimationClip>(path);
    }

    public void findClip(string shape, string type)
    {
        findClip("battle/Enemies/" + shape + "/" + type + "/" + clipNamed + "/" + clipNamed);
    }

    public void findClip(int floor, string type)
    {
        if (floor == 1)
            findClip("battle/Enemies/BossMain/" + type + "/01/" + clipNamed + "/" + clipNamed); //보스
        else if (floor % 20 == 1)
            findClip("battle/Enemies/BossMain/" + type + "/" + floor + "/" + clipNamed + "/" + clipNamed); //보스
        else
            findClip("battle/Enemies/BossMid/" + type + "/" + floor + "/" + clipNamed + "/" + clipNamed); //중간보스
    }

    public void findClip(string boss, int num, string type) {
        findClip("battle/Enemies/" + boss + num + "/" + type + "/" + clipNamed + "/" + clipNamed);
    }
}