using UnityEngine;
using System.Collections;

public class ClassedCombatActor : ScriptableObject, IActor, IClassModel, ICombat, IDataLoader {
	private string actorName;
	private int level;
	private ActorType type;
	
	private int experience;
	
	private int hits;
	private int energy;
	private int attack;
	private int defense;
	private int special;
	private float speed;
	
	private AbstractCombatClassModel classModel;
	
	public ClassedCombatActor(string name, ActorType type, AbstractCombatClassModel classModel, int experience) {
		this.classModel = classModel;
		this.actorName = name;
		this.type = type;
		AddExperience(experience);
	}
	
	#region IActor implementation
	public string Name {
		get {
			return actorName;
		}
		set {
			actorName = value;
		}
	}

	public ActorType Type {
		get {
			return type;
		}
		set {
			type = value;
		}
	}
	#endregion
	
	#region IClassModel implementation
	public bool AddExperience(int experience) {
		this.experience += experience;
		classModel.SetLevelByXp(this.experience);
		
		if (level < classModel.Level) {
			level = classModel.Level;
			hits = classModel.Hits;
			energy = classModel.Energy;
			attack = classModel.Attack;
			defense = classModel.Defense;
			special = classModel.Special;
			speed = classModel.Speed;
			return true;
		} else {
			return false;
		}
	}

	public int Experience {
		get {
			return experience;
		}
	}
	
	public int Level {
		get {
			return level;
		}
	}
	#endregion

	#region ICombat implementation
	public int Hits {
		get {
			return hits;
		}
		set {
			hits = value;
		}
	}

	public int Energy {
		get {
			return energy;
		}
		set {
			energy = value;
		}
	}

	public int Attack {
		get {
			return attack;
		}
		set {
			attack = value;
		}
	}

	public int Defense {
		get {
			return defense;
		}
		set {
			defense = value;
		}
	}

	public int Special {
		get {
			return special;
		}
		set {
			special = value;
		}
	}
	
	public float Speed {
		get {
			return speed;
		}
		set {
			speed = value;
		}
	}
	#endregion
	
	public AbstractCombatClassModel ClassModel {
		get {
			return classModel;
		}
		set {
			classModel = value;
		}
	}

	#region IDataLoader implementation
	/**
	 * Fake load method from IDataLoader
	 * Automatically loads the 
	 */
	public void Load ()
	{
		actorName = "Frank";
		type = ActorType.PLAYER;
		classModel = new GenericCombatClassExperienceModel();
		AddExperience(3000);
	}

	public void Unload ()
	{
		//do nothing
	}
	#endregion
}
