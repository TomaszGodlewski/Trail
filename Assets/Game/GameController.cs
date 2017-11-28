using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Collections;

public class GameController  
{
	public static bool BRUTE_FORCE_MODE = false; //przełączanie gry na testowy tryb przegladu zupelnego
	public static Population bruteForcePopulation;//populacja w ktorej znajduja sie wszystkie permutacje szlakow w trybie przegladu zupelnego
	int permutationCounter;//licznik permutacji wykorzystywany w trybie przegladu zupelnego
	public static Trail bruteForceBestTrail;


	private int _population; //wielkosc populacji
	private static int _trailLength;//liczba punktow
	private int _numberOfEvo;//liczba ewolucji
	private static String _difficultyLevel;//opis poziomu trudnosci
	public static Button endTurnBtn;


	ArrayList aiTrail;
	double aiTrailDistance;
	TableController tab = new TableController(_trailLength,true,_difficultyLevel);
	public  Button[,] bts = new Button[10,10];


	public GameController(int trailLength, int population,int numberOfEvo ,String difficultyLevel)
	{
		_population = population;
		_trailLength = trailLength;
		_numberOfEvo = numberOfEvo;
		_difficultyLevel = difficultyLevel;

		if(BRUTE_FORCE_MODE)
		{
			bruteForcePopulation = new Population (3628800, false);
			permutationCounter = 0;
		}
	}

	// Use this for initialization
	public void Start () 
	{		
		endTurnListener ();
		tab.hiddenListener ();
		tab.trailsListeners ();
		tab.quitMenulListeners ();
		tab.addButtonsListeners();
		pointsDistribution (_trailLength);

		if(BRUTE_FORCE_MODE)
		{
			bruteForce();
			tab.showPlayerResult ();
		}

		bts = tab.startGame (TrailPlan.getTrailPlan());

		if(BRUTE_FORCE_MODE)
		{
			bruteForceBestTrail = bruteForcePopulation.getFittest ();
			tab.showResultTrail (bruteForceBestTrail.getTrail(), true);
		}

	}

	// Update is called once per frame
	public void Update () 
	{	
		
	} 	

	public void OnGUI()
	{
		
	}

	private void bruteForce()
	{
		int n = TrailPlan.getNumberOfPoints();
		Point[] temp = new Point[n];
		for (int i = 0; i < n; i++) 
		{
			temp [i] = (Point)TrailPlan.getPoint (i);
		}
		foreach (Point[] permutation in Permutation.getPermutations<Point>(temp)) 
		{			
			ArrayList temporary = new ArrayList ();
			for (int j = 0; j < permutation.Length; j++) 
			{
				temporary.Add (permutation[j]);
			}
			Trail tempTrail = new Trail (temporary);
			bruteForcePopulation.saveTrail(permutationCounter,tempTrail);
			permutationCounter++;
		}
	}
		
	private void pointsDistribution(int arrayLength)
	{
		TrailPlan.addPoint (new Point ());
		while (TrailPlan.getNumberOfPoints() < arrayLength) 
		{
			Point tempPoint = new Point ();

			if ((!isContain (TrailPlan.getTrailPlan(),tempPoint))&&UnityEngine.Random.value<=0.2) 
			{
				TrailPlan.addPoint (tempPoint);
			}
		}
	}

	private bool isContain(ArrayList pointsArray, Point point)
	{		

		for (int i=0; i < pointsArray.Count; i++) 
		{
			Point pointFromArray = (Point)pointsArray [i];
			if(((pointFromArray.getX()==point.getX())&&(pointFromArray.getY()==point.getY())))
			{				
				return true;	
			}
		}
		return false;
	}


	private void endTurnListener()///łączenie nazw przycisków wykorzystywanych w UI z przyciskami zdefiniowanymi w skrypcie, dodawania listenerów wykorzystywanych w obsłudze gry
	{
		endTurnBtn = GameObject.Find ("BtnendTurnReset").GetComponent<Button> (); 
		endTurnBtn.GetComponentInChildren<Text>().color = Color.black;
		endTurnBtn.GetComponentInChildren<Image> ().color = BRUTE_FORCE_MODE ? Color.yellow : Color.grey;
		endTurnBtn.enabled = BRUTE_FORCE_MODE ? true : false;
		endTurnBtn.onClick.AddListener (() => {startAI ();}); 
	}




