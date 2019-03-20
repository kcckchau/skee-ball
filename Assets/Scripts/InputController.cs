using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	enum State
	{
		IDLE,
		READY,
		POWER_STARTED,
		POWER_IN_PROGRESS,
		POWER_ENDED
	}

	public static InputController instance;

	public float timeToMax;
	float power;
	int direction = 1;
	State state;

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

		state = State.READY;
		timeToMax = 1.5f;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneController.GotoMainScene();
		}

		if (GameController.instance.state == GameController.State.COUNT_DOWN ||
		GameController.instance.state == GameController.State.GAME_OVER)
			return;

		Touch[] touches = Input.touches;
		Touch touch;

		if (touches.Length <= 0)
		{
			return;
		}

		//Debug.Log("TEST touch length " + touches.Length);
		touch = touches[0];
		/*
		int index = 0;
		foreach (Touch touch in touches)
		{
			Debug.Log("TEST index:" + index + " touch pos: " + touch.position + "phase" + touch.phase);
			Debug.Log("TEST touch phase " + touch.phase);
			index++;
		}
		*/

		switch (state)
		{
			case State.IDLE:
			{
				break;
			}
			case State.READY:
			{
				if (touch.phase == TouchPhase.Began)
				{
					state = State.POWER_STARTED;
				}
				break;
			}
			case State.POWER_STARTED:
			{
				if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
					state = State.POWER_IN_PROGRESS;
				break;
			}
			case State.POWER_IN_PROGRESS:
			{
				switch (touch.phase)
				{
					case TouchPhase.Stationary:
					case TouchPhase.Moved:
					{
						if (power < 0f)
						{
							direction = 1;
						}
						else if (power > 1.0f)
						{
							direction = -1;
						}

						power += 1.0f * (Time.deltaTime / timeToMax + power * 0.1f) * direction;
						GameController.instance.UpdatePowerBar(power);

						Camera cam = Camera.main;
						Ray ray = cam.ScreenPointToRay(touch.position);
						RaycastHit rayCast;
						if(Physics.Raycast(ray.origin, ray.direction, out rayCast, 100))
						{
							float newX = rayCast.point.x;
							GameController.instance.UpdateBallPositionX(newX);
						}

						break;
					 }
					 case TouchPhase.Ended:
					 case TouchPhase.Canceled:
					 {
						 GameController.instance.Shoot(power);
						 power = 0;
						 state = State.POWER_ENDED;
						 break;
					 }
				}

				break;
			}
			case State.POWER_ENDED:
			{
				break;
			}
		}
	}

	public void Ready()
	{
		Debug.Log(this + "TEST Ready");
		state = State.READY;
	}

}
