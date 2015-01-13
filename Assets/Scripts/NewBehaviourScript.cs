using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{
    // ______________________________________________________________________________________________________________________________

    // speichert den Normalen-Vektor des Schlägers ab
    private Vector3 normal;

    // Größe des Spielfelds
    public float ySize = 125.0f;
    public float xSize = 200.0f;

    // PlayerScore
    public int[] playerScore = new int[2];

    //Geschwindigkeit des Balles
    private Vector3 velo;
    //Bool, ob Spieler1 oder nicht
    private bool player1;

    //Faktor, um den der Ball schneller wird
    private int i;

    //Int, wie oft der Ball oben/unten berührt, bevor er einen Spieler berührt
    private int bounce;

    //Bool, ob das Level1 gerade neu gestartet wurde
    //Sorgt dafür, das die "Start"-Methode erst bei Knopfdruck ausgeführt wird
    private bool startbool = true;
    private bool starterbool = false;

    float procentualy = 0f;
    float procentualx = 0f;

    // ______________________________________________________________________________________________________________________________

    // Initialisiere den Ball an der Ausgangslage und setze Velocity in die Spielrichtung
    void Starter()
    {

        if (starterbool)
        {
            resetBall();
            rigidbody.velocity = new Vector3(100, 0, 0);
        }
        startbool = false;

    }

    // ______________________________________________________________________________________________________________________________

    // Setze den Ball neu an die Ausgangsposition 
    void resetBall()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        i = 0;
        //StartCoroutine(ballSpeed());
        velo = new Vector3(0, 0, 0);
        bounce = 0;
    }

    // ______________________________________________________________________________________________________________________________

    // Prüfe in jedem Update() ob eine Kollision mit den Grenzen des Spielfelds vorliegt
    void Update()
    {
        //Debug.Log(GameData.Instance.buttonVal + " - " + GameData.Instance.SW2);
        if (GameData.Instance.powerup1 && GameData.Instance.player1)
        {
            StartCoroutine(ballspeedup());
        }
        if (startbool)
        {
            if (Input.GetKeyDown("o") || (GameData.Instance.buttonVal & GameData.Instance.SW1) != 0 || (GameData.Instance.buttonVal1 & GameData.Instance.SW1) != 0)
            {
                starterbool = true;
                Starter();

            }
        }
        checkBounds();
    }

    // ______________________________________________________________________________________________________________________________

    // Erhört die Geschwindigkeit um ein Viertel von i pro Sekunde
    //IEnumerator ballSpeed()
    //{
    //    if (!GameData.Instance.Out && GameData.Instance.pause == false)
    //    {
    //        for (i = 0; i < 41; i++)
    //        {
    //            //Wenn Spieler1, dann nach rechts beschleunigen
    //            if (player1)
    //            {
    //                velo = new Vector3(i / 4, i / 4, 0);
    //            }
    //            //sonst nach links
    //            else
    //            {
    //                velo = new Vector3(-i / 4, -i / 4, 0);
    //            }
    //            rigidbody.velocity = new Vector3(rigidbody.velocity.x + velo.x, rigidbody.velocity.y + velo.y, rigidbody.velocity.z);
    //            yield return new WaitForSeconds(1.0f);
    //        }
    //    }
    //    else
    //    {
    //        i = 0;
    //    }
    //}

    IEnumerator ballspeedup()
    {
        Vector3 speedup = new Vector3(4, 4, 0);
        for (int j = 0; j < 1; j++)
        {
            if (procentualx == 0f && procentualy == 0f)
            {
                procentualx = rigidbody.velocity.x / 100;
                procentualy = rigidbody.velocity.y / 100;
                Debug.Log("x " + procentualx);
                Debug.Log("y " + procentualy);
            }
        }
        //float x = rigidbody.velocity.x;
        //Debug.Log(x);
        //float y = rigidbody.velocity.y;
        //Debug.Log(x);
        //float z = rigidbody.velocity.z;
        //Debug.Log(x);
        rigidbody.velocity = new Vector3(rigidbody.velocity.x + (procentualx * speedup.x), rigidbody.velocity.y + (procentualx * speedup.y), rigidbody.velocity.z);
        yield return new WaitForSeconds(0.25f);
        rigidbody.velocity = new Vector3(rigidbody.velocity.x - (procentualx * speedup.x), rigidbody.velocity.y - (procentualx * speedup.y), rigidbody.velocity.z);
        procentualx = 0f;
        procentualy = 0f;
        GameData.Instance.powerup1 = false;
        Debug.Log("2: " + GameData.Instance.powerup1);
    }

    // ______________________________________________________________________________________________________________________________

    // Verhalten des Balls bei Kollision mit einer Grenze
    void checkBounds()
    {
        if (Mathf.Abs(transform.position.y) >= ySize)
        {
            resetBall();
        }
    }

    // ______________________________________________________________________________________________________________________________

    // Verhalten des Balls bei Kollision mit der oberen und unteren SpielfeldKante oder mit den Spielern
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Border")
        {
            // negiere y für Eingangswinkel = Ausfallswinkel an den oberen und unteren Grenzen
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y * (-1), rigidbody.velocity.z);
            bounce++;
        }

        if (collider.tag == "otherborder1")
        {
            // ball auf linker seite raus geflogen
            resetBall();
            rigidbody.velocity = new Vector3(+100, 0, 0);
            playerScore[0] += 1;
        }
        if (collider.tag == "otherborder2")
        {
            // ball auf rechter seite raus geflogen
            resetBall();
            rigidbody.velocity = new Vector3(-100, 0, 0);
            playerScore[1] += 1;
        }

        if (collider.tag == "Player")
        {
            // setze den Normalenvektor des GameObjects und berechne dann die neue velocity durch Spiegelung an normal
            if (collider.name == "Player 1")
            {
                Debug.Log("cikk");
                player1 = true;
                GameData.Instance.Touched = true;
                normal = new Vector3((collider.transform.right.x * (-1)), (collider.transform.right.y * (-1)), collider.transform.right.z);
                rigidbody.velocity = Vector3.Reflect(rigidbody.velocity, normal);
                bounce = 0;
            }
            if (collider.name == "Player 2")
            {
                Debug.Log("clikk");
                player1 = false;
                GameData.Instance.Touched = true;
                normal = new Vector3(collider.transform.right.x, collider.transform.right.y, collider.transform.right.z);
                rigidbody.velocity = Vector3.Reflect(rigidbody.velocity, normal);
                bounce = 0;
            }
        }
    }

    // ______________________________________________________________________________________________________________________________

    // gibt die Score der Spieler aus und "malt" das Spielfeld
    void OnGUI()
    {
        GUI.Label(new Rect(10, 50, 220, 30), "Spieler 1: " + playerScore[0]);
        GUI.Label(new Rect(10, 70, 220, 30), "Spieler 2: " + playerScore[1]);
        GUI.Label(new Rect(10, 150, 220, 30), "bla " + rigidbody.velocity.x + " " + rigidbody.velocity.y + " " + rigidbody.velocity.z);

        if (!GameData.Instance.pause && !startbool)
        {
            for (int i = 0; i < Screen.height; i = i + 10)
            {
                GUI.Label(new Rect(Screen.width / 2, i, 10, 15), "|");
            }
        }
        if (startbool)
        {
            if (GameData.Instance.toolbarInt == 0)
                GUI.Label(new Rect(Screen.width / 2 - 55, Screen.height / 3, 220, 30), "Drücke o zum starten");
            else
                GUI.Label(new Rect(Screen.width / 2 - 55, Screen.height / 3, 220, 40), "Drücke o zum starten\noder SW1 auf dem Kontroller");
        }
        if (GameData.Instance.pause)
        {
            GUI.Label(new Rect(Screen.width / 2 - 20, Screen.height / 3, 220, 30), "Pause");
        }
    }
}

