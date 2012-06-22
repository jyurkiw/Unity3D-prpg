using UnityEngine;
using System.Collections;

/**
 * Container class for sprite collection prefab objects.
 * Extended by child classes who implement specific
 * logic to allow procedural algorithms to select
 * specific tiles by type.
 */
[AddComponentMenu("PRPG/Cornerstone/Prefab Container")]
public class TilePrefabContainer : MonoBehaviour {
	public enum PrefabType {PATH = 0, OUTDOOR, DUNGEON, TOWN, DOODADS};
	
	public GameObject[] prefabs;
	public string[] prefabNames;
	public PrefabType[] prefabTypes;
}
