using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardController : MonoBehaviour {

	public static LeaderboardController instance;
	public LeaderboardData data;

	// Use this for initialization
	void Awake () {
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
	}

	// Update is called once per frame
	void Update () {

	}

	// return position
	public int NewScore(LeaderboardItem item)
	{
		return 1;
	}

	public void GetScoreList()
	{

	}
}
