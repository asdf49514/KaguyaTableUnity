using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    public ulong hits = 0;
    public ulong level = 1;
    public double curXp = 0;
    public double nextXp = 250;
    public double totalDmg = 0;
    public double statPoints = 0;

    public double tactics = 5;
    public double rage = 5;
    public double neetery = 5;
    public double bossiness = 5;
    public double impatience = 1;
    public double hardcoreness = 0;
    
    public int mastery = 0;
    public double curMasteryXp = 0;
    public double nextMasteryXp = 1000000000;
    public int masteryCutCount = 0;

    public int rebirths = 0;
    public string rebirthUIText = "";

    public List<GameObject> statusStatPointUI;


    private void Start()
    {
        mastery         = 0;
        curMasteryXp    = 0;
        nextMasteryXp   = Math.Pow(10, 9);
        masteryCutCount = 0;

        rebirths = 0;
        
        
        var statusUI = GameObject.FindGameObjectsWithTag("StatusUI");
        var statPointUI = GameObject.FindGameObjectsWithTag("StatPointUI");

        statusStatPointUI.AddRange(statusUI);
        statusStatPointUI.AddRange(statPointUI);
    }

    private void Update()
    {
        UpdateStatusUI();
    }

    private void UpdateStatusUI()
    {
        foreach(var ui in statusStatPointUI) {
            var uiText = ui.GetComponent<Text>();

            uiText.text = ui.name switch {
                "Hits"         => $"{hits} Hits",
                "Level"        => $"Level {level}",
                "XpCurrent"    => $"{Math.Round(curXp)} XP  ",
                "XpNext"       => $"{Math.Round(nextXp)} Next",
                "TotalDamage"  => $"{Math.Round(totalDmg)} Total Damage",
                "StatPoints"   => $"{Math.Round(statPoints)}  Stat Points",
                "Tactics"      => $"Tactics: {tactics}",
                "RAGE!!!"      => $"RAGE!!!: {rage}",
                "NEETery"      => $"NEETery: {neetery}",
                "Bossiness"    => $"Bossiness: {bossiness}",
                "Impatience"   => $"Impatience: {impatience}",
                "Hardcoreness" => $"Hardcoreness: {hardcoreness}",
                "Mastery"      => $"Mastery: {mastery}",
                "Rebirth"      => rebirthUIText,
                _              => throw new ArgumentOutOfRangeException($"Invalid value: \'{ui.name}\'")
            };
        }
    }
}
