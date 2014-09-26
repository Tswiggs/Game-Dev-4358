using UnityEngine;
using System.Collections;

public class MarbleCollisionHandler : MonoBehaviour {


	public AudioSource audioSource;

	public float onCollisionPowerIncrease;

	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.CompareTag(Constants.TAG_PLAYER) || collision.collider.CompareTag (Constants.TAG_MARBLE)) {
			if(audioSource != null){
				audioSource.Play();
			}
			rigidbody.AddForce(collision.contacts[0].normal * onCollisionPowerIncrease);

		}
		if (collision.collider.CompareTag(Constants.TAG_BUMPER)) {
			Vector3 forceDirection = new Vector3(0,0,0) + collision.contacts[0].normal;
			//forceDirection.Scale(new Vector3(-1,0,-1));

			rigidbody.AddForce(forceDirection * onCollisionPowerIncrease);
		}

		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
