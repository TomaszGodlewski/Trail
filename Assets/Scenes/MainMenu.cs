using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;

public class MainMenu : MonoBehaviour ///Skrypt obsługujący menu główne
{



	void Start () ///procedura obowiązkowa, wywoływana raz podczas startu aplikacji przez silnik
	{		
		
	}
	
	// Update is called once per frame
	void Update () ///procedura obowiązkowa, wywoływana cyklicznie co ramkę przez silnik
	{
		
		
//		if(Input.GetKey("escape")){
//			Application.Quit ();
//		}
	}



	public void onClickPlay() ///obsługa przycisku na scenie, wejscie do głownego menu
	{
		SceneManager.LoadScene("difficultyLevel");
	}

	public void onClickStatistics() ///obsługa przycisku na scenie, wejscie do głownego menu
	{
		SceneManager.LoadScene("statistics");
	}


	public void onClicQuit()///obsługa przycisku na scenie,wyjście z gry
	{		
		Application.Quit ();
	}


}
