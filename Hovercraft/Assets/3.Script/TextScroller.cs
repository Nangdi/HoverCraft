using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScroller : MonoBehaviour
{
    public GameController controller;

    public RectTransform texts;
    public RectTransform texts1;
    public RectTransform texts2;
    public RectTransform texts3;
    public float scrollSpeed = 50f;
    public float targetY;
    public Vector2 returnPos;
    public Vector2[] beginningPos = new Vector2[2];

    private void Start()
    {
        beginningPos = new Vector2[2];
        beginningPos[0] = texts.anchoredPosition;
        beginningPos[1] = texts1.anchoredPosition;
    }
    private void Update()
    {
        if (controller.mode != Mode.play)
        {
            scrollText(texts);
            scrollText(texts1);
            
        }
        else
        {
            texts.anchoredPosition = beginningPos[0];
            texts1.anchoredPosition = beginningPos[1];
        }
        scrollText(texts2);
        scrollText(texts3);
    }
    private void scrollText(RectTransform texts)
    {
        texts.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
        if (texts.anchoredPosition.y >= targetY)
        {
            texts.anchoredPosition = returnPos;
        }
    }
}
