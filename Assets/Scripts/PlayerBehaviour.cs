// Simple script that controls the player bat behaviour

using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{

    // ______________________________________________________________________________________________________________________________

    // Store button codes for up and down
    // Stored as public members, so we can modify them in the unity editor for the individual players

    // Speed of the player
    float yRange = 80f;
    float smooth = 3.0f;

    public int playerid;

    public float speed = 100.0f;

    bool paused = false;

    int smoother = 0;

    int smoothGrenze = 20;

    float[] smoothArray;

    float oldRange;
    // ______________________________________________________________________________________________________________________________

    // Use this for initialization
    void Start()
    {
        smoothArray = new float[smoothGrenze];
        oldRange = 0.0f;
    }

    // ______________________________________________________________________________________________________________________________

    // Update is called once per frame
    void Update()
    {
        #region Pausierung des Spiels
        if ((GameData.Instance.buttonVal & GameData.Instance.SW2) == 0 && (GameData.Instance.buttonVal1 & GameData.Instance.SW2) == 0)
        {
            GameData.Instance.lastButton = -1;
        }
        //else if (((GameData.Instance.buttonVal & GameData.Instance.SW2) != 0 && GameData.Instance.lastButton != 1) || ((GameData.Instance.buttonVal1 & GameData.Instance.SW2) != 0
            //&& GameData.Instance.lastButton != 1))
        //{


            //Hier wird die Pausierung ausgeführt!!!
        //}
        if (!paused)
        {
            if (Input.GetKeyDown("p") || ((GameData.Instance.buttonVal & GameData.Instance.SW2) != 0 && GameData.Instance.lastButton != 1)
                || ((GameData.Instance.buttonVal1 & GameData.Instance.SW2) != 0 && GameData.Instance.lastButton != 1))
            {
                Time.timeScale = 0;
                paused = true;
                GameData.Instance.pause = true;
                GameData.Instance.lastButton = 1;
            }
        }

        else if (paused)
        {
            if (Input.GetKeyDown("p") || ((GameData.Instance.buttonVal & GameData.Instance.SW2) != 0 && GameData.Instance.lastButton != 1)
                || ((GameData.Instance.buttonVal1 & GameData.Instance.SW2) != 0 && GameData.Instance.lastButton != 1))
            {
                Time.timeScale = 1;
                paused = false;
                GameData.Instance.pause = false;
                GameData.Instance.lastButton = 1;
            }
        }


        if (paused)
            return;
        #endregion

        float amountToMove = speed * Time.deltaTime;
        // Compute the amount to move from the speed and the elapsed time

        // if either the up or the down key was pressed, move the player up or down respectively
        // note: as long as the button is held down

        #region Steuerung des 1. Spielers mit Controller
        if (playerid == 1)
        {
            if (GameData.Instance.toolbarInt == 1 || GameData.Instance.toolbarInt == 2)
            {
                Vector3 playerY1 = transform.position;

                //Unterschiede zwischen Slider und Accelerometer ausgleichen
                if (GameData.Instance.extreme)
                {
                    playerY1.y = yRange * GameData.Instance.Y1 * 1.2f;

                    #region smoothProzess
                    if (smoother != smoothGrenze)
                    {
                        smoothArray[smoother] = playerY1.y;
                        smoother++;
                        Debug.Log(smoother);
                    }
                    float tempi = 0;
                    if (smoother == smoothGrenze)
                    {
                        for (int i = 0; i < smoother; i++)
                        {
                            tempi += smoothArray[i];
                        }
                        playerY1.y = tempi / (smoother);
                        transform.position = playerY1;
                        smoother = 0;
                    }
                    #endregion


                    transform.position = new Vector3(transform.position.x, Mathf.Lerp(oldRange, playerY1.y, smooth), transform.position.z);
                    oldRange = playerY1.y;
                }
                else
                {
                    playerY1.y = yRange * GameData.Instance.Y1;
                    transform.position = playerY1;
                }
                //Unterschiede zwischen Drehregler und Accelerometer ausgleichen
                float tiltAroundZ;
                if (GameData.Instance.extreme)
                    tiltAroundZ = (((float)(GameData.Instance.Axis1) / 34.13333333333333f) - 60) * 4.0f;
                else
                    tiltAroundZ = ((float)(GameData.Instance.Axis1) / 34.13333333333333f) - 60;
                Quaternion target = Quaternion.Euler(0, 0, tiltAroundZ);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            }
        }
        #endregion

        #region Steuerung des 1. Spielers mit Keyboard
        if (GameData.Instance.toolbarInt == 0 && playerid == 1)
        {
            if (Input.GetKey("up") && transform.position.y < yRange)
            {
                transform.Translate(Vector3.up * amountToMove, Space.World);
            }

            if (Input.GetKey("down") && transform.position.y > -yRange)
            {
                transform.Translate(Vector3.down * amountToMove, Space.World);
            }

            if (Input.GetKey("left"))
            {
                float tiltAroundZ = 60f;
                Quaternion target = Quaternion.Euler(0, 0, tiltAroundZ);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            }

            if (Input.GetKey("right"))
            {
                float tiltAroundZ = -60f;
                Quaternion target = Quaternion.Euler(0, 0, tiltAroundZ);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            }
        }
        #endregion

        #region Steuerung des 2. Spielers mit Controller
        if (playerid == 2)
        {
            if (GameData.Instance.toolbarInt == 1 || GameData.Instance.toolbarInt == 2)
            {
                Vector3 playerY2 = transform.position;

                //Unterschiede zwischen Slider und Accelerometer ausgleichen
                if (GameData.Instance.extreme)
                    playerY2.y = yRange * GameData.Instance.Y2 * 1.2f;
                else
                    playerY2.y = yRange * GameData.Instance.Y2;

                transform.position = playerY2;

                //Unterschiede zwischen Drehregler und Accelerometer ausgleichen
                float tiltAroundZ;
                if (GameData.Instance.extreme)
                    tiltAroundZ = (((float)(GameData.Instance.Axis2) / 34.13333333333333f) - 60) * 4.0f;
                else
                    tiltAroundZ = ((float)(GameData.Instance.Axis2) / 34.13333333333333f) - 60;

                Quaternion target = Quaternion.Euler(0, 0, tiltAroundZ);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            }
        }
        #endregion

        #region Steuerung des 2. Spielers mit Keyboard
        if (GameData.Instance.toolbarInt == 0 && playerid == 2)
        {
            if (Input.GetKey("w") && transform.position.y < yRange)
            {
                transform.Translate(Vector3.up * amountToMove, Space.World);
            }

            if (Input.GetKey("s") && transform.position.y > -yRange)
            {
                transform.Translate(Vector3.down * amountToMove, Space.World);
            }

            if (Input.GetKey("a"))
            {
                float tiltAroundZ = 60f;
                Quaternion target = Quaternion.Euler(0, 0, tiltAroundZ);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            }

            if (Input.GetKey("d"))
            {
                float tiltAroundZ = -60f;
                Quaternion target = Quaternion.Euler(0, 0, tiltAroundZ);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            }
        }
        #endregion

    }

    // ______________________________________________________________________________________________________________________________

    void OnGUI()
    {
        // Draw the scoreboard for the two palyers
        //GUI.Label(new Rect(10, 50, 220, 30), rotate1.w.ToString());
        //GUI.Label(new Rect(10, 70, 220, 30), rotate1.x.ToString());
        //GUI.Label(new Rect(10, 90, 220, 30), rotate1.y.ToString());
        //GUI.Label(new Rect(10, 0, 220, 30), rotate1.z.ToString());
    }
}
