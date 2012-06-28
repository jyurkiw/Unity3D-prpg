using UnityEngine;
using System.Collections;

/**
 * Procedural seed component. Used by procedural operations to seed
 * random number generators.
 */
[AddComponentMenu("PRPG/Procedural/Procedural Seeder")]
public class ProceduralSeed : MonoBehaviour {
	public int seed;	///< Seed value for procedural RNGs.
}
