using UnityEngine;
using UnityEditor;
using System.Collections;

public class GUIStyles{
	
	private static Texture MENU_UNPUSHED_BUTTON;
	private static Texture MENU_PUSHED_BUTTON;
	
	private static Texture CHARACTER_SELECT_BUTTON_NORMAL;
	private static Texture CHARACTER_SELECT_BUTTON_SELECTED;
	
	private static GUIStyle mainMenuStyle;
	private static GUIStyle characterSelectStyle;
	private static GUIStyle invertedCharacterSelectStyle;
	
	public static Texture getMenuUnpushedButton(){
		if(MENU_UNPUSHED_BUTTON == null){
			MENU_UNPUSHED_BUTTON = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Box_Assets/Pixel Rocket/Textures/GUI Textures/Button_Unpushed.png", typeof(Texture));
			
			return MENU_UNPUSHED_BUTTON;
		}
		else{
			return MENU_UNPUSHED_BUTTON;
		}
	}
	
	public static Texture getMenuPushedButton(){
		if(MENU_PUSHED_BUTTON == null){
			MENU_PUSHED_BUTTON = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Box_Assets/Pixel Rocket/Textures/GUI Textures/Button_Pushed.png", typeof(Texture));
			
			return MENU_PUSHED_BUTTON;
		}
		else{
			return MENU_PUSHED_BUTTON;
		}
		
	}
	
	public static Texture getCharacterSelectButtonNormal(){
		if(CHARACTER_SELECT_BUTTON_NORMAL == null){
			CHARACTER_SELECT_BUTTON_NORMAL = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Box_Assets/Pixel Rocket/Textures/GUI Textures/Character_Select_Button_Normal_2.png", typeof(Texture));
			
			return CHARACTER_SELECT_BUTTON_NORMAL;
		}
		else{
			return CHARACTER_SELECT_BUTTON_NORMAL;
		}
		
	}
	
	public static Texture getCharacterSelectButtonSelected(){
		if(CHARACTER_SELECT_BUTTON_SELECTED == null){
			CHARACTER_SELECT_BUTTON_SELECTED = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Box_Assets/Pixel Rocket/Textures/GUI Textures/Character_Select_Button_Selected_2.png", typeof(Texture));
			
			return CHARACTER_SELECT_BUTTON_SELECTED;
		}
		else{
			return CHARACTER_SELECT_BUTTON_SELECTED;
		}
		
	}
	
	public static GUIStyle getMainMenuStyle(){
		if(mainMenuStyle == null){
			mainMenuStyle = new GUIStyle();
			mainMenuStyle.normal.textColor = Color.white;
			mainMenuStyle.normal.background = (Texture2D)getMenuUnpushedButton();
			
			mainMenuStyle.hover.textColor = Color.black;
			mainMenuStyle.hover.background = (Texture2D)getMenuUnpushedButton();
			
			mainMenuStyle.active.textColor = Color.black;
			mainMenuStyle.active.background = (Texture2D)getMenuPushedButton();
			
			mainMenuStyle.fontSize = 36;
			
			mainMenuStyle.alignment = TextAnchor.MiddleCenter;
			mainMenuStyle.imagePosition = ImagePosition.ImageAbove;
			
			mainMenuStyle.stretchWidth = true;
			mainMenuStyle.stretchHeight = true;
			
			return mainMenuStyle;
			
		}
		else{
			return mainMenuStyle;
		}
	}
	
	public static GUIStyle getCharacterSelectStyle(){
		if(characterSelectStyle == null){
			characterSelectStyle = new GUIStyle();
			characterSelectStyle.normal.textColor = Color.black;
			characterSelectStyle.normal.background = (Texture2D)getCharacterSelectButtonNormal();
			
			characterSelectStyle.hover.textColor = Color.white;
			characterSelectStyle.hover.background = (Texture2D)getCharacterSelectButtonSelected();
			
			characterSelectStyle.active.textColor = Color.red;
			characterSelectStyle.active.background = (Texture2D)getCharacterSelectButtonSelected();
			
			characterSelectStyle.padding = new RectOffset(20,20,20,20);
			
			characterSelectStyle.fontSize = 28;
			
			characterSelectStyle.alignment = TextAnchor.MiddleCenter;
			characterSelectStyle.imagePosition = ImagePosition.ImageAbove;
			
			characterSelectStyle.stretchWidth = true;
			characterSelectStyle.stretchHeight = true;
			
			return characterSelectStyle;
			
		}
		else{
			return characterSelectStyle;
		}
	}
	
	public static GUIStyle getInvertedCharacterSelectStyle(){
		if(invertedCharacterSelectStyle == null){
			invertedCharacterSelectStyle = new GUIStyle();
			invertedCharacterSelectStyle.normal.textColor = Color.white;
			invertedCharacterSelectStyle.normal.background = (Texture2D)getCharacterSelectButtonSelected();
			
			invertedCharacterSelectStyle.hover.textColor = Color.white;
			invertedCharacterSelectStyle.hover.background = (Texture2D)getCharacterSelectButtonSelected();
			
			invertedCharacterSelectStyle.active.textColor = Color.white;
			invertedCharacterSelectStyle.active.background = (Texture2D)getCharacterSelectButtonSelected();
			
			invertedCharacterSelectStyle.padding = new RectOffset(10,10,20,20);
			
			invertedCharacterSelectStyle.fontSize = 28;
			
			invertedCharacterSelectStyle.alignment = TextAnchor.MiddleCenter;
			invertedCharacterSelectStyle.imagePosition = ImagePosition.ImageOnly;
			
			invertedCharacterSelectStyle.stretchWidth = true;
			invertedCharacterSelectStyle.stretchHeight = true;
			
			return invertedCharacterSelectStyle;
			
		}
		else{
			return invertedCharacterSelectStyle;
		}
	}
	
}
