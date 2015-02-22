using UnityEngine;
using System.Collections;

public class MarbleCollisionHandler : MonoBehaviour {


	public AudioSource audioSource;
	
	public AudioClip collisionSound;

	public float onCollisionPowerIncrease;
	public float onCollisionPowerMultiplier;
	public float onCollisionBumperPower;
	
	public bool useDefaults = true;
	
	protected bool hasCollided = false;
	
	public static float DEFAULT_COLLISION_POWER_INCREASE = 10f;
	public static float DEFAULT_COLLISION_POWER_MULTIPLIER = 10f;
	
	public delegate void collisionEvent(Collision collision, Rigidbody original);
	public static event collisionEvent playerHasCollided;

	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.CompareTag(Constants.TAG_PLAYER) || collision.collider.CompareTag (Constants.TAG_MARBLE)) {
			
			Vector3 forceVector = collision.relativeVelocity *rigidbody.mass;
			forceVector.Scale(new Vector3(getPowerMultiplier(),0,getPowerMultiplier()));
			rigidbody.AddForce(forceVector);
			
			if(this.CompareTag(Constants.TAG_PLAYER)){
				if(playerHasCollided != null){
					playerHasCollided(collision, rigidbody);
				}
			}
			

		}
		else if (collision.collider.CompareTag(Constants.TAG_BUMPER)) {
			Vector3 forceVector = new Vector3(collision.contacts[0].normal.x,0,collision.contacts[0].normal.z);
			
			rigidbody.velocity = Vector3.Reflect(rigidbody.velocity,forceVector);
			
			rigidbody.velocity += forceVector * (rigidbody.velocity.magnitude+getBumperPower());
			
			if(this.CompareTag(Constants.TAG_PLAYER)){
				if(playerHasCollided != null){
					playerHasCollided(collision, rigidbody);
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
