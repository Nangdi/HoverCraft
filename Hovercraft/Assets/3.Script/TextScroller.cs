using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
[Serializable]
public class TextOB
{
    public float returnPos ;
    public TMP_Text text;
    public RectTransform textRect;
    public int lenght;
    public bool isPlay = true;
}


public class TextScroller : MonoBehaviour
{
    public GameController controller;
    [SerializeField]
    private TextOB[] textOBs = new TextOB[4];
    public float scrollSpeed = 100;
    public float targetY;
    public Vector2 returnPos;
    public Vector2[] beginningPos = new Vector2[2];

    private void Start()
    {
        beginningPos = new Vector2[2];
        beginningPos[0] = textOBs[0].textRect.anchoredPosition;
        beginningPos[1] = textOBs[1].textRect.anchoredPosition;
        scrollSpeed = JsonManager.instance.gameSettingData.scrollSpeed;
        returnPos = JsonManager.instance.gameDynamicData.returnPos;
        //targetY = GetSpacing(JsonManager.instance.gameDynamicData.fontSize , )
        initTextClass();
        UpdateTextInfo();
    }
    private void Update()
    {
        if (controller.mode != Mode.play)
        {
            scrollText(textOBs[0], textOBs[1]);
            scrollText(textOBs[1], textOBs[0]);

        }
        else
        {
            textOBs[0].textRect.anchoredPosition = beginningPos[0];
            textOBs[1].textRect.anchoredPosition = beginningPos[1];
        }
        scrollText(textOBs[2],textOBs[3]);
        scrollText(textOBs[3],textOBs[2]);
    }
    private void scrollText(TextOB textob, TextOB nextOB)
    {
        textob.textRect.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
        if (textob.textRect.anchoredPosition.y >= textob.returnPos && textob.isPlay)
        {
            Debug.Log("다음텍스트 위치로");
            nextOB.textRect.anchoredPosition = returnPos;
            textob.isPlay = false;
            nextOB.isPlay = true;
        }
    }
    private float GetSpacing(int textLenght)
    {
        float fixValue = 1.123f;
        float result = fixValue * JsonManager.instance.gameDynamicData.fontSize * textLenght;
        
        return result- JsonManager.instance.gameDynamicData.textDistance;
    }
    public void UpdateTextInfo()
    {
        for (int i = 0; i < textOBs.Length; i++)
        {
            textOBs[i].lenght = textOBs[i].text.text.Length/2;
            textOBs[i].returnPos = GetSpacing(textOBs[i].lenght);
        }
       
    }
    private void initTextClass()
    {
        for (int i = 0; i < textOBs.Length; i++)
        {
            textOBs[i].text.fontSize = JsonManager.instance.gameDynamicData.fontSize;
        }
    }
}
