using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Game controller. Activates the tilemap component
 * when the game starts and handles user input.
 * 
 * Movement input is defined in Controls/RPGMovement.cs
 */
[AddComponentMenu("PRPG/Controllers/RPGController")]
public partial class RPGController : MonoBehaviour {
	//public Tilemap tilemap;
	public bool movementControlActive;
	public CornerstoneManager cornerstoneManager;
	public Camera mapCamera;
	public CombatController combatController;
	public int minimumStepsBetweenCombats;
	public int maximumStepsBetweenCombats;
	public float randomCombatPerStepChance;
	
	private int movementSinceLastCombat;
	private int nextCombatStep;
	
	private PRPGRandom rand;
	
	void Start() {
		rand = new PRPGRandom(579841L, 547892L, 124957L, 348975L, 34897632125L);
		ActivateMapControl();
	}
	
	public void Update() {
//		if (Input.GetKeyUp(KeyCode.R) && movementControlActive)
//			ActivateCombatControl();
//		if (Input.GetKeyDown(KeyCode.Z))
//			cornerstoneManager.AdvanceOneLevel();
//		if (Input.GetKeyDown(KeyCode.X))
//			cornerstoneManager.ReturnOneLevel();
	}
	
	private IEnumerator InputHandler() {
		while(movementControlActive) {
			if (Input.GetKey(KeyCode.W))
				HandleW();
			if (Input.GetKey(KeyCode.A))
				HandleA();
			if (Input.GetKey(KeyCode.S))
				HandleS();
			if (Input.GetKey(KeyCode.D))
				HandleD();
			
			if (Input.GetKey(KeyCode.Escape))
				HandleEscape();
			
			yield return new WaitForEndOfFrame();
		}
		
		yield break;
	}
	
	private IEnumerator CombatHandler() {
		Debug.Log(rand);
		//nextCombatStep = rand.Next(minimumStepsBetweenCombats, maximumStepsBetweenCombats + 1);
		nextCombatStep = 2;
		
		do {
			yield return new WaitForEndOfFrame();
		} while(movementSinceLastCombat < nextCombatStep);
		
		ActivateCombatControl();
		yield break;
	}
	
	/**
	 * Activate all map controls.
	 */
	public void ActivateMapControl() {
		movementControlActive = true;
		mapCamera.enabled = true;
		
		//call all control start sub-methods.
		MovementStart();
		
		//start collecting input from the user
		StartCoroutine(InputHandler());
		
		//start checking for combat
		movementSinceLastCombat = 0;
		StartCoroutine(CombatHandler());
	}
	
	//Deactivate all map controls
	private void DeactivateMapControl() {
		movementControlActive = false;
		IsProtected = true;
	}
	
	private void ActivateCombatControl() {
		DeactivateMapControl();
		mapCamera.enabled = false;
		combatController.ActivateCombatControl();
	}
	
	/**
	 * Handle the escape key. Breaks out of the InputHandler.
	 */
	private void HandleEscape() {
		movementControlActive = false;
		Application.Quit();
	}
	
	//Movement input
	partial void HandleW();
	partial void HandleA();
	partial void HandleS();
	partial void HandleD();
}
