using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardController : MonoBehaviour {

	public static LeaderboardController instance;
	public List<Leaderboard> leaderboardList;
	int currIndex;

	public Transform itemParent;
	public LeaderboardUIItem itemPrefab;

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
		leaderboardList = new List<Leaderboard>();

		for (int i = 0; i < 3; ++i)
		{
			Leaderboard item = new Leaderboard();
			item.Init(i);
			leaderboardList.Add(item);
		}

		ShowData();
		//leaderboardList[0].instance.ShowData();
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
		leaderboardList[currIndex].NewEntry(newItem);
		ShowData();

	}

	public void GotoMainMenu()
	{
		SceneController.GotoMainScene();
	}

	public void prevLeaderboard()
	{

	}

	public void nextLeaderboard()
	{

	}

	public void ShowData()
	{
		foreach (Transform child in itemParent)
		{
			Destroy(child.gameObject);
		}
		int index = 1;
		foreach (LeaderboardItem item in leaderboardList[currIndex].data.list)
		{
			LeaderboardUIItem uiItem = GameObject.Instantiate(itemPrefab, transform.position, Quaternion.identity);
			if (itemParent)
				uiItem.transform.SetParent(itemParent, false);
			uiItem.SetValue(index, item);
			index++;
		}
	}
}
