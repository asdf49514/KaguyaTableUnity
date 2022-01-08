using System;
using System.Collections;
using UnityEngine;
using static Util;
using Random = UnityEngine.Random;

public class HitRepetition : MonoBehaviour
{
    private int curFrame = -1;
    //private WaitForSeconds delayPerImgFrame;

    private Status status;

    private Sprite[] kaguyaSprites;

    private SpriteRenderer sr;

    private SaveLoadData slData;
    void Start()
    {
        QualitySettings.vSyncCount  = 0;
        Application.targetFrameRate = 24;

        //delayPerImgFrame = new WaitForSeconds(Time.deltaTime);
        
        status = GameObject.Find("StatusManager").GetComponent<Status>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        kaguyaSprites = Resources.LoadAll<Sprite>("Images/Sprites/DefineSprite_9");
        
        slData = GameObject.Find("StatusDataManager").GetComponent<SaveLoadData>();

        StartCoroutine(OneHit());
    }

    private IEnumerator OneHit()
    {
        while(true) {
            if(curFrame == 4) {
                curFrame = 0;

                slData.SyncStatusData();
                
                UpdateStatus();
            } 
            else curFrame += 1;
            
            sr.sprite = kaguyaSprites[curFrame];
            //Debug.Log($"{Time.deltaTime}");
            yield return null;
        }
    }


    private void UpdateStatus()
    {
        var tactics = status.tactics;
        var rage = status.rage;
        var neetery = status.neetery;
        var hardcoreness = status.hardcoreness;

        var damage = GetDamagePerHit(tactics, rage, hardcoreness);
        var exp = GetExpPerHit(neetery, hardcoreness, damage);

        status.hits     += 1;
        status.totalDmg += Math.Max(1, damage);
        status.curXp    += Math.Max(1, exp);

        if(status.curXp >= status.nextXp) {
            status.level      += 1;
            status.curXp      =  Math.Max(0, status.curXp - status.nextXp);
            status.nextXp     =  GetNextXp(status.level);
            status.statPoints += 3 + status.rebirths;
        }
    }
}
