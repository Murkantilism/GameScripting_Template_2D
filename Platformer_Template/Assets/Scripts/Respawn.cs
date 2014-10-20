using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {
	
	GameObject player;
	
	Transform spawn;
	
	bool playerDied = false;
	
	HUD hud;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		spawn = GameObject.Find("Spawn").transform;
		hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.y < -8.0f){
			player.transform.position = spawn.position;
			playerDied = true;
		}
		if(playerDied == true){
			playerDied = false;
			hud.numLives -= 1;
		}
	}
}
