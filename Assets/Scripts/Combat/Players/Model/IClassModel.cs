public interface IClassModel {
	int Level { get; }
	int Experience { get; }
	string ClassCode { get; }
	
	bool AddExperience(int experience);
}
