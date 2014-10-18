using UnityEngine;
using System.Collections;

public class WebTexture : MonoBehaviour {

	public string url;
	IEnumerator Start() {
		WWW www = new WWW(url);
		yield return www;
		if(www.error == null){
			renderer.material.mainTexture = www.texture;
		}
	}
}
