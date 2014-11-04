using UnityEngine;
using System.Collections;

public class CharacterSelectController : MonoBehaviour {
	
	private Vector2 scrollPosition;
	private ArrayList unlockedCharacters;
	private ArrayList charactersSelected;
	
	private int numberOfCharacters;
	
	
	private int interfaceNavigation;
	
	private const int CHARACTER_SELECT_BOX_WIDTH = 160;
	private const int CHARACTER_SELECT_BOX_HEIGHT = 160;
	// Use this for initialization
	void Start () {
		interfaceNavigation = 0;
		numberOfCharacters = 2;
		
		unlockedCharacters = new ArrayList();
		unlockedCharacters[0] = true;
		unlockedCharacters[1] = true;
		charactersSelected = new ArrayList();
	}
	
	void OnGUI(){
		
		GUIStyle redStyle = new GUIStyle(GUI.skin.button);
		redStyle.fontSize = 28;
		redStyle.alignment = TextAnchor.UpperCenter;
		redStyle.hover.textColor = Color.red;
		
		GUI.backgroundColor = new Color (27, 27, 27);
		
		float scrollBoxWidth = Mathf.Floor(((Screen.width - Screen.width/4)/CHARACTER_SELECT_BOX_WIDTH)) * CHARACTER_SELECT_BOX_WIDTH; 
		
		scrollPosition = GUI.BeginScrollView(new Rect(Screen.width/8, Screen.height/8, Screen.width - Screen.width/4, Screen.height-Screen.height/4),scrollPosition,new Rect(0,0, scrollBoxWidth, Screen.height*2));
		int xPos = 0;
		int yPos = 0;
		
		for(int num =0; num < 30; num++){
			if(xPos >= (scrollBoxWidth)){
				xPos = 0;
				yPos+= CHARACTER_SELECT_BOX_HEIGHT;
			}
			if (GUI.Button (new Rect(xPos,yPos,CHARACTER_SELECT_BOX_WIDTH,CHARACTER_SELECT_BOX_HEIGHT), num.ToString (),redStyle)){
				//Click Behavior
			}
			xPos += CHARACTER_SELECT_BOX_WIDTH;
		
		}
		GUI.EndScrollView();
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
