/**
 * Load/Unload interface contract.
 */
public interface IDataLoader {
	/**
	 * Load data from a data source.
	 */
	void Load();
	
	/**
	 * Unload data from the contracted class into serialized storage.
	 */
	void Unload();
}