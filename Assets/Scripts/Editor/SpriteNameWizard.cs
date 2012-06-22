using UnityEngine;
using UnityEditor;
using System.Collections;

public class SpriteNameWizard : ScriptableWizard {
	public tk2dSprite testSprite;
	
	[MenuItem("PRPG/Sprites/Sprite Name Wizard")]
	static void CreateWizard() {
		ScriptableWizard.DisplayWizard<SpriteNameWizard>("Sprite Name Check");
	}
	
	void OnWizardCreate() {
		string n = testSprite.spriteId.ToString();
		
		Debug.Log(n);
		
		testSprite.spriteId = 14;
		
		n = testSprite.spriteId.ToString();
		
		Debug.Log(n);
	}
}
