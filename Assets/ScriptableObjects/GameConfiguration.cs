using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ScorePointType
{
	TYPE_O,
	TYPE_BIG_O,
	TYPE_U
}

[System.Serializable]
public struct ScorePointItem
{
	public ScorePointType type;
	public float x;
	public float y;
	public float z;
	public int score;
}

[CreateAssetMenu(menuName = "MyAssets/Configuration")]
public class GameConfiguration : ScriptableObject {

	[Header("Game")]
	public int pregame_countDown;
	public int game_countDown;
	public List<ScorePointItem> scorePointList;
}
