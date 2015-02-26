using UnityEngine;
using System.Collections;

public class RotationAnimation : MonoBehaviour {

	public int maxAngle = 40;
	public int minAngle = -20;
	public int rotationStep = 20;

	bool playing = false;
	float initialAngle = 0.0f;
	
	// Animation initialization
	void Start () {
		playing = true;
		initialAngle = transform.eulerAngles.z;
	}

	// Update is called once per frame
	void Update () {
		if (playing) {
			if ((getAngleDifference() < minAngle && rotationStep < 0) ||
			    (getAngleDifference() > maxAngle && rotationStep > 0)) {
				rotationStep *= -1; // reverse rotation
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
