using UnityEngine;
using System.Collections;

/**
 * Bindings for a Player Panel GUI prefab.
 */
public class PlayerPanel : MonoBehaviour {
	public ClassedCombatActor player;
	
	public UILabel actorName;
	public UILabel hits;
	public UILabel energy;
	public UILabel gameClass;
	public UILabel level;
	
	public string Name {
		get { return actorName.text; }
		set { actorName.text = value; }
	}
	
	public int Hits {
		get { return int.Parse(hits.text); }
		set { hits.text = value.ToString(); }
	}
	
	public int Energy {
		get { return int.Parse(energy.text); }
		set { energy.text = value.ToString(); }
	}
	
	public string GameClass {
		get { return gameClass.text; }
		set { gameClass.text = value; }
	}
	
	public int Level {
		get { return int.Parse(level.text); }
		set { level.text = value.ToString(); }
	}
	
	public void Init(ClassedCombatActor player) {
		this.player = player;
		
		Name = player.Name;
		Hits = player.Hits;
		Energy = player.Energy;
		GameClass = player.ClassCode;
		Level = player.Level;
	}
}
