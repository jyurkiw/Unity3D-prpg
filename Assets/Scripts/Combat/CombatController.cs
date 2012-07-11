using UnityEngine;
using System.Collections;

/**
 * Controls combat operations.
 * 
 * Is activated by the RPG controller when combat is initiated.
 * When combat concludes, reactivates the RPG controller.
 * 
 * Is responsible for switching on it's camera.
 * Initaiates all combat data changes.
 */
[AddComponentMenu("PRPG/Controllers/Combat Controller")]
public class CombatController : MonoBehaviour {
	public RPGController rpgController; ///< The RPGController attached to the player sprite.
	public Camera combatCamera;	///< The camera attached to the combat node.
	public bool combatControlActive;	///< Flag that tells us if the combat controls are active.
	public CharacterManager characterManager;	///< Character Manager that controls the player's party.
	public CharacterManager enemyManager;		///< Character Manager that controls the enemies.
	
	public CombatController() {
		combatControlActive = false;
	}
	
	public void ActivateCombatControl() {
		combatControlActive = true;
		combatCamera.enabled = combatControlActive;
	}
	
	private void DeactivateCombatControl() {
		combatControlActive = false;
		combatCamera.enabled = combatControlActive;
	}
	
	public void Update() {
		if (Input.GetKeyUp(KeyCode.T) && combatControlActive) {
			ActivateMapControl();
		}
	}
	
	private void ActivateMapControl() {
		DeactivateCombatControl();
		rpgController.ActivateMapControl();
	}
}
