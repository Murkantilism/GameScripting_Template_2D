/*
This camera smoothes out rotation around the y-axis
Horizontal Distance to the target is always fixed.

There are many different ways to smooth the rotation but doing it this way gives you a lot of control over how the camera behaves.

For every of those smoothed values we calculate the wanted value and the current value.
Then we smooth it using the Lerp function.
Then we apply the smoothed values to the transform's position.
*/

// The target we are following
var target : Transform;
// The distance in the x-z plane to the target
var distance = 10.0;
var rotationDamping = 3.0;

var deadp : boolean = false;

// Place the script in the Camera-Control group in the component menu
@script AddComponentMenu("Camera-Control/Smooth Follow")

// Used to recieve message from Teli_Animation.cs
function death(bool : boolean){
	deadp = bool;
}

function LateUpdate () {
	// If Teli dies, freeze camera
	if(deadp == true){
		target = null;
	}else{
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	// Early out if we don't have a target
	if (!target)
		return;
	
	// Calculate the current rotation angles
	var wantedRotationAngle = target.eulerAngles.y;
		
	var currentRotationAngle = transform.eulerAngles.y;
	
	// Damp the rotation around the y-axis
	currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

	// Convert the angle into a rotation
	var currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
	
	// Set the x and z position of the camera to the player, ignore y (for side-scroller effect)
	transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
	transform.position -= Vector3.forward * distance;
	transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
}