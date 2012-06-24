using UnityEngine;
using System.Collections;

/**
 * Abstract base class for all tilemap components.
 * Extends MonoBehaviour.
 * Provides the OnTilemapOperation method fired
 * 	by the tilemap's TilemapOperations coroutine
 * 	every frame.
 */
public abstract class TilemapComponent : MonoBehaviour {
	public void Start() {
		RegisterWithTilemap();
	}
	
	/**
	 * This method gets fired by the Tilemap object every frame.
	 * It is the responsibility of the implimenting component to
	 * handle all opt-in/out logic.
	 */
	public abstract void OnTilemapOperation();
	
	protected void RegisterWithTilemap() {
		Tilemap tilemap = GetComponent<Tilemap>();
		tilemap.RegisterComponent(this);
		Debug.Log("Registered " + name + " with the tilemap.");
	}
}
