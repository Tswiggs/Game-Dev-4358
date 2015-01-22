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
		setButtonState();
		LaunchController.LaunchControllerEnabled += focusNewBallPower;
	}
	
	void OnEnable() {
		LaunchController.LaunchControllerEnabled += focusNewBallPower;
		setButtonState();
	}
	
	// Update is called once per frame
	void Update () {
		if(usable){
			if(Input.GetMouseButtonDown(0)){
				if(guiTexture.GetScreenRect().Contains(new Vector2(Input.mousePosition.x,Input.mousePosition.y))){
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
	
	void setButtonState(){
		if(target != null){
			if(target.powerCharged){
				usable = true;
				this.guiTexture.color = new Color(this.guiTexture.color.r,this.guiTexture.color.g,this.guiTexture.color.b,1f);
				if(target.readyToDeployPower){
					pressed = true;
					guiTexture.texture = pressedButton;
				}
				else{
					guiTexture.texture = unpressedButton;
					pressed = false;
				}
			}
			else{
				usable = false;
				guiTexture.texture = pressedButton;
				pressed = false;
				this.guiTexture.color = new Color(this.guiTexture.color.r,this.guiTexture.color.g,this.guiTexture.color.b,0.2f);
			}
		}
		else{
			usable = false;
			this.guiTexture.color = new Color(this.guiTexture.color.r,this.guiTexture.color.g,this.guiTexture.color.b,0.2f);
			guiTexture.texture = unpressedButton;
			pressed = false;
		}
		
	}

}
