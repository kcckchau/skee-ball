using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUIItem : MonoBehaviour {

	public Text dateTimeText;
	public Text posText;
	public Text playerText;
	public Text scoreText;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void SetValue(int pos, LeaderboardItem item)
  	{
		dateTimeText.text = item.dateTime.ToString("dd/MM HH:mm");
		posText.text = pos.ToString();
		playerText.text = item.name;
		scoreText.text = item.score.ToString();
	}
}
