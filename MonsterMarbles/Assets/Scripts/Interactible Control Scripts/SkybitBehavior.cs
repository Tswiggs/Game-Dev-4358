using UnityEngine;
using System.Collections;

public class SkybitBehavior : MonoBehaviour {
	
	public enum State {FALLING, FLOATING, RISE};
	private State currentState;
	private Vector3 anchorPoint;
	private float floatHeight = 1.0f;
	private float floatVariance = 0.5f;
	private float objectRadius = 0f;
	private float floatCycleTime = 3f;
	private float floatTimer = 0;
	
	private float riseTime = 1.2f;
	private float riseTimer;
	
	// Use this for initialization
	void Awake () {
		anchorPoint = new Vector3();
		floatTimer = 0;
		currentState = State.FALLING;
	}
	
	// Update is called once per frame
	void Update () {
		if(getCurrentState() == State.RISE){
			
			if(riseTimer > riseTime){
				transform.position = new Vector3(transform.position.x,anchorPoint.y+objectRadius+floatHeight,transform.position.z);
				setCurrentState(State.FLOATING);
			}
			else{
				riseTimer += Time.deltaTime;
				transform.position = new Vector3(transform.position.x,anchorPoint.y+objectRadius+floatHeight*(riseTimer/riseTime),transform.position.z);
			}
		}
		else if(getCurrentState() == State.FLOATING){
			if(floatTimer > floatCycleTime){
				floatTimer = 0;
			}
			else{
				floatTimer += Time.deltaTime;
			}
			
			transform.position = new Vector3(transform.position.x,anchorPoint.y+objectRadius+floatHeight+floatVariance*Mathf.Abs(floatTimer/floatCycleTime-0.5f)*2f,transform.position.z);
		}
	}
	
	public State getCurrentState(){
		return currentState;
	}
	
	public bool setCurrentState(State newState){
		//TODO: Verify we got a valid State pattern
		
		if(currentState == State.RISE){
			GetComponent<Rigidbody>().useGravity = true;
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		}
		else if(currentState == State.FLOATING){
			GetComponent<Rigidbody>().useGravity = true;
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		}
		
		currentState = newState;
		
		if(newState == State.FALLING){
			
		}
		else if (newState == State.RISE){
			riseTimer = 0;
			objectRadius = GetComponent<SphereCollider>().radius;
			GetComponent<Rigidbody>().useGravity = false;
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
		}
		else if (newState == State.FLOATING){
			floatTimer = floatCycleTime/2;
			objectRadius = GetComponent<SphereCollider>().radius;
			GetComponent<Rigidbody>().useGravity = false;
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
		}
		
		return true;
	}
	
	void OnCollisionEnter(Collision collision){
		if(getCurrentState() == State.FALLING){
			anchorPoint = collision.contacts[0].point;
			
			setCurrentState(State.RISE);
		}
	}
}
