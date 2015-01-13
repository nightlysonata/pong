using UnityEngine;
using System.Collections;

public class LvlAuswahl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 100, 150, 30), "Classic"))
        {
            Application.LoadLevel("Classic");
        };
        if (GUI.Button(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 50, 150, 30), "Matchball"))
        {

        };
        if (GUI.Button(new Rect(Screen.width / 2 - 25, Screen.height / 2 , 150, 30), "Endless"))
        {
            Application.LoadLevel("level1");
        };
        if (GameData.Instance.toolbarInt == 2)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 25, Screen.height / 2 + 50, 150, 30), "Extreme"))
            {
                GameData.Instance.extreme = true;
                Application.LoadLevel("level1");
            }
        }
        if (Input.GetKey("escape"))
            Application.LoadLevel("MainMenu");
        if (GUI.Button(new Rect(Screen.width / 2 , Screen.height / 2 +100, 100, 30), "Back"))
            Application.LoadLevel("MainMenu");
    }
}
