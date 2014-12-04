using UnityEngine;
using System.Collections;

public class SkyBitCollisionHandler : MarbleCollisionHandler {
	
	
	//public AudioSource audioSource;
	
	//public float onCollisionPowerIncrease;
	//public float onCollisionPowerMultiplier;
	
	//public bool useDefaults = true;
	
	public Color currentColor;
	private int collisionCount = 0;
	private float collisionTimer = 0f;
	
	//public static float DEFAULT_COLLISION_POWER_INCREASE = 10f;
	//public static float DEFAULT_COLLISION_POWER_MULTIPLIER = 10f;
	
	public static float COLLISION_DECAY_TIME = 3f;
	
	// Use this for initialization
	void Start () {
		updateCollisionColor();
	}
	
	void updateCollisionColor(){
		if(collisionCount == 0){
			currentColor = Color.white;
		}
		else if(collisionCount == 1){
			currentColor = Color.cyan;
		}
		else if(collisionCount == 2){
			currentColor = Color.blue;
		}
		else if(collisionCount >= 3){
			currentColor = Color.magenta;
		}
		
		GetComponent<ParticleSystem>().startColor = currentColor;
		this.transform.FindChild("Particle Streak").GetComponent<ParticleSystem>().startColor = currentColor;
	}
	
	void OnCollisionEnter(Collision collision) {
		if (hasCollided){
		
		}
		else if (collision.collider.CompareTag(Constants.TAG_PLAYER) || collision.collider.CompareTag (Constants.TAG_MARBLE)) {
			if(audioSource != null){
				audioSource.Play();
			}
			
			Vector3 forceVector = collision.relativeVelocity *rigidbody.mass;
			forceVector.Scale(new Vector3(getPowerMultiplier(),0,getPowerMultiplier()));
			rigidbody.AddForce(forceVector);
			
			hasCollided = true;
			
			incrementCollisionCount();
			
		}
		else if (collision.collider.CompareTag(Constants.TAG_BUMPER)) {
			if(audioSource != null){
				audioSource.Play();
			}
			
			Vector3 forceVector = collision.contacts[0].normal;

			rigidbody.velocity.Scale(new Vector3(-1,1,-1));
			
			rigidbody.velocity += forceVector * (rigidbody.velocity.magnitude+getBumperPower());
			
			hasCollided = true;
			
			incrementCollisionCount();
		}
		
		
	}
	
	
	void incrementCollisionCount(){
		collisionCount += 1;
		collisionTimer = 0f;
		updateCollisionColor();
	}
	
	// Update is called once per frame
	void Update () {
	
		hasCollided = false;
		
		if(collisionCount > 0){
			collisionTimer += Time.deltaTime;
			if(collisionTimer >= COLLISION_DECAY_TIME){
				collisionCount = 0;
				updateCollisionColor();
				collisionTimer = 0f;
			}
		}
	}
}