using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using AssemblyCSharp;
public class RingerController : MonoBehaviour {
	
	/// <summary>specifies whether the game will be conducted online</summary>
	public enum MULTIPLAYER_MODE
	{
		HOTSEAT, ONLINE
	}
	
	public int remainingSkybits;
	
	public MULTIPLAYER_MODE gameMode; 
	/// <summary>
	/// The players.
	/// </summary>
	public IList players;
	/// <summary>
	/// The active player.
	/// </summary>
	public Player activePlayer;
	/// <summary>
	/// The game camera.
	/// </summary>
	public CameraBoomController cameraBoom; 
	/// <summary>
	/// The "net" that catches game objects when they fall off of the arena. 
	/// </summary>
	public GameObject objectCatcher; 
	public GameController gameController;
	public PlayerBallCreator ballSpawner;
	
	public RingerScoreTracker scoreTracker;
	
	/// <summary>
	/// The game object in unity that displays the score.
	/// </summary>
	public GUIText scoreText; 
	/// <summary>
	/// The score that must be achieved to win the game. 
	/// </summary>
	public int winningScore = 15; 
	public GameObject GUIObject;
	
	public delegate void focusChange(int index);
	public static event focusChange PlayerTurnStartEvent;
	public static event focusChange PlayerTurnCompleteEvent;
	
	public delegate void spawnItems(int amount);
	public static event spawnItems dropSkybits;
	
	public bool isShooting=false;
	private int activePlayerIndex=0;
	private int activeZoogiIndex=0;
	
	private int teamSize = 3;
	
	private bool activePlayerGetsExtraTurn = false;
	private bool advanceToNextPlayer = false;
	
	private bool roundWonFlag = false;

	void Awake() {
		OutOfBoundsHandler.playerCollected += playerKOed;
		ShipCollectorCollisionHandler.CollectedPlayer += playerCollectedByShip;
		
		RingerScoreTracker.playerHasWonRound += playerHasWonRound;
		RingerScoreTracker.playerHasWonGame += playerHasWonGame;
	}

	void Start () {
		//players = new ArrayList (); 
		gameMode = MULTIPLAYER_MODE.HOTSEAT; 

		scoreTracker = new RingerScoreTracker(2,3,0);

		//Drop skybits after scoreTracker is intiated since it subscribes to an event
		if(dropSkybits != null){
			dropSkybits(7);
		}

		scoreText.text = "X 0";
		
	}
	
	public void initialize(GameController gameController, string multiplayerMode, ArrayList players){
		this.gameController=gameController;
		this.cameraBoom=GameObject.Find("CameraBoom").GetComponent<CameraBoomController>();
		OutOfBoundsHandler.pointCollected+= skyBitCollected;
		//TODO: change so that both RingerController and GameController are using the enum MULTIPLAYER_MODE
		gameMode=MULTIPLAYER_MODE.HOTSEAT;
		
		this.GUIObject = GameObject.Find("GUI");
		
		this.players=players;
		if(players.Count==0){print("Player List not initialized"); return;}
		activePlayer=players[activePlayerIndex] as Player;
		foreach( Player player in players){
			foreach( PlayerBallCreator.MONSTER_PREFABS character in player.getTeamSelection()){
				PlayerBall ball=new PlayerBall(player, ballSpawner.createPlayerBall(character));
				player.addPlayerBall(ball);
			}
		}
		
		waitForTurn();
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.LoadLevel("Main Menu");
		}
		
