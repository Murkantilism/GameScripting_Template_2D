using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	public int numLives = 3; // The number of starting lives
	public int numCups = 0; // The number of starting coffee cups
	bool keyAcquired = false; // Has the key been acquired?
	bool deadP = false; // Has our hero croaked?
	
	public SpriteRenderer heart0_sprr; // Sprr = Sprite Renderer
	public SpriteRenderer heart1_sprr;
	public SpriteRenderer heart2_sprr;
	
	public SpriteRenderer heart_empty_sprr; // Spr = Sprite
	
	SpriteRenderer heart0;
	SpriteRenderer heart1;
	SpriteRenderer heart2;
	
	public SpriteRenderer key_empty_sprr;
	public SpriteRenderer key_acquired_sprr;
	
	SpriteRenderer key;
	
	public Sprite coffeeCup_spr;
	
	public GUISkin guiSkin;

	// Use this for initialization
	void Start () {
		// Set starting HUD textures
		heart0 = heart0_sprr;
		heart1 = heart1_sprr;
		heart2 = heart2_sprr;
		
		key = key_empty_sprr;
	}
	
	#region EventListeners
	// As long as we haven't run out of lives, decrement
	public void DecrementLives(bool b){
		if (numLives > 0){
			numLives -= 1;
		}
	}
	public void IncrementCups(){
		numCups += 1;
	}
	// As long as we haven't run out, decrement the number of coffee cups
	public void DecrementCups(){
		if (numCups > 0){
			numCups -= 1;
		}
	}
	// We got the key!
	public void KeyAqcuired(){
		keyAcquired = true;
	}
	#endregion
	
	void OnGUI(){
		// Assign this GUI's skin to the skin assigned via inspector
		GUI.skin = guiSkin;
		// Create a GUI style for the score based on the skin's label
		GUIStyle hudStyle = GUI.skin.label;
		
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
}