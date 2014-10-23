#pragma strict

function OnTriggerEnter2D(col : Collider2D){
	if (col.name == "Player"){
		Application.LoadLevel("WinScene");
	}
}