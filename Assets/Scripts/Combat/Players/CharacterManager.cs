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
			partyCharacters[i].Init("Character" + i, ActorType.PLAYER, new GenericCombatClassExperienceModel(), 10000);
			partyCharacters[i].CombatGUIString = "Name: {0}\n\nHP: {1}\nEP: {2}\nClass: {3} LV{4}";
		}
	}
	
	public void LoadEnemies(int level, int numEnemies, PRPGRandom rand) {
		if (!playerParty) {
			partyCharacters = new ClassedCombatActor[System.Math.Min(maxCharactersInParty, numEnemies)];
			for (int i = 0; i < partyCharacters.Length; i++) {
				partyCharacters[i] = ScriptableObject.CreateInstance<ClassedCombatActor>();
				partyCharacters[i].Init("Derp" + (i + 1), ActorType.HUMANOID,
					new GenericCombatClassExperienceModel(), GenericCombatClassExperienceModel.Exp4Level(level - rand.Next(2)));
				partyCharacters[i].CombatGUIString = "Name: {0}\n\nHP: {1}\nClass: {2} LV{3}";
			}
		}
	}
}
