using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manage combat-capable characters.
 */
[AddComponentMenu("PRPG/Combat/Components/Character Manager")]
public class CharacterManager : MonoBehaviour {
	public int maxCharactersInParty;	//< The maximum number of characters allowd in the party.
	public ClassedCombatActor[] partyCharacters;	//< An array of characters in the party.
	public bool playerParty = true;		//< True if this CharacterManager is managing the player's party. False if NPCs or enemies.
	
	public int AverageLevel {
		get {
			int total = 0;
			
			foreach (ClassedCombatActor actor in partyCharacters) {
				total += actor.Level;
			}
			
			return total / partyCharacters.Length;
		}
	}
	
	public void Start() {
		if (playerParty)
			Load();
	}
	
	/**
	 * Dummy load method.
	 * In the future, this needs to do useful things.
	 * For now, just load test data.
	 */
	public void Load() {
		partyCharacters = new ClassedCombatActor[maxCharactersInParty];
		for (int i = 0; i < maxCharactersInParty; i++) {
			partyCharacters[i] = ScriptableObject.CreateInstance<ClassedCombatActor>();
			partyCharacters[i].Init("Character" + i, ActorType.PLAYER, PRPGClassFactory.GetInstance().GetGenericClass(), 10000);
		}
	}
	
	public void LoadEnemies(int level, int numEnemies, PRPGRandom rand) {
		if (!playerParty) {
			partyCharacters = new ClassedCombatActor[System.Math.Min(maxCharactersInParty, numEnemies)];
			for (int i = 0; i < partyCharacters.Length; i++) {
				partyCharacters[i] = ScriptableObject.CreateInstance<ClassedCombatActor>();
				partyCharacters[i].Init("Derp" + (i + 1), ActorType.HUMANOID,
					PRPGClassFactory.GetInstance().GetGenericClass(), PRPGClassFactory.GetLevel(level - rand.Next(2)));
			}
		}
	}
}
