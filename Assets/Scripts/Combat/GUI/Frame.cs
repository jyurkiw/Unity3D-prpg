using UnityEngine;
using System.Collections.Generic;

/**
 * Contains a number of labels that must be rendered, one below another.
 */
public class Frame : IGUI {
	public FrameOrientation orientation;
	public GUIStyle frameGUIStyle;
	
	private Vector2 framePosition;
	protected List<string> items;
	protected bool initialized;
	protected List<ContentRectPair> processedItems;
	protected Rect frameBoundaries;
	
	/**
	 * The position of the frame.
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
	 * Used to override the automatically generated Frame Boundaries value.
	 */
	public Rect BoundaryOverride {
		set { frameBoundaries = value; }
	}
	
	/*
	 * Read-only: Frame boundary rect.
	 * @return Rect Frame boundary.
	 */
	public Rect Boundary {
		get { return frameBoundaries; }
	}
	
	/**
	 * Create a PRPG GUI Frame.
	 * A PRPG GUI Frame is a UI element that lists string information
	 * in labeles in a box, one after the other.
	 * @param GUIStyle The game's current GUI Style.
	 * @param float The frame's x position.
	 * @param float The frame's y position.
	 */
	public Frame(GUIStyle frameGUIStyle, float x, float y) {
		initialized = false;
		framePosition = new Vector2(x, y);
		this.frameGUIStyle = frameGUIStyle;
		orientation = FrameOrientation.VERTICAL;
		items = new List<string>();
		processedItems = new List<ContentRectPair>();
	}
	
	/**
	 * Add an item to the frame for rendering.
	 * @param item The text to render.
	 */
	public void AddFrameItem(string item) {
		items.Add(item);
		initialized = false;
	}
	
	/**
	 * Remove a specific item from the frame.
	 * @param item The item to remove.
	 * @return bool True if successful, false otherwise.
	 */
	public bool RemoveFrameItem(string item) {
		initialized = false;
		return items.Remove(item);
	}
	
	/**
	 * Initialize the Frame.
	 * Is automatically called on Draw().
	 */
	public void Init() {
		Vector2 dim, pos = Vector2.zero, max = Vector2.zero;
		GUIContent content;
		processedItems.Clear();
		
		foreach(string item in items) {
			content = new GUIContent(item);
			dim = frameGUIStyle.CalcSize(content);
			max = Vector2.Max(dim, max);
			
			processedItems.Add(new ContentRectPair(content, new Rect(pos.x, pos.y, dim.x, dim.y)));
			
			if (orientation == FrameOrientation.VERTICAL)
				pos.y += dim.y;
			else pos.x += dim.x;
		}
		
		pos = Vector2.Max(pos, max);
		frameBoundaries = new Rect(framePosition.x, framePosition.y, pos.x, pos.y);
		
		initialized = true;
	}
	
	#region IGUI implementation
	public void Draw () {
		if (!initialized) Init();
		
		GUI.Box(frameBoundaries, new GUIContent());
		GUI.BeginGroup(frameBoundaries, new GUIContent());
		foreach(ContentRectPair pair in processedItems)
			GUI.Label(pair.rect, pair.content);
		GUI.EndGroup();
	}
	#endregion
	
	protected class ContentRectPair {
		public GUIContent content;
		public Rect rect;
		
		public ContentRectPair(GUIContent content, Rect rect) {
			this.content = content;
			this.rect = rect;
		}
	}
}