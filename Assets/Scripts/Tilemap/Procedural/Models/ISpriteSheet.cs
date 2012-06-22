using UnityEngine;

/**
 * Basic interface for sprite sheet models.
 */
public interface ISpriteSheet {
	int getSpriteID();
	GameObject getSprite(int ID);
}

public interface IKeyedSpriteSheet {
	int getSpriteID(int key);
	GameObject getSprite(int ID);
}