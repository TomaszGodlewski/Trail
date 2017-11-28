using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summary : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		//Screen.orientation = ScreenOrientation.LandscapeLeft;
		TableController tab = new TableController();
		TableController.clearVectList ();
		tab.showAllTrails ();
		tab.quitMenulListeners ();
		tab.trailsListeners ();
		tab.showAiResult ();
		tab.showPlayerResult ();
		tab.hiddenListener ();
		tab.statistics ();
		tab.resetAllTrails ();
		tab.showLastGameResult ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	void OnGUI()
	{		
		TableController.drawTrail (true);
		TableController.drawTrail (false);
	}
}
