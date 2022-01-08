using System.IO;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SaveLoadData : MonoBehaviour
{
    private Status status;
    private Image masteryPb;
    //싱글턴 선언
    private static GameObject Container { get; set; }

    private static SaveLoadData _instance;
    private static SaveLoadData Instance
    {
        get {
            if(!_instance) {
                Container = new GameObject { name = "SaveLoadData" };
                _instance  = Container.AddComponent(typeof(SaveLoadData)) as SaveLoadData;
                DontDestroyOnLoad(Container);
            }

            return _instance;
        }
    }
    
    
    // 게임 데이터 파일이름 설정
    public string gameDataFileName = "KaguyaTableSavData.json";

    public StatusData statusData;
    private StatusData StatusData
    {
        get {
            //게임이 시작되면 자동으로 실행되도록
            if(statusData == null) {
                LoadStatusData();
                SaveStatusData();
            }

            return statusData;
        }
    }

    private void LoadStatusData()
    {
        var filePath = Application.persistentDataPath + gameDataFileName;

        if(File.Exists(filePath)) {
            var fromJsonData = File.ReadAllText(filePath);
            statusData = JsonUtility.FromJson<StatusData>(fromJsonData);
            Debug.Log("세이브 불러오기 완료");
        }
        else {
            InitStatusValues();
            
            statusData = new StatusData();
            InitStatusDataValues();
            Debug.Log("새로운 세이브 파일 생성 완료");
        }
        
    }

    private void SaveStatusData()
    {
        var toJsonData = JsonUtility.ToJson(statusData);
        var filePath = Application.persistentDataPath + gameDataFileName;
        File.WriteAllText(filePath, toJsonData);
        Debug.Log("저장 완료");
    }


    private void Start()
    {
        status    = GameObject.Find("StatusManager").GetComponent<Status>();
        masteryPb = GameObject.Find("MasteryProgressBar").GetComponent<Image>();
        
        LoadStatusData();
        SaveStatusData();

        status.hits       = statusData.hits;
        status.level      = statusData.level;
        status.curXp      = statusData.curXp;
        status.nextXp     = statusData.nextXp;
        status.totalDmg   = statusData.totalDmg;
        status.statPoints = statusData.statPoints;

        status.tactics      = statusData.tactics;
        status.rage         = statusData.rage;
        status.neetery      = statusData.neetery;
        status.bossiness    = statusData.bossiness;
        status.impatience   = statusData.impatience;
        status.hardcoreness = statusData.hardcoreness;
        
        status.mastery         = statusData.mastery;
        status.curMasteryXp    = statusData.curMasteryXp;
        status.nextMasteryXp   = statusData.nextMasteryXp;
        status.masteryCutCount = statusData.masteryCutCount;
        masteryPb.fillAmount   = statusData.masteryPbFillAmount;
        
        status.rebirths      = statusData.rebirths;
        status.rebirthUIText = statusData.rebirthUIText;
    }


    private void OnDisable()
    {
        SyncStatusData();

        SaveStatusData();
    }
    
    private void OnApplicationQuit()
    {
        SyncStatusData();   

        SaveStatusData();
    }


    private void InitStatusValues()
    {
        status.hits       = 1;
        status.level      = 1;
        status.curXp      = 0;
        status.nextXp     = 500;
        status.totalDmg   = 0;
        status.statPoints = 0;

        status.tactics      = 5;
        status.rage         = 5;
        status.neetery      = 5;
        status.bossiness    = 5;
        status.impatience   = 1;
        status.hardcoreness = 0;
        
        status.mastery         = 0;
        status.curMasteryXp    = 0;
        status.nextMasteryXp   = 1000000000;
        status.masteryCutCount = 0;
        masteryPb.fillAmount   = 0;
        
        status.rebirths      = 0;
        status.rebirthUIText = "";
    }

    private void InitStatusDataValues()
    {
        statusData.hits       = 1;
        statusData.level      = 1;
        statusData.curXp      = 0;
        statusData.nextXp     = 500;
        statusData.totalDmg   = 0;
        statusData.statPoints = 0;

        statusData.tactics      = 5;
        statusData.rage         = 5;
        statusData.neetery      = 5;
        statusData.bossiness    = 5;
        statusData.impatience   = 1;
        statusData.hardcoreness = 0;
        
        statusData.mastery         = 0;
        statusData.curMasteryXp    = 0;
        statusData.nextMasteryXp   = 1000000000;
        statusData.masteryCutCount = 0;
        masteryPb.fillAmount   = 0;
        
        statusData.rebirths      = 0;
        statusData.rebirthUIText = "";
    }

    /*public void SyncStatusData()  //==> todo HitRepetition에서 일정 프레임마다 수행할 수 있도록 한다
    {
        _instance.statusData.hits       = status.hits;
        _instance.statusData.level      = status.level;
        _instance.statusData.curXp      = status.curXp;
        _instance.statusData.nextXp     = status.nextXp;
        _instance.statusData.totalDmg   = status.totalDmg;
        _instance.statusData.statPoints = status.statPoints;
        
        _instance.statusData.tactics      = status.tactics;
        _instance.statusData.rage         = status.rage;
        _instance.statusData.neetery      = status.neetery;
        _instance.statusData.bossiness    = status.bossiness;
        _instance.statusData.impatience   = status.impatience;
        _instance.statusData.hardcoreness = status.hardcoreness;
        
        _instance.statusData.mastery             = status.masteryCutCount;
        _instance.statusData.curMasteryXp        = status.curMasteryXp;
        _instance.statusData.nextMasteryXp       = status.nextMasteryXp;
        _instance.statusData.masteryCutCount     = status.masteryCutCount;
        _instance.statusData.masteryPbFillAmount = masteryPb.fillAmount;
        
        _instance.statusData.rebirths      = status.rebirths;
        _instance.statusData.rebirthUIText = status.rebirthUIText;
    }*/
    
    public void SyncStatusData() //==> todo HitRepetition에서 일정 프레임마다 수행할 수 있도록 한다
    {
        statusData.hits       = status.hits;
        statusData.level      = status.level;
        statusData.curXp      = status.curXp;
        statusData.nextXp     = status.nextXp;
        statusData.totalDmg   = status.totalDmg;
        statusData.statPoints = status.statPoints;
        
        statusData.tactics      = status.tactics;
        statusData.rage         = status.rage;
        statusData.neetery      = status.neetery;
        statusData.bossiness    = status.bossiness;
        statusData.impatience   = status.impatience;
        statusData.hardcoreness = status.hardcoreness;
        
        statusData.mastery             = status.masteryCutCount;
        statusData.curMasteryXp        = status.curMasteryXp;
        statusData.nextMasteryXp       = status.nextMasteryXp;
        statusData.masteryCutCount     = status.masteryCutCount;
        statusData.masteryPbFillAmount = masteryPb.fillAmount;
        
        statusData.rebirths      = status.rebirths;
        statusData.rebirthUIText = status.rebirthUIText;
    }
}
