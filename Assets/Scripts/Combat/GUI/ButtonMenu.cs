using UnityEngine;
using System.Collections.Generic;

/**
 * Render a type of frame that populates with buttons rather than labels.
 */
public class ButtonMenu : Frame {
	public delegate void GUIButtonAction();
	
	protected bool named;
	protected List<GUIButtonAction> actionList;
	
	public string Name {
		get {
			if (named)
				return items[0];
			else
				return "";
		}
		set {
			if (named)
				items[0] = value;
			else {
				items.Insert(0, value);
				actionList.Insert(0, Dummy);
				named = true;
			}
		}
	}
	
	/**
	 * Create a button menu.
	 * @param GUIStyle The game's current GUI Style.
	 * @param float The frame's x position.
	 * @param float The frame's y position.
	 */
	public ButtonMenu(GUIStyle frameGUIStyle, float x, float y) : base(frameGUIStyle, x, y) {
		named = false;
		actionList = new List<GUIButtonAction>();
	}
	
	public ButtonMenu(GUIStyle frameGUIStyle, float x, float y, string menuName) : base(frameGUIStyle, x, y) {
		named = true;
		actionList = new List<GUIButtonAction>();
		AddFrameItem(menuName, Dummy);
	}
	
	//Hide the base AddFrameItem method
	protected new void AddFrameItem(string item) {
		base.AddFrameItem(item);
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
		int index = items.IndexOf(item);
		actionList.RemoveAt(index);
		
		if (index == 0 && named) {
			named = false;
		}
		
		return base.RemoveFrameItem(item);
	}
	
	#region IGUI implementation
	public new void Draw () {
		if (!initialized) Init();
		
		GUI.Box(frameBoundaries, new GUIContent());
		GUI.BeginGroup(frameBoundaries, new GUIContent());
		for(int x = 0; x < processedItems.Count; x++) {
			if (actionList[x] != Dummy) {
				if(GUI.Button(processedItems[x].rect, processedItems[x].content)) {
					actionList[x]();
				}
			
			} else {
				GUI.Label(processedItems[x].rect, processedItems[x].content);
			}
		}
		GUI.EndGroup();
	}
	#endregion
	
	protected void Dummy() {}
}
