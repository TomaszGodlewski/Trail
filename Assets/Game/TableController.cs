using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Collections;

public class TableController 
{
	const int TABLE_LENGTH = 10;//Wielkosc tablicy gry 
	int _movesCount; //licznik ruchow, wyswietlany oraz inkrementowany po kazdym nacisnieciu punktu na tablicy gry

	int _numberOfPoints; //liczba punktow przez ktore nalezy przeprowadzic szlak
	bool _isGameMode = false; //zmienna od ktorej zalezy rodzaj wyswietlanych indormacji na scenie rozgrywki(true) lub sceny podsumowania(false)

	static ArrayList _playerTrail;//lista przechowujaca szlak gracza
	static ArrayList _aiTrail;//lista przechowujaca szlak przeciwnika SI
	static double _aiTrailDistance;//zmienna przechowujaca dlugosc szlaku przeciwnika SI
	static String _difficultyLevel;//zmienna przechowujaca nazwe poziomu trudnosci (Poczatkujacy, Umiarkowany, Wymagajacy)
	static String _lastGameResult;//zmienna przechowujaca wynik ostatniej rozgrywki (Wygrana, Przegrana, Remis), wykorzystywana dla napisu na scenie podsumowania

	Button[,] bBtns; //tablica przyciskow na ktorej dokonywane sa wszystkie operacje
	public Button resetBtn; //przycisk resetu na scenie rozgrywki
	public Button endTurnBtn; //przycisk stanu na scenie rozgywki
	public Button playerTrailBtn; //przycisk szlaku gracza
	public Button aiTrailBtn;//przycisk szlaku przeciwnika SI
	public Button quitBtn; //przycisk wyscia z gry 
	public Button mainMenuBtn; //przycisk powrotu do menu glownego

	static bool _drawPlayerSwitch; //flaga sterujaca wywolywaniem funkcji wykreslajacej szlak gracza
	static bool _drawAiSwitch = false; //flaga sterujaca wywolywaniem funkcji wykreslajacej szlak przeciwnika SI

	//definicja przyciskow pol na tablicy gry
	public Button btn09, btn19, btn29, btn39, btn49, btn59, btn69, btn79, btn89, btn99;
	public Button btn08, btn18, btn28, btn38, btn48, btn58, btn68, btn78, btn88, btn98;
	public Button btn07, btn17, btn27, btn37, btn47, btn57, btn67, btn77, btn87, btn97;
	public Button btn06, btn16, btn26, btn36, btn46, btn56, btn66, btn76, btn86, btn96;
	public Button btn05, btn15, btn25, btn35, btn45, btn55, btn65, btn75, btn85, btn95;
	public Button btn04, btn14, btn24, btn34, btn44, btn54, btn64, btn74, btn84, btn94;
	public Button btn03, btn13, btn23, btn33, btn43, btn53, btn63, btn73, btn83, btn93;
	public Button btn02, btn12, btn22, btn32, btn42, btn52, btn62, btn72, btn82, btn92;
	public Button btn01, btn11, btn21, btn31, btn41, btn51, btn61, btn71, btn81, btn91;
	public Button btn00, btn10, btn20, btn30, btn40, btn50, btn60, btn70, btn80, btn90;

	public static List<Vector2> _playerVectList = new List <Vector2>(); //Lista przechowujaca wektory wspolrzednych swiata dla szlaku gracza, wykorzystywana przy wykreslaniu szlaku
	public static List<Vector2> _aiVectList = new List <Vector2>();//Lista przechowujaca wektory wspolrzednych swiata dla szlaku przeciwnika SI, wykorzystywana przy wykreslaniu szlaku
	public static List<Vector2> vectList = new List <Vector2>();//Lista z biezacym szlakiem gracza lub przeciwnika SI 

	public TableController()
	{
		_movesCount = 0;
		createButtonsArrays ();
	}



	public TableController(int numberOfPoints, bool isGameMode, String difficultyLevel)
	{
		_isGameMode = true;
		_numberOfPoints = numberOfPoints;
		_movesCount = 0;
		_playerTrail = new ArrayList ();
		_aiTrail = new ArrayList ();
		_aiTrailDistance = 0;
		createButtonsArrays ();
		_difficultyLevel = difficultyLevel;
		_lastGameResult = "";
		vectList.Clear ();
		_playerVectList.Clear ();
		_aiVectList.Clear ();
		_drawPlayerSwitch = false;

	}

