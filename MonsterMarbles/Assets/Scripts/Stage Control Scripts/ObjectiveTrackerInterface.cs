using UnityEngine;
using System.Collections;

public interface ObjectiveTrackerInterface{
	
	bool isGameOver();
	
	int getPlayerWon();
	
	int getPlayerScore(int index);
	
	int getPlayerRanking(int index);
	
	string getPlayerScoreString(int index);
	
}
