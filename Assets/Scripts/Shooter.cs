using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

	// notify GameController
	public delegate void ReadyToGenerateNewBallDelegate();
	public event ReadyToGenerateNewBallDelegate onReadyToGenerateNewBall;

	public Ball ballPrefab;
	public Ball m_currBall;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void OnReadyToGenerateNewBallHandler(Ball ball)
	{
		// distance from camera case
		if (m_currBall == ball)
		{
			if (onReadyToGenerateNewBall != null)
				onReadyToGenerateNewBall();
		}
	}

	public void OnBallStateEventHandler(Ball ball, Ball.State state)
	{
		switch(state)
		{
			case Ball.State.IDLE:
			case Ball.State.MOVING:
			{
				break;
			}
			case Ball.State.DESTROYED:
			{
				if (ball)
				{
					ball.onStateChange -= OnBallStateEventHandler;
					ball.onReadyToGenerateNewBall -= OnReadyToGenerateNewBallHandler;
				}
				break;
			}
		}
	}

	public Ball GenerateNewBall()
	{
		if (ballPrefab)
		{
			 m_currBall = GameObject.Instantiate(ballPrefab, transform.position, Quaternion.identity);
			 m_currBall.onStateChange += OnBallStateEventHandler;
			 m_currBall.onReadyToGenerateNewBall += OnReadyToGenerateNewBallHandler;
			 return m_currBall;
		}
		return null;
	}

	public void Fire(float power)
	{
		if (m_currBall)
			m_currBall.Shoot(power);
	}

	public void UpdateBallPositionX(float newX)
	{
		if (m_currBall)
		{
			Vector3 pos = m_currBall.transform.position;
			m_currBall.UpdatePositionX(newX);
		}
	}
}