	private void createButtonsArrays()///Tworzenie tablicy przycisków
	{	

		bBtns = new Button[10,10];
		bBtns[0,9] = btn09; bBtns [1, 9] = btn19; bBtns [2, 9] = btn29; bBtns [3, 9] = btn39; bBtns [4, 9] = btn49; bBtns [5, 9] = btn59; bBtns [6, 9] = btn69; bBtns [7, 9] = btn79; bBtns [8, 9] = btn89; bBtns[9,9] =btn99;
		bBtns[0,8] = btn08; bBtns [1, 8] = btn18; bBtns [2, 8] = btn28; bBtns [3, 8] = btn38; bBtns [4, 8] = btn48; bBtns [5, 8] = btn58; bBtns [6, 8] = btn68; bBtns [7, 8] = btn78; bBtns [8, 8] = btn88; bBtns[9,8] =btn98;
		bBtns[0,7] = btn07; bBtns [1, 7] = btn17; bBtns [2, 7] = btn27; bBtns [3, 7] = btn37; bBtns [4, 7] = btn47; bBtns [5, 7] = btn57; bBtns [6, 7] = btn67; bBtns [7, 7] = btn77; bBtns [8, 7] = btn87; bBtns[9,7] =btn97;
		bBtns[0,6] = btn06; bBtns [1, 6] = btn16; bBtns [2, 6] = btn26; bBtns [3, 6] = btn36; bBtns [4, 6] = btn46; bBtns [5, 6] = btn56; bBtns [6, 6] = btn66; bBtns [7, 6] = btn76; bBtns [8, 6] = btn86; bBtns[9,6] =btn96;
		bBtns[0,5] = btn05; bBtns [1, 5] = btn15; bBtns [2, 5] = btn25; bBtns [3, 5] = btn35; bBtns [4, 5] = btn45; bBtns [5, 5] = btn55; bBtns [6, 5] = btn65; bBtns [7, 5] = btn75; bBtns [8, 5] = btn85; bBtns[9,5] =btn95;
		bBtns[0,4] = btn04; bBtns [1, 4] = btn14; bBtns [2, 4] = btn24; bBtns [3, 4] = btn34; bBtns [4, 4] = btn44; bBtns [5, 4] = btn54; bBtns [6, 4] = btn64; bBtns [7, 4] = btn74; bBtns [8, 4] = btn84; bBtns[9,4] =btn94;
		bBtns[0,3] = btn03; bBtns [1, 3] = btn13; bBtns [2, 3] = btn23; bBtns [3, 3] = btn33; bBtns [4, 3] = btn43; bBtns [5, 3] = btn53; bBtns [6, 3] = btn63; bBtns [7, 3] = btn73; bBtns [8, 3] = btn83; bBtns[9,3] =btn93;
		bBtns[0,2] = btn02; bBtns [1, 2] = btn12; bBtns [2, 2] = btn22; bBtns [3, 2] = btn32; bBtns [4, 2] = btn42; bBtns [5, 2] = btn52; bBtns [6, 2] = btn62; bBtns [7, 2] = btn72; bBtns [8, 2] = btn82; bBtns[9,2] =btn92;
		bBtns[0,1] = btn01; bBtns [1, 1] = btn11; bBtns [2, 1] = btn21; bBtns [3, 1] = btn31; bBtns [4, 1] = btn41; bBtns [5, 1] = btn51; bBtns [6, 1] = btn61; bBtns [7, 1] = btn71; bBtns [8, 1] = btn81; bBtns[9,1] =btn91;	
		bBtns[0,0] = btn00; bBtns [1, 0] = btn10; bBtns [2, 0] = btn20; bBtns [3, 0] = btn30; bBtns [4, 0] = btn40; bBtns [5, 0] = btn50; bBtns [6, 0] = btn60; bBtns [7, 0] = btn70; bBtns [8, 0] = btn80; bBtns[9,0] =btn90;

	}

	public void hiddenListener()///łączenie nazw przycisków wykorzystywanych w UI z przyciskami zdefiniowanymi w skrypcie, dodawania listenerów wykorzystywanych w obsłudze gry
	{
		resetBtn = GameObject.Find ("BtnReset").GetComponent<Button> (); //wyszukiwanie ukrytego przycisku "Reset"
		if (_isGameMode) 
		{			
			resetBtn.GetComponent<Image> ().color = new Color32 (216, 152, 121, 0);
			resetBtn.GetComponentInChildren<Text> ().color = GameController.BRUTE_FORCE_MODE ? Color.yellow : Color.red;
			resetBtn.GetComponentInChildren<Text> ().text = GameController.BRUTE_FORCE_MODE ? "Tryb przeglądu zupełnego!" : "" + _difficultyLevel + " przeciwnik SI";

			 //dodanie listenera do przycisku "Reset"
		}
		else 
		{
			Button difficultLevel;
			difficultLevel = GameObject.Find ("DifficultLevelButton").GetComponent<Button> ();//wyszukiwanie tekstu na scenie podsumowania
			difficultLevel.GetComponent<Image> ().color = new Color32 (216, 152, 121, 0);
			difficultLevel.GetComponentInChildren<Text> ().color = GameController.BRUTE_FORCE_MODE ? Color.yellow : Color.red;
			difficultLevel.GetComponentInChildren<Text> ().text = GameController.BRUTE_FORCE_MODE ? "Tryb przeglądu zupełnego!" :"" + _difficultyLevel + " przeciwnik SI";
		}
		resetBtn.onClick.AddListener (() => {reset();});
	}

