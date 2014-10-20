using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D col){
		if (col.name == "Player"){
			Application.LoadLevel("WinScene");
		}
	}
}
