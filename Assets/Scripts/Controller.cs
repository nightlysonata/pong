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
    string recievedData = "EMPTY";
    string recievedData1 = "EMPTY";
    string[] recieved;
    string[] recieved1;
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

    private float smoothing = 0.005f;

    #region smoothstuff
    public float player1Slider = -1.0f;
    private float player1SliderLastUsedValue = -1.0f;
    private CircularBuffer<float> player1Buffer = new CircularBuffer<float>(10);
    public float player2Slider = -1.0f;
    private float player2SliderLastUsedValue = -1.0f;
    private CircularBuffer<float> player2Buffer = new CircularBuffer<float>(10);
    #endregion


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
        #region Für Smoothing Anfangswerte einlesen
        if (GameData.Instance.toolbarInt == 1)
        {
            GameData.Instance.streams[0].Write("4");
            recievedData = GameData.Instance.streams[0].ReadLine();
            recieved = recievedData.Split(' ');

            player1Slider = System.Convert.ToInt32(recieved[3], 16) / (float)128.0f;
            player2Slider = System.Convert.ToInt32(recieved[4], 16) / (float)128.0f;
        }
        if (GameData.Instance.toolbarInt == 2)
        {
            if (GameData.Instance.extreme)
            {
                GameData.Instance.streams[0].Write("a");
                GameData.Instance.streams[1].Write("a");
                recievedData = GameData.Instance.streams[0].ReadLine();
                string[] elements = recievedData.Split(' ');
                recievedData1 = GameData.Instance.streams[1].ReadLine();
                string[] elements1 = recievedData1.Split(' ');
                player1X = System.Convert.ToInt32(elements[1], 16);

                player2X = System.Convert.ToInt32(elements1[1], 16);

                if (player1X > 127)
                    player1X = player1X - 256;

                if (player2X > 127)
                    player2X = player2X - 256;

                player1Slider = ((float)(player1X) / 64.0f);
                player2Slider = ((float)(player2X) / 64.0f);
            }
            else
            {
                GameData.Instance.streams[0].Write("4");
                GameData.Instance.streams[1].Write("4");
                recievedData = GameData.Instance.streams[0].ReadLine();
                recieved = recievedData.Split(' ');
                recievedData1 = GameData.Instance.streams[1].ReadLine();
                recieved1 = recievedData1.Split(' ');

                player1Slider = System.Convert.ToInt32(recieved[3], 16) / (float)128.0f;
                player2Slider = System.Convert.ToInt32(recieved1[3], 16) / (float)128.0f;
                Debug.Log(player2Slider + ", " + player1Slider);

            }

        }
        #endregion
    }
    // ______________________________________________________________________________________________________________________________

    // Update is called once per frame
    void Update()
    {

        #region 1 Kontroller einlesen
        if (GameData.Instance.toolbarInt == 1)
        {
            GameData.Instance.streams[0].Write("1");
            recievedData = GameData.Instance.streams[0].ReadLine();
            GameData.Instance.buttonVal = System.Convert.ToInt32(recievedData, 16);
        }
        #endregion

        #region 2 Kontroller einlesen
        if (GameData.Instance.toolbarInt == 2)
        {
            GameData.Instance.streams[0].Write("1");
            recievedData = GameData.Instance.streams[0].ReadLine();
            GameData.Instance.buttonVal = System.Convert.ToInt32(recievedData, 16);
            GameData.Instance.streams[1].Write("1");
            recievedData = GameData.Instance.streams[1].ReadLine();
            GameData.Instance.buttonVal1 = System.Convert.ToInt32(recievedData, 16);
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
            recievedData = GameData.Instance.streams[0].ReadLine();
            recieved = recievedData.Split(' ');

            Axis1 = System.Convert.ToInt32(recieved[1], 16);
            Axis2 = System.Convert.ToInt32(recieved[2], 16);

            GameData.Instance.Axis1 = Axis1;
            GameData.Instance.Axis2 = Axis2;

            recieved[3] = recieved[3].Remove(3);
            recieved[4] = recieved[4].Remove(3);

            Y1 = System.Convert.ToInt32(recieved[3], 16);
            Y2 = System.Convert.ToInt32(recieved[4], 16);

            player1Y = ((float)(Y1) / 128.0f) - 1.0f;
            player2Y = ((float)(Y2) / 128.0f) - 1.0f;


            player1Buffer.Add(player1Y);
            player2Buffer.Add(player2Y);

            //calculate new position
            float player1Sum = 0.0f;
            float player2Sum = 0.0f;
            for (int i = 0; i < player1Buffer.Count; i++)
            {
                player2Sum += player2Buffer.getValue(i);
                player1Sum += player1Buffer.getValue(i);
            }
            player1Slider = player1Sum / player1Buffer.Count;
            player2Slider = player2Sum / player2Buffer.Count;

            if (isInSmoothingArea(player1Slider, player1SliderLastUsedValue))
            {
                player1SliderLastUsedValue = player1Slider;
            }
            else
            {
                player1Slider = player1SliderLastUsedValue;
            }

            if (isInSmoothingArea(player2Slider, player2SliderLastUsedValue))
            {
                player2SliderLastUsedValue = player2Slider;
            }
            else
            {
                player2Slider = player2SliderLastUsedValue;
            }


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


            GameData.Instance.Y1 = player1Slider;
            GameData.Instance.Y2 = player2Slider;
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
                recievedData = GameData.Instance.streams[0].ReadLine();
                string[] elements = recievedData.Split(' ');
                recievedData1 = GameData.Instance.streams[1].ReadLine();
                string[] elements1 = recievedData1.Split(' ');
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


                player1Buffer.Add(player1X);
                player2Buffer.Add(player2X);

                //calculate new position
                float player1Sum = 0.0f;
                float player2Sum = 0.0f;
                for (int i = 0; i < player1Buffer.Count; i++)
                {
                    player2Sum += player2Buffer.getValue(i);
                    player1Sum += player1Buffer.getValue(i);
                }
                player1Slider = player1Sum / player1Buffer.Count;
                player2Slider = player2Sum / player2Buffer.Count;

                if (isInSmoothingArea(player1Slider, player1SliderLastUsedValue))
                {
                    player1SliderLastUsedValue = player1Slider;
                }
                else
                {
                    player1Slider = player1SliderLastUsedValue;
                }

                if (isInSmoothingArea(player2Slider, player2SliderLastUsedValue))
                {
                    player2SliderLastUsedValue = player2Slider;
                }
                else
                {
                    player2Slider = player2SliderLastUsedValue;
                }


                GameData.Instance.Axis1 = player1Y * 16.0f;
                GameData.Instance.Axis2 = player2Y * 16.0f;

                GameData.Instance.Y1 = player1Slider;
                GameData.Instance.Y2 = player2Slider;


            }
            #endregion

            #region Steuerung mit Slider/Drehregler
            else
            {
                GameData.Instance.streams[0].Write("4");
                GameData.Instance.streams[1].Write("4");
                recievedData = GameData.Instance.streams[0].ReadLine();
                recieved = recievedData.Split(' ');
                recievedData1 = GameData.Instance.streams[1].ReadLine();
                recieved1 = recievedData1.Split(' ');

                Axis1 = System.Convert.ToInt32(recieved[1], 16);
                Axis2 = System.Convert.ToInt32(recieved1[1], 16);

                GameData.Instance.Axis1 = Axis1;
                GameData.Instance.Axis2 = Axis2;

                recieved[3] = recieved[3].Remove(3);
                recieved1[3] = recieved1[3].Remove(3);                    //smooth
                Y1 = System.Convert.ToInt32(recieved[3], 16);
                Y2 = System.Convert.ToInt32(recieved1[3], 16);

                player1Y = ((float)(Y1) / 128.0f) - 1.0f;
                player2Y = ((float)(Y2) / 128.0f) - 1.0f;


                player1Buffer.Add(player1Y);
                player2Buffer.Add(player2Y);

                //calculate new position
                float player1Sum = 0.0f;
                float player2Sum = 0.0f;
                for (int i = 0; i < player1Buffer.Count; i++)
                {
                    player2Sum += player2Buffer.getValue(i);
                    player1Sum += player1Buffer.getValue(i);
                }
                player1Slider = player1Sum / player1Buffer.Count;
                player2Slider = player2Sum / player2Buffer.Count;

                if (isInSmoothingArea(player1Slider, player1SliderLastUsedValue))
                {
                    player1SliderLastUsedValue = player1Slider;
                }
                else
                {
                    player1Slider = player1SliderLastUsedValue;
                }

                if (isInSmoothingArea(player2Slider, player2SliderLastUsedValue))
                {
                    player2SliderLastUsedValue = player2Slider;
                }
                else
                {
                    player2Slider = player2SliderLastUsedValue;
                }

                //#region Springen des rechten Spielers am oberen Rand unterbinden
                //if (player1Y == 0.9921875f)
                //    jump1 = true;

                //if (jump1 && player1Y < 0.8f)
                //{
                //    Debug.Log("Dope!");
                //    player1Y = 0.9921875f;
                //}
                //if (jump1 && player1Y < 0.95f && player1Y > 0.80f)
                //{
                //    Debug.Log("Clear");
                //    jump1 = false;
                //}
                //#endregion

                GameData.Instance.Y1 = player1Slider;
                GameData.Instance.Y2 = player2Slider;

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

    // ______________________________________________________________________________________________________________________________

    private bool isInSmoothingArea(float current, float last)
    {
        if (current > last + last * smoothing || current < last - last * smoothing)
        {
            return true;
        }

        return false;
    }

    // ______________________________________________________________________________________________________________________________

    void OnGUI()
    {
        //GUI.Label(new Rect(200, 200, 150, 30), "Player1X: " + player1X.ToString());

    }

}
