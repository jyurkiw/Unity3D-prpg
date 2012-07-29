/**
 * Generic Class level data Singleton.
 * Used by the combat system to populate actors.
 */
public class GenericClass : ICombatClassDefinition {
	private static GenericClass instance;
	
	private GenericClass() {}
	
	public static GenericClass GetInstance() {
		if (instance == null) instance = new GenericClass();
		return instance;
	}
	
	#region ICombatClassDefinition implementation
	public int GetHits (int level) {
		return 30 + (level * 5);
	}

	public int GetEnergy (int level) {
		return 6 + (level * 3);
	}

	public int GetAttack (int level) {
		return 10 + (level * 4);
	}

	public int GetDefense (int level) {
		return 8 + (level * 3);
	}

	public int GetSpecial (int level) {
		return 5 + (level * 2);
	}

	public float GetSpeed (int level) {
		return (float)(1.0 + (0.15 * level));
	}
	
	public string GetClassCode () {
		return "GNC";
	}
	#endregion
	
}
