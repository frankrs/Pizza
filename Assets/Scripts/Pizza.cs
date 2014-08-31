using UnityEngine;
using System.Collections;

public class Pizza : MonoBehaviour {

	public float rotSpeed = 60f;

	void LateUpdate () {
		transform.Rotate(new Vector3(0f,rotSpeed * Time.deltaTime,0f));
	}

	void Eaten(){
		gameObject.SetActive(false);
	}
}
