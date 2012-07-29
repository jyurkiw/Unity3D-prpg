/**
 * Class that generates pre-defined ClassFrame objects via a
 * lite factory pattern. This factory is specific to this game.
 * All classes in the game are created with this factory.
 */
public class PRPGClassFactory {
	private static PRPGClassFactory instance;
	
	private PRPGClassFactory() {}
	
	/**
	 * Get the PRPG Class Factory instance.
	 */
	public static PRPGClassFactory GetInstance() {
		if (instance == null) instance = new PRPGClassFactory();
		return instance;
	}
	
	/**
	 * Get actor level based on experience.
	 */
	public static int GetLevel(int experience) {
		return (int)System.Math.Floor(System.Math.Pow((13+experience/13), 1.0/3.0));
	}
	
	/**
	 * Get experience needed for level.
	 */
	public static int GetExperience(int level) {
		return (int)(13*(System.Math.Pow(level, 3))-13);
	}
	
	/**
	 * Generate a class frame object for a Generic Class.
	 */
	public ClassFrame GetGenericClass() {
		ClassFrame frame = new ClassFrame();
		
		frame.GetAttack = GenericClass.GetInstance().GetAttack;
		frame.GetDefense = GenericClass.GetInstance().GetDefense;
		frame.GetEnergy = GenericClass.GetInstance().GetEnergy;
		frame.GetHits = GenericClass.GetInstance().GetHits;
		frame.GetSpecial = GenericClass.GetInstance().GetSpecial;
		frame.GetSpeed = GenericClass.GetInstance().GetSpeed;
		
		return frame;
	}
}