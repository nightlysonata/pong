using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {

    // ______________________________________________________________________________________________________________________________

    string[] toolbarStrings = new string[] {"Keyboard"};
    string[] toolbarStrings1 = new string[] { "Keyboard", "1 Controller"};
    string[] toolbarStrings2 = new string[] { "Keyboard", "1 Controller", "2 Controller" };

    public GUIStyle toolbar = new GUIStyle();
    public GUIStyle fail = new GUIStyle();
    public GUIStyle buttons2 = new GUIStyle();

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
        toolbar.fontSize = Screen.height / 25;
        fail.fontSize = Screen.height / 25;
        buttons2.fontSize = Screen.height / 35;
	}

    // ______________________________________________________________________________________________________________________________

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - Screen.height / 5, 100, 30), "Options:", GameData.Instance.modified);
        if (GameData.Instance.controller == 0)
        {
            GameData.Instance.toolbarInt = GUI.Toolbar(new Rect(Screen.width / 2 - 3 * Screen.width / 10, Screen.height / 2 - Screen.height / 15, Screen.width / 5, Screen.height / 12.5f), GameData.Instance.toolbarInt, toolbarStrings, toolbar);
            GUI.Label(new Rect(Screen.width / 2 - Screen.width / 10, Screen.height / 2 - Screen.height / 15, Screen.width / 5, Screen.height / 12.5f), "1 Controller", fail);
            GUI.Label(new Rect(Screen.width / 2 + Screen.width / 10, Screen.height / 2 - Screen.height / 15, Screen.width / 5, Screen.height / 12.5f), "2 Controller", fail);
        }
        if (GameData.Instance.controller == 1)
        {
            GameData.Instance.toolbarInt = GUI.Toolbar(new Rect(Screen.width / 2 - 225, Screen.height / 2 - 15, 300, 30), GameData.Instance.toolbarInt, toolbarStrings1, toolbar);
            GUI.Label(new Rect(Screen.width / 2 + 100, Screen.height / 2 - 12, 150, 30), "2 Controller");
        }
        if (GameData.Instance.controller == 2)
        {
            GameData.Instance.toolbarInt = GUI.Toolbar(new Rect(Screen.width / 2 - 225, Screen.height / 2 - 15, 450, 30), GameData.Instance.toolbarInt, toolbarStrings2, toolbar);
        }
        if (Input.GetKey("escape"))
            Application.LoadLevel("MainMenu");
        if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 18, Screen.height / 2 + 2 * Screen.height / 15, Screen.width / 9, Screen.height / 15), "Back", buttons2))
            Application.LoadLevel("MainMenu");
    }
}
