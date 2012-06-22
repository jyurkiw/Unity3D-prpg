using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manages a tilemap through a component system.
 */
[AddComponentMenu("PRPG/Cornerstone/Tilemap")]
public class Tilemap : MonoBehaviour {
	/**
	 * The tilemap workspace.
	 */
	public GameObject[,] map;
	
	/**
	 * The height of the map.
	 */
	public int mapHeight;
	
	/**
	 * The width of the map.
	 */
	public int mapWidth;
	
	/**
	 * The tilemap components attached to the tilemap game object.
	 * This list is iterated through every frame, and all
	 * OnTilemapOperation methods are fired.
	 * 
	 * Firing logic is left to the individual components.
	 * It is the responsibility of the individual components to know if they should opt-out
	 * 	of firing when they are called. The tilemap object fires *everything* every frame.
	 */
	private List<TilemapComponent> components;
	
	/**
	 * Operational done flag.
	 * Set to true to kill the TilemapOperations coroutine.
	 */
	private bool done;
	
	// Use this for initialization
	void Start () {
		components = new List<TilemapComponent>(GetComponents<TilemapComponent>());
	}
	
	/**
	 * Start the tilemap operations coroutine.
	 * Should be called by the main game controller.
	 */
	public void StartTilemapOperations() {
		done = false;
		StartCoroutine(TilemapOperations());
	}
	
	/**
	 * Should be called by the main game controller.
	 * Stop the tilemap operations coroutine.
	 */
	public void StopTilemapOperations() {
		done = true;
	}
	
	/**
	 * Main tilemap coroutine.
	 * Fires all TilemapComponent-based components attached to this game object
	 * every frame.
	 */
	public IEnumerator TilemapOperations() {
		while(!done) {
			foreach (TilemapComponent component in components) {
				component.OnTilemapOperation();
			}
			yield return new WaitForEndOfFrame();
		}
		yield break;
	}
}
