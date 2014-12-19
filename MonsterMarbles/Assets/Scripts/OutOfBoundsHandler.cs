using UnityEngine;
using System.Collections;

public class OutOfBoundsHandler : MonoBehaviour {

	public AudioSource audioSource;
    public GameObject playerBall;
	public delegate void pointCollectAction();
	public static event pointCollectAction pointCollected;
	public delegate void playerCollectAction(GameObject collectedPlayer);
	public static event playerCollectAction playerCollected;

	// Use this for initialization
	void Start () {
	}

	void OnTriggerEnter(Collider collectedObject) {
		if(collectedObject.CompareTag(Constants.TAG_MARBLE)){
			audioSource.Play();
			Destroy(collectedObject.gameObject);
			if(pointCollected != null){
				pointCollected();
			}
		}
		if (collectedObject.CompareTag(Constants.TAG_PLAYER))
        {
			collectedObject.transform.parent.gameObject.SetActive(false);
			collectedObject.transform.position = new Vector3(transform.position.x,0, transform.position.z);
			if(playerCollected != null){
				playerCollected(collectedObject.gameObject);
			}
        }
	}

	// Update is called once per frame
	void Update () {

	}
}
