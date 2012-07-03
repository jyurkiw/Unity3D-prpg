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
	public bool generated;	///< Level is generated flag.
	
	private PRPGRandom rand;	///< The RNG used to generate the level.
	private TilePrefabContainer prefabs;	///< The prefab container.
	
	private ProceduralSeed seedingInfo;
	
	// Use this for initialization
	new void Start () {
		base.Start();
		seedingInfo = GetComponent<ProceduralSeed>();
		
		rand = new PRPGRandom(seedingInfo.GetSeedArray());
		
		generated = false;
		
		initProceduralTileManager();
	}
	
	private void initProceduralTileManager() {
		//get TilePrefabContainer
		prefabs = GetComponent<TilePrefabContainer>();
	}
	
	#region implemented abstract members of TilemapComponent
	public override void OnTilemapOperation() {
		if (!generated) {
			initMapGeneration(); //method definition located in Scripts/Procedural/Algorithms/BasicDungeonLevelGenerator.cs
		}
	}
	#endregion
}
