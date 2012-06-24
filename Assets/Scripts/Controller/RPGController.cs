using UnityEngine;
using System.Collections;

/**
 * Game controller. Only basic functionality at this point.
 */
[AddComponentMenu("PRPG/Controllers/RPGController")]
public class RPGController : MonoBehaviour {
	public Tilemap tilemap;
	
	// Use this for initialization
	void Start () {
		tilemap.StartTilemapOperations();
	}
}
