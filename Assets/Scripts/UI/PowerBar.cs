using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour {

	public Image bg;
	public Image fg;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void UpdateValue(float value)
	{
		if (value < 0 || value > 1)
			return;

		if (fg)
		{
			Vector3 localScale = fg.rectTransform.localScale;
			fg.rectTransform.localScale = new Vector3(value, localScale.y, localScale.z);
		}
	}
}
