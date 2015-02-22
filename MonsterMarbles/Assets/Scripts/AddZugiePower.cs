using UnityEngine;
using System.Collections;

/// <summary>
/// This class should be added directly to a zugie prefab. After adding it, make sure that you modify the zugieName variable 
/// in UNITY to match the name of the zugie it is applied to, e.x. "wolfgang". The addPower function will then map the correct zugie power based on 
/// the zugie name. 
/// </summary>
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
