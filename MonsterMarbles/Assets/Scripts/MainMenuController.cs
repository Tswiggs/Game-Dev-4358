using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}

	void OnGUI(){

		GUI.backgroundColor = new Color (237, 27, 27);

		GUIStyle redStyle = new GUIStyle(GUI.skin.button);
		redStyle.fontSize = 36;
		redStyle.alignment = TextAnchor.MiddleCenter;
		redStyle.hover.textColor = Color.red;
		//redStyle.onHover.textColor = Color.red;

		if (GUI.Button (new Rect (Screen.width / 2 - 110, (Screen.height/4)*3 -60, 220, 120), "New Game", redStyle)) {
			Application.LoadLevel(Constants.SCENE_PILA_PLAINS);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
