﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	HUD hud;
	
	// movement config
	public float gravity = -15f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	// public float jumpWaitTime = 2.0;
	
	[HideInInspector]
	public float rawMovementDirection = 1;
	//[HideInInspector]
	public float normalizedHorizontalSpeed = 0;
	
	CharacterController2D _controller;
	Animator _animator;
	public RaycastHit2D lastControllerColliderHit;
	
	[HideInInspector]
	public Vector3 velocity;
	
	// Use this for initialization
	void Start () {
		hud = GameObject.Find("HUD").GetComponent<HUD>();
	}
	
	void Awake(){
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();
		_controller.onControllerCollidedEvent += onControllerCollider;
	}
	
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
			hud.DecrementLives(true);
		}
		
		if(Input.GetKeyUp(KeyCode.Z)){
			hud.KeyAqcuired();
		}
		
		if(Input.GetKeyUp(KeyCode.D)){
			hud.IncrementCups();
		}
		
		if(Input.GetKeyUp(KeyCode.F)){
			hud.DecrementCups();
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
			
			//if( _controller.isGrounded )
				//_animator.Play( Animator.StringToHash( "Run" ) );
		}
		else if( Input.GetKey( KeyCode.LeftArrow ) )
		{
			normalizedHorizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
			
			//if( _controller.isGrounded )
				//_animator.Play( Animator.StringToHash( "Run" ) );
		}
		else
		{
			normalizedHorizontalSpeed = 0;
			
			//if( _controller.isGrounded )
				//_animator.Play( Animator.StringToHash( "Idle" ) );
		}
		
		
		if( Input.GetKeyDown( KeyCode.UpArrow ) )
		{
			//to avoid DOUBLE JUMP
			if( _controller.isGrounded ) {
				var targetJumpHeight = 2f;
				velocity.y = Mathf.Sqrt( 2f * targetJumpHeight * -gravity );  
				//_animator.Play( Animator.StringToHash( "Jump" ) ); 
			}
		}
		
		
		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		velocity.x = Mathf.Lerp( velocity.x, normalizedHorizontalSpeed * rawMovementDirection * runSpeed, Time.deltaTime * smoothedMovementFactor );
		
		// apply gravity before moving
		velocity.y += gravity * Time.deltaTime;
		
		_controller.move( velocity * Time.deltaTime );
	}
}