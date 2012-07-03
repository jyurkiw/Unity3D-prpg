using UnityEngine;
using System.Collections;

/**
 * Procedural seed component. Used by procedural operations to seed
 * random number generators.
 */
[AddComponentMenu("PRPG/Procedural/Procedural Seeder")]
public class ProceduralSeed : MonoBehaviour {
	public long seed1;	///< Seed1 value for procedural RNGs.
	public long seed2;	///< Seed2 value for procedural RNGs.
	public long seed3;	///< Seed3 value for procedural RNGs.
	public long seed4;	///< Seed4 value for procedural RNGs.
	public long seed5;	///< Seed5 value for procedural RNGs.
	
	public long[] GetSeedArray() {
		long[] seedArray = {seed1, seed2, seed3, seed4, seed5};
		return seedArray;
	}
	
	public void SetSeeds(long[] seedArray) {
		seed1 = seedArray[0];
		seed2 = seedArray[1];
		seed3 = seedArray[2];
		seed4 = seedArray[3];
		seed5 = seedArray[4];
	}
}
