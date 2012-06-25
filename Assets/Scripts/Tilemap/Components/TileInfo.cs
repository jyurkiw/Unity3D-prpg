using UnityEngine;
using System.Collections;

public class TileInfo : MonoBehaviour {
	public enum TileLayer { FLOOR, DOODAD };
	
	public TileLayer layer;
	
	public TileInfo() {
		layer = TileLayer.FLOOR;
	}
}
