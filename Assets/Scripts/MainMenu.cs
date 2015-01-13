using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public GUIStyle modified = new GUIStyle();

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
	
	}

    // ______________________________________________________________________________________________________________________________

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 80, 100, 30), "Main Menu:", modified);

        //Lädt Level1-scene (= Spiel)
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 15, 100, 50), "Play"))
            Application.LoadLevel("lvlauswahl");

        //Lädt die Options-scene
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 50, 100, 50), "Options"))
            Application.LoadLevel("options");
    }
}
