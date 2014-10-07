using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class TimerPlacementSizer : MonoBehaviour {


	public GUIText guiText;
	public int divider = 10;
	public int screenW;
	public float offSetPercent;


	// Update is called once per frame
	void Update () {
		screenW = Screen.width;
		guiText.fontSize = screenW/divider;
		guiText.pixelOffset = new Vector2((screenW * offSetPercent),guiText.pixelOffset.y);
	}
}
