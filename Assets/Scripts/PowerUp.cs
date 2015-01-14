using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    //test

    public PlayerBehaviour player1;
    public PlayerBehaviour player2;
    public GameObject ball;
    // ______________________________________________________________________________________________________________________________

    private float speeda;
    public float speed = 100.0f;
    private float y;
    private bool pause = false;

    private int powerup1 = 0;
    private int powerup2 = 0;

    private CircularBuffer<int> powerupPlayer1 = new CircularBuffer<int>(4);
    private CircularBuffer<int> powerupPlayer2 = new CircularBuffer<int>(4);

    private int actual1;
    private int actual2;

    private int index1 = 0;
    private int index2 = 0;

    private int[] powerups = new int[8];
    private int powerupid;

    // ______________________________________________________________________________________________________________________________

    // Use this for initialization
    void Start()
    {
        SetPositionAndSpeed();
        pause = true;
        for (int i = 0; i < powerups.Length; i++)
        {
            powerups[i] = i;
        }
        actual1 = powerupPlayer1.getValue(0);
        actual2 = powerupPlayer2.getValue(0);
    }

    // ______________________________________________________________________________________________________________________________

    // Update is called once per frame
    void Update()
    {
        #region powerup activation player1 keyboard
        if (Input.GetKeyDown(KeyCode.Alpha1) && powerupPlayer1.Count > 0)
        {
            //Powerup(player1, powerupPlayer1.getValue(0));
            powerupPlayer1.RemoveAt(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && powerupPlayer1.Count > 1)
        {
            //Powerup(player1, powerupPlayer1.getValue(1));
            powerupPlayer1.RemoveAt(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && powerupPlayer1.Count > 2)
        {
            powerupPlayer1.RemoveAt(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && powerupPlayer1.Count > 3)
        {
            powerupPlayer1.RemoveAt(3);
        }
        #endregion
        #region powerup activation player2 keyboard
        if (Input.GetKeyDown(KeyCode.Keypad1) && powerupPlayer1.Count > 0)
        {
            Powerup(player2, powerupPlayer2.getValue(0));
            powerupPlayer2.RemoveAt(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) && powerupPlayer1.Count > 1)
        {
            powerupPlayer2.RemoveAt(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) && powerupPlayer1.Count > 2)
        {
            powerupPlayer2.RemoveAt(2);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4) && powerupPlayer1.Count > 3)
        {
            powerupPlayer2.RemoveAt(3);
        }
        #endregion

        #region powerup switch/activation player1 1 controller

        if ((GameData.Instance.buttonVal & GameData.Instance.SW6) == 0)
        {
            GameData.Instance.lastButton = -1;
        }

        if (GameData.Instance.toolbarInt == 1)
        {
            Debug.Log(index1);
            if ((GameData.Instance.buttonVal & GameData.Instance.SW6) != 0 && GameData.Instance.lastButton != 1)
            {
                if (index1 < 3)
                {
                    index1++;
                }
                else if (index1 == 3)
                {
                    index1 = 0;
                }
                GameData.Instance.lastButton = 1;
            }

            if ((GameData.Instance.buttonVal & GameData.Instance.SW4) == 0)
            {
                GameData.Instance.lastButton = -1;
            }

            if ((GameData.Instance.buttonVal & GameData.Instance.SW4) != 0 && GameData.Instance.lastButton != 1)
            {
                powerupPlayer1.RemoveAt(index1);
                GameData.Instance.lastButton = 1;
            }
        }

        #endregion
        #region powerup switch/activation player2 1 controller
        if (GameData.Instance.toolbarInt == 1)
        {
            Debug.Log(index2);
            if ((GameData.Instance.buttonVal & GameData.Instance.SW5) != 0)
            {
                if (index2 < 3)
                {
                    index2++;
                }
                else if (index2 == 3)
                {
                    index2 = 0;
                }
            }
            if ((GameData.Instance.buttonVal1 & GameData.Instance.SW3) != 0)
            {
                powerupPlayer2.RemoveAt(index2);
            }
        }
        #endregion
        #region powerup activation player1 2 controller
        if (GameData.Instance.toolbarInt == 2)
        {
            if (((GameData.Instance.buttonVal & GameData.Instance.SW6) != 0) && powerupPlayer1.Count > 0)
            {
                powerupPlayer1.RemoveAt(0);
            }
            if (((GameData.Instance.buttonVal & GameData.Instance.SW4) != 0) && powerupPlayer1.Count > 1)
            {
                powerupPlayer1.RemoveAt(1);
            }
            if (((GameData.Instance.buttonVal & GameData.Instance.SW3) != 0) && powerupPlayer1.Count > 2)
            {
                powerupPlayer1.RemoveAt(2);
            }
            if (((GameData.Instance.buttonVal & GameData.Instance.SW5) != 0) && powerupPlayer1.Count > 3)
            {
                powerupPlayer1.RemoveAt(3);
            }
        }

        #endregion
        #region powerup activation player2 2 controller
        if (GameData.Instance.toolbarInt == 2)
        {
            if (((GameData.Instance.buttonVal1 & GameData.Instance.SW6) != 0) && powerupPlayer2.Count > 0)
            {
                powerupPlayer2.RemoveAt(0);
            }
            if (((GameData.Instance.buttonVal1 & GameData.Instance.SW4) != 0) && powerupPlayer2.Count > 1)
            {
                powerupPlayer2.RemoveAt(1);
            }
            if (((GameData.Instance.buttonVal1 & GameData.Instance.SW3) != 0) && powerupPlayer2.Count > 2)
            {
                powerupPlayer2.RemoveAt(2);
            }
            if (((GameData.Instance.buttonVal1 & GameData.Instance.SW5) != 0) && powerupPlayer2.Count > 3)
            {
                powerupPlayer2.RemoveAt(3);
            }
        }

        #endregion

        if (powerupPlayer1.Count == 4 && powerupPlayer2.Count == 4)
        {
            transform.position = new Vector3(300, 300, 0);
            return;
        }
        else
        {
            pause = false;
        }

        if (Mathf.Abs(transform.position.x) > 200f && !pause)
        {
            SetPositionAndSpeedPause();
        }
        if (!pause)
        {
            if (speeda < 0)
            {
                float amtToMove = speed * Time.deltaTime;
                transform.Translate(Vector3.left * amtToMove);
            }
            else if (speeda >= 0)
            {
                float amtToMove = speed * Time.deltaTime;
                transform.Translate(Vector3.right * amtToMove);
            }
        }
    }

    // ______________________________________________________________________________________________________________________________

    void SetPositionAndSpeedPause()
    {
        transform.position = new Vector3(300, 300, 0);
        SetPositionAndSpeed();
        pause = true;
    }

    // ______________________________________________________________________________________________________________________________

    // id 1
    void SpeedUp()
    {

    }

    // ______________________________________________________________________________________________________________________________

    void SetPositionAndSpeed()
    {
        StartCoroutine(spawntime());
    }

    // ______________________________________________________________________________________________________________________________

    IEnumerator spawntime()
    {
        //float seconds = Random.Range(10.0f, 15.0f);
        yield return new WaitForSeconds(1);
        int id = Random.Range(1, 40);
        //Debug.Log(id%2);
        //powerupid = powerups[1];
        if (id%2 == 0)    
            renderer.material.color = Color.green;  
        else 
            renderer.material.color = Color.red;
        
        
        //Debug.Log(powerupid);
        //y = Random.Range(-200f, 200f);
        transform.position = new Vector3(0, 0, 0);
        if (pause)
        {
            speeda = Random.Range(-1.0f, 1.0f);
        }
        pause = false;
    }

    // ______________________________________________________________________________________________________________________________

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.name == "Player 1")
            {
                SetPositionAndSpeedPause();

                if (checkEffect(powerupid) == -1)
                    Powerup(player1, powerupid);

                else if (powerupPlayer1.Count < 4 && checkEffect(powerupid) == 1)
                {
                    powerupPlayer1.Add(powerupid);
                    powerup1++;
                } 

            }
            else if (other.name == "Player 2")
            {

                SetPositionAndSpeedPause();
                if (checkEffect(powerupid) == -1) {
                    Powerup(player2, powerupid);
                }
                else if (powerupPlayer2.Count < 4 && checkEffect(powerupid)==1)
                {
                    powerupPlayer2.Add(powerupid);
                    powerup2++;
                }

            }
        }
    }

    void Powerup(PlayerBehaviour player, int id){
        
        if (player.name == "Player 1")
        {
            player = player1;
        }
        else if (player.name == "Player 2")
            player = player2;

        switch (powerupid) { 
            case 0: //change player speed <faster>
                player.speed = 150f;
                break;
            case 1: // change player speed <slower>
                player.speed = 50f;
                break;
            default:
                break;
        }

    }

    //check if positive or negative power up
    int checkEffect(int i) {
        if (i==0)
            return 1;
        else
            return -1;
    }
    // ______________________________________________________________________________________________________________________________

    void OnGUI()
    {
        GUI.Label(new Rect(10, 90, 220, 30), powerupPlayer1.getValue(0).ToString() + " " + powerupPlayer1.getValue(1).ToString() + " " + powerupPlayer1.getValue(2).ToString() + " " + powerupPlayer1.getValue(3).ToString());
        GUI.Label(new Rect(10, 110, 220, 30), powerupPlayer2.getValue(0).ToString() + " " + powerupPlayer2.getValue(1).ToString() + " " + powerupPlayer2.getValue(2).ToString() + " " + powerupPlayer2.getValue(3).ToString()); ;
    }
}