		if(Input.GetKeyDown (KeyCode.A)){
			activePlayer.getActiveBall().getBallObject().SetActive(true);
			dropSkybits(1);
		}
	}
	
	public void addPlayer(Player player)
	{
		players.Add (player); 
	}
	public void focusCameraForTurnStart(GameObject objectToFocusOn)
	{
		cameraBoom.startOfTurn(objectToFocusOn.transform);
	}
	
	void shootingAction(){
		isShooting=true;
		LaunchController.launchCompleted-= shootingAction;
		//SteeringController.rollCompleted+= endOfTurnAction; 
	}
	
	//Skybit collected, sets flag to give player an extra turn (good job, player, have a biscuit)
	void skyBitCollected(){
		//activePlayerGetsExtraTurn = true;
	}
	
	void playerCollectedByShip(GameObject player){
		//First, see if the player has any skybits currently on them
		ZoogiController controller = player.GetComponent<ZoogiController>();
		int skybitsCarried = controller.removeAllSkybits();
		if (skybitsCarried > 0){
			scoreTracker.addPointsToCurrentPlayer(skybitsCarried);
		}
		else{
			//nothing we really need to do here guv'ner
		}
		player.transform.FindChild("Ball").gameObject.GetComponent<SteeringController>().forceEndTurn();
		
	}
	
	void endOfTurnAction(int index){
		//SteeringController.rollCompleted-= endOfTurnAction;
		//OutOfBoundsHandler.pointCollected-= skyBitCollected;
		ZoogiController.ZoogiTurnCompleteEvent -= endOfTurnAction;
		if (!isGameOver()) {
			
			if(activeZoogiIndex < teamSize-1 && !roundWonFlag){
				activeZoogiIndex += 1;
				activePlayer.nextBall();
			}
			/*if(activePlayerGetsExtraTurn && !roundWonFlag){
				
			}*/
			else {
				
				if(PlayerTurnCompleteEvent != null){
					PlayerTurnCompleteEvent(activePlayerIndex);
				}
				if(roundWonFlag){
					performChangeOfRoundEvents();
					roundWonFlag = false;
				}
				activePlayer.nextBall();
				advanceToNextPlayer = true;
				//advanceToNextPlayerTurn ();
			}
			activePlayerGetsExtraTurn = false;
			waitForTurn ();
		}
		else {
			Rect displayRect = new Rect(0,0,Screen.width, Screen.height); 
			GUI.Label(displayRect, "Player " + activePlayer.getUserID() + " Has won!");
		}
	}
	
	int checkForGameWon(){
		
		return -1;
	}
	
	void playerHasWonRound(int index){
		roundWonFlag = true;
		DisplayGUIText.displayFormattedText("Player {0}\n has won the round!",5);
	}
	
	void playerHasWonGame(int index){
		roundWonFlag = true;
		DisplayGUIText.displayFormattedText("Player {0}\n has won the game!",6);
	}
	
	void performChangeOfRoundEvents(){
		if(dropSkybits != null){
			dropSkybits(7-scoreTracker.skybitsInPlay());
		}
		DisplayGUIText.displayUnformattedText("Begin Round "+(scoreTracker.getCurrentRound()+1).ToString()+"!");
	}
	
	bool isGameOver()
	{
		if (activePlayer.getScore () >= winningScore) {
			return true;		
		} else {
			return false; 
		}
	}
	IEnumerator delaySeconds(int seconds){
		yield return new WaitForSeconds(seconds);
	}
	
	
	//TODO: Need to pull out nextPlayer logic into its own method.
	public void waitForTurn()
	{
		if (gameMode == MULTIPLAYER_MODE.ONLINE) {
			//show waiting screen
			//ping server until it is this players turn again
			
		} else if(gameMode == MULTIPLAYER_MODE.HOTSEAT) {
			
		}
		
		startOfTurn();
	}
	
	void startOfTurn(){
		//TODO: Show which players turn it is.
		//TODO: Have them tap a button to begin.
		if (advanceToNextPlayer) {
			advanceToNextPlayerTurn();
		}
		
		if(/*!activePlayer.getActiveBall().isOnBoard()*/!(activePlayer.getActiveBall().getBallObject().activeInHierarchy))
		{
			activePlayer.getActiveBall().getBallObject().transform.position=ballSpawner.spawnLocation.position;
			activePlayer.getActiveBall().getBallObject().transform.rotation=ballSpawner.spawnLocation.rotation;
			
			activePlayer.getActiveBall().getBallObject().GetComponent<ZoogiController>().refreshZoogiReferences();
			activePlayer.getActiveBall().getBallObject().GetComponent<ZoogiController>().resetBallPosition();
			
			activePlayer.getActiveBall().getBallObject().SetActive(true);
			
			//activePlayer.getActiveBall().getBallObject().GetComponentInChildren<AimPlayerBall>().enabled=false;
			//activePlayer.getActiveBall().getBallObject().GetComponentInChildren<LaunchController>().enabled=false;
			
			activePlayer.getActiveBall().initialize();
			
			
		}
		//LaunchController.launchCompleted+=shootingAction;
		ZoogiController.ZoogiTurnCompleteEvent += endOfTurnAction;
		activePlayer.getActiveBall().getBallObject().GetComponent<ZoogiController>().startTurn();
		GUIObject.transform.FindChild("Launch GUI").gameObject.SetActive(true);
		//activePlayer.getActiveBall().possess();
		focusCameraForTurnStart(activePlayer.getActiveBall().getBallObject().transform.FindChild("Character Root").gameObject);
		
	}
	
	void advanceToNextPlayerTurn(){
		if((activePlayerIndex+1) < players.Count)
		{
			activePlayerIndex++;
		}else{
			activePlayerIndex=0;
		}
		if(PlayerTurnStartEvent != null){
			PlayerTurnStartEvent(activePlayerIndex);
		}
		activeZoogiIndex = 0;
		activePlayer=players[activePlayerIndex] as Player;
		advanceToNextPlayer = false;
	}
	
	public void playerKOed(GameObject collectedPlayer){
		//TODO: Implement the detection of who this ball belonged to.
		// then add a power charge to the character who KOed it if it was
		// an enemy ball. Also maintain the playerball that was KOed to prepare 
		// it to be put back on the launch area.
		
		AimPlayerBall aimScript =collectedPlayer.GetComponent<AimPlayerBall>() as AimPlayerBall;
		if(aimScript!=null){
			if(aimScript.playerBall.Equals(this.activePlayer.getActiveBall())){
				activePlayerGetsExtraTurn = false;
				aimScript.gameObject.GetComponent<SteeringController>().forceEndTurn();
			}
			else if(aimScript.playerBall.getPlayer().getUserID() == this.activePlayer.getUserID()){
				//do nothing because you dont get superpowers for being a loser
				// and knocking your own guys out.
			}else{
				//if it reaches this logic branch then the KOed ball belonged to another player
				
				activePlayer.getActiveBall().getBallObject().GetComponent<ZoogiPower>().chargePower();
				DisplayGUIText.displayUnformattedText("Power Charged!");
				
			}
		}
	}
}