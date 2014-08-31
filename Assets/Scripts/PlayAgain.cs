using UnityEngine;
using System.Collections;

public class PlayAgain : MonoBehaviour {

	// Use this for initialization
	void OnMouseDown () {
		Time.timeScale = 1f;
		Application.LoadLevel(Application.loadedLevel);
	}
	

}
