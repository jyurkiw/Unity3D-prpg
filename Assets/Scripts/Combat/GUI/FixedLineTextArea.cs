using UnityEngine;
using System.Collections.Generic;

/**
 * A text area component.
 * This component takes text, scrolls text from top to bottom,
 * and automatically cuts the log down to the maximum viewable
 * lines.
 */
public class FixedLineTextArea : IGUI {
	private List<ContentAreaPair> textList;
	private Rect areaBoundaries;
	private bool changed;
	private GUIStyle textAreaGUIStyle;
	
	//calculated data
	private GUIContent displayContent;
	private int linesDisplayed;
	
	/**
	 * Get or set the area Boundaries.
	 */
	public Rect Boundaries {
		get { return areaBoundaries; }
		set { areaBoundaries = value; }
	}
	
	/**
	 * Get or set the number of lines this text area can display.
	 */
	public int DisplayLines {
		get { return (int)(areaBoundaries.height / textAreaGUIStyle.lineHeight); }
		set {
			areaBoundaries.height = textAreaGUIStyle.lineHeight * value;
			changed = true;
		}
	}
	
	/**
	 * * Create a FixedLineTextArea object with the passed guistyle and
	 * boundaries.
	 * @param GUIStyle The guiStyle to use in the text area.
	 * @param float The distance from the top of the screen.
	 * @param float The distance from the left side of the screen.
	 * @param float The height of the area.
	 * @param float The width of the area.
	 */
	public FixedLineTextArea(GUIStyle textAreaGUIStyle, float x, float y, float height, float width) {
		textList = new List<ContentAreaPair>();
		areaBoundaries = new Rect(x, y, width, height);
		this.textAreaGUIStyle = textAreaGUIStyle;
		displayContent = new GUIContent();
		linesDisplayed = 0;
	}
	
	/**
	 * Create a FixedLineTextArea object with the passed guistyle and
	 * boundaries.
	 * @param GUIStyle The guiStyle to use in the text area.
	 * @param Rect The boundaries to use for positioning.
	 */
	public FixedLineTextArea(GUIStyle textAreaGUIStyle, Rect boundaries) {
		textList = new List<ContentAreaPair>();
		areaBoundaries = boundaries;
		this.textAreaGUIStyle = textAreaGUIStyle;
		displayContent = new GUIContent();
		linesDisplayed = 0;
	}
	
	/**
	 * Add a line of text to the text area.
	 */
	public void AddLine(string text) {
		textList.Add(new ContentAreaPair(textAreaGUIStyle, text, areaBoundaries.width));
		changed = true;
	}
	
	#region IGUI implementation
	public void Draw () {
		GUI.Box(areaBoundaries, DisplayContent, textAreaGUIStyle);
	}
	#endregion
	
	//Cut the display list and text list down to size.
	private void PruneDisplayLines() {
		int reverseIndex = textList.Count;
		int linesRemaining = DisplayLines;
		
		for (int x = textList.Count - 1; x >= 0; x--) {
			if(linesRemaining > 0) reverseIndex--;
			linesRemaining -= textList[x].Lines;
			
			if (linesRemaining <= 0)
				break;
		}
		
		if (reverseIndex > 0)
			textList.RemoveRange(0, reverseIndex);
	}
	
	/**
	 * Read-only display content property.
	 * If change has occured, rebuild
	 * the display string before returning
	 * it.
	 */
	private GUIContent DisplayContent {
		get {
			if (changed) {
				PruneDisplayLines();
				string text = textList[0].text;
				for (int x = 1; x < textList.Count; x++)
					text += "\n" + textList[x].text;
				
				displayContent.text = text;
			}
			return displayContent;
		}
	}
	
	
	/**
	 * Pair class for text area content with additional functionality.
	 */
	protected class ContentAreaPair {
		public string text;
		public float height;
		
		protected int lines;
		
		protected GUIStyle textAreaGUIStyle;
		
		public int Lines {
			get { return lines; }
		}
		
		public bool Multiline {
			get { return height > textAreaGUIStyle.lineHeight; }
		}
		
		public ContentAreaPair(GUIStyle textAreaGUIStyle, string text, float width) {
			this.text = text;
			this.height = textAreaGUIStyle.CalcHeight(new GUIContent(text), width);
			this.textAreaGUIStyle = textAreaGUIStyle;
			lines = (int)(height / textAreaGUIStyle.lineHeight);
		}
	}
}