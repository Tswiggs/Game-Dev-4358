using UnityEngine;
using System.Collections;

public class OutOfBoundsHandler : MonoBehaviour {

	public delegate void pointCollectAction();
	public static event pointCollectAction pointCollected;
	public delegate void playerCollectAction(GameObject collectedPlayer);
	public static event playerCollectAction playerCollected;
	
	public delegate void objectOutOfBounds(GameObject collectedObject);
	public static event objectOutOfBounds playerOutOfBounds;
	public static event objectOutOfBounds skyBitOutOfBounds;
	
	public AudioClip playerFallSound;

	// Use this for initialization
	void Start () {
	}

	void OnTriggerEnter(Collider collectedObject) {
		if(collectedObject.CompareTag(Constants.TAG_MARBLE)){
			collectedObject.transform.parent.gameObject.SetActive(false);
			if(pointCollected != null){
				//pointCollected();
			}
			if(skyBitOutOfBounds != null){
				skyBitOutOfBounds(collectedObject.transform.parent.gameObject);
			}
		}
		if (collectedObject.CompareTag(Constants.TAG_PLAYER))
        {
			collectedObject.transform.parent.gameObject.SetActive(false);
			collectedObject.transform.position = new Vector3(transform.position.x,0, transform.position.z);
			if(playerFallSound != null){
				GameAudioController.playOneShotSound(playerFallSound);
			}
			if(playerCollected != null){
				playerCollected(collectedObject.gameObject);
			}
			if(playerOutOfBounds != null){
				playerOutOfBounds(collectedObject.transform.parent.gameObject);
			}
        }
	}

	// Update is called once per frame
	void Update () {

	}
}
