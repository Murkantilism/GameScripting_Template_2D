#pragma strict

var playerController : GameObject;
var hud : HUD;

function Start () {
	playerController = GameObject.FindGameObjectWithTag("Player");

	hud = GameObject.FindGameObjectWithTag("HUD").GetComponent(HUD);
}

function OnTriggerEnter2D(col : Collider2D){
	// When the player picks up a coffee, increase the score and his speed
	if (col.name == "Player"){
		hud.numCups += 1;
		
		playerController.SendMessage("IncrementRunSpeed");
		//playerController.runSpeed += 1;
		
		Destroy(gameObject);
		Destroy(this);
	}
}