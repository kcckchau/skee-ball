using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePoint : MonoBehaviour {

	public ScoreHole hole;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void SetScore(int score)
	{
		if (hole)
		{
			hole.score = score;
		}
	}
}
