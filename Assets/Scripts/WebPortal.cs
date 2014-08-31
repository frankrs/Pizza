using UnityEngine;
using System.Collections;

public class WebPortal : MonoBehaviour {

	public string website;
	public AudioClip transportSound;

//	void Start (){
//		gameObject.AddComponent<AudioSource>();
//	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			audio.Play();
			StartCoroutine("Teleport");
		}
	}

	IEnumerator Teleport (){
		while(audio.isPlaying){
			yield return null;
		}
		Application.OpenURL(website);
	}
}
