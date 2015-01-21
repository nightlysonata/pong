using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    // ______________________________________________________________________________________________________________________________

    private float speeda;
    public float speed = 100.0f;
    private float y;
    private bool pause = false;

    private CircularBuffer<int> powerupsP1 = new CircularBuffer<int>(4);
    private CircularBuffer<int> powerupsP2 = new CircularBuffer<int>(4);

    //for circularBuffer
    private int index1 = 0;
    private int index2 = 0;

    //kinds of powerups
    private int[] powerups = new int[8];
    private int powerupID;

    private int player;

    private bool small1;
    private bool small2;

    private bool big1;
    private bool big2;

    private bool middlel1;
    private bool middlel2;

    //private bool start = true; not used

    public GameObject Player1;
    public GameObject Player2;

    public GameObject ball;

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
    }

    // ______________________________________________________________________________________________________________________________

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x < 10  && transform.position.x > -10)
            renderer.enabled = false;
        else
            renderer.enabled = true;

        #region powerup activation player1 keyboard
        if (Input.GetKeyDown(KeyCode.Alpha1) && powerupsP1.Count > 0)
        {
            powerup(Player1, powerupsP1.getValue(0));
            powerupsP1.RemoveAt(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && powerupsP1.Count > 1)
        {
            powerup(Player1, powerupsP1.getValue(1));
            powerupsP1.RemoveAt(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && powerupsP1.Count > 2)
        {
            powerup(Player1, powerupsP1.getValue(2));
            powerupsP1.RemoveAt(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && powerupsP1.Count > 3)
        {
            powerup(Player1, powerupsP1.getValue(3));
            powerupsP1.RemoveAt(3);
        }
        #endregion
        #region powerup activation player2 keyboard
        if (Input.GetKeyDown(KeyCode.Alpha7) && powerupsP2.Count > 0)
        {
            powerup(Player2, powerupsP2.getValue(0));
            powerupsP2.RemoveAt(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) && powerupsP2.Count > 1)
        {
            powerup(Player2, powerupsP2.getValue(1));
            powerupsP2.RemoveAt(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) && powerupsP2.Count > 2)
        {
            powerup(Player2, powerupsP2.getValue(2));
            powerupsP2.RemoveAt(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0) && powerupsP2.Count > 3)
        {
            powerup(Player2, powerupsP2.getValue(3));
            powerupsP2.RemoveAt(3);
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
                powerupsP1.RemoveAt(index1);
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
                powerupsP2.RemoveAt(index2);
            }
        }
        #endregion

        #region powerup activation player1 2 controller
        if (GameData.Instance.toolbarInt == 2)
        {
            if (((GameData.Instance.buttonVal & GameData.Instance.SW6) != 0) && powerupsP1.Count > 0)
            {
                powerup(Player1, powerupsP1.getValue(0));
                powerupsP1.RemoveAt(0);
            }
            if (((GameData.Instance.buttonVal & GameData.Instance.SW4) != 0) && powerupsP1.Count > 1)
            {
                powerup(Player1, powerupsP1.getValue(1));
                powerupsP1.RemoveAt(1);
            }
            if (((GameData.Instance.buttonVal & GameData.Instance.SW3) != 0) && powerupsP1.Count > 2)
            {
                powerup(Player1, powerupsP1.getValue(2));
                powerupsP1.RemoveAt(2);
            }
            if (((GameData.Instance.buttonVal & GameData.Instance.SW5) != 0) && powerupsP1.Count > 3)
            {
                powerup(Player1, powerupsP1.getValue(3));
                powerupsP1.RemoveAt(3);
            }
        }

        #endregion
        #region powerup activation player2 2 controller
        if (GameData.Instance.toolbarInt == 2)
        {
            if (((GameData.Instance.buttonVal1 & GameData.Instance.SW6) != 0) && powerupsP2.Count > 0)
            {
                powerup(Player2, powerupsP2.getValue(0));
                powerupsP2.RemoveAt(0);
            }
            if (((GameData.Instance.buttonVal1 & GameData.Instance.SW4) != 0) && powerupsP2.Count > 1)
            {
                powerup(Player2, powerupsP2.getValue(1));
                powerupsP2.RemoveAt(1);
            }
            if (((GameData.Instance.buttonVal1 & GameData.Instance.SW3) != 0) && powerupsP2.Count > 2)
            {
                powerup(Player2, powerupsP2.getValue(2));
                powerupsP2.RemoveAt(2);
            }
            if (((GameData.Instance.buttonVal1 & GameData.Instance.SW5) != 0) && powerupsP2.Count > 3)
            {
                powerup(Player2, powerupsP2.getValue(3));
                powerupsP2.RemoveAt(3);
            }
        }

        #endregion

        if (powerupsP1.Count == 4 && powerupsP2.Count == 4)
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
       if (!pause) // if (!pause && !start)
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

    void powerup(GameObject player, int id) {

        GameObject opponent = null;
        if (player.name == "Player 1")
            opponent = Player2;
        else if (player.name == "Player 2")
            opponent = Player1;

        //0-4 negative powerups
        //5-9 positive powerups
        switch (id) { 
            case 0: //shrink player bat
                StartCoroutine(negscale(player));
                break;

            case 1: //bring player closer to the middle line
                StartCoroutine(middle(player));
                break;

            case 2: //make player invisible for 3 seconds
                StartCoroutine(invisible(player));
                break;

            case 3: //make ball on own side invisible for 1 round
                StartCoroutine(invisibleBall(player));
                break;

            case 4: //opponent bat gets bigger
                StartCoroutine(posscale(opponent));
                break;

            case 5: // shrink opponent bat
                StartCoroutine(negscale(opponent));
                break;

            case 6: // bring opponen bat closer to middle line
                StartCoroutine(middle(opponent));
                break;

            case 7: // make opponent invisible for 3 seconds
                StartCoroutine(invisible(opponent));
                break;

            case 8: //make ball on opponent side invisible for 1 round
                StartCoroutine(invisibleBall(opponent));
                break;

            case 9: //bat gets bigger
                StartCoroutine(posscale(player));
                break;
            default:
                break;
        }
    }

    #region Powerup Smaller Bat (Negative & Positive)

    IEnumerator negscale(GameObject player)
    {
        Vector3 first = player.transform.lossyScale;
        Debug.Log("firstnegscale1" + first);
        player.transform.localScale -= new Vector3(0, first.y * 0.3f, 0);
        small1 = true;
        yield return new WaitForSeconds(10.0f);
        player.transform.localScale = new Vector3(2.5f, 30, 1);
        small1 = false;
    }

    #endregion

    #region Powerup Bigger Bat (Negative & Positive)
    
    IEnumerator posscale(GameObject player)
    {
        Vector3 first = player.transform.lossyScale;
        Debug.Log("firstposscale1" + first);
        player.transform.localScale += new Vector3(0, first.y * 0.3f, 0);
        big2 = true;
        yield return new WaitForSeconds(10.0f);
        player.transform.localScale = first;
        big2 = false;
    }

    #endregion

    #region Powerup Middle Line (Negative & Positive)
   
    IEnumerator middle(GameObject player)
    {
        Vector3 first = player.transform.position;
        player.transform.position -= new Vector3(first.x * 0.1f, 0, 0);
        middlel2 = true;
        yield return new WaitForSeconds(10.0f);
        player.transform.position = new Vector3(player.transform.position.x * 1.10f, player.transform.position.y, 0);
        middlel2 = false;
    }
    #endregion

    #region Powerup Invisible Player

    IEnumerator invisible(GameObject player) {

        player.renderer.enabled = false;

        yield return new WaitForSeconds(3);

        player.renderer.enabled = true;
    }

    #endregion

    #region Powerup invisible Ball

    IEnumerator invisibleBall(GameObject player) {
        
        if (player == Player1) {

            if (ball.transform.position.x >= 0)
                while (ball.transform.position.x >= 0) 
                    yield return new WaitForEndOfFrame();

            
            while (ball.transform.position.x <= 0 && ball.transform.position.x > -160.0f){
                ball.renderer.enabled = false;
                yield return new WaitForEndOfFrame();
            }
            ball.renderer.enabled = true;
        }

        if (player == Player2) {

            if (ball.transform.position.x <= 0)
                while (ball.transform.position.x <= 0)
                    yield return new WaitForEndOfFrame();


            while (ball.transform.position.x >= 0 && ball.transform.position.x < 160.0f)
            {
                ball.renderer.enabled = false;
                yield return new WaitForEndOfFrame();
            }
            
            ball.renderer.enabled = true;
        }

        yield return null;
    }

    #endregion

    #region Powerup faster ball

    IEnumerator changeBallSpeed(GameObject player) {
        
        if (player == Player1)
        {
            while (ball.rigidbody.velocity.x < 0 || ball.transform.position.x > -1 )
                    yield return new WaitForEndOfFrame();

            float ballspeed = ball.rigidbody.velocity.x + 50.0f;
            while (ball.transform.position.x <= 0 && ball.transform.position.x > -160.0f)
            {
                ball.rigidbody.velocity = new Vector3(ballspeed, ball.rigidbody.velocity.y, ball.rigidbody.velocity.z);
                yield return new WaitForEndOfFrame();
            }
            ball.rigidbody.velocity = new Vector3(ball.rigidbody.velocity.x - 50.0f, ball.rigidbody.velocity.y, ball.rigidbody.velocity.z);
        }

        if (player == Player2)
        {


            while (ball.rigidbody.velocity.x > 0 || ball.transform.position.x < 1)
                    yield return new WaitForEndOfFrame();

            float ballspeed = ball.rigidbody.velocity.x + 50.0f;
            while (ball.transform.position.x >= 0 && ball.transform.position.x < 160.0f)
            {

                ball.rigidbody.velocity = new Vector3(ballspeed, ball.rigidbody.velocity.y, ball.rigidbody.velocity.z);
                yield return new WaitForEndOfFrame();
            }

            ball.rigidbody.velocity = new Vector3(ball.rigidbody.velocity.x - 50.0f, ball.rigidbody.velocity.y, ball.rigidbody.velocity.z);
        }

        yield return null;
    }

    #endregion

    // ______________________________________________________________________________________________________________________________

    void SetPositionAndSpeed()
    {
        StartCoroutine(spawntime());
    }

    // ______________________________________________________________________________________________________________________________

    float randomY() {

        return Random.Range(-90.0f, 90.0f);
    }

    IEnumerator spawntime()
    {
            yield return new WaitForSeconds(1);
            powerupID = Random.Range(0, 10);

            if (powerupID < 5)
                renderer.material.color = Color.red;
            else
                renderer.material.color = Color.green;

            transform.position = new Vector3(0, randomY(), 0);

            if (pause)
                speeda = Random.Range(-1.0f, 1.0f);

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

                //if negative pwup activate instantly
                if (powerupID < 5)
                    powerup(Player1, powerupID);
                //else save
                if (powerupsP1.Count < 4 && powerupID > 4)
                    powerupsP1.Add(powerupID);
            }
            //same for player 2
            else if (other.name == "Player 2")
            {

                SetPositionAndSpeedPause();

                if (powerupID < 5)
                    powerup(Player2, powerupID);

                if (powerupsP2.Count < 4 && powerupID > 4)
                    powerupsP2.Add(powerupID);
            }
        }

        //if (other.tag == "Ball"){
        //    this.transform.position = new Vector3(-this.transform.position.x, this.transform.position.y, this.transform.position.z);
        //}
    }

    // ______________________________________________________________________________________________________________________________

    void OnGUI()
    {
        GUI.Label(new Rect(10, 90, 220, 30), powerupsP1.getValue(0).ToString() + " " + powerupsP1.getValue(1).ToString() + " " + powerupsP1.getValue(2).ToString() + " " + powerupsP1.getValue(3).ToString());
        GUI.Label(new Rect(10, 110, 220, 30), powerupsP2.getValue(0).ToString() + " " + powerupsP2.getValue(1).ToString() + " " + powerupsP2.getValue(2).ToString() + " " + powerupsP2.getValue(3).ToString()); ;
    }
}