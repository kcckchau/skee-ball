using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardController : MonoBehaviour {

	public static LeaderboardController instance;

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

	}

	void Start()
	{
		Leaderboard.instance.Init();
		Leaderboard.instance.ShowData();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneController.GotoMainScene();
		}
	}

	// return position
	public void NewEntryTest()
	{
		LeaderboardItem newItem = new LeaderboardItem();
		newItem.score = Random.Range(20, 100);
		newItem.dateTime = System.DateTime.Now;
		Debug.Log("TEST2 " + newItem.score);
		newItem.name = "TEST";
		Leaderboard.instance.NewEntry(newItem);
		Leaderboard.instance.ShowData();

	}

	public void GetScoreList()
	{

	}
}
