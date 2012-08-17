using UnityEngine;
using System.Collections;

/**
 * Control the active state of a number of objects at once.
 */
public class Activator : MonoBehaviour {
	public GameObject[] controlledObjects;   ///< Array of objects controlled by this Activator.
	
	private bool objectsActive;
	
	/**
	 * The active state of the controlled objects.
	 */
	public bool Active {
		get { return objectsActive; }
		set { ToggleTo(value); }
	}
	
	/**
	 * Set the active state of all objects controlled by this activator.
	 * @param bool The active state to set.
	 */
	private void ToggleTo(bool state) {
		objectsActive = state;
		
		foreach(GameObject go in controlledObjects)
			go.SetActiveRecursively(state);
	}
}
