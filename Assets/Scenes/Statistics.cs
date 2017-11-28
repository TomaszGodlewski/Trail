using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;

public class Statistics : MonoBehaviour ///Skrypt obsługujący scenę statystyk
{
	public Text allStats;
	public Text winEasy;
	public Text winMedium;
	public Text winHard;
	public Text drawEasy;
	public Text drawMedium;
	public Text drawHard;
	public Text failureEasy;
	public Text failureMedium;
	public Text failureHard;


	private void Start () ///procedura obowiązkowa, wywoływana raz podczas startu sceny przez silnik
	{		
		findTexts ();
		prepareStatistics ();

	}

	void Update () ///procedura obowiązkowa, wywoływana cyklicznie co ramkę przez silnik
	{

	}



	public void onClickReset() ///obsługa przycisku na scenie, reset statystyk
	{		
		resetStatistics ();
		SceneManager.LoadScene("statistics");
	}

	public void onClickMainMenu() ///obsługa przycisku na scenie, wejscie do menu głownego 
	{
		SceneManager.LoadScene("mainMenu");
	}

	private void findTexts() /// powiązanie tekstów przygotowanych w kontrolerze Unity z obsługującymi je zmiennymi
	{
		allStats = GameObject.Find ("AllStatsText").GetComponent<Text> ();
		winEasy = GameObject.Find ("WinEasyText").GetComponent<Text> ();
		winMedium = GameObject.Find ("WinMediumText").GetComponent<Text> ();
		winHard = GameObject.Find ("WinHardText").GetComponent<Text> ();
		drawEasy = GameObject.Find ("DrawEasyText").GetComponent<Text> (); 
		drawMedium = GameObject.Find ("DrawMediumText").GetComponent<Text> ();
		drawHard = GameObject.Find ("DrawHardText").GetComponent<Text> ();
		failureEasy = GameObject.Find ("FailureEasyText").GetComponent<Text> ();
		failureMedium  = GameObject.Find ("FailureMediumText").GetComponent<Text> ();
		failureHard  = GameObject.Find ("FailureHardText").GetComponent<Text> ();
	}

	private void prepareStatistics() ///odczytanie zapisanych wynikow rozgrywek oraz przypisanie ich do poszczegolnych tekstow na scenie 
	{
		//odczytanie zapisanych wynikow rozgrywek
		int we = PlayerPrefs.GetInt ("easyWin");
		int wm = PlayerPrefs.GetInt ("mediumWin");
		int wh = PlayerPrefs.GetInt ("hardWin");
		int de = PlayerPrefs.GetInt ("easyDraw");
		int dm = PlayerPrefs.GetInt ("mediumDraw");
		int dh = PlayerPrefs.GetInt ("hardDraw");
		int fe = PlayerPrefs.GetInt ("easyFailure");
		int fm = PlayerPrefs.GetInt ("mediumFailure");
		int fh = PlayerPrefs.GetInt ("hardFailure"); 
		int summ = we + wm + wh + de + dm + dh + fe + fm + fh;

		//przypisanie wynikow do poszczegolnych tekstow na scenie
		allStats.GetComponentInChildren<Text> ().text = summ.ToString();
		winEasy.GetComponentInChildren<Text> ().text = we.ToString();
		winMedium.GetComponentInChildren<Text> ().text = wm.ToString();
		winHard.GetComponentInChildren<Text> ().text = wh.ToString();
		drawEasy.GetComponentInChildren<Text> ().text = de.ToString();
		drawMedium.GetComponentInChildren<Text> ().text = dm.ToString();
		drawHard.GetComponentInChildren<Text> ().text = dh.ToString();
		failureEasy.GetComponentInChildren<Text> ().text = fe.ToString();
		failureMedium.GetComponentInChildren<Text> ().text = fm.ToString();
		failureHard.GetComponentInChildren<Text> ().text = fh.ToString();
	}

	private void resetStatistics() ///wyzerowanie zapisanych wynikow rozgrywek
	{
		PlayerPrefs.SetInt ("easyWin",0);
		PlayerPrefs.SetInt ("mediumWin",0);
		PlayerPrefs.SetInt ("hardWin",0);
		PlayerPrefs.SetInt ("easyDraw",0);
		PlayerPrefs.SetInt ("mediumDraw",0);
		PlayerPrefs.SetInt ("hardDraw",0);
		PlayerPrefs.SetInt ("easyFailure",0);
		PlayerPrefs.SetInt ("mediumFailure",0);
		PlayerPrefs.SetInt ("hardFailure",0); 
	}

}
