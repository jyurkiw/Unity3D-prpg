using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("PRPG/Combat/Combat Runner")]
public class CombatRunner : MonoBehaviour {
	public bool combatGUIActive = false;	///< Is the combat GUI active? Setting true displays the GUI.
	public GUIStyle combatGUIStyle;			///< Current GUI style for the combat GUI.
	public GUIStyle combatLogGUIStyle;		///< Current GUI style for the combat log GUI.
	
	private CharacterManager playerParty;
	private CharacterManager enemyParty;
	
	//GUI objects
	private FrameList playerDisplay;
	private Frame enemyList;
	private FixedLineTextArea combatLog;
	
	/**
	 * Initialize the combat runner for use.
	 * Should be called before every combat.
	 * @param CharacterManager The player party data.
	 * @param CharacterManager The enemy party data.
	 */
	public void InitCombatRunner(CharacterManager playerParty, CharacterManager enemyParty) {
		this.playerParty = playerParty;
		this.enemyParty = enemyParty;
		playerDisplay = new FrameList(combatGUIStyle, 0, 0);
		playerDisplay.orientation = FrameOrientation.HORIZONTAL;
		playerDisplay.framePadding = 10.0f;
		
		//create the combat control panel.
		ButtonMenu combatControlPanel = new ButtonMenu(combatGUIStyle, 0, 0, "Action Menu");
		combatControlPanel.AddFrameItem("Attack", temp);
		combatControlPanel.AddFrameItem("Spell", temp);
		combatControlPanel.AddFrameItem("Defend", temp);
		combatControlPanel.AddFrameItem("Item", temp);
		
		playerDisplay.AddFrame(combatControlPanel);
		
		//create frames for the players and add them to the frame list
		foreach(ClassedCombatActor actor in playerParty.partyCharacters) {
			playerDisplay.AddFrame(new ActorFrame(combatGUIStyle, 0, 0, actor));
		}
		
		playerDisplay.Init();
		playerDisplay.Position = new Vector2((Screen.width - playerDisplay.Boundary.width) / 2.0f, (Screen.height - playerDisplay.Boundary.height - 30.0f));
		
		//create the enemy list
		enemyList = new Frame(combatGUIStyle, 10, 10);
		
		//add all enemy names to the enemy list
		foreach(ClassedCombatActor actor in enemyParty.partyCharacters) {
			enemyList.AddFrameItem(actor.Name);
		}
		
		//init the enemy list so we can size the combat log
		enemyList.Init();
		
		//initialize the combat log (10 px margin between enemy list and combat log)
		//combat log is 12 lines high
		float combatLogMargin = 10.0f;
		float combatLogLines = 12.0f;
		Rect combatLogDimensions = new Rect(0, 0, 0, 0);
		combatLogDimensions.x = enemyList.Boundary.x + enemyList.Boundary.width + combatLogMargin;
		combatLogDimensions.y = enemyList.Boundary.y;
		combatLogDimensions.width = Screen.width - combatLogDimensions.x;
		combatLog = new FixedLineTextArea(combatLogGUIStyle, combatLogDimensions);
		combatLog.DisplayLines = 12;
	}
	
	public void temp() {
		Debug.Log("A button was pressed");
	}
	
	public void DeinitCombatRunner() {
		playerParty = null;
		enemyParty = null;
	}
	
	public void OnGUI() {
		if (combatGUIActive) {
			playerDisplay.Draw();
			enemyList.Draw();
			combatLog.Draw();
		}
	}
	
	private void IntroduceEnemies() {
		foreach(ClassedCombatActor actor in enemyParty.partyCharacters) {
			combatLog.AddLine("A " + actor.Name + " appears!");
		}
	}
}
