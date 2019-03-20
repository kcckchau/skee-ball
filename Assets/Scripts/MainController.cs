using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour {

	public GameObject levelSelectMenu;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void ShowLevelSelectMenu()
	{
		if (levelSelectMenu)
			levelSelectMenu.SetActive(true);
	}

	public void GotoLeaderboard()
	{
		SceneController.GotoLeaderboard();
	}

	public void GotoGameLevel(int level)
	{
		ApplicationModel.currentLevel = level;
		SceneController.GotoGameScene();
	}

}
