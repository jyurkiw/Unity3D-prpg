using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manages the Cornerstones that make up the current level spread.
 * Keeps a list of all floor seeds for backtracking purposes.
 * Stores and organizes three maps at one time:
 * 	1. The previous tilemap.
 * 	2. The current tilemap.
 * 	3. The next tilemap.
 * 
 * When a new cornerstone is created, a random prefab is chosen from the list.
 * If you need more control or controlling logic, just extend the CornerstoneManager.
 * This class includes procedural logic.
 */
[AddComponentMenu("PRPG/Cornerstone Manager")]
public class CornerstoneManager : MonoBehaviour {
	public long[] seed;	///< The cornerstone seed.
	public int levels;	///< The number of levels in this unit of levels
	public List<GameObject> cornerstonePrefabs;	///< The cornerstone prefabs used to create levels.
	public GameObject previousCornerstone;	///< The previous cornerstone in the order.
	public GameObject activeCornerstone;	///< The active cornerstone tilemap.
	public GameObject nextCornerstone;		///< The next cornerstone in the order.
	public GameObject player; ///< The player object.
	
	private PRPGRandom rand;
	private List<CornerstoneInfo> cornerstones;
	private int cornerstoneIndex;
	
	protected enum TargetTilemap { NEXT, ACTIVE, PREVIOUS };
	private TargetTilemap lastMove;
	
	private static Vector3 previousPosition = new Vector3(0, 0, -1);
	private static Vector3 activePosition = Vector3.zero;
	private static Vector3 nextPosition = new Vector3(0, 0, 1);
	
	/**
	 * Returns a seed for an RNG.
	 * If the last move operation was NEXT it returns the seed for the next level.
	 * If the last move operation was RETURN if returns the seed for the previous level.
	 */
	public long[] GetInactiveSeed {
		get {
			if (lastMove == TargetTilemap.NEXT)
				return cornerstones[cornerstoneIndex + 1].rngSeed;
			else
				return cornerstones[cornerstoneIndex - 1].rngSeed;
		}
	}
	
	/**
	 * Initialize the cornerstone Manager.
	 */
	public CornerstoneManager() {
		seed = new long[5] {42L, 38254468L, 92444892L, 546874135L, 928672534L}; //Remove once default seeding is not necessary.
		cornerstones = new List<CornerstoneInfo>();
		cornerstonePrefabs = new List<GameObject>();
		cornerstoneIndex = 0;
	}
	
	/**
	 * On Awake, populate the seeds and level prefabs.
	 */
	public void Awake() {
		rand = new PRPGRandom(seed);
		
		cornerstones.Add(new CornerstoneInfo(
			cornerstonePrefabs[rand.Next(cornerstonePrefabs.Count)],
			rand.NextRNGSeedingSet(), CornerstoneInfo.MapType.ENTRANCE));
		
		for (int i = 1; i < levels - 1; i++) {
			cornerstones.Add(new CornerstoneInfo(
				cornerstonePrefabs[rand.Next(cornerstonePrefabs.Count)],
				rand.NextRNGSeedingSet(), CornerstoneInfo.MapType.STANDARD));
		}
		
		cornerstones.Add(new CornerstoneInfo(
			cornerstonePrefabs[rand.Next(cornerstonePrefabs.Count)],
			rand.NextRNGSeedingSet(), CornerstoneInfo.MapType.BOTTOM));
		
		rand = null;
		
		//populate the active and next cornerstones
		//since we're on level zero, there is no previous level.
		CreateTilemapFromPrefab(0, TargetTilemap.ACTIVE);
		CreateTilemapFromPrefab(1, TargetTilemap.NEXT);
		SetRPGControllerTilemapSize();
	}
	
	public void Start() {
		StartCoroutine(PlacePlayerInActiveStartPosition());
	}
	
	/**
	 * Dispose the previous level,
	 * build one level beyond the next level,
	 * advance all levels up one.
	 */
	public void AdvanceOneLevel() {
		//disable the destination stair case
		Vector2 startLocation = nextCornerstone.GetComponent<DungeonFloorInfo>().startLocation;
		nextCornerstone.GetComponent<Tilemap>().map[(int)startLocation.x, (int)startLocation.y].GetComponent<StairHandler>().enterProtected = true;
		
		lastMove = TargetTilemap.NEXT;
		GameObject toDestroy = previousCornerstone;
		previousCornerstone = activeCornerstone;
		activeCornerstone = nextCornerstone;
		nextCornerstone = null;
		cornerstoneIndex++;
		
		previousCornerstone.transform.localPosition = previousPosition;
		activeCornerstone.transform.localPosition = activePosition;
		if (cornerstoneIndex + 1 < levels)
			CreateTilemapFromPrefab(cornerstoneIndex + 1, TargetTilemap.NEXT);
		
		SetRPGControllerTilemapSize();
		StartCoroutine(PlacePlayerInActiveStartPosition());
		
		if (toDestroy != null)
			DestroyTilemapFromPrefab(toDestroy);
	}
	
