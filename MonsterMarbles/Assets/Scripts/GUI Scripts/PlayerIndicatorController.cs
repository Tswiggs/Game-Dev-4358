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
		GetComponent<GUIText>().font = GUIStyles.getMainMenuStyle().font;
		GetComponent<GUIText>().fontSize = maxFontSize;
		playerChanged(0);
	}
	
	void playerChanged(int playerIndex){
		currentPlayer = playerIndex;
		this.GetComponent<GUIText>().text ="Player "+(currentPlayer+1).ToString();
		GetComponent<GUIText>().color = new Color(GetComponent<GUIText>().color.r,GetComponent<GUIText>().color.g,GetComponent<GUIText>().color.b,1f);
		this.transform.localPosition = new Vector3(0.5f,0.8f,this.transform.localPosition.z);
		GetComponent<GUIText>().anchor = TextAnchor.MiddleCenter;
		GetComponent<GUIText>().fontSize = startingFontSize;
		beginAnimation = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(beginAnimation){
			if(animationTimer >= animationTime){
				animationTimer = 0;
				GetComponent<GUIText>().color = new Color(GetComponent<GUIText>().color.r,GetComponent<GUIText>().color.g,GetComponent<GUIText>().color.b,0.8f);
				GetComponent<GUIText>().fontSize = minimizedFontSize;
				this.GetComponent<GUIText>().text ="Player "+(currentPlayer+1).ToString();
				this.transform.localPosition = new Vector3(.98f,0.1f,this.transform.localPosition.z);
				GetComponent<GUIText>().anchor = TextAnchor.MiddleRight;
				beginAnimation = false;
				
			}
			else{
				if(animationTimer >= animationTime/2){
					float newAlphaValue = Mathf.Lerp(GetComponent<GUIText>().color.a,-1f,Time.deltaTime*0.5f);
					if(newAlphaValue < 0f){
						newAlphaValue = 0f;
					}
					GetComponent<GUIText>().color = new Color(GetComponent<GUIText>().color.r,GetComponent<GUIText>().color.g,GetComponent<GUIText>().color.b,newAlphaValue);
				}
				animationTimer += Time.deltaTime;
				GetComponent<GUIText>().fontSize = (int) Mathf.Lerp(GetComponent<GUIText>().fontSize,maxFontSize,Time.deltaTime);
			}
		}
	}
}
