using UnityEngine;
using System.Collections;

public class AirBoat : MonoBehaviour {

	public bool isBoat = false; 
	public float force = 0.0f; 
	public float maxSpeed = 0.0f; 
	public float turnForce = 0.0f; 
	public float maxTurnSpeed = 0.0f; 
	public float changingAltitudeForce = 0.0f; 
	public float maxChangingAltitudeSpeed = 0.0f; 
	public float maxHeight = 0.0f; 
	public float maxDepth = 0.0f; 
	public float softener = 0.0f; 
	//public float curSpeed = 0.0f; 
	//var props : Transform[]; 
	private float inputZ = 0.0f; 
	private float inputX = 0.0f; 
	private float inputY = 0.0f; 
	public float inertia = 1.0f;
	public float degreesTiltOnFullTurn = 30.0f;
	public float TiltSmoothing = 1.0f;

	// Use this for initialization
	void Start () {

		GetComponent<Rigidbody>().maxAngularVelocity = maxTurnSpeed; 
	}
	
	// Update is called once per frame
	void Update () {
		//curSpeed = Mathf.Clamp(GetComponent<Rigidbody>().velocity.magnitude, 0, maxSpeed); 
		// backwards or forwards? 
		inputZ = -Input.GetAxis("Vertical"); 
		//turn left or right? 
		inputX = Input.GetAxis("Horizontal"); 
		// up or down? 
		//inputY = Input.GetAxis("UpAndDown"); 

		if (inputZ > 0.0 ) 
		{ 
			GetComponent<Rigidbody>().AddRelativeForce (0, 0, force*Time.deltaTime); 
			//following commented out line is for analog stick movement
			//GetComponent<Rigidbody>().AddRelativeForce (0, 0, (force * inputZ));
		} 
		else if (inputZ < 0.0) 
		{ 
			GetComponent<Rigidbody>().AddRelativeForce (0, 0, -force*Time.deltaTime); 
		}
		
		if (inputX > 0.0) 
		{ 
			GetComponent<Rigidbody>().AddRelativeTorque (0, turnForce*Time.deltaTime, 0); 
		} 
		else if (inputX < 0.0) 
		{ 
			GetComponent<Rigidbody>().AddRelativeTorque (0, -turnForce*Time.deltaTime, 0); 
		}
		if (!isBoat) 
		{ 
			if (inputY > 0.0 && GetComponent<Rigidbody>().velocity.y < maxChangingAltitudeSpeed) 
			{ 
				GetComponent<Rigidbody>().AddRelativeForce (0, changingAltitudeForce*Time.deltaTime, 0); 
			} 
			else if (inputY < 0.0 && GetComponent<Rigidbody>().velocity.y > (0.0 - maxChangingAltitudeSpeed)) 
			{ 
				GetComponent<Rigidbody>().AddRelativeForce (0, -changingAltitudeForce*Time.deltaTime, 0); 
			}
		}


		//Don't think this is even used?
		//pos.x and pos.z are setting directly using rotation values and then never referenced again?
		//Commenting this below block out because seriously no idea what is going on with it.
		/*
		Vector3 pos = transform.position;
		pos.y = Mathf.Clamp(transform.position.y, maxDepth, maxHeight); 
		pos.x = Mathf.Clamp(transform.rotation.x, 0, 0); 
		pos.z = Mathf.Clamp(transform.rotation.z, 0, 0);
		*/
		
		// stop sliding
		inertia = Mathf.Clamp (inertia, 0.1f, inertia);
		GetComponent<Rigidbody>().AddForce(-Vector3.Project(GetComponent<Rigidbody>().velocity, transform.right) * (GetComponent<Rigidbody>().mass * 5 / inertia) );

		//soften are movment 
		/*
		if(inputX == 0.0f) 
			GetComponent<Rigidbody>().angularVelocity *= softener; 
		if (inputY == 0.0f) {
			Vector3 forwardBreak = GetComponent<Rigidbody> ().velocity;
			forwardBreak.y *= softener; 
			GetComponent<Rigidbody> ().velocity = forwardBreak;
		}

		if(inputZ == 0.0f) 
		{  
			Vector3 turnAngleBreak = GetComponent<Rigidbody> ().velocity;
			turnAngleBreak.x *= softener; 
			GetComponent<Rigidbody> ().velocity = turnAngleBreak;
			turnAngleBreak.z *= softener; 
			GetComponent<Rigidbody> ().velocity = turnAngleBreak;
		}
		*/
	}
}
