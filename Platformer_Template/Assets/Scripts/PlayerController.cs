﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	GameObject hud;
	
	// movement config
	public float gravity = -15f;
	public float runSpeed = 6f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 1.5f;
	// public float jumpWaitTime = 2.0;
	
	[HideInInspector]
	public float rawMovementDirection = 1;
	//[HideInInspector]
	public float normalizedHorizontalSpeed = 0;
	
	CharacterController2D _controller;
	public Animator _animator;
	public RaycastHit2D lastControllerColliderHit;
	
	[HideInInspector]
	public Vector3 velocity;
	
	// Use this for initialization
	void Start () {
		hud = GameObject.Find("HUD");
	}
	
	void Awake(){
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();
		_controller.onControllerCollidedEvent += onControllerCollider;
	}
	
	#region eventlisteners
	void IncrementRunSpeed(){
		runSpeed += 1;
	}
	#endregion
	
	void onControllerCollider( RaycastHit2D hit ){
		// bail out on plain old ground hits
		if( hit.normal.y == 1f )
			return;
		
		// logs any collider hits
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}
	
	
	void Update()
	{
		if(Input.GetKeyUp(KeyCode.A)){
			hud.SendMessage("DecrementLives", true);
		}
		
		if(Input.GetKeyUp(KeyCode.Z)){
			hud.SendMessage("KeyAqcuired");
		}
		
		if(Input.GetKeyUp(KeyCode.Space)){
			hud.SendMessage("DecrementCups");
			SmashCoffee();
		}
		
		// grab our current velocity to use as a base for all calculations
		velocity = _controller.velocity;
		
		if( _controller.isGrounded )
			velocity.y = 0;
		
		if( Input.GetKey( KeyCode.RightArrow ) )
		{
			normalizedHorizontalSpeed = 1;
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
			
			if( _controller.isGrounded )
				_animator.Play( Animator.StringToHash( "run" ) );
		}
		else if( Input.GetKey( KeyCode.LeftArrow ) )
		{
			normalizedHorizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
			
			if( _controller.isGrounded )
				_animator.Play( Animator.StringToHash( "run" ) );
		}
		else
		{
			normalizedHorizontalSpeed = 0;
			
			if( _controller.isGrounded )
				_animator.Play( Animator.StringToHash( "idle" ) );
		}
		
		
		if( Input.GetKeyDown( KeyCode.UpArrow ) )
		{
			//to avoid DOUBLE JUMP
			if( _controller.isGrounded ) {
				velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );  
				_animator.Play( Animator.StringToHash( "jump" ) ); 
			}
		}
		
		
		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		velocity.x = Mathf.Lerp( velocity.x, normalizedHorizontalSpeed * rawMovementDirection * runSpeed, Time.deltaTime * smoothedMovementFactor );
		
		// apply gravity before moving
		velocity.y += gravity * Time.deltaTime;
		
		_controller.move( velocity * Time.deltaTime );
	}
	
	void SmashCoffee(){
		Debug.Log("COFFEE SMASH!");
	}
}