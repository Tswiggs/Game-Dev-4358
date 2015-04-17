using UnityEngine;
using System.Collections;

public class ZoogiDragController : MonoBehaviour {
	
	private float noDragVelocity = 30f;
	private float maxDragVelocity = 3f;
	private float minimumDragPercentage = 0.1f;
	
	private float normalDrag;
	private Rigidbody rigidBody;
	
	private float stopVelocityThreshold = 0.5f;
	private float stopVelocityTime = 1.75f;
	private float stopTimer = 0;
	
	private bool wasOnGround;
	private bool onGround;
	
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		normalDrag = rigidBody.drag;
		
		wasOnGround = false;
		onGround = false;
	}
	
	void FixedUpdate() {
		setDragBasedOnVelocity();
		if(onGround){
			Debug.DrawRay(this.transform.position,Vector3.up*3);
			if(wasOnGround){
				wasOnGround = false;
			}
			else{
				onGround = false;
			}
		}
	}
	
	void OnCollisionStay(Collision collision){
		if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Terrain")){
			onGround = true;
			wasOnGround = true;
		}
	}
	
	private void setDragBasedOnVelocity(){
		if(!onGround){
			rigidBody.drag = normalDrag*minimumDragPercentage;
		}
		else{
			if(rigidBody.velocity.magnitude >= noDragVelocity){
				rigidBody.drag = normalDrag*minimumDragPercentage;
			}
			else if(rigidBody.velocity.magnitude <= maxDragVelocity){
				rigidBody.drag = normalDrag;
			}
			else{
				float percentage = Mathf.Pow(1f-(rigidBody.velocity.magnitude-maxDragVelocity),3f);
				if(percentage < minimumDragPercentage){
					percentage = minimumDragPercentage;
				}
				rigidBody.drag = normalDrag*percentage;
			}
		}
	}
}
