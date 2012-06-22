using UnityEngine;
using UnityEditor;
using System.Collections;

public class SpriteDimensionsWizard : ScriptableWizard {
	public tk2dSprite testSprite;
	
	[MenuItem("PRPG/Sprites/Sprite Dimension Wizard")]
	static void CreateWizard() {
		ScriptableWizard.DisplayWizard<SpriteDimensionsWizard>("Sprite Dimensions Check");
	}
	
	void OnWizardCreate() {
		Vector3 d = testSprite.collider.bounds.size;
		Debug.Log("X: " + d.x + " Y: " + d.y + " Z: " + d.z);
	}
}
