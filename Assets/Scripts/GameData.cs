using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class GameData
{

    // ______________________________________________________________________________________________________________________________

    private static GameData _instance = null;

    //Die Position der Spieler (Controller, PlayerBehaviour)
    public float Y1;
    public float Y2;

    //Die Achse der Spieler (Controller, PlayerBehaviour)
    public float Axis1;
    public float Axis2;

    //Ball hat Spieler berührt (Controller, NewBehaviourScript)
    public bool Touched;

    //Ball ist aus dem Spielfeld (Controller, NewBehaviourScript)
    public bool Out;

    //Wert, ob Keyboard oder Controller [0 = Keyboard, 1 = Controller] (Controller, PlayerBehaviour, Options)
    public int toolbarInt;

    //Wird bei Spielauswahl auf true gesetzt, wenn man Extreme mod wählt
    public bool extreme;

    //Pause...
    public bool pause;

    public bool powerup1;
    public bool player1;

    public int controller;

    public SerialPort[] streams;
    public string[] a;

    public int SW6;
    public int SW5;
    public int SW4;
    public int SW3;
    public int SW2;
    public int SW1;
    public int buttonVal;
    public int buttonVal1;
    public int lastButton;

    public GUIStyle modified = new GUIStyle();
    public GUIStyle buttons = new GUIStyle();

    public int gamemode;
    // ______________________________________________________________________________________________________________________________

    private GameData()
    {
        Y1 = 0.0f;
        Y2 = 0.0f;
        Axis1 = 0.0f;
        Axis2 = 0.0f;
        Touched = false;
        Out = false;
        toolbarInt = 0;
        pause = false;
        powerup1 = false;
        player1 = false;
        controller = 0;
        streams = null;
        a = null;
        SW6 = 0x0800;
        SW5 = 0x0400;
        SW4 = 0x0200;
        SW3 = 0x0100;
        SW2 = 0x0080;
        SW1 = 0x0040;
        buttonVal = 0;
        buttonVal1 = 0;
        lastButton = -1;
        extreme = false;
        gamemode = 0;
    }

    // ______________________________________________________________________________________________________________________________

    public static GameData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameData();
            }
            return _instance;
        }
    }

    public void stream()
    {
        a = SerialPort.GetPortNames();

        streams = new SerialPort[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            Debug.Log(a[i]);
        }

        if (streams.Length == 1)
        {
            controller = 1;
        }
        else if (streams.Length >= 2)
        {
            controller = 2;
        }
        else if (streams.Length == 0)
        {
            controller = 0;
        }
    }
    public void openstreams()
    {

        for (int i = 0; i < a.Length; i++)
        {
            streams[i] = new SerialPort(a[i], 115200);
            streams[i].Open();

        }
    }
}
