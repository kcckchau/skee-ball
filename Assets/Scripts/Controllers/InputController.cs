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
	float elapsedTime;
	float acceleration;
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
		power = 0;
		timeToMax = 1.5f;
	}

	void DesktopInputControl()
	{
		Debug.Log("DesktopInputControl");
		switch (state)
		{
			case State.IDLE:
			{
				break;
			}
			case State.READY:
			{
				if (Input.GetMouseButtonDown(0) )
				{
					state = State.POWER_IN_PROGRESS;
					elapsedTime = 0;
				}
				break;
			}
			case State.POWER_IN_PROGRESS:
			{
				if (Input.GetMouseButton(0))
				{

					if (power < 0f)
					{
						elapsedTime = 0;
						direction = 1;
					}
					else if (power > 1.0f)
					{
						elapsedTime = 0;
						direction = -1;
					}

					//power += 1.0f * (Time.deltaTime / timeToMax + power * 0.1f) * direction;
					elapsedTime += Time.deltaTime;
					if (direction > 0)
						power = 0.5f * acceleration * elapsedTime * elapsedTime;
					else
						power = 1 - (0.5f * acceleration * elapsedTime * elapsedTime);
					GameController.instance.UpdatePowerBar(power);

					Camera cam = Camera.main;
					Ray ray = cam.ScreenPointToRay(Input.mousePosition);
					RaycastHit rayCast;
					if(Physics.Raycast(ray.origin, ray.direction, out rayCast, 100))
					{
						float newX = rayCast.point.x;
						GameController.instance.UpdateBallPositionX(newX);
					}
				}
				else if (Input.GetMouseButtonUp(0))
				{
					 GameController.instance.Shoot(power);
					 power = 0;
					 elapsedTime = 0;
					 direction = 1;
					 state = State.POWER_ENDED;
				}
				break;
			}
			case State.POWER_ENDED:
			{
				break;
			}
		}

	}

	void MobileInputControl()
	{
		Debug.Log("MobileInputControl");
		Touch[] touches = Input.touches;
		Touch touch;

		if (touches.Length <= 0)
		{
			return;
		}

		touch = touches[0];

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
					elapsedTime = 0;
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
							elapsedTime = 0;
							direction = 1;
						}
						else if (power > 1.0f)
						{
							elapsedTime = 0;
							direction = -1;
						}

						//power += 1.0f * (Time.deltaTime / timeToMax + power * 0.1f) * direction;
						elapsedTime += Time.deltaTime;
						if (direction > 0)
							power = 0.5f * acceleration * elapsedTime * elapsedTime;
						else
							power = 1 - (0.5f * acceleration * elapsedTime * elapsedTime);
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
						 elapsedTime = 0;
						 direction = 1;
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

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneController.GotoMainScene();
		}

		GameController.State gameState = GameController.instance.state;

		if (gameState == GameController.State.INPUT_NAME ||
		gameState == GameController.State.COUNT_DOWN ||
		gameState == GameController.State.GAME_OVER)
			return;

		if (Application.platform == RuntimePlatform.Android ||
			Application.platform == RuntimePlatform.IPhonePlayer)
		{
			MobileInputControl();
		}
		else
		{
			// assume anything else is Desktop at the moment
			DesktopInputControl();
		}
	}

	public void SetTimeToReachMax(float time)
	{
		timeToMax = time;
		acceleration = (float) 2 / timeToMax / timeToMax;
	}

	public void Ready()
	{
		Debug.Log(this + "Ready");
		state = State.READY;
	}

}
