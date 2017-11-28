using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter  
{
	GameController gameController;
	static int _pointsNumber;
	static int _populationLength;
	static int _evolveNumber;
	static string _diffLevel;

	public Starter()
	{
		gameController = new GameController(_pointsNumber,_populationLength,_evolveNumber, _diffLevel);
	}

	public void Start () 
	{
		gameController.Start ();
	}	

	public void Update () 
	{
		gameController.Update ();
	}

	public void OnGUI ()
	{
		gameController.OnGUI ();
	}

	public static void setPointsNumber(int points)
	{
		_pointsNumber = points;
	}

	public static void setPopulationLength(int populationLength)
	{
		_populationLength = populationLength;
	}

	public static void setEvolveNumber(int evolveNumber)
	{
		_evolveNumber = evolveNumber;
	}

	public static void setDiffLevel(string diffLevel)
	{
		_diffLevel = diffLevel;
	}
}
