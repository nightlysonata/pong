using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public GUIStyle modified = new GUIStyle();
    public GUIStyle buttons = new GUIStyle();

    // ______________________________________________________________________________________________________________________________

    void Awake()
    {
        GameData.Instance.stream();
    }

	// Use this for initialization
	void Start () {
	
	}

    // ______________________________________________________________________________________________________________________________

	// Update is called once per frame
	void Update () {
        modified.fontSize = Screen.height / 11;
        buttons.fontSize = Screen.height / 25;
        GameData.Instance.modified = modified;
        GameData.Instance.buttons = buttons;

	}

    // ______________________________________________________________________________________________________________________________

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - Screen.height/5.5f, 100, 30), "Main Menu:", GameData.Instance.modified);

        //Lädt Level1-scene (= Spiel)
        if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 18, Screen.height / 2 - Screen.height / 15, Screen.width / 9, Screen.height/12.5f), "Play", GameData.Instance.buttons))
            Application.LoadLevel("lvlauswahl");

        //Lädt die Options-scene
        if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 18, Screen.height / 2 + Screen.height / 15, Screen.width / 9, Screen.height / 12.5f), "Options", GameData.Instance.buttons))
            Application.LoadLevel("options");
    }
}
