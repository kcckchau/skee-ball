using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public static void GotoMainScene()
	{
		SceneManager.LoadScene(0);
	}

	public static void GotoLeaderboard()
	{
		SceneManager.LoadScene(1);
	}

	public static void GotoGameScene()
	{
		SceneManager.LoadScene(2);
	}
}
