using UnityEngine;
using System.Collections;

public class CircleFollowing : MonoBehaviour {
	float mass;
	float maxSpeed;
	Vector3 velocity = Vector3.zero;
	Vector3 force = Vector3.zero;
	int currentWaypoint = 0;
	public GameObject[] waypoints;//i can also edit this array in the unity editor
	public float radius = 10;
	//float theta = (3.141592654*2)/10;
	float theta = (Mathf.PI*2)/10;//i guessed it :) y = 10*costheta x = 10 * sintheta
	int currentPathPosition = 0;//this keeps track of what path point the sphere is currently seeking
	// Use this for initialization

	public void CreatePath (){
		int i = 0;
		for(float angle = 0; angle <=2*Mathf.PI; angle = angle + theta){

			float xPos  = 10*Mathf.Sin (angle);
			float yPos = 0f;
			float zPos  = 10*Mathf.Cos (angle);
			Vector3 temp = new Vector3(xPos,yPos,zPos);
			waypoints[i].transform.position = temp;
			i = i + 1;

		}

	}

	public void DrawPath (){
		for(int j = 0; j< waypoints.Length;j++){
			if(j<waypoints.Length-1){//draw first 9 lines
				Debug.DrawLine (waypoints[j].transform.position,waypoints[j+1].transform.position,Color.green);
			}

			if(j == (waypoints.Length)-1){//draw line back to start point
				Debug.DrawLine (waypoints[j].transform.position,waypoints[0].transform.position,Color.green);

			}
			
		}
		
	}

	Vector3 Seek(Vector3 target){//seek method
		Vector3 desired = target-transform.position;
		desired.Normalize();
		desired = desired*maxSpeed;
		return(desired-velocity);
	}

	Vector3 FollowPath(){
		if((waypoints[currentPathPosition].transform.position-transform.position).magnitude<1f){//distance between path point and object <1
			if(currentPathPosition == waypoints.Length-1){
				currentPathPosition = 0;
			}
			currentPathPosition = currentPathPosition+1;
		}
		return(Seek(waypoints[currentPathPosition].transform.position));
	}

	void Start () {
		CreatePath ();
		mass = 1;
		maxSpeed = 5;

	}
	
	// Update is called once per frame
	void Update () {
		DrawPath();
		force = force+FollowPath();
		//force = force+Seek ();//testing
		Vector3 accel = force/mass;
		velocity = velocity + accel*Time.deltaTime;
		transform.position= transform.position+ velocity*Time.deltaTime;
		force = Vector3.zero;
		if(velocity.magnitude>float.Epsilon){
			transform.forward = Vector3.Normalize(velocity);
		}
		velocity = velocity*0.99f;//damping

	
	}
}
