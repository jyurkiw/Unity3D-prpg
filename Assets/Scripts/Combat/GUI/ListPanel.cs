using UnityEngine;
using System.Collections;

/**
 * Operate a panel with a set list of labels.
 */
public class ListPanel : MonoBehaviour {
	public UILabel[] labelList;	///< Array of labels to be managed.
	
	private int displayedValues;
	
	/**
	 * Read-Only: Number of displayed values in the list panel.
	 */
	public int Displayed {
		get { return displayedValues; }
	}
	
	/**
	 * Attempt to add an item to display on the list panel.
	 * @param string The item to display.
	 * @return bool True if there was an empty label, false otherwise.
	 */
	public bool Add(string item) {
		if (displayedValues < labelList.Length) {
			labelList[displayedValues].text = item;
			displayedValues++;
			return true;
		} else {
			return false;
		}
	}
	
	/**
	 * Clear the contents of the label list.
	 */
	public void ClearList() {
		for(int x = 0; x < displayedValues; x++) {
			labelList[x].text = "";
		}
	}
	
	/**
	 * Clear the contents of the label at index, then collapse the list down.
	 * @param int The index to clear.
	 */
	public void ClearList(int index) {
		for (int x = index; x < displayedValues - 2; x++) {
			labelList[x].text = labelList[x + 1].text;
		}
		
		labelList[displayedValues - 1].text = "";
	}
	
	/**
	 * Get the index of the passed item in the label list.
	 * TODO: NEEDS TO BE TESTED DUE TO WHILE LOOP!!!!!
	 * @param string The item to search for.
	 * @return int The index of the item or -1 if the item is not found.
	 */
	public int IndexOf(string item) {
		int index = 0;
		bool found = false;
		
		while (!(found = (labelList[index].text.CompareTo(item) == 0)) && index++ < displayedValues);
		
		if (found)
			return index-1;
		else
			return -1;
	}
}
