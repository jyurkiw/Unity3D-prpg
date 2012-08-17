using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Run combat between a player party, and an enemy party.
 */
[AddComponentMenu("PRPG/Combat/Combat Runner")]
public class CombatRunner : MonoBehaviour {
	public bool combatGUIActive = false;	///< Is the combat GUI active? Setting true displays the GUI.
	public CharacterManager playerParty;
	public CharacterManager enemyParty;
	
	public PlayerPanel[] playerUIPanels;	///< UI elements for player stats. Right now, we're working with four players.
	public UITextList combatLog;
	
	/**
	 * Initialize the combat runner for use.
	 * Should be called before every combat.
	 * @param CharacterManager Player party involved in combat.
	 * @param CharacterManager Enemy party involved in combat.
	 */
	public void InitCombatRunner(CharacterManager playerParty, CharacterManager enemyParty) {
		this.playerParty = playerParty;
		this.enemyParty = enemyParty;
		
		for(int x = 0; x < 4; x++) {
			playerUIPanels[x].Init(playerParty.partyCharacters[x]);
		}
		
		combatLog.Add("A wild derp appears!");
		combatLog.Add("This is a test!");
	}
	
	public void temp() {
		Debug.Log("A button was pressed");
	}
	
	/**
	 * Prep the combat runner for the next combat.
	 */
	public void DeinitCombatRunner() {
		playerParty = null;
		enemyParty = null;
	}
	
	/**
	 * Combat Coroutine
	 */
	public IEnumerator OnCombat() {
		Debug.Log("Combat has occurred.");
		yield break;
	}
	
	public void TogglePlayerUIPanels() {
		Debug.Log("Toggling Player UI Panels");
		foreach(PlayerPanel panel in playerUIPanels) {
			panel.gameObject.active = !panel.gameObject.active;
		}
	}
	
	public void ToggleCombatLog() {
		combatLog.gameObject.active = !combatLog.gameObject.active;
	}
}
