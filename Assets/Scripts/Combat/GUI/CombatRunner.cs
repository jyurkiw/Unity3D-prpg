using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("PRPG/Combat/Combat Runner")]
public class CombatRunner : MonoBehaviour {
	public bool combatGUIActive = false;	//< Is the combat GUI active? Setting true displays the GUI.
	public GUIStyle combatGUIStyle;			//< Current GUI style for the combat GUI
	
	private CharacterManager playerParty;
	private CharacterManager enemyParty;
	
	//player GUI variables
	private GUIContent[] playerGUIContent;
	private Vector2[] playerGUISizes;
	private Rect[] playerGUILocations;
	private Vector2 playerMaxDimensions;
	private float playerBreakWidth;
	private Rect playerGUIArea;
	
	//enemy GUI variables
	private GUIContent[] enemyGUIContent;
	private Vector2[] enemyGUISizes;
	private Rect[] enemyGUILocations;
	private Vector2 enemyMaxDimensions;
	private float enemyBreakWidth;
	private Rect enemyGUIArea;
	
	/**
	 * Calculate the constants needed to populate the screen.
	 * Should be called before every combat.
	 */
	public void InitCombatRunner(CharacterManager playerParty, CharacterManager enemyParty) {
		this.playerParty = playerParty;
		this.enemyParty = enemyParty;
		
		playerGUIContent = GetContentInfo(playerParty);
		playerGUISizes = GetSizingInfo(playerGUIContent);
		playerMaxDimensions = GetMaxSize(playerGUISizes);
		playerBreakWidth = GetBreakSize(playerGUISizes, playerParty.partyCharacters.Length);
		playerGUILocations = GetGUILocation(playerGUISizes, playerBreakWidth);
		playerGUIArea = new Rect(playerBreakWidth, Screen.height - playerMaxDimensions.y, Screen.width - 2 * playerBreakWidth, playerMaxDimensions.y);
		
		enemyGUIContent = GetContentInfo(enemyParty);
		enemyGUISizes = GetSizingInfo(enemyGUIContent);
		enemyMaxDimensions = GetMaxSize(enemyGUISizes);
		enemyBreakWidth = GetBreakSize(enemyGUISizes, enemyParty.partyCharacters.Length);
		enemyGUILocations = GetGUILocation(enemyGUISizes, enemyBreakWidth);
		enemyGUIArea = new Rect(enemyBreakWidth, 0, Screen.width - 2 * enemyBreakWidth, enemyMaxDimensions.y);
	}
	
	public void DeinitCombatRunner() {
		playerParty = null;
		enemyParty = null;
	}
	
	private GUIContent[] GetContentInfo(CharacterManager actors) {
		GUIContent[] actorContent = new GUIContent[actors.partyCharacters.Length];
		
		for (int i = 0; i < actorContent.Length; i++) {
			actorContent[i] = new GUIContent(GetCombatGUIString(actors.partyCharacters[i]));
		}
		
		return actorContent;
	}
	
	private Vector2[] GetSizingInfo(GUIContent[] actorContent) {
		Vector2[] sizingInfo = new Vector2[actorContent.Length];
		
		for(int i = 0; i < sizingInfo.Length; i++) {
			sizingInfo[i] = combatGUIStyle.CalcSize(actorContent[i]);
		}
		
		return sizingInfo;
	}

	public Rect[] GetGUILocation (Vector2[] guiSizes, float breakWidth) {
		Rect[] guiLocations = new Rect[guiSizes.Length];
		float left = 0;
		
		for (int i = 0; i < guiSizes.Length; i++) {
			guiLocations[i] = new Rect(left, 0, guiSizes[i].x, guiSizes[i].y);
			left += guiSizes[i].x + breakWidth;
		}
		
		return guiLocations;
	}
	
	private Vector2 GetMaxSize(Vector2[] actorSizes) {
		Vector2 max = actorSizes[0];
		for (int i = 1; i < actorSizes.Length; i++) {
			max = Vector2.Max(max, actorSizes[i]);
		}
		
		return max;
	}
	
	private float GetBreakSize(Vector2[] guiSizes, int numActors) {
		float freeSpace = Screen.width;
		
		foreach(Vector2 dim in guiSizes) {
			freeSpace -= dim.x;
		}
		
		return freeSpace / (numActors + 1);
	}
	
	/**
	 * Return a GUI string used to populate a stat box for an actor in combat.
	 * Required Stats in the GUI Format String:
	 * 	String:		Actor Name
	 * 	Integer:	Actor Hits
	 * 	Integer:	Actor Energy (players only)
	 * 	String:		Actor Class Code
	 * 	Integer:	Actor Experience Level
	 */
	private string GetCombatGUIString(ClassedCombatActor actor) {
		if (actor.Type == ActorType.PLAYER) {
			return string.Format(actor.CombatGUIString, actor.Name, actor.Hits, actor.Energy, actor.ClassCode, actor.Level);
		} else {
			return string.Format(actor.CombatGUIString, actor.Name, actor.Hits, actor.ClassCode, actor.Level);
		}
		
	}
	
	public void OnGUI() {
		if (combatGUIActive) {
			//draw enemy stat boxes
			enemyGUIContent = GetContentInfo(enemyParty);
			DrawBoxGroup(playerGUIContent, playerGUILocations, playerGUIArea);
			
			//draw player stat boxes
			playerGUIContent = GetContentInfo(playerParty);
			DrawBoxGroup(enemyGUIContent, enemyGUILocations, enemyGUIArea);
			
			//draw instructions
			GUIContent instructionlabel = new GUIContent("Press 't' to exit combat.");
			Vector2 size = combatGUIStyle.CalcSize(instructionlabel);
			Rect labelarea = new Rect(Screen.width / 2 - size.x / 2, Screen.height / 2 - size.y / 2, size.x, size.y);
			GUI.Label(labelarea, instructionlabel);
		}
	}
	
	private void DrawBoxGroup(GUIContent[] actorContent, Rect[] actorLocation, Rect groupArea) {
		GUI.BeginGroup(groupArea);
		for (int i = 0; i < actorContent.Length; i++) {
			GUI.Box(actorLocation[i], actorContent[i]);
		}
		GUI.EndGroup();
	}
}
