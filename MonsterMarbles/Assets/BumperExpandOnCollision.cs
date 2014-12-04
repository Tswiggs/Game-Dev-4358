using UnityEngine;
using System.Collections;

public class BumperExpandOnCollision : MonoBehaviour {
	
	public GameObject model;
	
	public float sproingScale;
	
	public float bumperSpeed;
	
	public AudioSource bumpSound;
	
	private float originalScale;
	
	private enum SproingState{
		NONE,UPSWING,DOWNSWING 
	};
	private SproingState sproingState;
	// Use this for initialization
	void Start () {
		sproingState = SproingState.NONE;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(sproingState == SproingState.NONE){
			
		}
		else if(sproingState == SproingState.UPSWING){
			if (model.transform.localScale.x < (sproingScale*0.9f)){
				float newScale = Mathf.Lerp(model.transform.localScale.x,sproingScale,Time.deltaTime*bumperSpeed);
				
				Debug.Log(newScale);
				
				model.transform.localScale = new Vector3(newScale,model.transform.localScale.y,newScale);
			}
			else{
				sproingState = SproingState.DOWNSWING;
			}
		}
		else if(sproingState == SproingState.DOWNSWING){
			if (model.transform.localScale.x > 1){
				float newScale = Mathf.Lerp(model.transform.localScale.x,0.9f,Time.deltaTime*bumperSpeed);
				
				model.transform.localScale = new Vector3(newScale,model.transform.localScale.y,newScale);
			}
			else{
				sproingState = SproingState.NONE;
				model.transform.localScale = new Vector3(1,model.transform.localScale.y,1);
				
			}
		}
	}
	
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.CompareTag(Constants.TAG_PLAYER) || collision.gameObject.CompareTag(Constants.TAG_MARBLE)){
			if(sproingState == SproingState.NONE){
				sproingState = SproingState.UPSWING;
				if(bumpSound != null){
					bumpSound.Play();
				}
			}
		}
	}
}
