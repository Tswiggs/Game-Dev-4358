using UnityEngine;
using System.Collections;

public class ShipCollectorCollisionHandler : MonoBehaviour {

	public AudioClip playerCollectedSound;
	
	public delegate void collected(Transform collector);
	public static event collected CollectedPlayer;
	public static event collected CollectedSkybit;
	// Use this for initialization
	void Start () {
		
	}
	
	void OnCollisionEnter(Collision collision) {
		
		//If a player has hit the collision dock
		if (collision.gameObject.CompareTag(Constants.TAG_PLAYER)) {
			GameAudioController.playOneShotSound(playerCollectedSound);
			
			collision.transform.parent.gameObject.SetActive(false);
			collision.transform.parent.position = new Vector3(transform.position.x,0, transform.position.z);
			
			if(CollectedPlayer != null){
				CollectedPlayer(collision.transform);
			}
			
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
