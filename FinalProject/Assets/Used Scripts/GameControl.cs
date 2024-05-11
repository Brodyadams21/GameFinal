 using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using JetBrains.Annotations;

public class GameControl : MonoBehaviour
{
	public static GameControl gc;
	public int lives = 3;
	public int score = 0;
	private int highscore;
	public int blockAmount;

	public string playAgainLevelToLoad;
	public string nextLevelToLoad;

	public string mainScreen;


	public TMP_Text mainLivesDisplay;
	public TMP_Text mainScoreDisplay;
	public TMP_Text mainBlockDisplay;
	public TMP_Text mainHighDisplay;

	// Use this for initialization
	void Start () 
	{
		if (gc == null) 
			gc = this.gameObject.GetComponent<GameControl>();
		
		blockAmount += 100;

		highscore = PlayerPrefs.GetInt("Highscore");
		
		mainLivesDisplay.text = lives.ToString();
		mainScoreDisplay.text = score.ToString();
		mainBlockDisplay.text = blockAmount.ToString();
		mainHighDisplay.text = highscore.ToString();



		Invoke ("displayBlocks", 3.0f);
		Invoke ("BlockSubtract", 3.5f);

	}


	void Update () 
	{
		if (blockAmount <= 0)
		{
			NextLevel();
		}
		if (score > highscore)
		{
			SetHighscore();
			mainHighDisplay.text = highscore.ToString();
		}
		if (lives <= 0)
		{
			Die();
		}
	}

	void SetHighscore()
	{
		if (score > highscore)
			PlayerPrefs.SetInt("Highscore", score);
	}

	public void enemyCollision (int lifeAmount)
	{
		// increase the score by the scoreAmount and update the text UI
		lives -= lifeAmount;
		mainLivesDisplay.text = lives.ToString ();

		// don't let it go negative
		if (lives <= 0)
			RestartGame();
	}

	public void addScore (int amount)
	{
		score += amount;
		mainScoreDisplay.text = score.ToString ();

	}

	public void removeBlock()
	{
		blockAmount -= 1;
		mainBlockDisplay.text = blockAmount.ToString ();
	}

	public void displayBlocks()
	{
		mainBlockDisplay.text = blockAmount.ToString();
	}
	public void BlockSubtract()
	{
		blockAmount -= 100;
	}

	public void RestartGame ()
	{
		SetHighscore();
        SceneManager.LoadScene(playAgainLevelToLoad);
	}

	public void NextLevel ()
	{
		SetHighscore();
        SceneManager.LoadScene(nextLevelToLoad);
	}

	public void Die ()
	{
		SetHighscore();
        SceneManager.LoadScene(mainScreen);
	}

}
