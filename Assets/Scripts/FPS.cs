using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
	void Start()
	{
		Application.targetFrameRate = 60;

	}

	float deltaTime = 0.0f;
	float timer = 0.0f;

	float timer2 = 0.0f;
	int ingredientsCount = 0;

	void Update()
	{
		timer = Time.time;
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}

	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color(1f, 1f, 1f, 1.0f);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label(rect, text, style);
		//GUI.Label(rect, "\n" + (timer - timer2) + "", style);
		//GUI.Label(rect, "\n\n" + ingredientsCount + "", style);

	}

	public void TimerReset()
    {
		timer2 = timer;
		ingredientsCount = 0;
	}

	public void IngredientsIncrease()
    {
		ingredientsCount++;
    }
}
