using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyAssets/Configuration")]
public class GameConfiguration : ScriptableObject {

	[Header("Game")]
	public int pregame_countDown;
	public int game_countDown;

}
