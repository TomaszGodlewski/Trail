using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Collections;

public class Point 
{
	int _x;
	int _y;

	public Point()
	{
		
		_x = UnityEngine.Random.Range(0,10);
		_y = UnityEngine.Random.Range(0,10);
	}

	// 
	public Point(int x, int y)
	{
		_x = x;
		_y = y;

	}

	// 
	public int getX()
	{
		return _x;
	}

	// 
	public int getY()
	{
		return _y;
	}

	// 
	public double distanceTo(Point point)
	{
		int xDistance = Math.Abs(getX() - point.getX());
		int yDistance = Math.Abs(getY() - point.getY());
		double distance = Math.Sqrt( (xDistance*xDistance) + (yDistance*yDistance) );

		return distance;
	}

	override public String ToString()
	{
		return "("+getX()+","+getY()+")";
	}

}
