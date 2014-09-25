using UnityEngine;
using System.Collections;

public class OutOfBoundsHandler : MonoBehaviour {

	public AudioSource audioSource;
	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Marble")){
			audioSource.Play();
			Destroy(other.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
