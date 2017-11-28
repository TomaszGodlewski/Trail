using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Collections;

public class Trail
{

	private ArrayList _trail; 

	private double _fitness = 0.0;
	private double _distance = 0.0;


	public Trail()
	{
		_trail = new ArrayList();
		for (int i = 0; i < TrailPlan.getNumberOfPoints(); i++)
		{
			_trail.Add(null);
		}
	}

	public Trail(ArrayList trail)
	{
		_trail = trail;
	}


	public void generateIndividual()
	{
		for (int i = 0; i < TrailPlan.getNumberOfPoints(); i++)
		{
			setPoint(i, TrailPlan.getPoint(i));
		}
		_trail = ShuffleList(_trail);
	}


	public Point getPoint(int i)
	{
		return (Point)_trail[i];
	}


	public void setPoint(int i, Point point)
	{
		_trail.RemoveAt (i);
		_trail.Insert (i, point);

		_fitness = 0.0;
		_distance = 0.0;
	}


	public double getFitness()
	{
		if (_fitness == 0.0) 
		{
			_fitness = 1/(double)getDistance();
		}
		return _fitness;
	}


	public double getDistance()
	{

		if (_distance == 0.0)
		{
			double tripDistance = 0.0;
			for (int i=0; i < trailSize(); i++)
			{
				Point fromPoint = getPoint(i);
				Point toPoint;
				if(i+1 < trailSize())
				{
					toPoint = getPoint(i+1);
				}
				else
				{
					break;
				}

				tripDistance += fromPoint.distanceTo(toPoint);
			}
			_distance = tripDistance;

		}
		return _distance;
	}


	public int trailSize()
	{
		return _trail.Count;
	}


	public bool containsPoint(Point point)
	{
		return _trail.Contains(point);
	}

	private ArrayList ShuffleList(ArrayList inputList)
	{
		ArrayList randomList = new ArrayList();

		System.Random r = new System.Random();
		int randomIndex = 0;
		int k = 0;
		while (inputList.Count > 0)
		{
			randomIndex = r.Next(0, inputList.Count); //
			k = r.Next(0,100);
			if (k % 2 == 0)
			{
				randomList.Add(inputList[randomIndex]); //
				inputList.RemoveAt(randomIndex); //
			}

		}

		return randomList; //
	}

	public ArrayList getTrail()
	{
		return _trail;
	}


	override public String ToString()
	{
		String geneString = "[START]->";
		for (int i = 0; i < trailSize(); i++)
		{
			geneString += getPoint(i)+"->";
		}
		geneString +="[STOP]";
		return geneString;
	}
}

