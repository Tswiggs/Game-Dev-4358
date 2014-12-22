using UnityEngine;
using System.Collections;

public class MapChangeButtonController : MonoBehaviour {
	
	public Texture mapChangeButton;
	public Texture returnButton;
	
	public delegate void buttonPressed(ViewType buttonType);
	
	private ViewType view = ViewType.BALL_VIEW;
	
	public enum ViewType {
		AERIAL_VIEW, BALL_VIEW 
	};
	
	public static event buttonPressed mapChangeButtonPressed;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			if(guiTexture.GetScreenRect().Contains(new Vector2(Input.mousePosition.x,Input.mousePosition.y))){
				
				if(view == ViewType.BALL_VIEW){
					if(mapChangeButtonPressed != null){
						mapChangeButtonPressed(ViewType.AERIAL_VIEW);
					}
					
					guiTexture.texture = returnButton;
					
					view = ViewType.AERIAL_VIEW;
				}
				else{
					if(mapChangeButtonPressed != null){
						mapChangeButtonPressed(ViewType.BALL_VIEW);
					}
					
					guiTexture.texture = mapChangeButton;
					
					view = ViewType.BALL_VIEW;
				}
			}
		}
	}
}
