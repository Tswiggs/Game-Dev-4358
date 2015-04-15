using UnityEngine;
using System.Collections;

public class MarbleCollisionHandler : MonoBehaviour {


	public AudioSource audioSource;
	
	public AudioClip collisionSound;

	public float onCollisionPowerIncrease;
	public float onCollisionPowerMultiplier;
	public float onCollisionBumperPower;
	
	public Rigidbody rigidbody;
	
	public bool useDefaults = true;
	
	protected bool hasCollided = false;
	
	public static float DEFAULT_COLLISION_POWER_INCREASE = 20f;
	public static float DEFAULT_COLLISION_POWER_MULTIPLIER = 1.0f;
	
	public delegate void collisionEvent(Collision collision, Rigidbody original);
	public static event collisionEvent playerHasCollided;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.CompareTag(Constants.TAG_PLAYER) || collision.collider.CompareTag (Constants.TAG_MARBLE)) {
			if(rigidbody.velocity.magnitude > collision.rigidbody.velocity.magnitude){
				Vector3 forceVector = collision.relativeVelocity;
				forceVector.Scale(new Vector3(getPowerMultiplier(),0,getPowerMultiplier()));
				rigidbody.AddForce(forceVector,ForceMode.Impulse);
			}
			
			if(this.CompareTag(Constants.TAG_PLAYER)){
				if(playerHasCollided != null){
					playerHasCollided(collision, GetComponent<Rigidbody>());
				}
			}
			

		}
		else if (collision.collider.CompareTag(Constants.TAG_BUMPER)) {
			Vector3 forceVector = new Vector3(collision.contacts[0].normal.x,0,collision.contacts[0].normal.z);
			
			GetComponent<Rigidbody>().velocity = Vector3.Reflect(GetComponent<Rigidbody>().velocity,forceVector);
			
			GetComponent<Rigidbody>().velocity += forceVector * (GetComponent<Rigidbody>().velocity.magnitude+getBumperPower());
			
			if(this.CompareTag(Constants.TAG_PLAYER)){
				if(playerHasCollided != null){
					playerHasCollided(collision, GetComponent<Rigidbody>());
				}
			}
			
		}
		
		if((collision.collider.CompareTag(Constants.TAG_PLAYER) || collision.collider.CompareTag(Constants.TAG_PLAYER)) && audioSource != null && collisionSound != null){
			audioSource.PlayOneShot(collisionSound);
		}

		
	}
	
	protected float getPowerIncrease(){
		if(useDefaults){
			return Constants.DEFAULT_MARBLE_COLLISION_POWER_INCREASE;
		}
		else{
			return onCollisionPowerIncrease;
		}
	}
	
	protected float getPowerMultiplier(){
		if(useDefaults){
			return Constants.DEFAULT_MARBLE_COLLISION_POWER_MULTIPLIER;
		}
		else{
			return onCollisionPowerMultiplier;
		}
	}
	
	protected float getBumperPower(){
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
