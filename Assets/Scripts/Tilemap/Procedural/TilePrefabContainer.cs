using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Container class for sprite collection prefab objects.
 * Extended by child classes who implement specific
 * logic to allow procedural algorithms to select
 * specific tiles by type.
 */
[AddComponentMenu("PRPG/Cornerstone/Prefab Container")]
public class TilePrefabContainer : MonoBehaviour {
	public GameObject[] prefabs;
	public string[] prefabNames;
	
	private Dictionary<string,GameObject> prefabDictionary;
	
	public GameObject GetPrefab(string name) {
		return prefabDictionary[name];
	}
	
	public TilePrefabContainer() {
		prefabDictionary = new Dictionary<string, GameObject>();
	}
	
	void Awake() {
		//construct the prefab dictionary
		if (prefabs.Length != prefabNames.Length)
			throw new System.ArgumentException("prefab.Length(" + prefabs.Length + ") is not the same as prefabNames.Length(" + prefabNames.Length + ")");
		
		for (int i = 0; i < prefabs.Length; i++)
			prefabDictionary.Add(prefabNames[i], prefabs[i]);
	}
}
