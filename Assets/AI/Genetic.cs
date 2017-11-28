using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Collections;

class Genetic///Klasa implementuje proces ewolucji z operacjami genetycznymi oraz procesem selekcji.
{

	private const float MUTATION_RATE = 0.15f; //stała odpowiadająca za współczynnik mutacji wykorzystywany w funkcji implementującej operację mutacji
	private const int SELECT_SIZE= 10; //stała reprezentująca wielkość „podpopulacji” z której zostaje wybrany najlepszy osobnik do procesu krzyżowania
	private const bool ELITE = true; //stała reprezentująca możliwość zadziałania algorytmu genetycznego w trybie elitaryzmu


	public static Population evolve(Population pop) ///funkcja implementująca proces ewolucji populacji wywołując proces selekcji oraz dwie operacje genetyczne
	{
		Population newPop = new Population(pop.populationSize(), false);

		int elitism = 0;
		if (ELITE)
		{
			newPop.saveTrail(0, pop.getFittest());
			elitism = 1;
		}

		for (int i = elitism; i < newPop.populationSize(); i++)
		{

			Trail parent1 = selection(pop);
			Trail parent2 = selection(pop);
			Trail child = crossover(parent1, parent2);

			newPop.saveTrail(i, child);
		}

		for (int i = elitism; i < newPop.populationSize(); i++)
		{
			mutate(newPop.getTrail(i));
		}

		return newPop;
	}

	private static Trail selection(Population pop) /// funkcja implementująca proces selekcji wykorzystywany w operacji genetycznej krzyżowania

	{
		Population select = new Population(SELECT_SIZE, false);
		for (int i = 0; i < SELECT_SIZE; i++)
		{
			int randomId = (int) (safeRandom() * pop.populationSize());
			select.saveTrail(i, pop.getTrail(randomId));
		}
		return select.getFittest();
	}

	private static Trail crossover(Trail parent1, Trail parent2)//funkcja implementująca operację genetyczną krzyżowania
	{
		Trail child = new Trail();

		int startPos = (int) (safeRandom() * parent1.trailSize());
		int endPos = (int) (safeRandom() * parent1.trailSize());

		for (int i = 0; i < child.trailSize(); i++)
		{

			if (startPos < endPos && i > startPos && i < endPos)
			{
				child.setPoint(i, parent1.getPoint(i));
			}
			else if (startPos > endPos)
			{
				if (!(i < startPos && i > endPos))
				{
					child.setPoint(i, parent1.getPoint(i));
				}
			}
		}

		for (int i = 0; i < parent2.trailSize(); i++)
		{
			if (!child.containsPoint(parent2.getPoint(i)))
			{
				for (int j = 0; j < child.trailSize(); j++)
				{
					if (child.getPoint(j) == null)
					{
						child.setPoint(j, parent2.getPoint(i));
						break;
					}
				}
			}
		}
		return child;
	}

	private static void mutate(Trail trail)//procedura implementująca operację genetyczną mutacji.

	{
		for(int position1=0; position1 < trail.trailSize(); position1++)
		{
			
			if(UnityEngine.Random.value < MUTATION_RATE)
			{
				
				int position2 = (int) (trail.trailSize() * safeRandom());

				Point point1 = trail.getPoint(position1);
				Point point2 = trail.getPoint(position2);

				trail.setPoint(position2, point1);
				trail.setPoint(position1, point2);
			}
		}
	}

	private static float safeRandom()
	{
		float ans = UnityEngine.Random.value;
		ans = ans == 1.0f ? 0.9f : ans;
		return ans;
	}
}
