using UnityEngine;
using System.Collections;

/**
 * Model of the dungeon dirt spritesheet.
 * Future models will be more generic, and
 * sprite sheets will have to adhere to standards
 * defined by the models.
 */
public class DungeonDirtModel : IKeyedSpriteSheet {
	/**
	 * Simple enum so that the algorithm can tell the model what kind of sprite it needs.
	 */
	public enum TileType { DIRT = 5, WALL = 2, UPSTAIR = 0, DOWNSTAIR = 3, OPENCHEST = 1, CLOSECHEST = 4 };
	
	public GameObject dirtPrefab;	///< The sprite prefab. Pulled from a TilePrefabContainer.
	public GameObject doodadPrefab;	///< The doodad prefab. Pulled from a TilePrefabContainer.
	
	private string nameDirt = "DungeonDirt";
	private string nameDoodads = "DungeonDoodads";
	private int numDirtTiles;
	private System.Random rand;
	
	/**
	 * Constructs a new Dungeon Dirt Model.
	 * Requires the DungeonDirt prefab object,
	 * and the TileManager's RNG object.
	 */
	public DungeonDirtModel(TilePrefabContainer prefabContainer, System.Random rand) {
		dirtPrefab = prefabContainer.GetPrefab(nameDirt);
		doodadPrefab = prefabContainer.GetPrefab(nameDoodads);
		
		this.rand = rand;
		
		//sprite ids are hardcoded for this test class.
		//future models will use some sort of data source.
		numDirtTiles = 9;
	}
	
	/**
	 * The name of the spritesheet used with this model.
	 */
	public string DIRTSHEETNAME {
		get { return nameDirt; }
	}
	
	public string DOODADSHEETNAME {
		get { return nameDoodads; }
	}
	
	
	#region IKeyedSpriteSheet implementation
	/**
	 * Get a sprite. The key is just the integer-cast of a DungeonDirtModel.TileType enum plus some magic
	 * for dirt tiles.
	 * 
	 * @param key The DungeonDirtModel.TileType key that tells the model which kind of tile you need.
	 */
	public int getSpriteID (int key) {
		if (key >= 5)
			key += rand.Next(numDirtTiles);
		
		return key;
	}
	
	/**
	 * Instantiate and return a sprite.
	 * If the key is 0-4 return a doodad.
	 * If the key is 5+, return a random dirt tile.
	 * 
	 * @param ID The id of the sprite we want to display. Usually fetched with a getSpriteID(key) call.
	 */
	public GameObject getFloor (int ID) {
		GameObject prefab;
		bool? trigger = null;
		bool doodad = false;
		
		//if ID > 5 it's dirt
		if (ID >= 5) {
			prefab = dirtPrefab;
			ID -= 5;
		}
		//otherwise it's a doodad
		else {
			doodad = true;
			prefab = doodadPrefab;
			if((TileType)ID == TileType.DOWNSTAIR || (TileType)ID == TileType.UPSTAIR)
				trigger = true;
			else {
				trigger = false;
			}
		}
		
		GameObject newTile = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		tk2dSprite sprite = newTile.GetComponent<tk2dSprite>();
		
		if(trigger != null) {
			newTile.collider.isTrigger = (bool)trigger;
		}
		
		//add tileinfo component for the draw manager
		TileInfo info = newTile.AddComponent<TileInfo>();
		if (doodad)
			info.layer = TileInfo.TileLayer.DOODAD;
		
		sprite.spriteId = ID;
		
		return newTile;
	}
	
	public GameObject getDoodad (int ID) {
		return getFloor(ID);
	}
	#endregion
}