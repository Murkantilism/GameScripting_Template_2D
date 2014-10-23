#pragma strict

public var numLives : int = 3; // The number of starting lives
public var numCups : int = 0; // The number of starting coffee cups
var keyAcquired : boolean = false; // Has the key been acquired?
var deadP : boolean = false; // Has our hero croaked?

public var heart0_sprr : SpriteRenderer; // Sprr = Sprite Renderer
public var heart1_sprr : SpriteRenderer;
public var heart2_sprr : SpriteRenderer;

public var heart_empty_sprr : SpriteRenderer; // Spr = Sprite

var heart0 : SpriteRenderer;
var heart1 : SpriteRenderer;
var heart2 : SpriteRenderer;

public var key_empty_sprr : SpriteRenderer;
public var key_acquired_sprr : SpriteRenderer;

var key : SpriteRenderer;

public var coffeeCup_spr : Sprite;

public var guiSkin : GUISkin;

function Start () {
	// Set starting HUD textures
	heart0 = heart0_sprr;
	heart1 = heart1_sprr;
	heart2 = heart2_sprr;
	
	key = key_empty_sprr;
}

// As long as we haven't run out of lives, decrement
public function DecrementLives(b : boolean){
	if (numLives > 0){
		numLives -= 1;
	}
}
public function IncrementCups(){
	numCups += 1;
}
// As long as we haven't run out, decrement the number of coffee cups
public function DecrementCups(){
	if (numCups > 0){
		numCups -= 1;
	}
}
// We got the key!
public function KeyAqcuired(){
	keyAcquired = true;
}

function OnGUI(){
	// Assign this GUI's skin to the skin assigned via inspector
	GUI.skin = guiSkin;
	// Create a GUI style for the score based on the skin's label
	var hudStyle : GUIStyle = GUI.skin.label;
	
	// Position HUD elemnts (hearts/cups/key) based on screen size
	GUI.DrawTexture(new Rect(Screen.width*3/64,      Screen.height*3/32,50,45), heart0.sprite.texture);
	GUI.DrawTexture(new Rect(Screen.width*3/64 + 64, Screen.height*3/32,50,45), heart1.sprite.texture);
	GUI.DrawTexture(new Rect(Screen.width*3/64 + 128,Screen.height*3/32,50,45), heart2.sprite.texture);
	
	GUI.DrawTexture(new Rect(Screen.width*3/64 + 200, Screen.height*3/32,50, 45), key.sprite.texture);
	
	GUI.DrawTexture(new Rect(Screen.width*3/64, Screen.height*6/32, 30, 25), coffeeCup_spr.texture);
	
	GUI.Label (new Rect(Screen.width*3/32,Screen.height*6/32,Screen.width*7/32,Screen.height*2/32), "x " + numCups.ToString(), hudStyle);
	
	// Change textures based on num lives
	if(numLives == 2){
		heart2 = heart_empty_sprr;
	}else if(numLives == 1){
		heart1 = heart_empty_sprr;
	}else if(numLives == 0){
		heart0 = heart_empty_sprr;
		Application.LoadLevel("LoseScene");
		deadP = true;
	}
	
	if(keyAcquired == true){
		key = key_acquired_sprr;
	}
}