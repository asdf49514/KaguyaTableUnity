using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatusData
{
    public ulong hits;
    public ulong level;
    public double curXp;
    public double nextXp;
    public double totalDmg;
    public double statPoints;

    public double tactics;
    public double rage;
    public double neetery;
    public double bossiness;
    public double impatience;
    public double hardcoreness;
    
    public int mastery;
    public double curMasteryXp;
    public double nextMasteryXp;
    public int masteryCutCount;
    public float masteryPbFillAmount;

    public int rebirths;
    public string rebirthUIText = "";
}
