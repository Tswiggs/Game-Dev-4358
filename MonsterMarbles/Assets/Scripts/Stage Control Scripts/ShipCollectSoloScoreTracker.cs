using UnityEngine;
using System.Collections;

public class ShipCollectSoloScoreTracker : ObjectiveTrackerInterface {
	
	public enum CollectObject {CLOUD_BIT, SKY_BIT};
	
	private CollectObject objectToCollect;
	private bool finishOnPlayerCollect;
	private int amountToCollect;
	
	private int score = 0;
	private int turnsTaken = 0;
	
	private int star1Time = 0;
	private int star2Time = 2;
	private int star3Time = 1;
	
	private bool victoryAchieved;
	private bool gameFinished;
	
	public ShipCollectSoloScoreTracker(CollectObject ObjectToCollect, int AmountToCollect, bool FinishOnPlayerCollect, int Star1Time, int Star2Time, int Star3Time){
		objectToCollect = ObjectToCollect;
		amountToCollect = AmountToCollect;
		finishOnPlayerCollect = FinishOnPlayerCollect;
		
		setStarTimes(Star1Time,Star2Time,Star3Time);
		
		victoryAchieved = false;
		gameFinished = false;
		score = 0;
		
		TurnFlowController.TurnEndEvent += incrementTurnCounter;
		OutOfBoundsHandler.playerCollected += playerCollectedFinished;
		
		if(finishOnPlayerCollect){
			ShipCollectorCollisionHandler.CollectedPlayer += playerCollectedFinished;
			ShipCollectorCollisionHandler.CollectedPlayer += playerCollectedVictory;
		}
		
		if(objectToCollect == CollectObject.SKY_BIT){
			//ShipCollectorCollisionHandler.SkybitsCollected += objectsCollected;
			//ShipCollectorCollisionHandler.CollectedSkybit += objectCollected;
			ZoogiController.skyBitAcquired += objectCollected;
		}
	}
	
	public bool isGameOver() {
		return gameFinished;
	}
	
	public int getPlayerWon(){
		if(victoryAchieved){
			return 0;
		}
		else{
			return -1;
		}
	}
	
	private void objectsCollected(int amount){
		score += amount;
	}
	
	private void objectCollected(GameObject collected){
		score += 1;
	}
	
	private void playerCollectedFinished(GameObject collected){
		gameFinished = true;
	}
	
	private void playerCollectedVictory(GameObject collected){
		victoryAchieved = true;
	}
	
	
	public int getPlayerScore(int index){
		return score;
	}
	
	public string getPlayerScoreString(int index){
		return score.ToString()+" / "+amountToCollect.ToString();
	}
	
	public int getPlayerRanking(int index){
		if(index == 0){
			if(victoryAchieved){
				if(score >= amountToCollect){
					if(star3Time == 0 || (turnsTaken <= star3Time)){
						return 3;
					}
					return 2;
				}
				return 1;
			}
			return 0;
		}
		
		return -1;
	}
	
	private bool setStarTimes(int oneStar, int twoStar, int threeStar){
		if(oneStar <= 0 || oneStar >= twoStar && twoStar >= threeStar && threeStar >= 0){
			star1Time = oneStar;
			star2Time = twoStar;
			star3Time = threeStar;
			return true;
		}
		else{
			return false;
		}
	}
	
	public void incrementTurnCounter(GameObject zoogi){
		turnsTaken += 1;
	}
	
	void OnDestroy(){
		TurnFlowController.TurnEndEvent -= incrementTurnCounter;
		OutOfBoundsHandler.playerCollected -= playerCollectedFinished;
		
		if(finishOnPlayerCollect){
			ShipCollectorCollisionHandler.CollectedPlayer -= playerCollectedFinished;
			ShipCollectorCollisionHandler.CollectedPlayer -= playerCollectedVictory;
		}
		
		if(objectToCollect == CollectObject.SKY_BIT){
			//ShipCollectorCollisionHandler.SkybitsCollected -= objectsCollected;
			//ShipCollectorCollisionHandler.CollectedSkybit -= objectCollected;
			ZoogiController.skyBitAcquired -= objectCollected;
		}
	}
}
