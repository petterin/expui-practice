using UnityEngine;
using System.Collections;

public class RotationAnimation : MonoBehaviour {

	public int maxAngle = 40;
	public int minAngle = -20;
	public int rotationStep = 20;
	public int timesToRepeat = 0; // 0 = infinite
	public bool playing = true;

	int halfRounds = 0;
	float initialAngle = 0.0f;
	bool returningToInitial = false;
	float tolerance = 1.0f;
	
	// Animation initialization
	void Start () {
		initialAngle = transform.eulerAngles.z;
	}

	// Update is called once per frame
	void Update () {
		// Condition to finish
		if (timesToRepeat > 0 && halfRounds >= 2*timesToRepeat) {
			returningToInitial = true;
		}

		// Condition to stop
		if (returningToInitial) {
			if (Mathf.Abs(getAngleDifference()) <= tolerance) {
				returningToInitial = false;
				playing = false;
				halfRounds = 0;
			} else if (getAngleDifference() > 0) {
				rotationStep = -Mathf.Abs(rotationStep);
			} else if (getAngleDifference() < 0) {
				rotationStep = Mathf.Abs(rotationStep);
			}
		}


		if (playing) {
			// Rotating
			if ((!returningToInitial) &&
				(
					(getAngleDifference() < minAngle && rotationStep < 0) ||
			    	(getAngleDifference() > maxAngle && rotationStep > 0)
				)) {
				rotationStep *= -1; // reverse rotation
				halfRounds++; // One half animation round completed
				//Debug.Log ("Animation (half) count is " + halfRounds);
			}
			transform.Rotate(new Vector3(0, 0, rotationStep) * Time.deltaTime);
		}
	}

	float getAngleDifference() {
		// eulerAngles are 0...360, never negative or over 360
		float diff = transform.eulerAngles.z - initialAngle;
		if (diff > 180) {
			return diff - 360;
		} else {
			return diff;
		}
	}

}
