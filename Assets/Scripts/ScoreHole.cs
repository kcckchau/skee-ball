using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHole : MonoBehaviour {

	public Text scoreText;
	public int score;

	// Use this for initialization
	void Start () {
		if (scoreText)
		{
			scoreText.text = score.ToString();
		}
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnCollisionEnter(Collision collision)
	{
		Debug.Log("OnCollisionEnter");
	}

	public void OnTriggerEnter(Collider collider)
	{
		if (GameController.instance.state == GameController.State.COUNT_DOWN ||
		GameController.instance.state == GameController.State.GAME_OVER)
		{
			return;
		}
		Debug.Log("OnTriggerEnter");
		if (collider.CompareTag("ball"))
		{
			Ball ball = collider.GetComponent<Ball>();
			int bonus = 0;
			if (ball)
			{
				ball.Finish();
				bonus = ball.bonus;
			}
			Debug.Log("bonus" + bonus);
			GameController.instance.Score(score + bonus);
		}
	}
}
