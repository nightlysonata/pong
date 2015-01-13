using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {

    // ______________________________________________________________________________________________________________________________

    string[] toolbarStrings = new string[] {"Keyboard"};
    string[] toolbarStrings1 = new string[] { "Keyboard", "1 Controller"};
    string[] toolbarStrings2 = new string[] { "Keyboard", "1 Controller", "2 Controller" };

    public GUIStyle modified = new GUIStyle();

    // ______________________________________________________________________________________________________________________________

    //void Awake()
    //{
    //    GameData.Instance.stream();
    //}

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
        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 80, 100, 30), "Options:", modified);
        if (GameData.Instance.controller == 0)
        {
            GameData.Instance.toolbarInt = GUI.Toolbar(new Rect(Screen.width / 2 - 225, Screen.height / 2 - 15, 150, 30), GameData.Instance.toolbarInt, toolbarStrings);
            GUI.Label(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 12, 150, 30), "1 Controller");
            GUI.Label(new Rect(Screen.width / 2 + 100, Screen.height / 2 - 12, 150, 30), "2 Controller");
        }
        if (GameData.Instance.controller == 1)
        {
            GameData.Instance.toolbarInt = GUI.Toolbar(new Rect(Screen.width / 2 - 225, Screen.height / 2 - 15, 300, 30), GameData.Instance.toolbarInt, toolbarStrings1);
            GUI.Label(new Rect(Screen.width / 2 + 100, Screen.height / 2 - 12, 150, 30), "2 Controller");
        }
        if (GameData.Instance.controller == 2)
        {
            GameData.Instance.toolbarInt = GUI.Toolbar(new Rect(Screen.width / 2 - 225, Screen.height / 2 - 15, 450, 30), GameData.Instance.toolbarInt, toolbarStrings2);
        }
        if (Input.GetKey("escape"))
            Application.LoadLevel("MainMenu");
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 50, 100, 30), "Back"))
            Application.LoadLevel("MainMenu");
    }
}
