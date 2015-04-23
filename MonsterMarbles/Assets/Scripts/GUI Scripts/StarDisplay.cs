using UnityEngine;
using System.Collections;

public class StarDisplay : MonoBehaviour {
	
	public Animator star1;
	public Animator star2;
	public Animator star3;
	
	public AudioClip starAcquiredSound;
	
	private float timeBetweenStars = 0.35f;
	private float starTimer = 0f;
	
	public int starsAcquired = 0;
	public int currentStar = 0;
	
	public bool playAnimation = false;
	// Use this for initialization
	void Start () {
		star1 = transform.FindChild("Star Holder").GetChild(0).GetChild(0).GetComponent<Animator>();
		star2 = transform.FindChild("Star Holder").GetChild(1).GetChild(0).GetComponent<Animator>();
		star3 = transform.FindChild("Star Holder").GetChild(2).GetChild(0).GetComponent<Animator>();
		
		GameFlowController.RankingAchieved += setStarsAcquired;
		GameGUIController.changeToEndGameSoloState += menuOpened;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(playAnimation){
			if(starTimer >= timeBetweenStars){
				currentStar += 1;
				if(currentStar < starsAcquired){
					starTimer = 0;
					if(currentStar == 0){
						star1.SetTrigger("playExpand");
					}
					else if(currentStar == 1){
						star2.SetTrigger("playExpand");
					}
					else if(currentStar == 2){
						star3.SetTrigger("playExpand");
					}
				}
				else{
					if(starsAcquired >= 3){
						if(starTimer >= timeBetweenStars){
							star1.SetTrigger("playSpecialSpin");
							star2.SetTrigger("playSpecialSpin");
							star3.SetTrigger("playSpecialSpin");
							playAnimation = false;
							currentStar = 0;
							starTimer = 0;
						}
						else{
							starTimer += Time.deltaTime;
						}
					}
					else{
						playAnimation = false;
						currentStar = 0;
						starTimer = 0;
					}
				}
			}
			else{
				starTimer += Time.deltaTime;
			}
		}
	}
	
	void sUpdate () {
		
		if(playAnimation){
			if(currentStar < 1){
				if(currentStar == 0 && starsAcquired > 0){
					currentStar = 1;
					star1.SetTrigger("playExpand");
					if(starAcquiredSound != null){
						GameAudioController.playOneShotSound(starAcquiredSound);
					}
				}
				else{
					playAnimation = false;
				}
			}
			
			if(currentStar <= starsAcquired){
				Animator selectedAnimator;
				if(currentStar == 1){
					selectedAnimator = star1;
				}
				else if(currentStar == 2){
					selectedAnimator = star2;
				}
				else{
					selectedAnimator = star3;
				}
				
				if(starTimer >= timeBetweenStars){
					starTimer = 0;
					if(currentStar == 1  && (starsAcquired >= 2)){
						star2.SetTrigger("playExpand");
						if(starAcquiredSound != null){
							GameAudioController.playOneShotSound(starAcquiredSound);
						}
						currentStar = 2;
					}
					else if(currentStar == 2 && (starsAcquired >= 3)){
						star3.SetTrigger("playExpand");
						if(starAcquiredSound != null){
							GameAudioController.playOneShotSound(starAcquiredSound);
						}
						currentStar = 3;
					}
					else{
						if(starsAcquired >= 3){
							star1.SetTrigger("playSpecialSpin");
							star2.SetTrigger("playSpecialSpin");
							star3.SetTrigger("playSpecialSpin");
						}
						playAnimation = false;
					}
				}
				else{
					starTimer += Time.deltaTime;
				}
			}
		}
	}
	
	public void menuOpened(){
		playStarAnimation(starsAcquired);
	}
	
	public void playStarAnimation(int StarsAcquired){
		currentStar = 0;
		starTimer = 0;
		if(StarsAcquired > 2){
			starsAcquired = 3;
		}
		else{
			starsAcquired = StarsAcquired;
		}
		
		if(starsAcquired > 0){
			star1.SetTrigger("playExpand");
		}
		playAnimation = true;
	}
	
	public void setStarsAcquired(int amount){
		if(amount >= 3){
			starsAcquired = 3;
		}
		else if (amount <= 0){
			starsAcquired = 0;
		}
		else{
			starsAcquired = amount;
		}
	}
	
	void OnDestroy(){
		GameFlowController.RankingAchieved -= setStarsAcquired;
		GameGUIController.changeToEndGameSoloState -= menuOpened;
	}
}
