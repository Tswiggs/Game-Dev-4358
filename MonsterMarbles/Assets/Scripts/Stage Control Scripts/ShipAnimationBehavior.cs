using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipAnimationBehavior : MonoBehaviour {
	
	
	public enum State {INACTIVE, SKYBIT_DISPLAY};
	private State currentState;
	
	public delegate void stateAction(State state);
	public static event stateAction animationComplete;
	
	public GameObject fakeSkyBit;
	public AudioClip skybitDisplaySound;
	
	private int skybitsCollected = 0;
	private List<GameObject> fakeSkyBits;
	private GameObject currentFakeBit;
	private float skybitImpulse = 60f;
	private float timeBetweenSkybits = 0.8f;
	private float skybitLiveTime = 0.4f;
	private float skybitDisplayTimer = 0;
	// Use this for initialization
	void Start () {
		fakeSkyBits = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if(getCurrentState() == State.SKYBIT_DISPLAY){
			if(skybitDisplayTimer == 0){
				if(fakeSkyBits.Count > 0){
					currentFakeBit = fakeSkyBits[0];
					currentFakeBit.transform.position = new Vector3(this.transform.position.x,this.transform.position.y+1,this.transform.position.z);
					currentFakeBit.SetActive(true);
					currentFakeBit.GetComponent<Rigidbody>().AddForce(new Vector3(0,skybitImpulse,0),ForceMode.Impulse);
					GameAudioController.playOneShotSound(skybitDisplaySound);
					fakeSkyBits.RemoveAt(0);
				}
				else{
					setCurrentState(State.INACTIVE);
				}
			}
			
			if(currentFakeBit != null && skybitDisplayTimer >= skybitLiveTime){
				DestroyObject(currentFakeBit);
			}
			
			skybitDisplayTimer += Time.deltaTime;
			
			if(skybitDisplayTimer >= timeBetweenSkybits){
				skybitDisplayTimer = 0;
			}
		}
	}
	
	public State getCurrentState(){
		return currentState;
	}
	
	public bool setCurrentState(State newState){
		//TODO: Verify we got a valid State pattern
		
		if(currentState == State.SKYBIT_DISPLAY){
			if(animationComplete != null){
				animationComplete(State.SKYBIT_DISPLAY);
			}
		}
		
		currentState = newState;
		
		if(newState == State.SKYBIT_DISPLAY){
			skybitDisplayTimer = 0;
			//GameCameraController.setFocusTarget(transform);
			for( int x = 0; x < skybitsCollected; x++){
				GameObject bit = Instantiate(fakeSkyBit);
				bit.SetActive(false);
				fakeSkyBits.Add(bit);
			}
		}
		
		return true;
	}
	
	public bool playSkybitDisplay(int numberOfSkyBits){
		skybitsCollected = numberOfSkyBits;
		setCurrentState(State.SKYBIT_DISPLAY);
		
		return true;
	}
}
