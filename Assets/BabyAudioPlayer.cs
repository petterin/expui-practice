using UnityEngine;
using System.Collections;

public class BabyAudioPlayer : MonoBehaviour {
	public AudioSource crying;
	public AudioSource hungry;
	bool keepPlaying = true;
	int hungryInterval = 20;

	// Use this for initialization
	void Start () {
		StartCoroutine(SoundOut());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator SoundOut()
	{	
		while (keepPlaying){
			crying.Pause();
			hungry.PlayOneShot(hungry.clip);
			StartCoroutine(CryingResume(hungry.clip.length));
			yield return new WaitForSeconds(hungryInterval + hungry.clip.length);
		}
	}

	IEnumerator CryingResume(float timeToWait)
	{	
		yield return new WaitForSeconds(timeToWait);
		crying.Play();
	}
}
