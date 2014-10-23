#pragma strict

function OnGUI(){
	if(GUI.Button(new Rect(Screen.width/2, Screen.height/2, 150, 80), "Play again?")){
		Application.LoadLevel("CoffeeShop0");
	}
}