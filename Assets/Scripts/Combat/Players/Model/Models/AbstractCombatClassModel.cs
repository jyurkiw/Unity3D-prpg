using UnityEngine;
using System.Collections;

/**
 * Abstract class that defines a combat class.
 * Used to bridge the actor data structure and the class models.
 */
public abstract class AbstractCombatClassModel : IClassModel, ICombat {
	public abstract void SetLevelByXp(int experience);
	public abstract int GetExpNeededForLevel(int level);
	public abstract int GetLevelForExp(int exp);

	#region IClassModel implementation
	public abstract bool AddExperience(int experience);
	public abstract int Level { get; }
	public abstract int Experience { get; }
	public abstract string ClassCode { get; }
	#endregion

	#region ICombat implementation
	public abstract int Hits { get; set; }
	public abstract int Energy { get; set; }
	public abstract int Attack { get; set; }
	public abstract int Defense { get; set; }
	public abstract int Special { get; set; }
	public abstract float Speed { get; set; }
	#endregion
}