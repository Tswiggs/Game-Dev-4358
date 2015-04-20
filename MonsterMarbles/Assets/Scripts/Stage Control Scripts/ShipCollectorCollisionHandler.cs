using UnityEngine;
using System.Collections;

public class ShipCollectorCollisionHandler : MonoBehaviour {

	public AudioClip skybitsCollectedSound;
	
	public delegate void collected(GameObject collected);
	public delegate void amountCollected(int amount);
	public static event collected CollectedPlayer;
	public static event collected CollectedSkybit;
	public static event amountCollected SkybitsCollected;
	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter(Collider collision) {
		
		//If a player has hit the collision dock
		if (collision.gameObject.CompareTag(Constants.TAG_PLAYER)) {
			
			ZoogiController controller = collision.transform.parent.gameObject.GetComponent<ZoogiController>();
			int numberOfSkybits = controller.removeAllSkybits();
			
			collision.transform.parent.gameObject.SetActive(false);
			
			if(numberOfSkybits > 0){
				if(SkybitsCollected != null){
					SkybitsCollected(numberOfSkybits);
				}
				GetComponent<ShipAnimationBehavior>().playSkybitDisplay(numberOfSkybits);
			}
			
			if(CollectedPlayer != null){
				CollectedPlayer(collision.transform.parent.gameObject);
			}
			
		}
		else if (collision.gameObject.CompareTag(Constants.TAG_MARBLE)) {
			collision.gameObject.SetActive(false);
			if(SkybitsCollected != null){
				SkybitsCollected(1);
			}
			GetComponent<ShipAnimationBehavior>().playSkybitDisplay(1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
