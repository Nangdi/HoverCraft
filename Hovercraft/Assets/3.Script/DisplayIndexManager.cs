using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayIndexManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Camera cam1;
    [SerializeField] private Camera cam2;
    [SerializeField] private Canvas canvas1;
    [SerializeField] private Canvas canvas2;
    //[SerializeField] private Camera cam3;
    void Start()
    {
        cam1.targetDisplay = JsonManager.instance.gameSettingData.displayIndex[0];
        canvas1.targetDisplay = JsonManager.instance.gameSettingData.displayIndex[0];
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
            cam2.targetDisplay = JsonManager.instance.gameSettingData.displayIndex[1];
            canvas2.targetDisplay = JsonManager.instance.gameSettingData.displayIndex[1];
        }
        //if (Display.displays.Length > 2)
        //{
        //    Display.displays[2].Activate();
        //    cam3.targetDisplay = JsonManager.instance.gameSettingData.displayIndex[2];
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
