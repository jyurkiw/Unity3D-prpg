using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("PRPG/Combat/Combat Runner")]
public class CombatRunner : MonoBehaviour {
	public bool combatGUIActive = false;	///< Is the combat GUI active? Setting true displays the GUI.
	public GUIStyle combatGUIStyle;			///< Current GUI style for the combat GUI
	
	private CharacterManager playerParty;
	private CharacterManager enemyParty;
	
	//GUI objects
	private FrameList playerDisplay;
	
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
		
		foreach(ClassedCombatActor actor in playerParty.partyCharacters) {
			playerDisplay.AddFrame(new ActorFrame(combatGUIStyle, 0, 0, actor));
		}
		
		playerDisplay.Init();
		playerDisplay.Position = new Vector2((Screen.width - playerDisplay.Boundary.width) / 2.0f, (Screen.height - playerDisplay.Boundary.height - 30.0f));
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
		}
	}
	
	
}
