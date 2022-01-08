using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static Util;

public class ButtonClickEvent : MonoBehaviour
{
    public GameObject[] statPointButtons;
    public List<GameObject> skillButtons;
    public GameObject rebirthButton;

    private Status status;

    private Image masteryPb;

    private void Start()
    {
        statPointButtons = GameObject.FindGameObjectsWithTag("StatPointUIButton");
        skillButtons = GameObject.FindGameObjectsWithTag("SkillButton").ToList();
        foreach(var button in skillButtons)
            button.SetActive(false);
        
        rebirthButton = GameObject.Find("RebirthButton");
        rebirthButton.SetActive(false);

        status = GameObject.Find("StatusManager").GetComponent<Status>();

        masteryPb = GameObject.Find("MasteryProgressBar").GetComponent<Image>();
    }

    private void Update()
    {
        if(status.hardcoreness >= 5 && !skillButtons[0].activeSelf)
            skillButtons[0].SetActive(true);
        else if(status.hardcoreness < 5 && skillButtons[0].activeSelf)
            skillButtons[0].SetActive(false);
        
        if(status.hardcoreness >= 20 && !skillButtons[1].activeSelf)
            skillButtons[1].SetActive(true);
        else if(status.hardcoreness < 20 && skillButtons[1].activeSelf)
            skillButtons[1].SetActive(false);
        
        if(status.hardcoreness >= 50 && !skillButtons[2].activeSelf)
            skillButtons[2].SetActive(true);
        else if(status.hardcoreness < 50 && skillButtons[2].activeSelf)
            skillButtons[2].SetActive(false);
        
        if(status.mastery >= 100 && !rebirthButton.activeSelf)
            rebirthButton.SetActive(true);
        else if(status.mastery < 100 && rebirthButton.activeSelf)
            rebirthButton.SetActive(false);
    }

    public void TacticsButton_Click()
    {
        var tactics = status.tactics;
        var impatience = status.impatience;
        var statPoints = status.statPoints;

        status.tactics    = statPoints >= impatience ? tactics    + impatience : tactics + statPoints;
        status.statPoints = statPoints >= impatience ? statPoints - impatience : 0;
    }

    public void RageButton_Click()
    {
        var rage = status.rage;
        var impatience = status.impatience;
        var statPoints = status.statPoints;

        status.rage       = statPoints >= impatience ? rage       + impatience : rage + statPoints;
        status.statPoints = statPoints >= impatience ? statPoints - impatience : 0;
    }

    public void NeeteryButton_Click()
    {
        var neetery = status.neetery;
        var impatience = status.impatience;
        var statPoints = status.statPoints;

        status.neetery    = statPoints >= impatience ? neetery    + impatience : neetery + statPoints;
        status.statPoints = statPoints >= impatience ? statPoints - impatience : 0;
    }

    public void BossinessButton_Click()
    {
        var bossiness = status.bossiness;
        var impatience = status.impatience;
        var statPoints = status.statPoints;

        status.bossiness  = statPoints >= impatience ? bossiness  + impatience : bossiness + statPoints;
        status.statPoints = statPoints >= impatience ? statPoints - impatience : 0;
    }

    public void ImpatienceButton_Click()
    {
        var impatience = status.impatience;
        var statPoints = status.statPoints;

        status.impatience = statPoints >= impatience ? impatience + impatience : impatience + statPoints;
        status.statPoints = statPoints >= impatience ? statPoints - impatience : 0;
    }

    public void HardcorenessButton_Click()
    {
        var hardcoreness = status.hardcoreness;
        var statPoints = status.statPoints;

        status.hardcoreness = statPoints >= 10000 ? hardcoreness + 1 : hardcoreness;
        status.statPoints   = statPoints >= 10000 ? statPoints - 10000 : statPoints;
    }

    public void MasteryButton_Click()
    {
        status.curMasteryXp += status.curXp;
        status.curXp       =  0;

        if(status.curMasteryXp >= status.nextMasteryXp) {
            var curMXp = status.curMasteryXp;
            var mastery = status.mastery;
            var mCutCnt = status.masteryCutCount;

            var masteryStack = 0;

            for(var l=mastery; ; l++) {
                var nextMXp = GetNextMasteryXp(l, mCutCnt);

                if(nextMXp >= curMXp) {
                    status.curMasteryXp  =  curMXp;
                    status.mastery       += masteryStack;
                    status.nextMasteryXp =  GetNextMasteryXp(status.mastery, status.masteryCutCount);
                    break;
                }
                
                curMXp -= nextMXp;
                masteryStack++;
            }
        }

        masteryPb.fillAmount = (float)(status.curMasteryXp / status.nextMasteryXp);
    }

    public void Skill1_Click()
    {
        if(status.hardcoreness < 5) return;
        
        status.statPoints   += status.impatience - 1;
        status.impatience   =  1;
        status.hardcoreness -= 5;
    }

    public void Skill2_Click()
    {
        if(status.hardcoreness < 20) return;

        var curXp = status.curXp;
        var level = status.level;

        var levelStack = 0uL;

        for(var l=level; ; l++) {
            var nextXp = GetNextXp(l);

            if(nextXp >= curXp) {
                status.curXp        =  curXp;
                status.level        += levelStack;
                status.statPoints   += (ulong) (3 + status.rebirths) * levelStack;
                status.hardcoreness -= 20;
                break;
            }
            
            curXp -= nextXp;
            levelStack++;
        }
    }

    public void Skill3_Click()
    {
        if(status.hardcoreness < 50) return;
        
        status.masteryCutCount++;
        status.nextMasteryXp =  GetNextMasteryXp(status.mastery, status.masteryCutCount);
        masteryPb.fillAmount =  (float)(status.curMasteryXp / status.nextMasteryXp);
        status.hardcoreness  -= 50;
    }

    public void Rebirth_Click()
    {
        //애니메이션 추가

       
        status.rebirths++;
        status.rebirthUIText = $"Rebirths: {status.rebirths}";

        status.level      = 1;
        status.curXp      = 0;
        status.nextXp     = 500;
        status.totalDmg   = 0;
        status.statPoints = 0;

        status.tactics    = 5 + status.rebirths;
        status.rage       = 5 + status.rebirths;
        status.neetery    = 5 + status.rebirths;
        status.bossiness  = 5 + status.rebirths;
        status.impatience = 1;
        
        status.mastery         = 0;
        status.masteryCutCount = 0;
        status.curMasteryXp    = 0;
        status.nextMasteryXp   = GetNextMasteryXp(status.mastery, status.masteryCutCount);
        masteryPb.fillAmount   = 0;
    }
}
