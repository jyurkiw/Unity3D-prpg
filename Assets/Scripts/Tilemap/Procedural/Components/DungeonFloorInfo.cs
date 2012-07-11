using UnityEngine;
using System.Collections;

/**
 * Information used to procedurally generate the floor of the dungeon.
 * 
 * Information:
 * 	Does the floor have a staircase that leads deeper into the dungeon?
 * 	Does the floor have a staircase that leads back to the entrance?
 * 
 * Future Possible Information:
 * 	Does this floor have a weapon/armor/item merchant?
 * 	Does this floor have a priest? (ressurect slain party members)
 * 	Does this floor have a wandering adventurer? (replace a slain party member)
 * 	Does this floor have mercenary guards? (an inn where you can rest)
 */
[AddComponentMenu("PRPG/Procedural/Procedural Dungeon Floor Info")]
public class DungeonFloorInfo : AbstractProceduralFloorInfo {
	public bool hasStairUp;		///< Does this floor have a staircase that goes back towards the entrance?
	public bool hasStairDown;	///< Does this floor have a staircase that goes away from the entrance?
	public Vector2 startLocation;	///< This floor's start location on the grid.
	public Vector2 exitLocation;	///< This floor's exit location on the grid.
	
	private int mapHeight;
	private int mapWidth;
	private PRPGRandom rand;
	
	#region implemented abstract members of AbstractProceduralFloorInfo
	public override void GenerateDependentInformation() {
		ProceduralSeed seedInfo = GetComponent<ProceduralSeed>();
		
		rand = new PRPGRandom(seedInfo.GetSeedArray());
		rand.InvertSeeding();
		
		hasStairUp = (floorType == FloorType.GENERAL || floorType == FloorType.ENTRANCE);
		hasStairDown = (floorType == FloorType.GENERAL || floorType == FloorType.BOTTOM);
		
		Tilemap tilemap = GetComponent<Tilemap>();
		mapHeight = tilemap.mapHeight;
		mapWidth = tilemap.mapWidth;
		
		startLocation = GenRandomStartPoint();
		exitLocation = GenRandomExitPoint();
	}
	#endregion
	
	/**
	 * Generate a random start point for the level.
	 * If it's an entrance level, the start point is a gap in the wall.
	 * If it's any other level, the start point is a random square in the level.
	 */
	private Vector2 GenRandomStartPoint() {
		if(floorType == FloorType.ENTRANCE)
			return GenRandomStartPointInWall();
		else
			return GenRandomPointInLevel();
	}
	
	/**
	 * Generate a random exit point for the level.
	 * Pick a random square in the level that isn't the start point.
	 */
	private Vector2 GenRandomExitPoint() {
		return GenRandomPointInLevel(startLocation);
	}
	
	private Vector2 GenRandomStartPointInWall() {
		Direction direction = (Direction)rand.Next(4);
		float xf, yf;
		
		if (direction == Direction.NORTH) {
			xf = (float)rand.Next(1, mapWidth - 1);
			yf = 0;
			direction = Direction.SOUTH;
		} else if (direction == Direction.SOUTH) {
			xf = (float)rand.Next(1, mapWidth - 1);
			yf = (float)mapHeight - 1;
			direction = Direction.NORTH;
		} else if (direction == Direction.WEST) {
			xf = 0;
			yf = (float)rand.Next(1, mapHeight - 1);
			direction = Direction.EAST;
		} else {
			xf = (float)mapWidth - 1;
			yf = (float)rand.Next(1, mapHeight - 1);
			direction = Direction.WEST;
		}
		
		return new Vector2(xf, yf);
	}
	
	private Vector2 GenRandomPointInLevel() {
		return GenRandomPointInLevel(null);
	}
	
	private Vector2 GenRandomPointInLevel(Vector2? exclusionPoint) {
		int mapHeight = this.mapHeight - 1;
		int mapWidth = this.mapWidth - 1;
		Vector2 point;
		
		do {
			point = new Vector2((float)rand.Next(mapWidth), (float)rand.Next(mapHeight));
			
			if (exclusionPoint == null)
				return point;
		} while (point == (Vector2)exclusionPoint);
		
		return point;
	}
}
