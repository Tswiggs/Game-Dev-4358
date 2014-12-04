using UnityEngine;
using System.Collections;

public class MarbleCollisionHandler : MonoBehaviour {


	public AudioSource audioSource;

	public float onCollisionPowerIncrease;
	public float onCollisionPowerMultiplier;
	public float onCollisionBumperPower;
	
	public bool useDefaults = true;
	
	public static float DEFAULT_COLLISION_POWER_INCREASE = 10f;
	public static float DEFAULT_COLLISION_POWER_MULTIPLIER = 10f;

	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.CompareTag(Constants.TAG_PLAYER) || collision.collider.CompareTag (Constants.TAG_MARBLE)) {
			if(audioSource != null){
				audioSource.Play();
			}
			
			Vector3 forceVector = collision.relativeVelocity *rigidbody.mass;
			forceVector.Scale(new Vector3(getPowerMultiplier(),getPowerMultiplier(),getPowerMultiplier()));
			rigidbody.AddForce(forceVector);

		}
		if (collision.collider.CompareTag(Constants.TAG_BUMPER)) {
			Vector3 forceVector = collision.contacts[0].normal;
			//forceVector = forceVector - Vector3.Scale(forceVector, new Vector3(2,2,2));
			//forceVector = forceVector.normalized * getPowerIncrease();
			//forceDirection.Scale(new Vector3(-1,0,-1));
			rigidbody.velocity = forceVector * (rigidbody.velocity.magnitude+getPowerIncrease());
			//rigidbody.AddForce(forceVector);
		}

		
	}
	
	private float getPowerIncrease(){
		if(useDefaults){
			return DEFAULT_COLLISION_POWER_INCREASE;
		}
		else{
			return onCollisionPowerIncrease;
		}
	}
	
	private float getPowerMultiplier(){
		if(useDefaults){
			return DEFAULT_COLLISION_POWER_MULTIPLIER;
		}
		else{
			return onCollisionPowerMultiplier;
		}
	}
	
	private float getBumperPowerMultiplier(){
		if(useDefaults){
			return Constants.DEFAULT_BUMPER_POWER;
		}
		else {
			return onCollisionBumperPower;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
