using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;

public class DifficultyLevel : MonoBehaviour ///Skrypt obsługujący menu główne
{
//	static GameController start;
	void Start () ///procedura obowiązkowa, wywoływana raz podczas startu aplikacji przez silnik
	{		
		
	}

	// Update is called once per frame
	void Update ()///procedura obowiązkowa, wywoływana cyklicznie co ramkę przez silnik 
	{


		//		if(Input.GetKey("escape")){
		//			Application.Quit ();
		//		}
	}

	public void onClickEasy()///obsługa przycisku na scenie, latwy poziom trudnosci
	{
		
		setStarter(GameController.BRUTE_FORCE_MODE ? 10 : 15,60,100,"Poczatkujacy");
		loadTable ();
	}

	public void onClickMedium()///obsługa przycisku na scenie, sredni poziom trudnosci
	{
		setStarter(GameController.BRUTE_FORCE_MODE ? 10 : 15,480,500,"Umiarkowany");
		loadTable ();

	}

	public void onClickHard()///obsługa przycisku na scenie, trudny poziom trudnosci
	{
		setStarter(GameController.BRUTE_FORCE_MODE ? 10 : 15,960,1000,"Wymagajacy");
		loadTable ();
	}

	public void setStarter(int pointsNumber, int populationLength, int evolveNumber, string diffLevel)
	{
		Starter.setPointsNumber(pointsNumber);
		Starter.setPopulationLength (populationLength);
		Starter.setEvolveNumber (evolveNumber);
		Starter.setDiffLevel (diffLevel);
	}

	public void onClicMainMenu()///obsługa przycisku na scenie, wyjście z gry
	{
		SceneManager.LoadScene ("mainMenu");
	}

	private void loadTable ()
	{
		SceneManager.LoadScene ("table");
		SceneManager.LoadScene ("table");
	}

}
