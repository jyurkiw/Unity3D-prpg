using UnityEngine;
using System.Collections.Generic;

/**
 * Binds a Frame to a CombatClassActor.
 * Specialized kind of Frame for rendering actor combat data.
 * 
 *  ===============
 * | ActorName     |
 * | Hp: hits      |
 * | En: energy    |
 * | class         |
 * | Lv: lvl       |
 *  ===============
 */
public class ActorFrame : Frame {
	public ClassedCombatActor actor; //< Actor to represent in the frame.
	
	public bool changed;
	
	public static string HPLABEL = "Hp: "; //< Hit Point label for ActorFrames.
	public static string ENLABEL = "En: "; //< Energy Point label for ActorFrames.
	public static string LVLABEL = "Lv: "; //< Level label for ActorFrames.
	
	/**
	 * Create a frame bound to an actor.
	 * @param GUIStyle The game's current GUI Style.
	 * @param float The frame's x position.
	 * @param float The frame's y position.
	 * @param ClassedCombatActor The actor to bind to the frame.
	 */
	public ActorFrame(GUIStyle frameGUIStyle, float x, float y, ClassedCombatActor actor) : base(frameGUIStyle, x, y) {
		this.actor = actor;
		changed = false;
		AddDataToFrame();
	}
	
	//hiding the add frame item method.
	protected new void AddFrameItem(string item) {
		base.AddFrameItem(item);
	}
	
	//hiding the remove frame item method.
	protected new bool RemoveFrameItem(string item) {
		return base.RemoveFrameItem(item);
	}
	
	/**
	 * Clear the items list and re-build all visible labels in the frame.
	 */
	protected void AddDataToFrame() {
		items.Clear();
		
		//add actor name item
		AddFrameItem(actor.Name);
		
		//add hits line
		AddFrameItem(HitsLine);
		
		//add energy line
		AddFrameItem(EnergyLine);
		
		//add class token
		AddFrameItem(actor.ClassCode);
		
		//add level line
		AddFrameItem(LevelLine);
		
		initialized = false;
	}
	
	protected string HitsLine {
		get {
			return ActorFrame.HPLABEL + actor.Hits.ToString();
		}
	}
	
	protected string EnergyLine {
		get {
			return ActorFrame.ENLABEL + actor.Energy.ToString();
		}
	}
	
	protected string LevelLine {
		get {
			return ActorFrame.LVLABEL + actor.Level.ToString();
		}
	}
	
	#region IGUI implementation
	public new void Draw () {
		if (changed)
			AddDataToFrame();
		base.Draw();
	}
	#endregion
}