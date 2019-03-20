using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public enum State
	{
		COUNT_DOWN,
		GAME_STARTED,
		GAME_OVER
	}

	public static GameController instance;
	public GameOverMenu gameOverMenu;
	public PowerBar powerBar;
	public Shooter shooter;
	public Text pregame_countDownText;

	public float pregame_countDownValue;
	float pregame_countDown;

	public float game_countDownValue;
	public Text game_countDownText;
	float game_countDown;

	public float game_score;
	public Text game_scoreText;

	public State state;

	// Use this for initialization
	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		if (!powerBar)
			Debug.LogWarning("powerBar not found");

		if (shooter != null)
			shooter.onReadyToGenerateNewBall += OnReadyToGenerateNewBall;

		pregame_countDown = pregame_countDownValue;
		pregame_countDownText.text = pregame_countDown.ToString("0.0");
		game_countDown = game_countDownValue;
		game_countDownText.text = game_countDown.ToString("0.0");
		GenerateNewBall();
	}

	// Update is called once per frame
	void Update () {
		switch (state)
		{
			case State.COUNT_DOWN:
			{
				pregame_countDown -= Time.deltaTime;
				if (pregame_countDown < 0f)
				{
					pregame_countDownText.gameObject.SetActive(false);
					state = State.GAME_STARTED;
					return;
				}
				pregame_countDownText.text = pregame_countDown.ToString("0.0");
				break;
			}
			case State.GAME_STARTED:
			{
				game_countDown -= Time.deltaTime;
				if (game_countDown < 0f)
				{
					state = State.GAME_OVER;
					return;
				}
				game_countDownText.text = game_countDown.ToString("0.0");
				break;
			}
			case State.GAME_OVER:
			{
				if (gameOverMenu)
					gameOverMenu.Show();
				break;
			}
		}

	}

	public void OnReadyToGenerateNewBall()
	{
		GenerateNewBall();
		InputController.instance.Ready();
	}

	public void GenerateNewBall()
	{
		if (shooter)
		{
			Ball ball = shooter.GenerateNewBall();

		}
	}

	public void UpdateBallPositionX(float x)
	{
		if (shooter)
			shooter.UpdateBallPositionX(x);
	}

	public void UpdatePowerBar(float power)
	{
		if (powerBar)
			powerBar.UpdateValue(power);
	}

	public void Shoot(float power)
	{
		Debug.Log("TEST Shoot with Power" + power);
		if (shooter)
			shooter.Fire(power);
	}

	public void Score(int score)
	{
		game_score += score;
		if (game_scoreText)
			game_scoreText.text = game_score.ToString();
	}
}
