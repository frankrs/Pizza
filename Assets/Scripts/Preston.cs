using UnityEngine;
using System.Collections;

public class Preston : MonoBehaviour {

	public GameManager gameManager;

	void Start(){
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Pizza"){
			gameManager.EatPizza();
			col.SendMessage("Eaten");
		}
	}
}
