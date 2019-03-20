using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public enum State
	{
		IDLE,
		MOVING,
		DESTROYED
	}

	// notify GameController
	public delegate void ReadyToGenerateNewBallDelegate(Ball ball);
	public event ReadyToGenerateNewBallDelegate onReadyToGenerateNewBall;

	// notify Shooter
	public delegate void StateChangeDelegate(Ball ball, Ball.State state);
	public event StateChangeDelegate onStateChange;

	public float mass_ul; // upper limit
	public float mass_ll; // lower limit
	public float up;

	public int bonus;

	Rigidbody rb;
	float countDown;
	bool m_isShooted;
	bool m_notifyGameController;
	State state;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		if (rb == null)
		{
			Debug.LogError(this + " Rigidbody not exist");
			return;
		}

		Renderer r = GetComponent<Renderer>();

		countDown = 10f;
		//mass_ll = 0.65f;
		//mass_ul = 1.35f;
		mass_ll = 0.8f;
		mass_ul = 1.2f;
		float value = Random.Range(mass_ll, mass_ul) * 0.75f;
		transform.localScale = new Vector3(value, value, value);
		rb.mass = value;
		if (r)
		{
			float percent = (value - mass_ll) / (mass_ul - mass_ll);
			r.material.color = new Color(0, 0, percent * 255);
		}
		bonus = (int)Mathf.Ceil(Random.Range(0f, 15f));
		GameController.instance.UpdateBallBonus(bonus);
	}

	void Update ()
	{
		Camera cam = Camera.main;
		Vector3 camPos = cam.transform.position;
		camPos = new Vector3(0, 0, camPos.z);
		Vector3 currPos = transform.position;
		currPos = new Vector3(0, 0, currPos.z);
		float distance = Vector3.Distance(camPos, currPos);
		//Debug.Log(distance);

		if (!m_notifyGameController && distance > 35f)
		{
			if (onReadyToGenerateNewBall != null)
			{
				onReadyToGenerateNewBall(this);
			}
			m_notifyGameController = true;
			return;
		}

		if (state == State.MOVING)
		{
			if (countDown < 0)
			{
				if (onReadyToGenerateNewBall != null)
				{
					onReadyToGenerateNewBall(this);
				}
				Finish();
				return;
			}
			countDown -= Time.deltaTime;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		//rb.AddForce(transform.forward * 10);
		//rb.AddForce(Vector3.down * 10);
	}

	public void Finish()
	{
		if (state == State.DESTROYED)
			return;

		state = State.DESTROYED;

		if (onReadyToGenerateNewBall != null)
		{
			onReadyToGenerateNewBall(this);
		}
		if (onStateChange != null)
		{
			onStateChange(this, state);
		}
		Destroy(gameObject);
	}

	public void Shoot(float power)
	{
		if (state == State.MOVING)
			return;

		state = State.MOVING;
		power += 0.5f;
		rb.AddForce(transform.forward * 2700);
		rb.AddForce(transform.up * power * 850);

		//rb.AddForce(transform.forward * power * 800);
		//rb.AddForce(transform.up * power * 750);
	}

	public void UpdatePositionX(float newX)
	{
		if (state == State.IDLE)
		{
			Vector3 pos = transform.position;
			transform.position = new Vector3(newX, pos.y, pos.z);
		}
	}

}
