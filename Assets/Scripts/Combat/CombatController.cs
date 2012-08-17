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
	
	private CombatRunner combatRunner;	///< Combat Runner used to run combat.
	private PRPGRandom combatSeeder;	///< RNG used to seed combat.
	
	public void Start() {
		combatRunner = GetComponent<CombatRunner>();
		combatSeeder = new PRPGRandom(PRPGRandom.GetInstance().NextRNGSeedingSet());
	}
	
	public CombatController() {
		combatControlActive = false;
	}
	
	public void ActivateCombatControl() {
		combatControlActive = true;
		enemyManager.LoadEnemies(characterManager.AverageLevel, combatSeeder.Next(1, enemyManager.maxCharactersInParty + 1), combatSeeder);
		combatRunner.InitCombatRunner(characterManager, enemyManager);
		combatCamera.enabled = combatControlActive;
		combatRunner.combatGUIActive = true;
	}
	
	private void DeactivateCombatControl() {
		combatRunner.combatGUIActive = false;
		combatRunner.DeinitCombatRunner();
		combatControlActive = false;
		combatCamera.enabled = combatControlActive;
	}
	
	public void Update() {
		if (combatControlActive) {
			if (Input.GetKeyUp(KeyCode.T)) {
				ActivateMapControl();
			}
			if (Input.GetKeyUp(KeyCode.F)) {
				combatRunner.TogglePlayerUIPanels();
				combatRunner.ToggleCombatLog();
			}
			if (Input.GetKeyUp(KeyCode.G)) {
				Debug.Log("Adding Test! to log");
				combatRunner.combatLog.Add("test!");
			}
		}
	}
	
	private void ActivateMapControl() {
		DeactivateCombatControl();
		rpgController.ActivateMapControl();
	}
}
