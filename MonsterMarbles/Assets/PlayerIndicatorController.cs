using UnityEngine;
using System.Collections;

public class PlayerIndicatorController : MonoBehaviour {
	
	public int startingFontSize = 86;
	public int maxFontSize = 108;
	public int minimizedFontSize = 42;
	
	public float animationTime = 3;
	private float animationTimer = 0;
	private bool beginAnimation = false;
	
	private int currentPlayer = 0;
	
	// Use this for initialization
	void Start () {
		RingerController.PlayerTurnStartEvent += playerChanged;
		guiText.font = GUIStyles.getMainMenuStyle().font;
		guiText.fontSize = maxFontSize;
		playerChanged(0);
	}
	
	void playerChanged(int playerIndex){
		currentPlayer = playerIndex;
		this.guiText.text ="Player "+(currentPlayer+1).ToString();
		guiText.color = new Color(guiText.color.r,guiText.color.g,guiText.color.b,1f);
		this.transform.localPosition = new Vector3(0.5f,0.8f,this.transform.localPosition.z);
		guiText.anchor = TextAnchor.MiddleCenter;
		guiText.fontSize = startingFontSize;
		beginAnimation = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(beginAnimation){
			if(animationTimer >= animationTime){
				animationTimer = 0;
				guiText.color = new Color(guiText.color.r,guiText.color.g,guiText.color.b,0.8f);
				guiText.fontSize = minimizedFontSize;
				this.guiText.text ="Player "+(currentPlayer+1).ToString();
				this.transform.localPosition = new Vector3(.98f,0.1f,this.transform.localPosition.z);
				guiText.anchor = TextAnchor.MiddleRight;
				beginAnimation = false;
				
			}
			else{
				if(animationTimer >= animationTime/2){
					float newAlphaValue = Mathf.Lerp(guiText.color.a,-1f,Time.deltaTime*0.5f);
					if(newAlphaValue < 0f){
						newAlphaValue = 0f;
					}
					guiText.color = new Color(guiText.color.r,guiText.color.g,guiText.color.b,newAlphaValue);
				}
				animationTimer += Time.deltaTime;
				guiText.fontSize = (int) Mathf.Lerp(guiText.fontSize,maxFontSize,Time.deltaTime);
			}
		}
	}
}
