#pragma strict

var player : GameObject;

var spawn : Transform;

var playerDied : boolean = false;

var hud : HUD;

function Start () {
	player = GameObject.Find("Player");
	spawn = GameObject.Find("Spawn").transform;
	hud = GameObject.FindGameObjectWithTag("HUD").GetComponent(HUD);
}

function Update () {
	if(player.transform.position.y < -8.0f){
		player.transform.position = spawn.position;
		playerDied = true;
	}
	if(playerDied == true){
		playerDied = false;
		hud.numLives -= 1;
	}
}