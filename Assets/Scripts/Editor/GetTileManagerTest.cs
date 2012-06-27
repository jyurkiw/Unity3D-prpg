using UnityEngine;
using UnityEditor;
using System.Collections;

public class GetTileManagerTest : ScriptableWizard {
	public GameObject cornerstonePrefab;
	
	[MenuItem("PRPG/TileManagers/Get Tile Manager Test")]
	static void CreateWizard() {
		ScriptableWizard.DisplayWizard<GetTileManagerTest>("Get Tile Manager Test");
	}
	
	void OnWizardCreate() {
		TileManager tm = cornerstonePrefab.GetComponent<TileManager>();
		Debug.Log(tm);
	}
}