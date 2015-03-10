using UnityEngine;
using System.Collections;

public class UsePowerButtonController : MonoBehaviour {
	
	public Texture unpressedButton;
	public Texture pressedButton;
	
	private bool pressed = false;
	
	private bool usable = true;
	
	private ZoogiPower target;
	
	public delegate void buttonPressed(bool isPressed);
	public static event buttonPressed usePowerButtonPressed;
	
	// Use this for initialization
	void Start () {
		LaunchController.LaunchControllerEnabled += focusNewBallPower;
		setButtonState();
	}
	
	void OnEnable() {
		LaunchController.LaunchControllerEnabled += focusNewBallPower;
		ZoogiController.ZoogiTurnStartEvent += recheckButtonState;
		setButtonState();
	}
	
	// Update is called once per frame
	void Update () {
		if(usable){
			if(Input.GetMouseButtonDown(0)){
				if(GetComponent<GUITexture>().GetScreenRect().Contains(new Vector2(Input.mousePosition.x,Input.mousePosition.y))){
					pressed = target.setPowerDeployState(!pressed);
					setButtonState();
				}
			}
		}
	}
	
	void focusNewBallPower(GameObject ball){
		target = ball.transform.parent.gameObject.GetComponent<ZoogiPower>();
		setButtonState();
	}
	
	void recheckButtonState(int index){
		setButtonState();
	}
	
	void setButtonState(){
		if(target != null){
			if(target.powerCharged){
				usable = true;
				this.GetComponent<GUITexture>().color = new Color(this.GetComponent<GUITexture>().color.r,this.GetComponent<GUITexture>().color.g,this.GetComponent<GUITexture>().color.b,1f);
				if(target.readyToDeployPower){
					pressed = true;
					GetComponent<GUITexture>().texture = pressedButton;
				}
				else{
					GetComponent<GUITexture>().texture = unpressedButton;
					pressed = false;
				}
			}
			else{
				usable = false;
				GetComponent<GUITexture>().texture = pressedButton;
				pressed = false;
				this.GetComponent<GUITexture>().color = new Color(this.GetComponent<GUITexture>().color.r,this.GetComponent<GUITexture>().color.g,this.GetComponent<GUITexture>().color.b,0.2f);
			}
		}
		else{
			usable = false;
			this.GetComponent<GUITexture>().color = new Color(this.GetComponent<GUITexture>().color.r,this.GetComponent<GUITexture>().color.g,this.GetComponent<GUITexture>().color.b,0.2f);
			GetComponent<GUITexture>().texture = unpressedButton;
			pressed = false;
		}
		
	}

}
