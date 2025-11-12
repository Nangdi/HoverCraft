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
                if (gameController.mode == Mode.Ready)
                {
                    gameController.SetMode(Mode.Wait);
                }
                break;
            case "DP":
                if (gameController.mode == Mode.Wait)
                {
                    gameController.SetMode(Mode.play);
                }
                break;
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ReceivedData("DW");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ReceivedData("DP");
        }
    }
}
