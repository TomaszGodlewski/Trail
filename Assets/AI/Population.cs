using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population///definiuje populację oraz implementuje operacje wykonywane na populacji.
{

	private Trail[] _trails;


	public Population(int populationSize, bool initialise)
	{
		_trails = new Trail[populationSize];
		if (initialise)
		{

			for (int i = 0; i < populationSize; i++)
			{
				Trail newTrail = new Trail();
				newTrail.generateIndividual();
				saveTrail(i, newTrail);
			}
		}
	}




	public void saveTrail(int i, Trail trail)
	{
		_trails[i] = trail;
	}

	public Trail getTrail(int i)
	{
		return _trails[i];
	}

	public Trail getFittest()
	{
		Trail fittest = _trails[0];
		for (int i = 1; i < populationSize(); i++)
		{
			if (fittest.getFitness() <= getTrail(i).getFitness())
			{
				fittest = getTrail(i);
			}
		}
		return fittest;
	}


	public int populationSize()
	{
		return _trails.Length;
	}


}
