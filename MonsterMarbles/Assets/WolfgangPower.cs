using UnityEngine;
using TouchScript.Gestures;
using System.Collections;
using System;
/// <summary>
/// Listens for the user to tap the screen, then activates the special ability.
/// </summary>

/**
 * This class should never be directly applied to a prefab. It is automatically mapped by the "AddZugiePower" script, which is what should be added to prefabs. 
 **/ 
public class WolfgangPower : ZoogiPower {

	/// <summary>
	/// the original wolf gang ball. Point this to Wolfgang's "Ball" object. 
	/// </summary>
	public GameObject wolfgangBallOriginal;
	/// <summary>
	/// The ghost prefab object that will be instantiated when Wolfgang's power is activated. 
	/// </summary>
	public GameObject ghostWolfgangPrefab;
	/// <summary>
	/// How far away the ghost Wolfgangs should be from the main Wolfgang.
	/// The first is the x value (side to side) and will be NEGATIZED for the second clone.
	/// The second value is the z value (forward/backward) and is the same for both. 
	/// </summary>
	public Vector2 ghostDistance = new Vector2(2f,-0.5f);
	/// <summary>
	/// wolfgang's first duplicate. 
	/// </summary>
	private GameObject wolfgangBall2;
	/// <summary>
	/// wolfgang's second duplicate. 
	/// </summary>
	private GameObject wolfgangBall3;
	/// <summary>
	/// Is the ability activated yet? 
	/// </summary>
	private bool isActivated = false; 
	// Use this for initialization
	void Start () {
		wolfgangBallOriginal = transform.FindChild("Ball").gameObject;
		//wolfGangBallOriginal.GetComponent<TapGesture> ().Tapped += createGangOfWolves;
		SteeringController.rollCompleted += rollComplete;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown("space")){
			activatePower ();
		}
		if (isActivated) {
						wolfgangBall2.transform.rotation = wolfgangBallOriginal.transform.rotation; 
						wolfgangBall2.rigidbody.velocity = wolfgangBallOriginal.rigidbody.velocity;
						wolfgangBall3.transform.rotation = wolfgangBallOriginal.transform.rotation;
						wolfgangBall3.rigidbody.velocity = wolfgangBallOriginal.rigidbody.velocity;
				}
	}
	
	override public bool activatePower(){
		if(powerReady){
			createGangOfWolves(this, new EventArgs());
			return true;
		}
		else{
			return false;
		}
	}

	public void createGangOfWolves(object sender, EventArgs e)
	{
		if ((!isActivated) && (wolfgangBallOriginal.GetComponent<SteeringController>().isRolling == true)) {
			wolfgangBall2 = Instantiate (ghostWolfgangPrefab, wolfgangBallOriginal.transform.position /*+ new Vector3 (2, 0, -2)*/, wolfgangBallOriginal.transform.rotation) as GameObject;
			wolfgangBall2.transform.parent = wolfgangBallOriginal.transform.parent;
			wolfgangBall2.transform.localPosition = new Vector3(wolfgangBall2.transform.localPosition.x+ghostDistance.x,wolfgangBall2.transform.localPosition.y,wolfgangBall2.transform.localPosition.z+ghostDistance.y);
			wolfgangBall2 = wolfgangBall2.transform.Find("Ball").gameObject;
			wolfgangBall3 = Instantiate (ghostWolfgangPrefab, wolfgangBallOriginal.transform.position /*+ new Vector3 (-2, 0, 2)*/, wolfgangBallOriginal.transform.rotation) as GameObject;
			wolfgangBall3.transform.parent = wolfgangBallOriginal.transform.parent;
			wolfgangBall3.transform.localPosition = new Vector3(wolfgangBall3.transform.localPosition.x-ghostDistance.x,wolfgangBall3.transform.localPosition.y,wolfgangBall3.transform.localPosition.z+ghostDistance.y);
			wolfgangBall3 = wolfgangBall3.transform.Find("Ball").gameObject;
     		isActivated = true; 
		}

	}

	public void rollComplete()
	{
		if(wolfgangBall2 != null){
			Destroy (wolfgangBall2.transform.parent.gameObject);
		}
		
		if(wolfgangBall3 != null){
			Destroy (wolfgangBall3.transform.parent.gameObject);
		}
		isActivated = false; 
	}
}
