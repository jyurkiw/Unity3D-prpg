using UnityEngine;
using System.Collections.Generic;

/**
 * Draws a list of frame objects in an area.
 */
public class FrameList : IGUI {
	public FrameOrientation orientation; //< Orientation of the frames. Default: VERTICAL.
	public GUIStyle frameListGUIStyle;
	
	public float framePadding; //< Padding between frames.
	
	protected Vector2 framePosition;
	protected List<Frame> items;
	protected Rect listBoundaries;
	protected bool initialized;
	
	/*
	 * Read-only: Frame boundary rect.
	 * @return Rect Frame boundary.
	 */
	public Rect Boundary {
		get { return listBoundaries; }
	}
	
	/**
	 * The position of the list.
	 */
	public Vector2 Position {
		get {
			return Position;
		}
		set {
			initialized = false;
			framePosition = value;
		}
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
		listBoundaries = new Rect(x, y, 0, 0);
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
			
			if (orientation == FrameOrientation.VERTICAL) {
				curry += frame.Boundary.height + framePadding;
				listBoundaries.height += frame.Boundary.height + framePadding;
				listBoundaries.width = Mathf.Max(listBoundaries.width, frame.Boundary.width);
			} else {
				currx += frame.Boundary.width + framePadding;
				listBoundaries.height = Mathf.Max(listBoundaries.height, frame.Boundary.height);
				listBoundaries.width += frame.Boundary.width + framePadding;
			}
		}
		
		initialized = true;
	}
	
	#region IGUI implementation
	public void Draw () {
		if (!initialized)
			Init();
		
		foreach(Frame frame in items) {
			if (frame is ButtonMenu) {
				ButtonMenu buttonMenu = (ButtonMenu)frame;
				buttonMenu.Draw();
			} else
				frame.Draw();
		}
	}
	#endregion
}