using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
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

    private int collision;

    private int player;

    private bool small1;
    private bool small2;

    private bool big1;
    private bool big2;

    private bool middlel1;
    private bool middlel2;

    private bool start = true;

    public GameObject Player1;
    public GameObject Player2;

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
        if (transform.position == new Vector3(0, 300, 0))
        {
            start = true;
        }
        else
        {
            start = false;
        }
        #region powerup activation player1 keyboard
        if (Input.GetKeyDown(KeyCode.Alpha1) && powerupPlayer1.Count > 0)
        {
            player = 1;
            switch (powerupid)
            {
                case 2:
                    smallerBat();
                    break;
                case 4:
                    biggerBat();
                    break;
                case 6:
                    middle();
                    break;
            }
            powerupPlayer1.RemoveAt(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && powerupPlayer1.Count > 1)
        {
            player = 1;
            switch (powerupid)
            {
                case 2:
                    smallerBat();
                    break;
                case 4:
                    biggerBat();
                    break;
                case 6:
                    middle();
                    break;
            }
            powerupPlayer1.RemoveAt(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && powerupPlayer1.Count > 2)
        {
            player = 1;
            switch (powerupid)
            {
                case 2:
                    smallerBat();
                    break;
                case 4:
                    biggerBat();
                    break;
                case 6:
                    middle();
                    break;
            }
            powerupPlayer1.RemoveAt(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && powerupPlayer1.Count > 3)
        {
            player = 1;
            switch (powerupid)
            {
                case 2:
                    smallerBat();
                    break;
                case 4:
                    biggerBat();
                    break;
                case 6:
                    middle();
                    break;
            }
            powerupPlayer1.RemoveAt(3);
        }
        #endregion
        #region powerup activation player2 keyboard
        if (Input.GetKeyDown(KeyCode.Alpha7) && powerupPlayer2.Count > 0)
        {
            player = 2;
            switch (powerupid)
            {
                case 2:
                    smallerBat();
                    break;
                case 4:
                    biggerBat();
                    break;
                case 6:
                    middle();
                    break;
            }
            powerupPlayer2.RemoveAt(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) && powerupPlayer2.Count > 1)
        {
            player = 2;
            switch (powerupid)
            {
                case 2:
                    smallerBat();
                    break;
                case 4:
                    biggerBat();
                    break;
                case 6:
                    middle();
                    break;
            }
            powerupPlayer2.RemoveAt(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) && powerupPlayer2.Count > 2)
        {
            player = 2;
            switch (powerupid)
            {
                case 2:
                    smallerBat();
                    break;
                case 4:
                    biggerBat();
                    break;
                case 6:
                    middle();
                    break;
            }
            powerupPlayer2.RemoveAt(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0) && powerupPlayer2.Count > 3)
        {
            player = 2;
            switch (powerupid)
            {
                case 2:
                    smallerBat();
                    break;
                case 4:
                    biggerBat();
                    break;
                case 6:
                    middle();
                    break;
            }
            powerupPlayer2.RemoveAt(3);
        }
        #endregion

        #region powerup switch/activation player1 1 controller

        if ((GameData.Instance.buttonVal & GameData.Instance.SW6) == 0)
        {
            GameData.Instance.lastButton1 = -1;
        }

        if (GameData.Instance.toolbarInt == 1)
        {
//            Debug.Log(index1);
            if ((GameData.Instance.buttonVal & GameData.Instance.SW6) != 0 && GameData.Instance.lastButton1 != 1)
            {
                if (index1 < 3)
                {
                    index1++;
                }
                else if (index1 == 3)
                {
                    index1 = 0;
                }
                GameData.Instance.lastButton1 = 1;
            }

            if ((GameData.Instance.buttonVal & GameData.Instance.SW4) == 0)
            {
                GameData.Instance.lastButton1 = -1;
            }

            if ((GameData.Instance.buttonVal & GameData.Instance.SW4) != 0 && GameData.Instance.lastButton1 != 1)
            {
                powerupPlayer1.RemoveAt(index1);
                GameData.Instance.lastButton1 = 1;
            }
        }

        #endregion
        #region powerup switch/activation player2 1 controller
        if (GameData.Instance.toolbarInt == 1)
        {
//            Debug.Log(index2);
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
            transform.position = new Vector3(0, 300, 0);
            return;
        }
        else
        {
            pause = false;
        }

        if (Mathf.Abs(transform.position.x) > 180f && !pause)
        {
            SetPositionAndSpeedPause();
        }
        if (!pause && !start)
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
        transform.position = new Vector3(0, 300, 0);
        SetPositionAndSpeed();
        pause = true;
    }

    // ______________________________________________________________________________________________________________________________

    #region Powerup Smaller Bat (Negative & Positive)

    void smallerBat()
    {
        if (powerupid == 1)
        {
            if (collision == 1)
            {
                StartCoroutine(negscale1());
            }

            else if (collision == 2)
            {
                StartCoroutine(negscale2());
            }
        }

        if (powerupid == 2)
        {
            if (player == 1)
            {
                StartCoroutine(negscale2());
            }
            else if (player == 2)
            {
                StartCoroutine(negscale1());
            }
        }
    }

    IEnumerator negscale1()
    {
        Vector3 first = Player1.transform.lossyScale;
        Debug.Log("firstnegscale1" + first);
        Player1.transform.localScale -= new Vector3(0, first.y * 0.3f, 0);
        small1 = true;
        yield return new WaitForSeconds(10.0f);
        Player1.transform.localScale = new Vector3(2.5f, 30, 1);
        small1 = false;
    }

    IEnumerator negscale2()
    {
        Vector3 first = Player2.transform.lossyScale;
        Debug.Log("firstnegscale2" + first);
        Player2.transform.localScale -= new Vector3(0, first.y * 0.3f, 0);
        small2 = true;
        yield return new WaitForSeconds(10.0f);
        Player2.transform.localScale = new Vector3(2.5f, 30, 1);
        small2 = false;
    }

    #endregion

    #region Powerup Bigger Bat (Negative & Positive)
    void biggerBat()
    {
        if (powerupid == 3)
        {
            if (collision == 1)
            {
                StartCoroutine(posscale1());
            }

            else if (collision == 2)
            {
                StartCoroutine(posscale2());
            }
        }

        if (powerupid == 4)
        {
            if (player == 1)
            {
                StartCoroutine(posscale2());
            }
            else if (player == 2)
            {
                StartCoroutine(posscale1());
            }
        }
    }

    IEnumerator posscale1()
    {
        Vector3 first = Player2.transform.lossyScale;
        Debug.Log("firstposscale1" + first);
        Player2.transform.localScale += new Vector3(0, first.y * 0.3f, 0);
        big2 = true;
        yield return new WaitForSeconds(10.0f);
        Player2.transform.localScale = first;
        big2 = false;
    }

    IEnumerator posscale2()
    {
        Vector3 first = Player1.transform.lossyScale;
        Debug.Log("firstposscale2" + first);
        Player1.transform.localScale += new Vector3(0, first.y * 0.3f, 0);
        big1 = true;
        yield return new WaitForSeconds(10.0f);
        Player1.transform.localScale = first;
        big1 = false;
    }
    #endregion

    #region Powerup Middle Line (Negative & Positive)
    void middle()
    {
        if (powerupid == 5)
        {
            if (collision == 1)
            {
                StartCoroutine(middle2());
            }

            else if (collision == 2)
            {
                StartCoroutine(middle1());
            }
        }

        if (powerupid == 6)
        {
            if (player == 1)
            {
                StartCoroutine(middle2());
            }
            else if (player == 2)
            {
                StartCoroutine(middle1());
            }
        }
    }

    IEnumerator middle1()
    {
        Vector3 first = Player2.transform.position;
        Player2.transform.position -= new Vector3(first.x * 0.1f, 0, 0);
        middlel2 = true;
        yield return new WaitForSeconds(10.0f);
        Player2.transform.position = new Vector3(Player2.transform.position.x * 1.10f, Player2.transform.position.y, 0);
        middlel2 = false;
    }

    IEnumerator middle2()
    {
        Vector3 first = Player1.transform.position;
        Player1.transform.position -= new Vector3(first.x * 0.1f, 0, 0);
        middlel1 = true;
        yield return new WaitForSeconds(10.0f);
        Player1.transform.position = new Vector3(Player1.transform.position.x * 1.10f, Player1.transform.position.y, 0);
        middlel1 = false;
    }
    #endregion

    // ______________________________________________________________________________________________________________________________

    void SetPositionAndSpeed()
    {
        StartCoroutine(spawntime());
    }

    // ______________________________________________________________________________________________________________________________

    IEnumerator spawntime()
    {
        if (start) 
        {
            yield return new WaitForSeconds(10);
            y = Random.Range(-90f, 90f);
            transform.position = new Vector3(0, y, 0);
        }
    }

    // ______________________________________________________________________________________________________________________________

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.name == "Player 1")
            {
                SetPositionAndSpeedPause();
                if (powerupPlayer1.Count < 4 && powerupid != 1 && powerupid != 3 && powerupid != 5)
                {
                    powerupPlayer1.Add(powerupid);
                    powerup1++;
                }
                collision = 1;

                switch (powerupid)
                {
                    case 1:
                        smallerBat();
                        break;
                    case 3:
                        biggerBat();
                        break;
                    case 5:
                        middle();
                        break;
                }
            }
            else if (other.name == "Player 2")
            {
                SetPositionAndSpeedPause();
                if (powerupPlayer2.Count < 4 && powerupid != 1 && powerupid != 3 && powerupid != 5)
                {
                    powerupPlayer2.Add(powerupid);
                    powerup2++;
                }
                collision = 2;

                switch (powerupid)
                {
                    case 1:
                        smallerBat();
                        break;
                    case 3:
                        biggerBat();
                        break;
                    case 5:
                        middle();
                        break;
                }
            }
        }
        if (other.tag == "Ball")
        {
            this.transform.position = new Vector3(-this.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
    }

    // ______________________________________________________________________________________________________________________________

    void OnGUI()
    {
        GUI.Label(new Rect(10, 90, 220, 30), powerupPlayer1.getValue(0).ToString() + " " + powerupPlayer1.getValue(1).ToString() + " " + powerupPlayer1.getValue(2).ToString() + " " + powerupPlayer1.getValue(3).ToString());
        GUI.Label(new Rect(10, 110, 220, 30), powerupPlayer2.getValue(0).ToString() + " " + powerupPlayer2.getValue(1).ToString() + " " + powerupPlayer2.getValue(2).ToString() + " " + powerupPlayer2.getValue(3).ToString()); ;
    }
}