using UnityEngine;
using System.Collections;
using UnityEditor;

/**
 * Custome inspector for Tile Prefab Containers.
 */
[CustomEditor(typeof(TilePrefabContainer))]
public class TilePrefabContainerInspector : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();
	}
}