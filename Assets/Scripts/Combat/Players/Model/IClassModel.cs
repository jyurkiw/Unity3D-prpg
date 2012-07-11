public interface IClassModel {
	int Level { get; }
	int Experience { get; }
	
	bool AddExperience(int experience);
}
