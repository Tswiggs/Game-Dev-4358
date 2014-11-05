using UnityEngine;
using System.Collections;

public class AddZugiePower : MonoBehaviour {

	public string monster; 
	// Use this for initialization
	void Start () {
		addPower (); 
	}
	
	/// <summary>
	/// Adds the power script to a specified Zugie's "Ball". 
	/// </summary>
	void addPower()
	{
	switch (monster) 
		{
		case "wolfgang": 
			gameObject.transform.Find ("Ball").gameObject.AddComponent<WolfgangPower>();
			break;
		case "hotstreak":
			break;
		default:
			break;
		}
	}
}
