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
	public AudioClip ghostCloudSound;
	
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
	/// <summary>
	/// Has the ball this ability is attached to launched yet? 
	/// </summary>
	private bool hasLaunched = false;
	// Use this for initialization
	void Start () {
	
		ghostWolfgangPrefab = Resources.Load("Ghost Wolfgang") as GameObject;
		ghostCloudSound = Resources.Load ("Wolf Power magical-burst") as AudioClip;
		TurnFlowController.TurnBeginEvent += turnStarted;
	}
	
	public void turnStarted(GameObject zoogi){
		powerCharged = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(GetComponent<ZoogiController>().getCurrentState() == ZoogiController.State.ROLLING){
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
				if(powerCharged){
					summonWolves();
				}
				powerCharged = false;
			}
		}
		
		if (isActivated && !hasLaunched) {
				wolfgangBall2.transform.rotation = wolfgangBallOriginal.transform.rotation; 
				wolfgangBall2.GetComponent<Rigidbody>().velocity = wolfgangBallOriginal.GetComponent<Rigidbody>().velocity;
				wolfgangBall3.transform.rotation = wolfgangBallOriginal.transform.rotation;
				wolfgangBall3.GetComponent<Rigidbody>().velocity = wolfgangBallOriginal.GetComponent<Rigidbody>().velocity;
		}
	}
	
	public void summonWolves(){
		GameAudioController.playOneShotSound(ghostCloudSound);
		GameObject ghost1 = Instantiate(ghostWolfgangPrefab) as GameObject;
		ghost1.transform.SetParent(transform);
		ghost1.transform.position = transform.FindChild("Ball").position+transform.FindChild("Ball").transform.right;
		ghost1.GetComponent<Rigidbody>().AddForce((transform.FindChild("Ball").transform.right)*(ZoogiLaunchBehavior.MAX_LAUNCH_POWER/5f),ForceMode.Impulse);
		GameObject ghost2 = Instantiate(ghostWolfgangPrefab) as GameObject;
		ghost2.transform.SetParent(transform);
		ghost2.transform.position = transform.FindChild("Ball").position+transform.FindChild("Ball").transform.right*-1;
		ghost2.GetComponent<Rigidbody>().AddForce((transform.FindChild("Ball").transform.right*-1)*(ZoogiLaunchBehavior.MAX_LAUNCH_POWER/5f),ForceMode.Impulse);
		GameObject ghost3 = Instantiate(ghostWolfgangPrefab) as GameObject;
		ghost3.transform.SetParent(transform);
		ghost3.transform.position = transform.FindChild("Ball").position+transform.FindChild("Character Root").transform.forward;
		ghost3.GetComponent<Rigidbody>().AddForce((transform.FindChild("Character Root").transform.forward)*(ZoogiLaunchBehavior.MAX_LAUNCH_POWER/2f),ForceMode.Impulse);
	}
	
	override public bool deployPower(){
		if(powerCharged){
			powerCharged = false;
			readyToDeployPower = false;
			createGangOfWolves(this, new EventArgs());
			LaunchController.sendLaunchInformation += launchGhostWolves;
			
			return true;
		}
		return false;
	}
	
	public void launchGhostWolves(GameObject ball, Vector3 launchVector, float xTorque, Vector3 position){
		if(!hasLaunched){
			wolfgangBall2.GetComponent<Rigidbody>().AddForce(launchVector);
			wolfgangBall2.GetComponent<Rigidbody>().AddRelativeTorque(xTorque, 0f, 0f);
			wolfgangBall3.GetComponent<Rigidbody>().AddForce(launchVector);
			wolfgangBall3.GetComponent<Rigidbody>().AddRelativeTorque(xTorque, 0f, 0f);
			LaunchController.sendLaunchInformation -= launchGhostWolves;
			SteeringController.rollCompleted += rollComplete;
			hasLaunched = true;
		}
	}
	

	public void createGangOfWolves(object sender, EventArgs e)
	{
		if (!isActivated) {
			
			
			wolfgangBall2 = Instantiate(ghostWolfgangPrefab, wolfgangBallOriginal.transform.position /*+ new Vector3 (2, 0, -2)*/, wolfgangBallOriginal.transform.rotation) as GameObject;
			//wolfgangBall2.transform.parent = wolfgangBallOriginal.transform.parent;
			
			wolfgangBall2.transform.position += wolfgangBallOriginal.transform.right*ghostDistance.x;
			
			//wolfgangBall2.transform.localPosition = new Vector3(wolfgangBall2.transform.localPosition.x+ghostDistance.x,wolfgangBall2.transform.localPosition.y,wolfgangBall2.transform.localPosition.z+ghostDistance.y);
			wolfgangBall2 = wolfgangBall2.transform.Find("Ball").gameObject;
			wolfgangBall3 = Instantiate (ghostWolfgangPrefab, wolfgangBallOriginal.transform.position /*+ new Vector3 (-2, 0, 2)*/, wolfgangBallOriginal.transform.rotation) as GameObject;
			//wolfgangBall3.transform.parent = wolfgangBallOriginal.transform.parent;
			
			wolfgangBall3.transform.position += wolfgangBallOriginal.transform.right*ghostDistance.x*-1;
			
			//wolfgangBall3.transform.localPosition = new Vector3(wolfgangBall3.transform.localPosition.x-ghostDistance.x,wolfgangBall3.transform.localPosition.y,wolfgangBall3.transform.localPosition.z+ghostDistance.y);
			wolfgangBall3 = wolfgangBall3.transform.Find("Ball").gameObject;
     		isActivated = true; 
     		
			wolfgangBallOriginal.GetComponent<AudioSource>().PlayOneShot(ghostCloudSound);
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
		hasLaunched = false;
		SteeringController.rollCompleted -= rollComplete;
	}
	
	private void onEnable(){
		wolfgangBallOriginal = transform.FindChild("Ball").gameObject;
	}
	
	private void onDisable(){
		LaunchController.sendLaunchInformation -= launchGhostWolves;
	}
}
