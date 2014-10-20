using UnityEngine;
using System.Collections;

public class CoffeePickup : MonoBehaviour {
	PlayerController playerController;
	HUD hud;
	
	void Start(){
		hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
		
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	void OnTriggerEnter2D(Collider2D col){
		// When the player picks up a coffee, increase the score and his speed
		if (col.name == "Player"){
			hud.numCups += 1;
			
			playerController.runSpeed += 1;
			
			Destroy(gameObject);
			Destroy(this);
		}
	}
}
