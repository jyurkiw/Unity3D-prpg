using UnityEngine;
using System.Collections;

/**
 * Procedurally generate a dungeon tilemap.
 * 
 * This is a basic procedural tile manager used to
 * rough out a dungeon level. More advanced procedural
 * tile managers will be created to build other types of
 * maps.
 */
[AddComponentMenu("PRPG/Cornerstone/Tile Managers/Procedural Tile Manager")]
public partial class ProceduralDungeonTileManager : TileManager {
	public int seed;		///< The value used to see the RNG
	public bool generated;	///< Has the level been generated?
	
	private System.Random rand;	///< The RNG used to generate the level.
	private TilePrefabContainer prefabs;	///< The prefab container.
	
	// Use this for initialization
	new void Start () {
		base.Start();
		
		if (seed > 0)
			rand = new System.Random(seed);
		else
			rand = new System.Random();
		
		generated = false;
		
		initProceduralTileManager();
	}
	
	private void initProceduralTileManager() {
		//get TilePrefabContainer
		prefabs = GameObject.Find("Cornerstone").GetComponent<TilePrefabContainer>();
	}
	
	#region implemented abstract members of TilemapComponent
	public override void OnTilemapOperation() {
		if (!generated) {
			initMapGeneration(); //method definition located in Scripts/Procedural/Algorithms/BasicDungeonLevelGenerator.cs
		}
	}
	#endregion
}
