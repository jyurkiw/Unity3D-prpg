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
			partyCharacters[i] = new ClassedCombatActor("Character" + i, ActorType.PLAYER, new GenericCombatClassExperienceModel(), 0);
		}
	}
}
