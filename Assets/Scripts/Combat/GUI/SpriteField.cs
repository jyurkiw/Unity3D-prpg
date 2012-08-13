using UnityEngine;
using System.Collections.Generic;

/**
 * A component that handles the drawing of multiple sprites
 * in an organized fashion along a horizontal axis.
 */
[AddComponentMenu("PRPG/Combat/Sprite Field")]
class SpriteField : MonoBehaviour {
	public GameObject MonsterNode; 	///< The primary node that monsters are drawn at.
	public Camera CombatCamera;		///< The camera used for combat.
	
	//monster prefabs
	public GameObject TinyMonster;		///< A tiny monster prefab.
	public GameObject SmallMonster;		///< A small monster prefab.
	public GameObject MediumMonster;	///< A medium monster prefab.
	public GameObject LargeMonster;		///< A large monster prefab.
	
	private List<tk2dSprite> monsterSprites;
	
	public void InitSprites(CharacterManager monsterParty) {
		
	}
}
