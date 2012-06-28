using UnityEngine;
using System.Collections;

/**
 * Storage class that holds information about the floor.
 */
public abstract class AbstractProceduralFloorInfo : MonoBehaviour {
	public FloorType floorType; ///< Is the floor an entrance floor, general floor, or bottom floor? (default: GENERAL)
	public int floorNumber;
	
	public AbstractProceduralFloorInfo() {
		floorType = FloorType.GENERAL;
	}
	
	public abstract void GenerateDependentInformation();
}
