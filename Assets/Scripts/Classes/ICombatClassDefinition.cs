/**
 * Implementation contract for a Class Singleton.
 * 
 * All interface members take the Actor's level as
 * an input, and return an integer or float value
 * depending on the stat being returned.
 */
public interface ICombatClassDefinition {
	int GetHits(int level);
	int GetEnergy(int level);
	int GetAttack(int level);
	int GetDefense(int level);
	int GetSpecial(int level);
	float GetSpeed(int level);
}