	private void reset()///procedura resetująca rozdanie
	{	
		TrailPlan.clearTrailPlan ();
		if(_isGameMode)
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
		else
		{
			SceneManager.LoadScene ("table");
		}
	}

	private void resetTurnListener(Button endTurnBtn)///łączenie nazw przycisków wykorzystywanych w UI z przyciskami zdefiniowanymi w skrypcie, dodawania listenerów wykorzystywanych w obsłudze gry
	{
		endTurnBtn.enabled = true;
		endTurnBtn.onClick.RemoveAllListeners();
		endTurnBtn.onClick.AddListener (() => {	SceneManager.LoadScene ("summary");}); //dodanie listenera do przycisku "Jeszcze raz"
	}



	public void trailsListeners()///łączenie nazw przycisków wykorzystywanych w UI z przyciskami zdefiniowanymi w skrypcie, dodawania listenerów wykorzystywanych w obsłudze gry
	{

		playerTrailBtn = GameObject.Find ("BtnPlayerTrail").GetComponent<Button> (); //wyszukiwanie przycisku szlaku gracza
		playerTrailBtn.GetComponentInChildren<Image>().color = Color.grey;
		playerTrailBtn.enabled = GameController.BRUTE_FORCE_MODE ? true : false;
		if(GameController.BRUTE_FORCE_MODE)
		{
			playerTrailBtn.onClick.AddListener (()=> {clearVectList();showResultTrail (GameController.bruteForceBestTrail.getTrail(), true);});
		}
		else
		{
			playerTrailBtn.onClick.AddListener (()=> {clearVectList(); showResultTrail(getPlayerTrail(),true); }); //dodanie listenera do przycisku "Jeszcze raz"
		}
		aiTrailBtn = GameObject.Find ("BtnAiTrail").GetComponent<Button> (); //wyszukiwanie przycisku szlaku przeciwnika SI
		aiTrailBtn.GetComponentInChildren<Image>().color = Color.grey;
		aiTrailBtn.enabled = false;
		aiTrailBtn.onClick.AddListener (()=> {clearVectList(); showResultTrail(_aiTrail,false); });

	}

	public void quitMenulListeners()///łączenie nazw przycisków wykorzystywanych w UI z przyciskami zdefiniowanymi w skrypcie, dodawania listenerów wykorzystywanych w obsłudze gry
	{

		quitBtn = GameObject.Find ("BtnQuitGame").GetComponent<Button> (); //wyszukiwanie przycisku "Zakończ grę"
		quitBtn.enabled = true;


		mainMenuBtn = GameObject.Find ("BtnMainMenu").GetComponent<Button> (); //wyszukiwanie przycisku "Menu główne"
		mainMenuBtn.enabled = true;
		mainMenuBtn.onClick.AddListener (() => {
			reset ();
			SceneManager.LoadScene ("mainMenu");
		});
		if (_isGameMode) {
			quitBtn.onClick.AddListener (() => {
				reset();
			});
		} 
		else 
		{
			quitBtn.onClick.AddListener (() => {
				Application.Quit ();
			});
		}
	}

	public void addButtonsListeners()///powiązanie nazw przycisków wykorzystywanych w UI z przyciskami zdefiniowanymi w skrypcie, dodawania listenerów wykorzystywanych w grze przycisków
	{
		GameController.endTurnBtn.GetComponentInChildren<Text>().text = GameController.BRUTE_FORCE_MODE ? "Tryb przeglądu zupełnego! Rozpocznij ture SI" : "Rozpocznij swój szlak";
		for (int x = 0; x < TABLE_LENGTH; x++)
		{
			//Text btnText;
			for (int y = 0; y < TABLE_LENGTH; y++) 
			{
				int coordX = x;
				int coordY = y;
				findButton("Btn",x,y).onClick.AddListener (() => {playerBtnClick (coordX,coordY);});
			}
		}
	}

