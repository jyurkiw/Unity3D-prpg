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
	public bool play;
	public CornerstoneManager cornerstoneManager;
	
	void Start() {
		play = true;
		
		//call all control start sub-methods.
		MovementStart();
		
		//Start up the tilemap.
		//tilemap.StartTilemapOperations();
		
		//start collecting input from the user
		StartCoroutine(InputHandler());
	}
	
//	public void Update() {
//		if (Input.GetKeyDown(KeyCode.Z))
//			cornerstoneManager.AdvanceOneLevel();
//		if (Input.GetKeyDown(KeyCode.X))
//			cornerstoneManager.ReturnOneLevel();
//	}
	
	private IEnumerator InputHandler() {
		while(play) {
			if (Input.GetKey(KeyCode.W))
				HandleW();
			if (Input.GetKey(KeyCode.A))
				HandleA();
			if (Input.GetKey(KeyCode.S))
				HandleS();
			if (Input.GetKey(KeyCode.D))
				HandleD();
			
			yield return new WaitForEndOfFrame();
		}
		//tilemap.StopTilemapOperations();
		Application.Quit();
		yield break;
	}
	
	/**
	 * Handle the escape key. Breaks out of the InputHandler.
	 */
	private void HandleEscape() {
		Debug.Log("Escape Pressed");
		play = false;
	}
	
	//Movement input
	partial void HandleW();
	partial void HandleA();
	partial void HandleS();
	partial void HandleD();
}
