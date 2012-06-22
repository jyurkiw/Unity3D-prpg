using UnityEngine;
using System.Collections;

/**
 * Model of the dungeon dirt spritesheet.
 * Future models will be more generic, and
 * sprite sheets will have to adhere to standards
 * defined by the models.
 */
public class DungeonDirtModel : IKeyedSpriteSheet {
	public enum TileType { DIRT, WALL };	///< Simple enum so that the algorithm can tell the model what kind of sprite it needs.
	
	public tk2dSprite prefab;	///< The sprite prefab. Pulled from a TilePrefabContainer.
	
	private string name = "DungeonDirt";
	private int wall;
	private int[] dirt;
	private System.Random rand;
	
	/**
	 * Constructs a new Dungeon Dirt Model.
	 * Requires the DungeonDirt prefab object,
	 * and the TileManager's RNG object.
	 */
	public DungeonDirtModel(TilePrefabContainer prefabContainer, System.Random rand) {
		int prefabIndex = 0;
		for(int x = 0; x < prefabContainer.prefabNames.Length; x++) {
			if(prefabContainer.prefabNames[x] == SPRITESHEETNAME) {
				prefabIndex = x;
				x = prefabContainer.prefabNames.Length;
			}
		}
		
		this.rand = rand;
		
		//sprite ids are hardcoded for this test class.
		//future models will use some sort of data source.
		wall = 4;
		dirt = new int[] {0, 1, 2, 3, 5, 6, 7, 8};
	}
	
	/**
	 * The name of the spritesheet used with this model.
	 */
	public string SPRITESHEETNAME {
		get { return name; }
	}
	
	
	#region IKeyedSpriteSheet implementation
	/**
	 * Get a sprite. The key is just the integer-cast of a DungeonDirtModel.TileType enum.
	 * 
	 * @param key The DungeonDirtModel.TileType key that tells the model which kind of tile you need.
	 */
	public int getSpriteID (int key) {
		if (key == (int)TileType.WALL) return wall;
		else return dirt[rand.Next(dirt.Length)];
	}
	
	/**
	 * Instantiate and return a sprite.
	 * 
	 * @param ID The id of the sprite we want to display. Usually fetched with a getSpriteID(key) call.
	 */
	public GameObject getSprite (int ID) {
		GameObject newTile = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		tk2dSprite sprite = newTile.GetComponent<tk2dSprite>();
		
		sprite.spriteId = ID;
		
		return newTile;
	}
	#endregion
}