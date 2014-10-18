using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Stats stats;
	public AudioClip yum;
	public FreeLookCam flc;
	public GUIMenu guiMenu;

	// Use this for initialization
	void Start () {
		InvokeRepeating("TakeTime",0f,1f);
		SetPizzas();
	}
	
	// Update is called once per frame
	void Update () {
		//check for back button to shut down
		if(Input.GetKeyDown("escape")){
			Pause();
		}
	
	}

	public void EatPizza (){
		stats.pizzasEaten = stats.pizzasEaten + 1;
		SetPizzas();
		audio.PlayOneShot(yum);
		if(stats.pizzasEaten == stats.pizzasLeft){
			Win();
		}
	}

	public void SetPizzas (){
		stats.pizzaCounter.text = "Pizzas: " + stats.pizzasEaten.ToString() + "/" + stats.pizzasLeft.ToString();
	}


	void TakeTime(){
		stats.time = stats.time - 1;
		stats.timeCounter.text = "Time: " + stats.time.ToString();
		if(stats.time == 0){
			Lose();
		}
	}


	public void Lose(){
		Debug.Log("Lost");
		Time.timeScale = 0f;
		guiMenu.menuPage = MenuPage.lost;
		guiMenu.enabled = true;
	}

	public void Win (){
		//win_lose.gameObject.SetActive(true);
		//win_lose.text = "You Win!";
		Time.timeScale = 0f;
		guiMenu.menuPage = MenuPage.won;
		guiMenu.enabled = true;
	}

	public void Pause (){
		Time.timeScale = 0f;
		guiMenu.menuPage = MenuPage.paused;
		guiMenu.enabled = true;
	}

}


[System.Serializable]
public class Stats {
	public int pizzasEaten;
	public int pizzasLeft;
	public int time;
	public GUIText pizzaCounter;
	public GUIText timeCounter;
}