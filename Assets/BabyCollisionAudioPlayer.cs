using UnityEngine;
using System.Collections;

public class BabyAudioPauser : MonoBehaviour {

	public GameObject objectToTrigger1;
	public GameObject objectToTrigger2;
	public AudioSource sourceToPause;
	public AudioSource sourceToPlayOnCollision;

	void Start () {
		sourceToPlayOnCollision = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject == objectToTrigger1 || other.gameObject == objectToTrigger2) {
			sourceToPause.Pause();
			sourceToPlayOnCollision.PlayOneShot(sourceToPlayOnCollision.clip);
			Destroy(other.gameObject);
			StartCoroutine(resumePausedAfterDelay(sourceToPlayOnCollision.clip.length));
		}
	}

	IEnumerator resumePausedAfterDelay(float timeToWait)
	{	
		yield return new WaitForSeconds(timeToWait);
		sourceToPause.Play();
	}
}