	private Button findButton(String buttonsName, int x, int y) //blokada niewylosowanych punktow na tablicy gry
	{
		
		bBtns [x,y] = GameObject.Find (buttonsName + x +y ).GetComponent<Button> ();
		bBtns [x,y].GetComponentInChildren<Image>().color = Color.grey;
		bBtns [x,y].enabled = false;
		return bBtns [x, y];
	}




	public void showAllTrails()//funkcja wyswietlajaca szlaki gracza i przeciwnika SI na scenie podsumowania
	{
		for (int x = 0; x < TABLE_LENGTH; x++)
		{			
			for (int y = 0; y < TABLE_LENGTH; y++) 
			{
				findButton("Btnn",x,y);
			}
		}
		showResultTrail(_aiTrail,false);

		for (int x = 0; x < TABLE_LENGTH; x++)
		{			
			for (int y = 0; y < TABLE_LENGTH; y++) 
			{
				findButton("Btn",x,y);
			}
		}
		showResultTrail(_playerTrail,true);

	}

	public void statistics()//obsluga przycisku statystyki na scenie podsumowania
	{
		endTurnBtn = GameObject.Find ("BtnendTurnReset").GetComponent<Button> ();
		endTurnBtn.onClick.AddListener (() => {reset(); SceneManager.LoadScene ("statistics");});

	}



	private void playerBtnClick (int x, int y)//funkcja obslugujaca wybrany przez gracza punkt
	{
		
		if (_playerTrail.Count < _numberOfPoints) 
		{			
			_playerTrail.Add (new Point (x, y));
			btnClicked (x, y, true);
			GameController.endTurnBtn.GetComponentInChildren<Text>().text = "Zakończ ture za "+(_numberOfPoints - getMovesCount())+" ruchów";
			if (_playerTrail.Count == _numberOfPoints) 
			{				
				showPlayerResult();
				playerTrailBtn.enabled = true;
				GameController.endTurnBtn.GetComponentInChildren<Image>().color = new Color32 (0x51, 0x6F, 0xFF, 0xFF);
				GameController.endTurnBtn.GetComponentInChildren<Text> ().color = Color.white;
				GameController.endTurnBtn.GetComponentInChildren<Text> ().text = "Zakończ ture!";
				GameController.endTurnBtn.enabled = true;
			}

		} 

	}

	public void aiBtnClick (int x, int y)//funkcja obslugujaca wybrany przez przeciwnika SI punkt
	{

		btnClicked (x, y, false);
	}



	private void btnClicked (int x, int y, bool isPlayer)//funkcja wspolna do obslugi wybranych punktow przez gracza i przeciwnika SI
	{
		Vector2 temp;
		temp.x = (int)bBtns [x, y].transform.position.x;
		temp.y = Screen.height - bBtns [x, y].transform.position.y;

		if (isPlayer) 
		{			
			_playerVectList.Add (temp);
		} 
		else 
		{
			_aiVectList.Add (temp);
		}

		_movesCount++;	
		bBtns [x,y].GetComponent<Image>().color = new Color32(216,152,121,0);
		if (isPlayer) 
		{	
			drawAiSwitch (false);
			bBtns [x, y].GetComponentInChildren<Text> ().color = GameController.BRUTE_FORCE_MODE ? Color.yellow : Color.green;
			drawPlayerSwitch (true);
		} 
		else 
		{	
			drawPlayerSwitch (false);
			bBtns [x, y].GetComponentInChildren<Text> ().color = Color.red;
			TableController.drawAiSwitch (true);
		}

		bBtns [x,y].GetComponentInChildren<Text>().text = ""+_movesCount;
		bBtns [x,y].GetComponentInChildren<Text>().fontSize = _isGameMode? 80 : 50;
		bBtns [x,y].enabled = false;


	}



	public void showLastGameResult()//obsluga wyswietlania wyniku na scenie podsumowania
	{
		Button lastgameResult;
		lastgameResult = GameObject.Find ("LastGameResultButton").GetComponent<Button> ();
		lastgameResult.GetComponent<Image> ().color = new Color32 (216, 152, 121, 0);
		switch (_lastGameResult) 
		{
		case "Przegrana!":
			lastgameResult.GetComponentInChildren<Text> ().color = Color.red;
			lastgameResult.GetComponentInChildren<Text> ().text = "" + _lastGameResult;
			break;
		case "Zwycięstwo!":
			lastgameResult.GetComponentInChildren<Text> ().color = Color.green;
			lastgameResult.GetComponentInChildren<Text> ().text = "" + _lastGameResult;
			break;
		case "Remis!":
			lastgameResult.GetComponentInChildren<Text> ().color = Color.blue;
			lastgameResult.GetComponentInChildren<Text> ().text = "" + _lastGameResult;
			break;
		}
	}

