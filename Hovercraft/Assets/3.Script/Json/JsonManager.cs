using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class GameSettingData
{
    public bool useUnityOnTop;
    public float endTime = 90f;
    public float restTime = 30f;
    public float changeTime = 5f;
    public float blinkDelay = 3.0f;
}
public class GameDynamicData
{
    public string[] readyText_F;/*= "지금은 체험이가능합니다";*/
    public string[] readyText_B;/*= "안전벨트를 착용해주세요";*/
    public string[] waitText_F;/* "다른체험자가 준비중입니다";*/
    public string[] waitText_B; /* "시작버튼을 눌러주세요";*/
    public string[] playText_F; /* "체험중입니다  80초";*/
    public string[] playText_B;/* "버튼을이용해 움직여보세요";*/
    public string[] endText_F;  /*"체험이 종료되었습니다";*/
    public string[] endText_B;  /*"다음체험자를위해 퇴장";*/
}
public class PortJson
{
    public string com = "COM4";
    public int baudLate = 19200;
}

public class JsonManager : MonoBehaviour
{

    public static JsonManager instance;
    public GameSettingData gameSettingData;
    public PortJson portJson;
    public GameDynamicData gameDynamicData;
    private string gameDataPath;
    private string gameDynamicDataPath;
    private string portPath;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        InitAraay();
        portPath = Path.Combine(Application.streamingAssetsPath, "port.json");
        gameDynamicDataPath = Path.Combine(Application.streamingAssetsPath, "Setting.json");
        gameDataPath = Path.Combine(Application.persistentDataPath, "gameSettingData.json");

        gameSettingData = LoadData(gameDataPath, gameSettingData);
        gameDynamicData = LoadData(gameDynamicDataPath, gameDynamicData);
        portJson= LoadData(portPath, portJson);
    }

    //저장할 json 객체 , 경로설정
    public static void SaveData<T>(T jsonObject, string path) where T : new()
    {
        if (jsonObject == null)
            jsonObject = new T();  // 기본 생성자로 객체 초기화
        string json = JsonUtility.ToJson(jsonObject, true);
        File.WriteAllText(path, json);
        Debug.Log($"저장됨: {path}");
    }

    public static T LoadData<T>(string path, T data) where T : new()
    {
        {
            if (!File.Exists(path))
            {
                Debug.LogWarning("JSON 파일이 존재하지 않습니다.");
                SaveData(data, path);
            }
            Debug.Log("JSON로드");
            string json = File.ReadAllText(path);
            T jsonData = JsonUtility.FromJson<T>(json);
            return jsonData;
        }

        //예시 실행코드
        //JsonManager.LoadData(파일경로 , 데이터클래스);

    }
    private void InitAraay()
    {

        gameDynamicData = new GameDynamicData();
        gameDynamicData.readyText_F = new string[] { "지금은 체험이가능합니다", };/*= "지금은 체험이가능합니다";*/
        gameDynamicData.readyText_B = new string[] { "안전벨트를 착용해주세요", };/*= "안전벨트를 착용해주세요";*/
        gameDynamicData.waitText_F = new string[] { "다른체험자가 준비중입니다", "잠시만 기다려주세요" };/* "다른체험자가 준비중입니다";*/
        gameDynamicData.waitText_B = new string[] { "시작버튼을 눌러주세요", }; /* "시작버튼을 눌러주세요";*/
        gameDynamicData.playText_F = new string[] { "체험중입니다  " }; /* "체험중입니다  80초";*/
        gameDynamicData.playText_B = new string[] { "버튼을이용해 움직여보세요",  };/* "버튼을이용해 움직여보세요";*/
        gameDynamicData.endText_F = new string[] { "체험이 종료되었습니다", "잠시후 체험이시작됩니다" };  /*"체험이 종료되었습니다";*/
        gameDynamicData.endText_B = new string[] { "체험이 종료되었습니다", "다음체험자를위해 퇴장해주세요" };  /*"다음체험자를위해 퇴장";*/
    }
}
