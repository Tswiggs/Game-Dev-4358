using UnityEngine;
using System.Collections;

public class SkybitCollectBehavior : MonoBehaviour {

	public AudioClip pickUpSound;
	
	public delegate void collected(Transform collector);
	public static event collected PlayerCollect;
	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter(Collider collider) {
		
		if (collider.CompareTag(Constants.TAG_PLAYER)) {
			if(collider.GetComponentInParent<ZoogiController>().addSkyBitToZoogi()){
				GameAudioController.playOneShotSound(pickUpSound);
				
				if(PlayerCollect != null){
					PlayerCollect(collider.transform);
				}
				
				Destroy(transform.parent.parent.gameObject);
			}
			else{
				//Nothing happens, cannot pick up bit
			}
			
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

