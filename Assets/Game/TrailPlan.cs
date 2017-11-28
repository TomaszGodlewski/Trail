using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Collections;

public class TrailPlan
{
	// 
	private static ArrayList _trailPlan = new ArrayList();

	// 
	public static void addPoint(Point point)
	{
		_trailPlan.Add(point);
	}

	// 
	public static Point getPoint(int i)
	{
		return (Point)_trailPlan[i];
	}

	// 
	public static int getNumberOfPoints()
	{
		return _trailPlan.Count;
	}

	public static void  clearTrailPlan()
	{
		_trailPlan.Clear();
	}

	public static ArrayList getTrailPlan()
	{
		return _trailPlan;
	}

}
