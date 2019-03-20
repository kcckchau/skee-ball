using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

	public Text descriptionText;
	public Text posText;
	public Text scoreText;
	public Button back;
	public Button retry;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void Show(LeaderboardItem item)
	{
		if (posText)
			posText.text = (item.pos + 1).ToString();
		if (scoreText)
			scoreText.text = item.score.ToString();
		if (back)
			back.gameObject.SetActive(true);
		if (item.pos == 0)
		{
			if (descriptionText)
				descriptionText.text = "Amazing! You got the highest score!";
		}
		else if (item.pos > 0 && item.pos < 5)
		{
			if (descriptionText)
				descriptionText.text = "Amazing! You got the highest score!";
			if (retry)
				retry.gameObject.SetActive(true);
		}
		else
		{
			if (descriptionText)
				descriptionText.text = "Your score is low. Try again";
			if (retry)
				retry.gameObject.SetActive(true);
		}

		gameObject.SetActive(true);
	}

	public void GotoMainMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void Retry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

}
