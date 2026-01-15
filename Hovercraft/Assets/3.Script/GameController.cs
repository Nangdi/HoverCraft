using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Mode
{
    Ready,
    Wait,
    play,
    End
}

public class GameController : MonoBehaviour
{
    [SerializeField] private TextScroller scroller;

    public Mode mode = Mode.Ready;
    GameDynamicData data;
    GameSettingData settingData;
    public float lapseTimer;
    public float changeTimer;
    public TMP_Text backText;
    public TMP_Text backText1;
    public TMP_Text frontText;
    public TMP_Text frontText1;
    public Image[] colorImage;
    public Image[] cashingColor;
    public int index =0;

    [Header("SettingValue")]
    float endTime = 60;
    float restTime = 30f;
    float changeTime = 5f;
    int returnNum = 7;
    public void SetMode(Mode _mode)
    {
        mode = _mode;
        index = 0;
        changeTimer = 0;
        UpdateText();
        SetColor();
        initData();
    }
    //대기상태에서 RW받으면 Wait모드 , RP받으면 Play모드 , Play모드에선 90초 타이머 , 타이머종료후 End모드 30초대기시간 , 대기시간 후 Ready모드

    private void Start()
    {
        data = JsonManager.instance.gameDynamicData;
        SetMode(Mode.Ready);
    }
    private void initData()
    {
        settingData = JsonManager.instance.gameSettingData;
        restTime = settingData.restTime;
        endTime = settingData.endTime;
        changeTime = settingData.changeTime;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetMode(Mode.Ready);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            SetMode(Mode.Wait);
        }
        else if (Input.GetKeyDown(KeyCode.E)) 
        { 
            SetMode(Mode.play);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        { 
            SetMode(Mode.End);
        }

        //changeTimer += Time.deltaTime;
        //if(changeTimer >= changeTime)
        //{
        //    changeTimer = 0;
        //    index++;
        //    UpdateText();
        //}

        

        if (mode == Mode.Wait || mode == Mode.Ready)
        {
            return;
        }

        lapseTimer += Time.deltaTime;
        if (mode == Mode.play)
        {
            frontText.text = ConvertPlayText(data.playText_F[0], endTime);
            frontText1.text = ConvertPlayText(data.playText_F[0], endTime);
            backText.text = ConvertPlayText(data.playText_B[0], endTime);
            backText1.text = ConvertPlayText(data.playText_B[0], endTime);

            if (lapseTimer >= endTime)
            {
                SetMode(Mode.End);
                lapseTimer = 0;
            Debug.Log("체험시간종료");
            }
        }
        if (mode == Mode.End )
        {
            frontText.text = ConvertPlayText(data.endText_F[0], restTime);
            frontText1.text = ConvertPlayText(data.endText_F[1], restTime);
            backText.text = ConvertPlayText(data.endText_B[0], restTime);
            backText1.text = ConvertPlayText(data.endText_B[1], restTime);

            if (lapseTimer >= restTime)
            {
                SetMode(Mode.Ready);
                lapseTimer = 0;
            Debug.Log("휴식시간종료");
            }

        }
        //텍스트 2가지 번갈아가면서
        // 텍스트 송출 후 80
    }

    public void EndCraft()
    {

    }
    private void UpdateText()
    {

        switch (mode)
        {
            case Mode.Ready:
                backText.text = ConvertToVerticalText(data.readyText_B[0]);
                backText1.text = ConvertToVerticalText(data.readyText_B[0]);
                frontText.text = ConvertToVerticalText(data.readyText_F[0]);
                frontText1.text = ConvertToVerticalText(data.readyText_F[1]);
                break;
            case Mode.Wait:
                backText.text = ConvertToVerticalText(data.waitText_B[0]);
                backText1.text = ConvertToVerticalText(data.waitText_B[0]);
                frontText.text = ConvertToVerticalText(data.waitText_F[0]);
                frontText1.text = ConvertToVerticalText(data.waitText_F[1]);
                break;
            case Mode.play:
                backText.text = ConvertToVerticalText(data.playText_B[0]);
                backText1.text = ConvertToVerticalText(data.playText_B[0]);
                frontText.text = ConvertToVerticalText(data.playText_F[0]);
                frontText1.text = ConvertToVerticalText(data.playText_F[0]);
                break;
            case Mode.End:
                backText.text = ConvertToVerticalText(data.endText_B[0]);
                backText1.text = ConvertToVerticalText(data.endText_B[1]);
                frontText.text = ConvertToVerticalText(data.endText_F[0]);
                frontText1.text = ConvertToVerticalText(data.endText_F[1]);
                //lapseTimer = 0;
                break;
        }
        scroller.UpdateTextInfo();
    }
    private string ConvertToVerticalText(string texts)
    {
        //int tempIndex = index % texts.Length;
        //string currentString = texts[tempIndex];
        string tempSting = "";
        string currentString = texts;
        foreach (var item in currentString)
        {
            if (item == ' ')
            {
                tempSting += "<size=50%> </size>\n";
            }
            else
            {

            tempSting += item + "\n";
            }
        }
        //tempSting.Replace(" \n", "<size=50%> </size>\n");

        return tempSting;
    }
    private string ConvertPlayText(string text  ,float time)
    {
        string tempSting = "";


        //string currentString = texts[0];

        foreach (var item in text)
        {
            if (item == ' ')
            {
                tempSting += "<size=50%> </size>\n";
            }
            else
            {

                tempSting += item + "\n";
            }
        }
        tempSting += $"<color=#FFFE00>{time - (int)lapseTimer}</color>\n초";
        return tempSting;
    }
    private void SetColor()
    {
        colorImage[0].color = cashingColor[(int)mode].color;
        colorImage[1].color = cashingColor[(int)mode].color;
    }

}
