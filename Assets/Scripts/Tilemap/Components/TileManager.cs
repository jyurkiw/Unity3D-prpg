using UnityEngine;
using System.Collections;

/**
 * Base version of a Tile Manager.
 * Not intended to be used, but rather extended into a more specific
 * type of Tile Manager.
 * 
 * See ProceduralTileManager for specific example.
 * 
 * A TileManager tells its associated DrawManager what tiles to draw where
 * and is used by the DrawManager to populate the Tilemap.
 */
public class TileManager : TilemapComponent {
	protected DrawManager drawManager;	///< Draw Manager used by the cornerstone.
	
	protected int mapWidth;		///< Width of the map in tiles.
	protected int mapHeight;	///< Height of the map in tiles.
	
	// Use this for initialization
	public void Start () {
		getMapBounds();
		getDrawManager();
	}
	
	protected void getMapBounds() {
		Tilemap tilemap = GameObject.Find("Cornerstone").GetComponent<Tilemap>();
		mapWidth = tilemap.mapWidth;
		mapHeight = tilemap.mapHeight;
	}
	
	protected void getDrawManager() {
		drawManager = GameObject.Find("Cornerstone").GetComponent<DrawManager>();
	}
	
	#region implemented abstract members of TilemapComponent
	public override void OnTilemapOperation () {
		
	}
	#endregion
}
