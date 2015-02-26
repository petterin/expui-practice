using UnityEngine;
using System.Collections;

public class BabyCollisionAudioPlayer : MonoBehaviour {

	public string triggeringObjectPrefix;
	public AudioSource sourceToPause;
	public AudioSource sourceToPlayOnCollision;
	public bool destroyObject = true;
	public int pauseLength = 3;

	void Start () {
		sourceToPlayOnCollision = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other) {
		//Debug.Log(string.Format("BabyCollisionAudioPlayer {0} was hit with {1}.", this.name, other.name));
		if (other.name.StartsWith(triggeringObjectPrefix)) {
			sourceToPause.Pause();
			if (!sourceToPlayOnCollision.isPlaying) {
				sourceToPlayOnCollision.Play();
			}
			if (destroyObject) {
				//Debug.Log(string.Format("Deleting object {0}.", other.name));
				Destroy(other.gameObject);
			}
			StartCoroutine(resumePausedAfterDelay(sourceToPlayOnCollision.clip.length + pauseLength));
		}
	}

	IEnumerator resumePausedAfterDelay(float timeToWait)
	{
		//Debug.Log(string.Format("BabyCollisionAudioPlayer {0} was paused for {1} secs.", this.name, timeToWait));
		yield return new WaitForSeconds(timeToWait);
		sourceToPause.Play();
	}
}
