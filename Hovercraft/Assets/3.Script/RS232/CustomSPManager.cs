using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSPManager : SerialPortManager
{
    [SerializeField] GameController gameController;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void ReceivedData(string data)
    {
        switch (data)
        {
            case "DW":
                gameController.SetMode(Mode.Wait);
                break;
            case "DP":
                gameController.SetMode(Mode.play);
                break;
        }

    }
}
