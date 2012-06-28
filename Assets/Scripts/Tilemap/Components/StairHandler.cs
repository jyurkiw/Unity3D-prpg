using UnityEngine;
using System.Collections;

public class StairHandler : MonoBehaviour {
	public TileType tileType;
	public bool enterProtected;
	
	public StairHandler() {
		enterProtected = false;
	}
	
	public void OnTriggerEnter(Collider other) {
		if (!enterProtected)
			StartCoroutine(TriggerStair());
	}
	
	public void OnTriggerExit(Collider other) {
		if (enterProtected) {
			enterProtected = false;
		}
	}
	
	public IEnumerator TriggerStair() {
		CornerstoneManager cornerstoneManager = GameObject.FindGameObjectWithTag("CornerstoneManager").GetComponent<CornerstoneManager>();
		RPGController player = GameObject.FindGameObjectWithTag("GameController").GetComponent<RPGController>();
		
		while(!player.IsStopped)
			yield return new WaitForEndOfFrame();
		
		player.IsProtected = true;
		
		if (tileType == TileType.DOWNSTAIR)
			cornerstoneManager.AdvanceOneLevel();
		else if (tileType == TileType.UPSTAIR)
			cornerstoneManager.ReturnOneLevel();
		
		player.IsProtected = false;
	}
}
