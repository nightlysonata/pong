using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System;


//class PlayerStream
//{
//    public int playerID;
//    public SerialPort stream;
//    public string recievedData = "EMPTY";
//}



public class Controller : MonoBehaviour
{


    // ______________________________________________________________________________________________________________________________
    #region Initialisierung
    SerialPort stream;
    SerialPort stream1;
    string receivedData = "EMPTY";
    string receivedData1 = "EMPTY";
    string[] received;
    string[] received1;
    public bool[] open = new bool[9];

    public GameObject player1;
    public GameObject player2;

    float Y1;
    float Y2;

    float Axis1;
    float Axis2;

    float player1X, player2X;
    float player1Y;
    float player2Y;

    bool jump1 = false;

    public float smooth = 2.0F;

    #endregion
    // ______________________________________________________________________________________________________________________________

    // Use this for initialization

    //void Awake()
    //{
    //    GameData.Instance.stream();
    //}

    void Start()
    {
        GameData.Instance.openstreams();
    }
    // ______________________________________________________________________________________________________________________________

    // Update is called once per frame
    void Update()
    {

        #region 1 Kontroller einlesen
        if (GameData.Instance.toolbarInt == 1)
        {
            GameData.Instance.streams[0].Write("1");
            receivedData = GameData.Instance.streams[0].ReadLine();
            GameData.Instance.buttonVal = System.Convert.ToInt32(receivedData, 16);
        }
        #endregion

        #region 2 Kontroller einlesen
        if (GameData.Instance.toolbarInt == 2)
        {
            GameData.Instance.streams[0].Write("1");
            receivedData = GameData.Instance.streams[0].ReadLine();
            GameData.Instance.buttonVal = System.Convert.ToInt32(receivedData, 16);
            GameData.Instance.streams[1].Write("1");
            receivedData = GameData.Instance.streams[1].ReadLine();
            GameData.Instance.buttonVal1 = System.Convert.ToInt32(receivedData, 16);
        }
        #endregion

        if (GameData.Instance.pause)
            return;

        #region RumbleMotor im ControllerSkript

        if (GameData.Instance.Touched && GameData.Instance.toolbarInt == 1)
        {
            StartCoroutine(BallTouchedPlayer());
            GameData.Instance.Touched = false;
        }
        if (GameData.Instance.Out && GameData.Instance.toolbarInt == 1)
        {
            StartCoroutine(BallOutOfField());
            GameData.Instance.Out = false;
        }
        #endregion

        /*
         *Konvertieren der Werte, die vom Controller übergeben werden 
         */
        #region 1 Kontroller (Slider/Drehregler)
        if (GameData.Instance.toolbarInt == 1)
        {
            GameData.Instance.streams[0].Write("4");
            receivedData = GameData.Instance.streams[0].ReadLine();
            received = receivedData.Split(' ');

            Axis1 = System.Convert.ToInt32(received[1], 16);
            Axis2 = System.Convert.ToInt32(received[2], 16);
            Debug.Log(Axis1 + ", " + Axis2);
            GameData.Instance.Axis1 = Axis1;
            GameData.Instance.Axis2 = Axis2;

            received[3] = received[3].Remove(3);
            received[4] = received[4].Remove(3);

            Y1 = System.Convert.ToInt32(received[3], 16);
            Y2 = System.Convert.ToInt32(received[4], 16);

            player1Y = ((float)(Y1) / 128.0f) - 1.0f;
            player2Y = ((float)(Y2) / 128.0f) - 1.0f;

            #region Springen des rechten Spielers am oberen Rand unterbinden
            if (player1Y == 0.9921875f)
                jump1 = true;

            if (jump1 && player1Y < 0.8f)
            {
                Debug.Log("Dope!");
                player1Y = 0.9921875f;
            }
            if (jump1 && player1Y < 0.95f && player1Y > 0.80f)
            {
                Debug.Log("Clear");
                jump1 = false;
            }
            #endregion

            GameData.Instance.Y1 = player1Y;
            GameData.Instance.Y2 = player2Y;
        }
        #endregion

        #region 2 Kontroller
        if (GameData.Instance.toolbarInt == 2)
        {
            #region Steuerung mit Accelerometer
            if (GameData.Instance.extreme)
            {
                GameData.Instance.streams[0].Write("a");
                GameData.Instance.streams[1].Write("a");
                receivedData = GameData.Instance.streams[0].ReadLine();
                string[] elements = receivedData.Split(' ');
                receivedData1 = GameData.Instance.streams[1].ReadLine();
                string[] elements1 = receivedData1.Split(' ');
                player1Y = System.Convert.ToInt32(elements[2], 16);
                player1X = System.Convert.ToInt32(elements[1], 16);

                player2Y = System.Convert.ToInt32(elements1[2], 16);
                player2X = System.Convert.ToInt32(elements1[1], 16);

                if (player1X > 127)
                    player1X = player1X - 256;
                if (player1Y > 127)
                    player1Y = player1Y - 256;
                player1Y += 127;

                if (player2X > 127)
                    player2X = player2X - 256;
                if (player2Y > 127)
                    player2Y = player2Y - 256;
                player2Y += 127;

                player1X = ((float)(player1X) / 64.0f);
                player2X = ((float)(player2X) / 64.0f);

                GameData.Instance.Axis1 = player1Y * 16.0f;
                GameData.Instance.Axis2 = player2Y * 16.0f;
                
                GameData.Instance.Y1 = player1X;
                GameData.Instance.Y2 = player2X;


            }
            #endregion

            #region Steuerung mit Slider/Drehregler
            else
            {
                GameData.Instance.streams[0].Write("4");
                GameData.Instance.streams[1].Write("4");
                receivedData = GameData.Instance.streams[0].ReadLine();
                received = receivedData.Split(' ');
                receivedData1 = GameData.Instance.streams[1].ReadLine();
                received1 = receivedData1.Split(' ');

                Axis1 = System.Convert.ToInt32(received[1], 16);
                Axis2 = System.Convert.ToInt32(received1[1], 16);

                GameData.Instance.Axis1 = Axis1;
                GameData.Instance.Axis2 = Axis2;

                received[3] = received[3].Remove(3);
                received1[3] = received1[3].Remove(3);                    //smooth
                Y1 = System.Convert.ToInt32(received[3], 16);
                Y2 = System.Convert.ToInt32(received1[3], 16);

                player1Y = ((float)(Y1) / 128.0f) - 1.0f;
                player2Y = ((float)(Y2) / 128.0f) - 1.0f;


                #region Springen des rechten Spielers am oberen Rand unterbinden
                if (player1Y == 0.9921875f)
                    jump1 = true;

                if (jump1 && player1Y < 0.8f)
                {
                    Debug.Log("Dope!");
                    player1Y = 0.9921875f;
                }
                if (jump1 && player1Y < 0.95f && player1Y > 0.80f)
                {
                    Debug.Log("Clear");
                    jump1 = false;
                }
                #endregion

                GameData.Instance.Y1 = player1Y;
                GameData.Instance.Y2 = player2Y;

            }
            #endregion

        }
        #endregion
    }

    // ______________________________________________________________________________________________________________________________

    IEnumerator BallOutOfField()
    {
        GameData.Instance.streams[0].Write("m 1000\r\n");
        GameData.Instance.streams[0].ReadLine();
        yield return new WaitForSeconds(0.75f);
        GameData.Instance.streams[0].Write("m 0\r\n");
        GameData.Instance.streams[0].ReadLine();
    }

    // ______________________________________________________________________________________________________________________________

    IEnumerator BallTouchedPlayer()
    {
        GameData.Instance.streams[0].Write("m 500\r\n");
        GameData.Instance.streams[0].ReadLine();
        yield return new WaitForSeconds(0.25f);
        GameData.Instance.streams[0].Write("m 0\r\n");
        GameData.Instance.streams[0].ReadLine();
    }

    void OnGUI()
    {
        //GUI.Label(new Rect(200, 200, 150, 30), "Player1X: " + player1X.ToString());

    }

}
