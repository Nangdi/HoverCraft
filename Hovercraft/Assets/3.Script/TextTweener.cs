using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextTweener : MonoBehaviour
{
    public TMP_Text targetText1;     // 깜빡일 텍스트
    public TMP_Text targetText2;     // 깜빡일 텍스트
    public float fadeTime = 0.5f;   // 페이드 속도
    public float delay = 3.0f;      // 대기 시간 (선택사항)
    private Tween blinkTween;
    private Sequence seq;
    void Start()
    {

        delay = JsonManager.instance.gameSettingData.blinkDelay;

        StartTweening(targetText1);
        StartTweening(targetText2);
    }
    private void StartTweening(TMP_Text targetText)
    {
        // 알파값(투명도)을 반복해서 바꾸기
        seq = DOTween.Sequence()
            // 페이드아웃
            .Append(targetText.DOFade(0f, fadeTime))
            // 페이드인
            .Append(targetText.DOFade(1f, fadeTime))
            // 3초 대기
            .AppendInterval(delay)
            // 반복
            .SetLoops(-1); // -1 = 무한반복
    }
}
