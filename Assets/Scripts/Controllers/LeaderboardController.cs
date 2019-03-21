using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour {

	public static LeaderboardController instance;
	public List<Leaderboard> leaderboardList;
	int m_currIndex;

	public Transform itemParent;
	public LeaderboardUIItem itemPrefab;
	public Text leaderboardTitleText;

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
		UpdateLeaderboardTitleText();
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
		newItem.name = "Player";
		leaderboardList[m_currIndex].NewEntry(newItem);
		ShowData();

	}

	public void GotoMainMenu()
	{
		SceneController.GotoMainScene();
	}

	public void PrevLeaderboard()
	{

	}

	public void NextLeaderboard()
	{
		m_currIndex++;
		m_currIndex %= 3;
		Debug.Log("Next leader board: " + m_currIndex);
		UpdateLeaderboardTitleText();
		ShowData();
	}

	public void ShowData()
	{
		foreach (Transform child in itemParent)
		{
			Destroy(child.gameObject);
		}
		int index = 1;
		Debug.Log("curr index" + m_currIndex);
		if (leaderboardList[m_currIndex] != null)
		{
			foreach (LeaderboardItem item in leaderboardList[m_currIndex].data.list)
			{
				LeaderboardUIItem uiItem = GameObject.Instantiate(itemPrefab, transform.position, Quaternion.identity);
				if (itemParent)
					uiItem.transform.SetParent(itemParent, false);
				uiItem.SetValue(index, item);
				index++;
			}
		}
	}

	void UpdateLeaderboardTitleText()
	{
		if (leaderboardTitleText)
		{
			leaderboardTitleText.text = "Leaderboard " + (m_currIndex + 1).ToString();
		}
	}

	public void Reset()
	{
		foreach (Leaderboard item in leaderboardList)
		{
			if (item != null)
			{
				item.Reset();
			}
			else
			{
				Debug.Log("Item is null");
			}
		}
	}
}
