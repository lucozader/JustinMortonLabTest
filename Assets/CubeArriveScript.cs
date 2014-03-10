using UnityEngine;
using System.Collections;

public class CubeArriveScript : MonoBehaviour {
		float mass;
		float maxSpeed;
		Vector3 velocity = Vector3.zero;
		Vector3 force = Vector3.zero;
		Vector3 randomInsideSphere = Vector3.zero;//cube will arrive at this point
		// Use this for initialization

		
		void Start () {
			randomInsideSphere= 10*Random.insideUnitSphere;
			mass = 1;
			maxSpeed = 5;
			
		}

		Vector3 Arrive(Vector3 target){
			Vector3 desired = target-transform.position;
			float distance = desired.magnitude;
			float slowDistance = 2;
			float deaccelerationAdjust = 10;
			if(distance == 0){
				return Vector3.zero;
			}
			float rampedspeed = maxSpeed*(distance/(slowDistance*deaccelerationAdjust));
			float clampedspeed = Mathf.Min(maxSpeed,rampedspeed);
			Vector3 desiredvelocity  = desired*(clampedspeed/distance);
			Debug.DrawLine(transform.position,randomInsideSphere,Color.red);
			return desiredvelocity-velocity;
		}

		
		// Update is called once per frame
		void Update () {
			force = force+Arrive(randomInsideSphere);
			Vector3 accel = force/mass;
			velocity = velocity + accel*Time.deltaTime;
			transform.position= transform.position+ velocity*Time.deltaTime;
			force = Vector3.zero;
			if(velocity.magnitude>float.Epsilon){
				transform.forward = Vector3.Normalize(velocity);//facing
			}
			velocity = velocity*0.99f;//damping
			float tempDistance = (randomInsideSphere-transform.position).magnitude;//distance between cube and the point that it is travelling to
			if(tempDistance<1){
			randomInsideSphere= 10*Random.insideUnitSphere;//create new random point to travel to
			}	

			
		}
	}
