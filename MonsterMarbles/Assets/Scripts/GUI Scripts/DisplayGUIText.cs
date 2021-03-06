﻿using UnityEngine;
using System.Collections;

public class DisplayGUIText : MonoBehaviour {
	
	public static float DEFAULT_ANIMATION_TIME = 3;
	
	public int startingFontSize = 86;
	public int maxFontSize = 108;
	public int minimizedFontSize = 42;
	
	public float animationTime = 3;
	private float animationTimer = 0;
	private bool beginAnimation = false;
	
	private int currentPlayer = 0;
	
	public delegate void showTimedText(string text, float time);
	public static event showTimedText formattedText;
	public static event showTimedText unformattedText;
	
	// Use this for initialization
	void Start () {
		RingerController.PlayerTurnStartEvent += playerChanged;
		DisplayGUIText.formattedText += displayFText;
		DisplayGUIText.unformattedText += displayUfText;
		GetComponent<GUIText>().font = GUIStyles.getMainMenuStyle().font;
		GetComponent<GUIText>().fontSize = maxFontSize;
		playerChanged(0);
	}
	
	void playerChanged(int playerIndex){
		currentPlayer = playerIndex;
	}
	
	public static void displayFormattedText(string text, float animTime = 0){
		if(formattedText != null){
			formattedText(text,animTime);
		}
	}
	
	public static void displayUnformattedText(string text, float animTime = 0){
		if(unformattedText != null){
			unformattedText(text,animTime);
		}
	}
	
	public void displayFText(string text, float animTime = 0){
		this.GetComponent<GUIText>().text = string.Format(text, currentPlayer+1);
		GetComponent<GUIText>().color = new Color(GetComponent<GUIText>().color.r,GetComponent<GUIText>().color.g,GetComponent<GUIText>().color.b,1f);
		this.transform.localPosition = new Vector3(0.5f,0.4f,this.transform.localPosition.z);
		GetComponent<GUIText>().anchor = TextAnchor.MiddleCenter;
		GetComponent<GUIText>().fontSize = startingFontSize;
		if(animTime <= 0){
			animationTime = DEFAULT_ANIMATION_TIME;
		}
		else{
			animationTime = animTime;
		}
		beginAnimation = true;
	}
	
	public void displayUfText(string text, float animTime = 0){
		this.GetComponent<GUIText>().text = text;
		GetComponent<GUIText>().color = new Color(GetComponent<GUIText>().color.r,GetComponent<GUIText>().color.g,GetComponent<GUIText>().color.b,1f);
		this.transform.localPosition = new Vector3(0.5f,0.4f,this.transform.localPosition.z);
		GetComponent<GUIText>().anchor = TextAnchor.MiddleCenter;
		GetComponent<GUIText>().fontSize = startingFontSize;
		if(animTime <= 0){
			animationTime = DEFAULT_ANIMATION_TIME;
		}
		else{
			animationTime = animTime;
		}
		beginAnimation = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(beginAnimation){
			if(animationTimer >= animationTime){
				animationTimer = 0;
				GetComponent<GUIText>().color = new Color(GetComponent<GUIText>().color.r,GetComponent<GUIText>().color.g,GetComponent<GUIText>().color.b,0.8f);
				this.GetComponent<GUIText>().text ="";
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
