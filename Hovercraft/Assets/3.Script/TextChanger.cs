using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextChanger : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject text1;
    public GameObject text2;
    public TMP_Text[] texts;
    void Start()
    {
        initTextClass();
        StartCoroutine(ChangeText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ChangeText()
    {

        while (true)
        {
            yield return new WaitForSeconds(JsonManager.instance.gameSettingData.changeTime);
            SetActiveText();

        }
    }

    private void SetActiveText()
    {
        text1.SetActive(!text1.activeSelf);
        text2.SetActive(!text1.activeSelf);
    }
    private void initTextClass()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].fontSize = JsonManager.instance.gameDynamicData.fontSize;
        }
    }
}
