using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHole : MonoBehaviour {

	public int score;

	// Use this for initialization
	void Start () {

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
		Debug.Log("OnTriggerEnter");
		if (collider.CompareTag("ball"))
		{
			GameController.instance.Score(score);
			Ball ball = collider.GetComponent<Ball>();
			if (ball)
				ball.Finish();
		}
	}
}
