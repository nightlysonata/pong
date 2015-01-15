using UnityEngine;
using System.Collections;

public class LvlAuswahl : MonoBehaviour {

    public GUIStyle tooltips = new GUIStyle();
    public GUIStyle buttons2 = new GUIStyle();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        tooltips.fontSize = Screen.height / 30;
        buttons2.fontSize = Screen.height / 35;
	}

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 30, Screen.height / 2 - Screen.height / 3, 100, 30), "Gamemode:", GameData.Instance.modified);
        Rect but1 = new Rect(Screen.width / 2 - Screen.width / 14, Screen.height / 2 - 3 * Screen.height / 15, Screen.width / 7, Screen.height / 12.5f);
        Rect but2 = new Rect(Screen.width / 2 - Screen.width / 14, Screen.height / 2 - Screen.height / 15, Screen.width / 7, Screen.height / 12.5f);
        Rect but3 = new Rect(Screen.width / 2 - Screen.width / 14, Screen.height / 2 + Screen.height / 15, Screen.width / 7, Screen.height / 12.5f);
        Rect but4 = new Rect(Screen.width / 2 - Screen.width / 14, Screen.height / 2 + 3 * Screen.height / 15, Screen.width / 7, Screen.height / 12.5f);

        if (but1.Contains(Event.current.mousePosition))
        {
            GUI.Label(new Rect(Screen.width / 2 + 3 * Screen.width / 14, Screen.height / 2 - Screen.height / 15, Screen.width / 7, Screen.height / 8), "Klassisches Pong, ohne PowerUps, mit endlosem Counter fuer beide Spieler", tooltips);
        }

        if (but2.Contains(Event.current.mousePosition))
        {
            GUI.Label(new Rect(Screen.width / 2 + 3 * Screen.width / 14, Screen.height / 2 - Screen.height / 15, Screen.width / 7, Screen.height / 8), "Pong mit PowerUps, bis zu einem festgelegten Spielstand von 21 Punkten", tooltips);
        }

        if (but3.Contains(Event.current.mousePosition))
        {
            GUI.Label(new Rect(Screen.width / 2 + 3 * Screen.width / 14, Screen.height / 2 - Screen.height / 15, Screen.width / 7, Screen.height / 8), "Pong mit PowerUps, mit endlosem Counter fuer beide Spieler", tooltips);
        }

        if (but4.Contains(Event.current.mousePosition) && GameData.Instance.toolbarInt == 2)
        {
            GUI.Label(new Rect(Screen.width / 2 + 3 * Screen.width / 14, Screen.height / 2 - Screen.height / 15, Screen.width / 7, Screen.height / 8), "Pong mit PowerUps, mit Steuerung ueber das Accelero- meter, bis zu einem festgelegten Spielstand von 21 Punkten", tooltips);
        }

        if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 14, Screen.height / 2 - 3*Screen.height / 15, Screen.width / 7, Screen.height / 12.5f), "Classic", GameData.Instance.buttons))
        {
            GameData.Instance.gamemode = 1;
            Application.LoadLevel("Classic");
        };
        if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 14, Screen.height / 2 - Screen.height / 15, Screen.width / 7, Screen.height / 12.5f), "Matchball", GameData.Instance.buttons))
        {
            GameData.Instance.gamemode = 2;

        };
        if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 14, Screen.height / 2 + Screen.height / 15, Screen.width / 7, Screen.height / 12.5f), "Endless", GameData.Instance.buttons))
        {
            GameData.Instance.gamemode = 3;
            Application.LoadLevel("level1");
        };
        if (GameData.Instance.toolbarInt == 2)
        {
        if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 14, Screen.height / 2 + 3*Screen.height / 15, Screen.width / 7, Screen.height / 12.5f), "Extreme", GameData.Instance.buttons))
            {
                GameData.Instance.gamemode = 4;
                GameData.Instance.extreme = true;
                Application.LoadLevel("level1");
            }
        }
        if (Input.GetKey("escape"))
            Application.LoadLevel("MainMenu");
        if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 18, Screen.height / 2 + 5 * Screen.height / 15, Screen.width / 9, Screen.height / 15), "Back", buttons2))
            Application.LoadLevel("MainMenu");
    }
}
