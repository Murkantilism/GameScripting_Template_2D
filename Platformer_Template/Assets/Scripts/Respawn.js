#pragma strict

var player : GameObject;

var spawn : Transform;

public var playerDied : boolean = false;

var hud : HUD;

function Start () {
	player = GameObject.Find("Player");
	spawn = GameObject.Find("Spawn").transform;
	hud = GameObject.FindGameObjectWithTag("HUD").GetComponent(HUD);
}

function Update () {
	if(player.transform.position.y < -8.0f){
		playerDied = true;
	}
	if(playerDied == true){
		playerDied = false;
		player.transform.position = spawn.position;
		hud.numLives -= 1;
	}
}