using UnityEngine;
using System.Collections;

public class GameAudioController : MonoBehaviour {
	
	public static AudioSource mainAudioSource;
	
	public AudioSource localSource;
	
	// Use this for initialization
	void Start () {
		mainAudioSource = localSource;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static void playOneShotSound(AudioClip clip){
		mainAudioSource.PlayOneShot(clip);
	}
	
	public static void playOneShotSound(AudioClip clip, float volumeScale){
		mainAudioSource.PlayOneShot(clip, volumeScale);
	}
}
