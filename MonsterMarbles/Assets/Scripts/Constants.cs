﻿using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {
	
	public const string TAG_PLAYER = "Player";
	public const string TAG_BUMPER = "Bumper";
	public const string TAG_MARBLE = "Marble";
	
	public const string SCENE_MAIN_MENU = "Main Menu";
	public const string SCENE_PILA_PLAINS = "Pila_Plains_Prototype";
	public const string SCENE_FROSTWIND_MOUNTAIN = "FrostwindMountain";
	
	public static string[] CHARACTER_INDEX = new string[30];
	
	public const float DEFAULT_BUMPER_POWER = 7f;
	public const float DEFAULT_MARBLE_COLLISION_POWER_MULTIPLIER = 10f;
	public const float DEFAULT_MARBLE_COLLISION_POWER_INCREASE = 100f;
	
	
	public static void setupConstants(){
		CHARACTER_INDEX[0] = "Wolfgang";
		CHARACTER_INDEX[1] = "Hotstreak";
		CHARACTER_INDEX[2] = "Lars";
		CHARACTER_INDEX[3] = "Pinpoint";
		CHARACTER_INDEX[4] = "Aurora";
		CHARACTER_INDEX[5] = "Pinpoint";
		CHARACTER_INDEX[6] = "Reginald";
		CHARACTER_INDEX[7] = "Seneshell";
		CHARACTER_INDEX[8] = "Bandit";
		CHARACTER_INDEX[9] = "Jangles";
		CHARACTER_INDEX[10] = "Vanish";
		CHARACTER_INDEX[11] = "Tam-Tam";
		CHARACTER_INDEX[12] = "Juan";
		CHARACTER_INDEX[13] = "Krackles";
		CHARACTER_INDEX[14] = "Razor";
		CHARACTER_INDEX[15] = "Nol";
		CHARACTER_INDEX[16] = "Park";
		CHARACTER_INDEX[17] = "Hundred Track";
	}
	
}