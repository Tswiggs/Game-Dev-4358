using UnityEngine;
using System.Collections;

public class AddZugiePower : MonoBehaviour {

	/// <summary>
	/// The lower case string name of the zugie.
	/// </summary>
	public string zugieName; 
	// Use this for initialization
	void Start () {
		addPower (); 
	}
	
	/// <summary>
	/// Adds the power script to a specified Zugie's "Ball". 
	/// </summary>
	void addPower()
	{
	switch (zugieName.ToLower()) 
		{
		case "wolfgang": 
			gameObject.transform.Find ("Ball").gameObject.AddComponent<WolfgangPower>();
			break;
		case "hotstreak":
			gameObject.transform.Find ("Ball").gameObject.AddComponent<HotStreakPower>();

			break;
		default:
			break;
		}
	}
}
