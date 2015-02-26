using UnityEngine;
using System.Collections;

public class collisionHeadAnimation : MonoBehaviour {

	public GameObject objectToAnimate1 = null;
	public GameObject objectToAnimate2 = null;
	public int timesToAnimate = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		if (objectToAnimate1 != null) {
			Debug.Log (string.Format("Starting animations on {0}.", objectToAnimate1.name));
			RotationAnimation anim = objectToAnimate1.GetComponent<RotationAnimation> ();
			anim.playing = true;
			anim.timesToRepeat = timesToAnimate;
		}
		if (objectToAnimate2 != null) {
			Debug.Log (string.Format("Starting animations on {0}.", objectToAnimate2.name));
			RotationAnimation anim = objectToAnimate2.GetComponent<RotationAnimation> ();
			anim.playing = true;
			anim.timesToRepeat = timesToAnimate;
		}
	}
}
