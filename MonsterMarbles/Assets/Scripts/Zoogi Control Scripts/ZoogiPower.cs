using UnityEngine;
using System.Collections;

public class ZoogiPower : MonoBehaviour {

	public bool powerCharged;
	public bool readyToDeployPower;
	
	
	void Start () {

	}
	
	void OnEnable(){
		LaunchController.launchCompleted += attemptToUsePowerAtLaunch;
	}
	
	void OnDisable(){
		LaunchController.launchCompleted -= attemptToUsePowerAtLaunch;
		
		readyToDeployPower = false;
		
	}
	
	
	public bool setPowerDeployState(bool active)
	{
		if(powerCharged){
			if(active){
				readyToDeployPower = true;
				return true;
			}
			else {
				readyToDeployPower = false;
				return false;
			}
		}
		return readyToDeployPower;
		
	}
	
	public void attemptToUsePowerAtLaunch(){
		if(readyToDeployPower){
			deployPower();
		}
	}
	
	virtual public bool deployPower(){
		if(powerCharged){
			powerCharged = false;
			readyToDeployPower = false;
			return true;
		}
		
		return false;
	}
	
	public void chargePower(){
		powerCharged = true;
	}
	
}
