using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct LeaderboardItem
{
	string name;
	System.DateTime dateTime;
	int score;
}

[System.Serializable]
public struct LeaderboardData
{
	string version;
	List<LeaderboardItem> leaderboard;
}

public class Leaderboard : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void GotoMainScene()
	{
		SceneManager.LoadScene(0);
	}
}
