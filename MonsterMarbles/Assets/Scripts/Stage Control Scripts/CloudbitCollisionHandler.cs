using UnityEngine;
using System.Collections;

public class CloudbitCollisionHandler : MonoBehaviour {

	public AudioClip pickUpSound;

	public delegate void collected(Transform collector);
	public static event collected PlayerCollect;
	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter(Collider collider) {

		if (collider.CompareTag(Constants.TAG_PLAYER)) {
			//Somehow play pickUpSound
			Debug.Log ("Hit!");
			if(PlayerCollect != null){
				PlayerCollect(collider.transform);
			}

			Destroy(gameObject);

		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
