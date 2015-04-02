using UnityEngine;
using System.Collections;

public class SkybitInventoryDisplayController : MonoBehaviour {
	
	int currentAmount = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool setToInventoryState(int amount){
		if(amount == 0){
			if(currentAmount != 0){
				transform.FindChild(currentAmount.ToString()).gameObject.SetActive(false);
			}
			return true;
		}
		else if(currentAmount != amount){
			Transform child = transform.FindChild(amount.ToString());
			if(child != null){
				if(currentAmount != 0){
					transform.FindChild(currentAmount.ToString ()).gameObject.SetActive(false);
				}
				child.gameObject.SetActive(true);
				currentAmount = amount;
				return true;
				
			}
			else{
				return false;
			}
		}
		else{
			return false;
		}
	}
	
	public bool incrementInventoryState(){
		if(currentAmount < transform.childCount){
			return setToInventoryState(currentAmount+1);
		}
		else{
			return false;
		}
	}
}
