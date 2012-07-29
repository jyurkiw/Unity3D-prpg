/**
 * A simple framework class for a character class.
 * Takes delegate references to the static methods
 * provided by ICombatClassDefinition contractors.
 */
public class ClassFrame {
	public delegate T ClassSupport<T>(int level);
	
	public ClassFrame() {}
	
	public ClassSupport<int> GetHits;
	public ClassSupport<int> GetEnergy;
	public ClassSupport<int> GetAttack;
	public ClassSupport<int> GetDefense;
	public ClassSupport<int> GetSpecial;
	public ClassSupport<float> GetSpeed;
}