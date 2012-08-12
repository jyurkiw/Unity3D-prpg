using UnityEngine;
using System.Collections.Generic;

/**
 * Draws a list of frame objects in an area.
 */
public class FrameList : IGUI {
	public FrameOrientation orientation; //< Orientation of the frames. Default: VERTICAL.
	public GUIStyle frameListGUIStyle;
	public Vector2 framePosition;
	
	public float framePadding; //< Padding between frames.
	
	protected List<Frame> items;
	protected Rect listBoundaries;
	protected bool initialized;
	
	/**
	 * Used to override the automatically generated Frame Boundaries value.
	 */
	public Rect BoundaryOverride {
		set { listBoundaries = value; }
	}
	
	/*
	 * Read-only: Frame boundary rect.
	 * @return Rect Frame boundary.
	 */
	public Rect Boundary {
		get { return listBoundaries; }
	}
	
	/**
	 * Create a FrameList frame manager object.
	 * @param GUIStyle The game's current GUI Style.
	 * @param float The frame's x position.
	 * @param float The frame's y position.
	 */
	public FrameList(GUIStyle frameListGUIStyle, float x, float y) {
		orientation = FrameOrientation.VERTICAL;
		this.frameListGUIStyle = frameListGUIStyle;
		framePosition = new Vector2(x, y);
		
		items = new List<Frame>();
		initialized = false;
	}
	
	/**
	 * Add a new frame to the FrameList.
	 * @param Frame The frame to add (frame position data will be controlled by the frame list).
	 */
	public void AddFrame(Frame frame) {
		initialized = false;
		items.Add(frame);
	}
	
	/**
	 * Remove the frame at the passed index
	 * and return it.
	 * @param int The index of the frame to remove.
	 * @return Frame The frame that was removed.
	 */
	public Frame RemoveFrame(int index) {
		initialized = false;
		Frame frame = items[index];
		items.RemoveAt(index);
		return frame;
	}
	
	/**
	 * Initialize the frame list.
	 * Automatically called on draw if the frame list has not
	 * been initialized.
	 */
	public void Init() {
		float currx = framePosition.x;
		float curry = framePosition.y;
		
		foreach (Frame frame in items) {
			frame.Init();
			frame.Position = new Vector2(currx, curry);
			
			if (orientation == FrameOrientation.VERTICAL)
				curry += frame.Boundary.height + framePadding;
			else
				currx += frame.Boundary.width + framePadding;
		}
		
		initialized = true;
	}
	
	#region IGUI implementation
	public void Draw () {
		if (!initialized)
			Init();
		
		foreach(Frame frame in items) {
			frame.Draw();
		}
	}
	#endregion
}