	public void afterAiTurn()//obsluga przyciskow po zakonczeniu drugiej tury 
	{	

		GameController.endTurnBtn.enabled = false;
		showAiResult ();
		aiTrailBtn.enabled = true;
		resetTurnListener (GameController.endTurnBtn);
	}

	public void showAiResult()//obsluga przycisku szlaku przeciwnika SI
	{
		aiTrailBtn.GetComponentInChildren<Image> ().color = Color.red;
		aiTrailBtn.GetComponentInChildren<Text> ().color = Color.black;
		aiTrailBtn.GetComponentInChildren<Text> ().text = "Długość szlaku SI: " + getAiTrailDistance ();
	}

	public void showPlayerResult()//obsluga przycisku szlaku gracza
	{
		playerTrailBtn.GetComponentInChildren<Image> ().color = GameController.BRUTE_FORCE_MODE ? Color.yellow : Color.green;
		playerTrailBtn.GetComponentInChildren<Text> ().color = Color.black;
		playerTrailBtn.GetComponentInChildren<Text> ().text = GameController.BRUTE_FORCE_MODE ? "Najkrótsza długość "+Math.Round(GameController.bruteForcePopulation.getFittest ().getDistance (),2) : "Długość szlaku gracza: " + getPlayerDistance ();
	}


	public void showResultTrail (ArrayList resultTrail, bool isPlayer)//wykreslanie szlakow gracza i przeciwnika si po nacisnieciu przyciskow szlaku
	{
		_movesCount = 0;
		for (int i = 0; i < resultTrail.Count; i++) 
		{
			Point pointFromArray = (Point)resultTrail [i];
			btnClicked (pointFromArray.getX (), pointFromArray.getY (), isPlayer);
		}
	}

	public Button[,] startGame(ArrayList randomPoints)//obsluga wylosowanych punktow na tablicy gry
	{
		int arrayLength = randomPoints.Count;
		for (int i = 0; i < arrayLength; i++) 
		{
			Point temp = (Point)randomPoints [i];
			bBtns[temp.getX (),temp.getY ()].GetComponentInChildren<Image>().color = Color.blue;
			bBtns[temp.getX (),temp.getY ()].enabled = GameController.BRUTE_FORCE_MODE ? false:true;
		}
		return bBtns;
	}

	public static void drawTrail (bool isPlayer)//wykreslanie szlaku pomiedzy dwoma wybranymi punktami
	{

		vectList = isPlayer ? _playerVectList : _aiVectList;

		for (int i = 0; i < vectList.Count; i++) 
		{
			if ((i + 1) < vectList.Count) 
			{
				Draw.drawLine (vectList [i+1], vectList [i], (isPlayer ?  (GameController.BRUTE_FORCE_MODE ? Color.yellow : Color.green) : Color.red), 15f);
			}
		}

	}

	public void movesCounterReset ()
	{
		_movesCount = 0;
	}

	public int getMovesCount()
	{
		return _movesCount;
	}

	public ArrayList getPlayerTrail()
	{
		return _playerTrail;
	}

	public double getPlayerDistance()
	{
		Trail playerTrail = new Trail (_playerTrail);
		return Math.Round(playerTrail.getDistance(),2);;
	}

	public void setAiTrail(ArrayList aiTrail)
	{
		_aiTrail = aiTrail;
	}

	public void setAiTrailDistance(double aiTrailDistance)
	{
		_aiTrailDistance = aiTrailDistance;
	}

	public double getAiTrailDistance()
	{
		return Math.Round(_aiTrailDistance,2);
	}

	public void setLastGameResult(String result)
	{
		_lastGameResult = result;	
	}

	public void resetAllTrails()
	{
		_aiTrail.Clear();
		_playerTrail.Clear();
		_aiTrailDistance = 0;
//		_playerDistance = 0;
	}

	public static bool drawPlayerSwitchStatus()
	{
		return _drawPlayerSwitch;
	}

	public static void drawPlayerSwitch(bool drawPlayerSwitch)
	{
		_drawPlayerSwitch = drawPlayerSwitch;
	}

	public static bool drawAiSwitchStatus()
	{
		return _drawAiSwitch;
	}

	public static void drawAiSwitch(bool drawSwitch)
	{
		_drawAiSwitch = drawSwitch;
	}

	public static void clearVectList()
	{
		clearVect ();
		_playerVectList.Clear ();
		_aiVectList.Clear ();
	}

	private static void clearVect()
	{
		vectList.Clear ();
	}
}
