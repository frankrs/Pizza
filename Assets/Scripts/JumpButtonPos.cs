﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class JumpButtonPos : MonoBehaviour {

	public GUITexture guiTexture;
	public int screenW;
	public int screenH;
	public float offSetPercentW;
	public float offSetPercentH;
	public float size;
	public Button button;
	
	// Update is called once per frame
	void Awake () {
		screenW = Screen.width;
		screenH = Screen.height;
		guiTexture.pixelInset = new Rect((screenW * offSetPercentW),(screenH * offSetPercentH),screenW*size,screenW*size/2);
		//button.enabled = true;
	}
}
