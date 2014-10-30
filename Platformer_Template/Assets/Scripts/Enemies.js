#pragma strict
public var target : Vector3;
public var origin : Vector3;
	
var pathing : boolean = false;
	
var moveSpeed : float = 1.0f;

function Start () {

}

function Update () {

}
	
function OnTriggerEnter2D(col : Collider2D){
	if(col.name == "Player"){
		Debug.Log("PLAYER HIT BY BAT");
		GameObject.Find("Spawn").GetComponent(Respawn).playerDied = true;
	}
}