	private void startAI()
	{	
		
		TableController.drawPlayerSwitch (false);
		TableController.drawAiSwitch (false);
		tab.movesCounterReset ();
		Debug.Log ("Uruchamianie AI");
		Population pop = new Population(_population, true);
		Debug.Log("Dlugosc najkrotszej drogi we wstepnej populacji: " + pop.getFittest().getDistance());
		var watch = System.Diagnostics.Stopwatch.StartNew();
		double minimumChecker = pop.getFittest().getDistance();
		double currentAiDistance = minimumChecker;
		for (int i = 0; i < _numberOfEvo; i++)
		{
			
			pop = Genetic.evolve(pop);
			currentAiDistance = pop.getFittest ().getDistance ();
			Debug.Log("Dlugosc najkrotszej drogi w populacji po ewolucji nr "+(i+1)+" : " + currentAiDistance);

			if (!BRUTE_FORCE_MODE) 
			{
				if ((i + 1) % 100 == 0) 
				{
					Debug.Log ("Checker!");
					if (minimumChecker == currentAiDistance) 
					{
						Debug.Log ("Minimum");
						break; 

					} else 
					{					
						minimumChecker = currentAiDistance;
					}
				}

				if (tab.getPlayerDistance () > currentAiDistance) {
					Debug.Log ("Cheater!");
					break;
				}
			}

		}
		watch.Stop();
		var elapsedMs = watch.ElapsedMilliseconds;
		Debug.Log("Czas przetwarzania: " + elapsedMs);
		Debug.Log("Dlugosc najkrotszego szlaku po zakonczeniu ewolucji: " + pop.getFittest().getDistance());
		Debug.Log("Najkotszy szlak SI: ");
		Debug.Log(pop.getFittest().ToString());
		if (BRUTE_FORCE_MODE) 
		{
			Debug.Log ("Przegląd zupełny liczba osobników: "+permutationCounter);
			Debug.Log ("Przegląd zupełny najkrótszy szlak: "+bruteForcePopulation.getFittest ().getDistance ());
		}

		aiTrail = pop.getFittest ().getTrail ();
		tab.setAiTrail (aiTrail);
		aiTrailDistance = pop.getFittest().getDistance();
		long elapsedTime =  elapsedMs;
		endAiComputation(elapsedTime);

	}



	private void endAiComputation(long elapsedTime)
	{
		for (int i = 0; i < aiTrail.Count; i++) 
		{
			Point temp = (Point)aiTrail [i];
			tab.aiBtnClick (temp.getX(), temp.getY());
		}

		tab.setAiTrailDistance (aiTrailDistance);
		tab.afterAiTurn();
		endTurnBtn.GetComponentInChildren<Text> ().color = BRUTE_FORCE_MODE ? Color.black : Color.white;
		endTurnBtn.enabled = BRUTE_FORCE_MODE ? false : true;
		endTurnBtn.GetComponentInChildren<Text> ().text = /*"Czas: "+elapsedTime+*/BRUTE_FORCE_MODE ? "Tryb przeglądu zupełnego! Zresetuj rozgrywkę ":/*"Czas: "+elapsedTime*/""  + theWinnerIs () + " Przejdź do podsumowania!";
	}

	private String theWinnerIs()
	{
		
		if (tab.getAiTrailDistance() < tab.getPlayerDistance ()) 
		{
			endTurnBtn.GetComponentInChildren<Image>().color = Color.red;
			saveResult (_difficultyLevel, "Failure");
			String result = "Przegrana!";
			tab.setLastGameResult (result);
			return result;
		}
		if (tab.getAiTrailDistance() > tab.getPlayerDistance ()) 
		{
			endTurnBtn.GetComponentInChildren<Image>().color = Color.green;
			saveResult (_difficultyLevel, "Win");
			String result = "Zwycięstwo!";
			tab.setLastGameResult (result);
			return result;
		} 
		else 
		{	
			saveResult (_difficultyLevel, "Draw");
			String result = "Remis!";
			tab.setLastGameResult (result);
			return result;
		}	
	}

	private void saveResult(string diffLvl, string result)
	{
		switch (diffLvl) 
		{
		case "Poczatkujacy":
			saveToStatistics ("easy", result);								
			break;
		case "Umiarkowany":
			saveToStatistics ("medium", result);
			break;
		case "Wymagajacy":
			saveToStatistics ("hard", result);
			break;

			
		}
	}

	private void saveToStatistics(string diff, string result)
	{
		int temp = 0;
		if (PlayerPrefs.HasKey (diff + result)) 
		{
			temp = PlayerPrefs.GetInt (diff + result);
			PlayerPrefs.SetInt (diff + result, temp + 1);
		} 
		else 
		{
			PlayerPrefs.SetInt (diff + result,1);
		}
	}



}
