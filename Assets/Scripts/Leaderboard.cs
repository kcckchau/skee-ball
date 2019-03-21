using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct LeaderboardItem
{
	[SerializeField]
	public System.DateTime dateTime;
	public int pos;
	public string name;
	public int score;
}

[System.Serializable]
public class LeaderboardData
{
	public string version;
	public List<LeaderboardItem> list;
}

public class Leaderboard {

	public static Leaderboard instance;
	public LeaderboardData data;
	public string dataPath;

	// Use this for initialization
	void Awake()
	{
		//dataPath = Path.Combine(Application.streamingAssetsPath, "leaderboard.dat");
		/*
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}
		*/
	}

	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void Init(int level)
	{
		string fileName = string.Format("leaderboard{0}.dat", level);
		dataPath = Path.Combine(Application.persistentDataPath, fileName);

		BinaryFormatter formatter = new BinaryFormatter();
		if (! System.IO.File.Exists(dataPath))
		{
			Debug.Log("SaveData not exist, create a new one");
			LeaderboardData newData = new LeaderboardData();
			newData.list = new List<LeaderboardItem>();
			FileStream stream = new FileStream(dataPath, FileMode.Create);
			formatter.Serialize(stream, newData);
			stream.Close();
			data = newData;
		}
		else
		{
			Debug.Log("Load data");
			FileStream stream = new FileStream(dataPath, FileMode.Open);
			data = formatter.Deserialize(stream) as LeaderboardData;
			stream.Close();
		}
	}

	public int NewEntry(LeaderboardItem newItem)
	{
		//List<LeaderboardItem> list = data.list;

		if (data.list.Count == 0)
		{
			newItem.pos = 0;
			data.list.Add(newItem);
			Save();
			return 0;
		}

		data.list.Sort( delegate(LeaderboardItem i1, LeaderboardItem i2)
		{
			if (i1.score < i2.score)
				return 1;
			else if (i1.score == i2.score)
				return 0;
			else
				return -1;
		});

		int index = 0;
		bool isAdded = false;
		foreach (LeaderboardItem item in data.list)
		{
			if (newItem.score > item.score)
			{
				newItem.pos = index;
				data.list.Insert(index, newItem);
				isAdded = true;
				break;
			}
			index++;
		}

		if (data.list.Count > 5)
		{
			data.list.RemoveAt(data.list.Count - 1);
		}
		else
		{
			if (!isAdded && data.list.Count < 5)
			{
				isAdded = true;
				newItem.pos = index;
				data.list.Insert(index, newItem);
			}
		}

		Save();

		return index;
	}

	public void Save()
	{
		Debug.Log("Saving Data");
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(dataPath, FileMode.Create);
		formatter.Serialize(stream, data);
		stream.Close();
		Debug.Log("Saving Data Done");
	}

	public LeaderboardData Load()
	{
		Debug.Log("Loading Data");
		BinaryFormatter formatter = new BinaryFormatter();
		if (System.IO.File.Exists(dataPath))
		{
			FileStream stream = new FileStream(dataPath, FileMode.Open);
			data = formatter.Deserialize(stream) as LeaderboardData;
			Debug.Log("Exist ");
			stream.Close();
			Debug.Log("Loading Data done");
			return data;
		}
		Debug.Log("Loading Data Failed");
		return null;
	}

	public void Reset()
	{
		LeaderboardData newData = new LeaderboardData();
		newData.list = new List<LeaderboardItem>();
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(dataPath, FileMode.Create);
		formatter.Serialize(stream, newData);
		stream.Close();
	}

/*
	public void ShowData()
	{
		Debug.Log("data list Count" + data.list.Count);
		foreach (Transform child in itemParent)
		{
			Destroy(child.gameObject);
		}
		int index = 1;
		foreach (LeaderboardItem item in data.list)
		{
			LeaderboardUIItem uiItem = GameObject.Instantiate(itemPrefab, transform.position, Quaternion.identity);
			if (itemParent)
				uiItem.transform.SetParent(itemParent, false);
			uiItem.SetValue(index, item);
			index++;
		}
	}
*/
}
