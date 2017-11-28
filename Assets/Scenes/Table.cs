using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour 
{
	
	Starter starter = new Starter();
	// Use this for initialization
	void Start () 
	{
		starter.Start ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		starter.Update ();
		
	}

	void OnGUI()
	{
		starter.OnGUI ();
		if (TableController.drawPlayerSwitchStatus ()) 
		{
			TableController.drawTrail (true);

		} 
		if(TableController.drawAiSwitchStatus ())
		{
			TableController.drawTrail (false);

		}
	}
}
