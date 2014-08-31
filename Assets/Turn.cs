using UnityEngine;
using System.Collections;

public class Turn : MonoBehaviour {

	public float turnSpeed = 60;
	
	// Update is called once per frame
	void LateUpdate () {
		transform.Rotate(new Vector3(0f,turnSpeed * Time.deltaTime,0f));
	}
}
