using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {

	public const string TAG_PLAYER = "Player";
	public const string TAG_BUMPER = "Bumper";
	public const string TAG_MARBLE = "Marble";

	public const string SCENE_MAIN_MENU = "Main Menu";
	public const string SCENE_PILA_PLAINS = "Pila_Plains_Prototype";
	
	public static string[] CHARACTER_INDEX = new string[30];
	
	public const float DEFAULT_BUMPER_POWER = 7f;
	public const float DEFAULT_MARBLE_COLLISION_POWER_MULTIPLIER = 10f;
	public const float DEFAULT_MARBLE_COLLISION_POWER_INCREASE = 100f;
	
	
	public static void setupConstants(){
		CHARACTER_INDEX[0] = "Wolfgang";
		CHARACTER_INDEX[1] = "Hotstreak";
		CHARACTER_INDEX[2] = "Lars";
		CHARACTER_INDEX[3] = "Snowdrop";
		CHARACTER_INDEX[4] = "Aurora";
	}
	
}
