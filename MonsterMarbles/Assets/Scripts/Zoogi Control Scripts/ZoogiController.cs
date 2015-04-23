using UnityEngine;
using System.Collections;

public class ZoogiController : MonoBehaviour {

	private GameObject zoogiBall;
	private GameObject zoogiCharacterRoot;
	private SkybitInventory skybitInventory;
	
	private int skyBitsCarried = 0;
	private int maximumSkyBitsCarried = 5;
	public static float SKY_BIT_MAX_POWER_DECREASE = 5;
	
	public enum State {CONTROLS_ACTIVE, ACTIVE, INACTIVE, ROLLING, HOPPING};
	private State currentState;
	
	public delegate void focusChange(int index);
	public static event focusChange ZoogiTurnStartEvent;
	public static event focusChange ZoogiTurnCompleteEvent;
	
	public delegate void itemAcquired(GameObject zoogi);
	public static event itemAcquired skyBitAcquired;

	// Use this for initialization
	void Start () {

	}
	
	void OnEnable(){

	}
	
	public void initialize(){
		zoogiBall = this.transform.FindChild("Ball").gameObject;
		zoogiCharacterRoot = this.transform.FindChild("Character Root").gameObject;
		skybitInventory = zoogiCharacterRoot.GetComponentInChildren<SkybitInventory>();
		currentState = State.INACTIVE;
		//setCurrentState(State.INACTIVE);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public State getCurrentState(){
		return currentState;
	}
	
	public bool setCurrentState(State newState){
		//TODO: Verify we got a valid State pattern
		
		if(currentState == State.CONTROLS_ACTIVE){
			zoogiCharacterRoot.transform.FindChild("CharacterGUI").gameObject.SetActive(false);
			zoogiBall.GetComponent<ZoogiLaunchBehavior>().enabled = false;
			zoogiBall.GetComponent<AimPlayerBall>().enabled = false;
		}
		else if (currentState == State.ACTIVE){
			
		}
		else if( currentState == State.ROLLING){
			zoogiBall.GetComponent<ZoogiRollBehavior>().enabled = false;
		}
		else if( currentState == State.HOPPING){
			zoogiBall.GetComponent<ZoogiHopBehavior>().enabled = false;
		}
		
		currentState = newState;
		
		if(newState == State.CONTROLS_ACTIVE){
			stopBallMotion();
			zoogiCharacterRoot.transform.FindChild("CharacterGUI").gameObject.SetActive(true);
			zoogiBall.GetComponent<ZoogiLaunchBehavior>().enabled = true;
			zoogiBall.GetComponent<ZoogiLaunchBehavior>().setCurrentState(ZoogiLaunchBehavior.State.IDLE);
			zoogiBall.GetComponent<AimPlayerBall>().enabled = true;
		}
		else if( newState == State.ACTIVE){
			
		}
		else if( newState == State.INACTIVE){
			zoogiBall.GetComponent<ZoogiRollBehavior>().enabled = false;
			zoogiBall.GetComponent<ZoogiLaunchBehavior>().enabled = false;
			zoogiBall.GetComponent<ZoogiHopBehavior>().enabled = false;
			zoogiBall.GetComponent<AimPlayerBall>().enabled = false;
		}
		else if( newState == State.ROLLING){
			zoogiBall.GetComponent<ZoogiRollBehavior>().enabled = true;
			zoogiBall.GetComponent<ZoogiRollBehavior>().setCurrentState(ZoogiRollBehavior.State.ROLLING);
		}
		else if( newState == State.HOPPING){
			zoogiBall.GetComponent<ZoogiHopBehavior>().enabled = true;
			zoogiBall.GetComponent<ZoogiHopBehavior>().setCurrentState(ZoogiHopBehavior.State.HOPPING);
		}
		return true;
	}
	
	public bool isUpright(){
		Quaternion standingUp = new Quaternion();
		standingUp.x=0f;
		standingUp.y=transform.rotation.y;
		standingUp.z=0f;
		return (Quaternion.Angle(transform.rotation, standingUp)>=0.003f);
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

	public void activateLaunching(){
		stopBallMotion();
		zoogiCharacterRoot.transform.FindChild("CharacterGUI").gameObject.SetActive(true);
		zoogiBall.GetComponent<SteeringController>().enabled = false;
		zoogiBall.GetComponent<LaunchController>().enabled = true;
		zoogiBall.GetComponent<AimPlayerBall>().enabled = true;
	}
	
	public void activateSteering(){
		zoogiCharacterRoot.transform.FindChild("CharacterGUI").gameObject.SetActive(false);
		LaunchController.launchCompleted -= activateSteering;
		zoogiBall.GetComponent<AimPlayerBall>().enabled = false;
		zoogiBall.GetComponent<LaunchController>().enabled = false;
		zoogiBall.GetComponent<SteeringController>().enabled = true;
	}
	
	public void deactivateControlScripts(){
		zoogiBall.GetComponent<AimPlayerBall>().enabled = false;
		zoogiBall.GetComponent<LaunchController>().enabled = false;
		zoogiBall.GetComponent<SteeringController>().enabled = false;
	}
	
	public bool addSkyBitToZoogi(){
		if(skyBitsCarried < maximumSkyBitsCarried){
			skyBitsCarried += 1;
			if(skybitInventory != null){
				skybitInventory.incrementInventoryState();
			}
			if(skyBitAcquired != null){
				skyBitAcquired(gameObject);
			}
			return true;
		}
		else{
			return false;
		}
	}
	
	//Removes all skybits currently carried by this zoogi, returns number removed
	public int removeAllSkybits(){
		skybitInventory.setToInventoryState(0);
		int skybitsRemoved = skyBitsCarried;
		skyBitsCarried = 0;
		return skybitsRemoved;
	}
	
	//Attempts to remove amount skybits from this zoogi, returns actual amount removed
	public int removeSkybits(int amount){
		int skybitsRemoved;
		if(amount <= skyBitsCarried){
			skyBitsCarried -= amount;
			skybitsRemoved = amount;
		}
		else{
			skybitsRemoved = skyBitsCarried;
			skyBitsCarried = 0;
		}
		
		skybitInventory.setToInventoryState(skyBitsCarried);
		return skybitsRemoved;
	}
	
	public float getMaxPowerDecrease(){
		return skyBitsCarried * SKY_BIT_MAX_POWER_DECREASE;
	}
	
	public int getNumberOfSkyBitsCarried(){
		return skyBitsCarried;
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
