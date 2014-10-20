using UnityEngine;
using System.Collections;

public class Enemies : MonoBehaviour {
	public Vector3 target;
	public Vector3 origin;
	
	bool pathing = false;
	
	float moveSpeed = 1.0f;
	
	// Use this for initialization
	void Start () {
	/*
		// Define path array with max length of 2 members
		path = new Vector3[2];
		// Set the first member of the array to 5 units in front of the enemy
		path[0] = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
		// Set the second member of the array to the original position
		path[1] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		*/
		target = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
		origin = transform.position;
		StartCoroutine(MoveToPosition(target));
		InvokeRepeating("SwitchTarget", 0, 5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// Move the enemy to the given position
	public IEnumerator MoveToPosition(Vector3 target){
		while(transform.position != target){
			transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
			
			if(pathing == false){
				target = origin;
			}if (pathing == true){
				target = new Vector3(origin.x + 5, origin.y, origin.z);;
			}
			
			yield return 0;
		}
	}
	
	void SwitchTarget(){
		pathing = !pathing;
	}
}