	/**
	 * Despose the next level,
	 * build one level before the previous level,
	 * move all levels back one.
	 */
	public void ReturnOneLevel() {
		//disable the destination stair case
		Vector2 exitLocation = previousCornerstone.GetComponent<DungeonFloorInfo>().exitLocation;
		previousCornerstone.GetComponent<Tilemap>().map[(int)exitLocation.x, (int)exitLocation.y].GetComponent<StairHandler>().enterProtected = true;
		
		lastMove = TargetTilemap.PREVIOUS;
		GameObject toDestroy = nextCornerstone;
		nextCornerstone = activeCornerstone;
		activeCornerstone = previousCornerstone;
		previousCornerstone = null;
		cornerstoneIndex--;
		
		if (cornerstoneIndex - 1 > 0) {
			CreateTilemapFromPrefab(cornerstoneIndex - 1, TargetTilemap.PREVIOUS);
		}
		activeCornerstone.transform.localPosition = activePosition;
		nextCornerstone.transform.localPosition = nextPosition;
		
		SetRPGControllerTilemapSize();
		StartCoroutine(PlacePlayerInActiveStartPosition());
		
		if (toDestroy != null)
			DestroyTilemapFromPrefab(toDestroy);
	}
	
	/**
	 * Create a tilemap from a prefab in the generated cornerstones list.
	 */
	protected void CreateTilemapFromPrefab(int prefabIndex, TargetTilemap target) {
		GameObject cornerstone;
		
		/*
		 * Create a new cornerstone object based on it's location in the scene.
		 * The active target should only be used on game start, and when transitioning from
		 * a dungeon to the world map.
		 */
		if (target == TargetTilemap.ACTIVE) {
			activeCornerstone = (GameObject)Instantiate(cornerstones[prefabIndex].cornerstonePrefab, activePosition, Quaternion.identity);
			cornerstone = activeCornerstone;
		}
		else if (target == TargetTilemap.NEXT) {
			nextCornerstone = (GameObject)Instantiate(cornerstones[prefabIndex].cornerstonePrefab, nextPosition, Quaternion.identity);
			cornerstone = nextCornerstone;
		}
		else {
			previousCornerstone = (GameObject)Instantiate(cornerstones[prefabIndex].cornerstonePrefab, previousPosition, Quaternion.identity);
			cornerstone = previousCornerstone;
		}
		
		//name the cornerstone based on the level
		cornerstone.name = "Cornerstone(Level " + prefabIndex + ")";
		
		//parent the cornerstone to the cornerstone manager
		cornerstone.transform.parent = transform;
		
		//set the seed used to create the level we just instantiated before we generate it.
		ProceduralSeed seedingInfo = cornerstone.GetComponent<ProceduralSeed>();
		seedingInfo.SetSeeds(cornerstones[prefabIndex].rngSeed);
		
		//set any necessary floor information before generation.
		AbstractProceduralFloorInfo floorInfo = cornerstone.GetComponent<AbstractProceduralFloorInfo>();
		if (prefabIndex == 0)
			floorInfo.floorType = FloorType.ENTRANCE;
		else if (prefabIndex == levels - 1)
			floorInfo.floorType = FloorType.BOTTOM;
		floorInfo.floorNumber = prefabIndex;
		floorInfo.GenerateDependentInformation();
		
		Tilemap tilemap = cornerstone.GetComponent<Tilemap>();
		tilemap.StartTilemapOperations();
	}
	
	protected void DestroyTilemapFromPrefab(GameObject tilemapObject) {
		Tilemap tilemap = tilemapObject.GetComponent<Tilemap>();
		tilemap.StopTilemapOperations();
		
		Destroy(tilemapObject);
	}
	
	protected void SetRPGControllerTilemapSize() {
		player.GetComponent<RPGController>().tileSize = activeCornerstone.GetComponent<DrawManager>().tileSize;
	}
	
	protected void SetPlayerPosition() {
		float tileSize = activeCornerstone.GetComponent<DrawManager>().tileSize;
		Vector2 startLocation;
		
		if (lastMove == TargetTilemap.NEXT)
			startLocation = activeCornerstone.GetComponent<DungeonFloorInfo>().startLocation;
		else
			startLocation = activeCornerstone.GetComponent<DungeonFloorInfo>().exitLocation;
		player.transform.localPosition = new Vector3(startLocation.x * tileSize, startLocation.y * tileSize, player.transform.localPosition.z);
	}
	
	protected IEnumerator PlacePlayerInActiveStartPosition() {
		ProceduralDungeonTileManager tileManager = null;
		
		while (tileManager == null) {
			yield return new WaitForEndOfFrame();
			tileManager = activeCornerstone.GetComponent<ProceduralDungeonTileManager>();
		}
		
		//darken screen here
		
		while (!tileManager.generated)
			yield return new WaitForEndOfFrame();
		SetPlayerPosition();
		
		//remove shroud here
	}
	
	protected class CornerstoneInfo {
		public enum MapType { ENTRANCE, BOTTOM, STANDARD };
		public GameObject cornerstonePrefab;
		public long[] rngSeed;
		public MapType type;
		
		public CornerstoneInfo(GameObject cornerstonePrefab, long[] rngSeed, MapType type) {
			this.cornerstonePrefab = cornerstonePrefab;
			this.rngSeed = rngSeed;
			this.type = type;
		}
	}
}
