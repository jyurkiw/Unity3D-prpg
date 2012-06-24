using UnityEngine;
using System.Collections;

/**
 * Component that tells the tilemap how to display it's tiles if they have been sullied.
 * This component primarily just sets spacing, but more advanced draw managers could
 * possibly do more.
 */
[AddComponentMenu("PRPG/Cornerstone/Draw Manager")]
public class DrawManager : TilemapComponent {
	/**
	 * Sullied tiles flag.
	 * If true this component should fire in the next frame's tilemap operation sequence.
	 * Otherwise opt-out.
	 */
	public bool tilesAreDirty;
	
	/*
	 * Size of the tiles in terms of Unity3D space.
	 * Example: 64x64 px tile is 0.2
	 */
	public float tileSize;
	
	/**
	 * The tilemap object this component tells how to draw stuff.
	 */
	public Tilemap tilemap;
	
	/*
	 * Return the tile at the given indicies.
	 */
	public GameObject getTile(int x, int y) {
		return tilemap.map[x, y];
	}
	
	/*
	 * Set the tile at the given indicies to the given tile.
	 * Set the tileAreDirty variable so OnTilemapOperation
	 * doesn't opt-out next frame.
	 */
	public void setTile(int x, int y, GameObject tile) {
		tilemap.map[x, y] = tile;
		tilesAreDirty = true;
	}
	
	// Use this for initialization
	void Start () {
		base.Start();
		tilesAreDirty = false;
	}
	
	#region implemented abstract members of TilemapComponent
	/**
	 * Component operation. Fires every frame. Opts-out of operations
	 * when tiles are clean, and opts-in when they are dirty.
	 * 
	 * Tiles are dirty when they have been set by the setTile method
	 * but have not been positioned by the Draw Manager.
	 */
	public override void OnTilemapOperation ()
	{
		if (tilesAreDirty) {
			for (int y = 0; y < tilemap.mapHeight; y++) {
				for (int x = 0; x < tilemap.mapWidth; x++) {
					if (tilemap.map[x, y] is GameObject) {
						tilemap.map[x, y].transform.localPosition = new Vector3(x * tileSize, y * tileSize, 0);
					}
				}
			}
			tilesAreDirty = false;
		}
	}
	#endregion
}
