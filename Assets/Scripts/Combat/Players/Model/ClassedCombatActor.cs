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
	
	private ClassFrame classModel;
	
	public void Init(string name, ActorType type, ClassFrame classModel, int experience) {
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
	public bool AddExperience(int addExperience) {
		experience += addExperience;
		
		if (level < PRPGClassFactory.GetLevel(experience)) {
			level = PRPGClassFactory.GetLevel(experience);
			hits = classModel.GetHits(level);
			energy = classModel.GetEnergy(level);
			attack = classModel.GetAttack(level);
			defense = classModel.GetDefense(level);
			special = classModel.GetSpecial(level);
			speed = classModel.GetSpeed(level);
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
	
	public string ClassCode {
		get {
			return classModel.GetClassCode();
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
	
	public ClassFrame ClassModel {
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
		classModel = PRPGClassFactory.GetInstance().GetGenericClass();
		AddExperience(3000);
	}

	public void Unload ()
	{
		//do nothing
	}
	#endregion
}
