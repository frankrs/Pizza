using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class GUIMenu : MonoBehaviour {

	public MenuPage menuPage;

	public Vector2 hw;

	public GUISkin skin;

	public GUIStyle buttonStyle;

	public GUIStyle boxStyle;


	void Update (){
		hw = new Vector2(Screen.height,Screen.width);
		boxStyle.fontSize = Mathf.RoundToInt(hw.y / 25);
		buttonStyle.fontSize = Mathf.RoundToInt(hw.y / 25);
	}

	// Use this for initialization
	void OnGUI () {
		//GUI.skin = skin;
		GUILayout.BeginArea(new Rect(hw.y * .25f, hw.x * .25f, hw.x * .75f, hw.y * .75f));

		if(menuPage == MenuPage.paused){
			GUILayout.Box("Paused",boxStyle);
			if(GUILayout.Button("Resume",buttonStyle)){
				Time.timeScale = 1.0f;
				this.enabled = false;
			}
			if(GUILayout.Button("Quit",buttonStyle)){
				Application.Quit();
			}
		}

		if(menuPage == MenuPage.lost){
			GUILayout.Box("Times Up",boxStyle);
			if(GUILayout.Button("TryAgain",buttonStyle)){
				Time.timeScale = 1.0f;
				Application.LoadLevel(Application.loadedLevel);
			}
			if(GUILayout.Button("Quit",buttonStyle)){
				Application.Quit();
			}
		}


		if(menuPage == MenuPage.won){
			GUILayout.Box("You Win",boxStyle);
			if(GUILayout.Button("Prize",buttonStyle)){
				menuPage = MenuPage.prize;
			}
			if(GUILayout.Button("PlayAgain",buttonStyle)){
				Time.timeScale = 1.0f;
				Application.LoadLevel(Application.loadedLevel);
			}
			if(GUILayout.Button("Quit",buttonStyle)){
				Application.Quit();
			}
		}

		if(menuPage == MenuPage.prize){
			GUILayout.Box("Show Cashier Your\nDevice to\nCollect Prize",boxStyle);
			if(GUILayout.Button("PlayAgain",buttonStyle)){
				Time.timeScale = 1.0f;
				Application.LoadLevel(Application.loadedLevel);
			}
			if(GUILayout.Button("Quit",buttonStyle)){
				Application.Quit();
			}
		}

		GUILayout.EndArea();
	}
	

}


[System.Serializable]
public enum MenuPage{
	paused,won,lost,prize
}