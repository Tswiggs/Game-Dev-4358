using UnityEngine;
using System.Collections;

public class OutOfBoundsHandler : MonoBehaviour {

	public AudioSource audioSource;
	public GUIText counter;
    public GameObject playerBall;
	private int count = 0;
	// Use this for initialization
	void Start () {
	}

	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Marble")){
			audioSource.Play();
			Destroy(other.gameObject);
			count += 1;
			if(count == 9){
				counter.text = "Victory!";
				counter.color = new Color(1.0f,0.0f,0.0f);
			}
			else {
				counter.text = "Sky Bits Acquired: "+count;
			}
		}
        /*if (other.CompareTag("Player"))
        {
            playerBall.position = new Vector3(4.373713, 43.2134, -4828992);
        }*/
	}

	// Update is called once per frame
	void Update () {

	}
}
