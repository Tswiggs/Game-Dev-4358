using UnityEngine;
using System.Collections;

public class ZoogiController : MonoBehaviour {

	private GameObject zoogiBall;
	private GameObject zoogiCharacterRoot;
	
	private int skyBitsCarried;
	private int maximumSkyBitsCarried = 5;
	public static float SKY_BIT_MASS_ADD = 5;
	
	public delegate void focusChange(int index);
	public static event focusChange ZoogiTurnStartEvent;
	public static event focusChange ZoogiTurnCompleteEvent;
	
	public delegate void itemAcquired(GameObject zoogi);
	public static event itemAcquired skyBitAcquired;

	// Use this for initialization
	void Start () {
		zoogiBall = this.transform.FindChild("Ball").gameObject;
		zoogiCharacterRoot = this.transform.FindChild("Character Root").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void refreshZoogiReferences(){
		zoogiBall = this.transform.FindChild("Ball").gameObject;
		zoogiCharacterRoot = this.transform.FindChild("Character Root").gameObject;
	}
	
	public void startTurn(){
		
		SteeringController.rollCompleted += endTurn;
		LaunchController.launchCompleted += activateSteering;
		if(ZoogiTurnStartEvent != null){
			ZoogiTurnStartEvent(getObjectID());
		}
		activateLaunching();
	}
	
	public void endTurn(){
		SteeringController.rollCompleted -= endTurn;
		deactivateControlScripts();
		if(ZoogiTurnCompleteEvent != null){
			ZoogiTurnCompleteEvent(getObjectID());
		}
	}

	void activateLaunching(){
		stopBallMotion();
		zoogiCharacterRoot.transform.FindChild("CharacterGUI").gameObject.SetActive(true);
		zoogiBall.GetComponent<SteeringController>().enabled = false;
		zoogiBall.GetComponent<LaunchController>().enabled = true;
		zoogiBall.GetComponent<AimPlayerBall>().enabled = true;
	}
	
	void activateSteering(){
		zoogiCharacterRoot.transform.FindChild("CharacterGUI").gameObject.SetActive(false);
		LaunchController.launchCompleted -= activateSteering;
		zoogiBall.GetComponent<AimPlayerBall>().enabled = false;
		zoogiBall.GetComponent<LaunchController>().enabled = false;
		zoogiBall.GetComponent<SteeringController>().enabled = true;
	}
	
	void deactivateControlScripts(){
		zoogiBall.GetComponent<AimPlayerBall>().enabled = false;
		zoogiBall.GetComponent<LaunchController>().enabled = false;
		zoogiBall.GetComponent<SteeringController>().enabled = false;
	}
	
	public bool addSkyBitToZoogi(){
		if(skyBitsCarried < maximumSkyBitsCarried){
			skyBitsCarried += 1;
			zoogiBall.GetComponent<Rigidbody>().mass += SKY_BIT_MASS_ADD;
			if(skyBitAcquired != null){
				skyBitAcquired(gameObject);
			}
			return true;
		}
		else{
			return false;
		}
	}

	public int getObjectID(){
		return this.gameObject.GetInstanceID();
	}

	public void resetBallPosition(){
		
		zoogiBall.transform.localRotation = Quaternion.identity;
		zoogiCharacterRoot.transform.localRotation = Quaternion.identity;
		
		zoogiBall.transform.localPosition = Vector3.zero;
		zoogiCharacterRoot.transform.localPosition = Vector3.zero;
		
		zoogiBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
		zoogiBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
	}

	public void stopBallMotion(){
		zoogiBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
		zoogiBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
	}
}
