using UnityEngine;
using System.Collections.Generic;

/**
 * Render a type of frame that populates with buttons rather than labels.
 */
public class ButtonMenu : Frame {
	public delegate void GUIButtonAction();
	
	protected List<GUIButtonAction> actionList;
	
	/**
	 * Create a button menu.
	 * @param GUIStyle The game's current GUI Style.
	 * @param float The frame's x position.
	 * @param float The frame's y position.
	 */
	public ButtonMenu(GUIStyle frameGUIStyle, float x, float y) : base(frameGUIStyle, x, y) {
		actionList = new List<GUIButtonAction>();
	}
	
	/**
	 * Add an item to the button menu.
	 * @param string The name of the button.
	 * @param GUIButtonAction The delegate event handler.
	 */
	public void AddFrameItem(string item, GUIButtonAction action) {
		base.AddFrameItem(item);
		actionList.Add(action);
	}
	/**
	 * Remove a specific item from the Button menu.
	 * @param item The item to remove.
	 * @return bool True if successful, false otherwise.
	 */
	public new bool RemoveFrameItem(string item) {
		actionList.RemoveAt(items.IndexOf(item));
		return base.RemoveFrameItem(item);
	}
	
	#region IGUI implementation
	public new void Draw () {
		if (!initialized) Init();
		
		GUI.Box(frameBoundaries, new GUIContent());
		GUI.BeginGroup(frameBoundaries, new GUIContent());
		for(int x = 0; x < processedItems.Count; x++)
			if(GUI.Button(processedItems[x].rect, processedItems[x].content))
				actionList[x]();
		GUI.EndGroup();
	}
	#endregion
}
