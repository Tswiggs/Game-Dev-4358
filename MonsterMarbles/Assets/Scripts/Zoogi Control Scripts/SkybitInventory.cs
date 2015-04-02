using UnityEngine;
using System.Collections;

public class SkybitInventory : MonoBehaviour {

	private int currentAmount = 0;
	public static int SKYBIT_MAX = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public int getSkybitAmount(){
		return currentAmount;
	}
	
	public bool setToInventoryState(int amount){
		if(amount == 0){
			if(currentAmount != 0){
				transform.FindChild(currentAmount.ToString()).gameObject.SetActive(false);
			}
			currentAmount = 0;
			return true;
		}
		else if (amount > SKYBIT_MAX){
			return false;
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

