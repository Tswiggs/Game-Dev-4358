using UnityEngine;
using System.Collections;

public class BonusShotIndicatorController : MonoBehaviour {
	
	public GUITexture bonusShotGUITexture;
	
	private float originalScale;
	public float maxScale;
	
	public float animationTime = 2;
	private float animationTimer = 0;
	private bool beginAnimation = false;
	
	// Use this for initialization
	void Start () {
		originalScale = transform.localScale.x;
		OutOfBoundsHandler.pointCollected += skyBitCollected;
		bonusShotGUITexture.enabled = false;
	}
	
	void skyBitCollected(){
		beginAnimation = true;
		bonusShotGUITexture.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(beginAnimation){
			if(animationTimer >= animationTime){
				animationTimer = 0;
				beginAnimation = false;
				bonusShotGUITexture.enabled = false;
				bonusShotGUITexture.color = new Color(bonusShotGUITexture.color.r,bonusShotGUITexture.color.g,bonusShotGUITexture.color.b,1);
				transform.localScale = new Vector3(originalScale,originalScale,transform.localScale.z);
				
				
			}
			else{
				if(animationTimer >= animationTime-1){
					float newAlphaValue = Mathf.Lerp(bonusShotGUITexture.color.a,-1f,Time.deltaTime*1f);
					if(newAlphaValue < 0f){
						newAlphaValue = 0f;
					}
					bonusShotGUITexture.color = new Color(bonusShotGUITexture.color.r,bonusShotGUITexture.color.g,bonusShotGUITexture.color.b,newAlphaValue);
				}
				animationTimer += Time.deltaTime;
				float xScale  = Mathf.Lerp(transform.localScale.x,maxScale,Time.deltaTime);
				transform.localScale = new Vector3(xScale,xScale,transform.localScale.z);
			}
		}
	}
}
