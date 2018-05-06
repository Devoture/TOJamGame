using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenuDoor : MonoBehaviour {
	public bool isOpening = false;
	
	
	// Update is called once per frame
	void Update () {
		if(isOpening){
			this.transform.Rotate(new Vector3( 0,0,0.2f));
			this.transform.Translate(new Vector3(-0.02f,0,0));
		}
	}
}
