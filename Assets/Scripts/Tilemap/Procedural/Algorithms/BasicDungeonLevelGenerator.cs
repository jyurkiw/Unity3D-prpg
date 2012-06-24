using UnityEngine;
using System.Collections;

public partial class ProceduralDungeonTileManager : TileManager {
	public float minInnerSquares;	///< Float representing the proportion of squares that should be left walls.
	public float sameDirectionBias; ///< Float representing the increased tendency to go in the same direction as the previous step.
	
	private enum Direction { NORTH = 0, SOUTH = 1, EAST = 2, WEST = 3 };
	private Direction direction;
	
	private GameObject cornerstone;
	
	/**
	 * Floor grid. Read like bool[x][y] to keep things simple.
	 */
	private bool[,] floor;
	
	/**
	 * Implementation of a biased drunkard walk.
	 * 
	 * Come in off an edge (because I don't have stairs).
	 * 
	 * Loop until we have cleared enough spaces to meet our minInnerSquares ratio.
	 * 
	 * Each Loop:
	 * 	Check to make sure the destination square is a valid square for movement.
	 * 	If the move is legal:
	 * 		Move one square in the current direction.
	 * 	If the move is not legal:
	 * 		Get new direction.
	 */
	public void initMapGeneration() {
		System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
		stopwatch.Start();
		
		int totalSquares = ((mapHeight - 2) * (mapWidth - 2));
		int floorTiles = 0;
		
		DungeonDirtModel dungeonDirtModel = new DungeonDirtModel(prefabs, rand);
		
		floor = new bool[mapWidth, mapHeight];
		
		for(int x = 0; x < mapWidth; x++) {
			for(int y = 0; y < mapHeight; y++) {
				floor[x,y] = false;
			}
		}
		
		Vector2 location = genRandomStartPoint();
		
		do {
			//dunkard walk logic
			//see if bias is valid
			if (VerifyDestinationLegality(CurrentDestinationLocation(location))) {
				//get a new direction until the resulting new location is valid
				do {
					direction = NextBiasedDirection;
				}while(!VerifyDestinationLegality(CurrentDestinationLocation(location)));
			} else {
				//previous forward direction is invalid, so get new direction ignoring old forward
				do {
					direction = NewDirectioin;
				}while(!VerifyDestinationLegality(CurrentDestinationLocation(location)));
			}
			//we have new forward with bias for previous direction
			//set new location
			location = CurrentDestinationLocation(location);
			
			//if new location is not already floor, set it to floor
			//otherwise do nothing. It will be set to wall automatically.
			if(!floor[(int)location.x, (int)location.y]) {
				floor[(int)location.x, (int)location.y] = true;
				floorTiles++;
			}
		} while((1.0f - ((float)floorTiles / (float)totalSquares)) >= minInnerSquares);
		
		//generate the tiles for the map
		GameObject tile;
		
		for(int x = 0; x < mapWidth; x++) {
			for(int y = 0; y < mapHeight; y++) {
				if(floor[x, y])
					tile = dungeonDirtModel.getSprite(dungeonDirtModel.getSpriteID((int)DungeonDirtModel.TileType.DIRT));
				else
					tile = dungeonDirtModel.getSprite(dungeonDirtModel.getSpriteID((int)DungeonDirtModel.TileType.WALL));
				
				drawManager.setTile(x, y, tile);
				tile.transform.parent = cornerstone.transform;
			}
		}
		
		generated = true;
		Debug.Log("Level generation took " + stopwatch.Elapsed.Seconds + " seconds.");
	}
	
	/**
	 * Biased drunken walk NextDirection logic
	 * 
	 * Each direction has 1 in 4 chance of being selected.
	 * Biased direction has 1 in 4 plus sameDirectionBias chance.
	 * 
	 * Generate a number between  0 and 100 + 25 * sameDirectionBias.
	 * If number greater than or equal to 75, return old direction (biased direction selected)
	 * If number below 75, figure out turn direction and return new direction.
	 */
	private Direction NextBiasedDirection {
		get {
			int directionTotal = 100 + (int)(25 * sameDirectionBias);
			int rDirectionValue = rand.Next(directionTotal);
			int directionEnumVal = (int)direction;
			
			if (rDirectionValue >= 75) {
				return direction;
			} else if (rDirectionValue > 50) {
				directionEnumVal++;
			} else if (rDirectionValue > 25) {
				directionEnumVal += 2;
			} else {
				directionEnumVal += 3;
			}
			
			if (directionEnumVal > 3) {
				directionEnumVal -= 4;
			}
			
			return (Direction)directionEnumVal;
		}
	}
	
	private Direction NewDirectioin {
		get {
			int directionTotal = 75;
			int rDirectionValue = rand.Next(directionTotal);
			int directionEnumVal = (int)direction;
			
			if (rDirectionValue > 50) {
				directionEnumVal++;
			} else if (rDirectionValue > 25) {
				directionEnumVal += 2;
			} else {
				directionEnumVal += 3;
			}
			
			if (directionEnumVal > 3) {
				directionEnumVal -= 4;
			}
			
			return (Direction)directionEnumVal;
		}
	}
	
	private Vector2 CurrentDestinationLocation(Vector2 location) {
		return location + DestinationOffset;
	}
	
	private Vector2 DestinationOffset {
		get {
			if (direction == Direction.NORTH) {
				return new Vector2(0, -1.0f);
			} else if (direction == Direction.SOUTH) {
				return new Vector2(0, 1.0f);
			} else if (direction == Direction.EAST) {
				return new Vector2(1.0f, 0);
			} else {
				return new Vector2(-1.0f, 0);
			}
		}
	}
	
	private bool VerifyDestinationLegality(Vector2 destination) {
		if ((destination.x > 0 && destination.x < mapWidth - 1) &&
			(destination.y > 0 && destination.y < mapHeight - 1))
			return true;
		else return false;
	}
	
	private Vector2 genRandomStartPoint() {
		direction = (Direction)rand.Next(4);
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
		
		floor[(int)xf, (int)yf] = true;
		
		return new Vector2(xf, yf);
	}
	
	public void DisposeLevel() {
		for(int x = 0; x < mapWidth; x++) {
			for(int y = 0; y < mapHeight; y++) {
				Destroy(drawManager.tilemap.map[x, y]);
			}
		}
		generated = false;
	}
}