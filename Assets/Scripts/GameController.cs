using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public enum State
	{
		INPUT_NAME,
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

	public Text bonusText;

	DateTime dateTime;
	public Text player_nameText;
	string player_name;

	int game_score;
	public Text game_scoreText;

	public GameObject inputFieldObj;
	public InputField inputField;

	public GameConfiguration gameConfiguration;
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
		game_countDown = game_countDownValue;

		if (gameConfiguration != null)
		{
			pregame_countDown = gameConfiguration.pregame_countDown;
			game_countDown = gameConfiguration.game_countDown;
		}
		pregame_countDownText.text = pregame_countDown.ToString("0.0");
		game_countDownText.text = game_countDown.ToString("0.0");

		/*
		inputField.OnFocus.AddListener(delegate {
			TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true);
		});
		*/

		if (inputFieldObj)
			inputFieldObj.SetActive(true);
		//TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true);

		GenerateNewBall();
	}

	void Start()
	{
		Leaderboard.instance.Init();
	}

	// Update is called once per frame
	void Update () {
		switch (state)
		{
			case State.INPUT_NAME:
			{
				break;
			}
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
					if (gameOverMenu)
					{
						LeaderboardItem item = new LeaderboardItem();
						item.score = game_score;
						item.name = player_name;
						item.dateTime = System.DateTime.Now;
						int pos = Leaderboard.instance.NewEntry(item);
						item.pos = pos;
						gameOverMenu.Show(item);
					}
					return;
				}
				game_countDownText.text = game_countDown.ToString("0.0");
				break;
			}
			case State.GAME_OVER:
			{
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
			shooter.GenerateNewBall();
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

	public void UpdateBallBonus(int bonus)
	{
			bonusText.text = bonus.ToString();
	}

	public void SetPlayerName(InputField input)
	{
		if (input.text.Length > 10)
			player_name = input.text.Substring(0, 10);
		else if (input.text.Length > 0 && input.text.Length <= 10)
			player_name = input.text;
		else
			player_name = "Player";

		if (player_nameText)
			player_nameText.text = player_name;
		state = State.COUNT_DOWN;

		if (inputFieldObj)
			inputFieldObj.SetActive(false);
	}
}
