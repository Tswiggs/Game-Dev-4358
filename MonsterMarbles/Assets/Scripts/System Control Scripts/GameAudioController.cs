using UnityEngine;
using System.Collections;

public class GameAudioController : MonoBehaviour {
	
	public static AudioSource mainAudioSource;
	public static AudioSource backgroundAudioSource;
	
	public AudioSource localSource;
	public AudioSource backgroundMusic;
	
	
	// Use this for initialization
	void Start () {
		mainAudioSource = localSource;
		backgroundAudioSource = backgroundMusic;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static void playOneShotSound(AudioClip clip){
		if(clip != null){
			mainAudioSource.PlayOneShot(clip);
		}
	}
	
	public static void playOneShotSound(AudioClip clip, float volumeScale){
		if(clip != null){
			mainAudioSource.PlayOneShot(clip, volumeScale);
		}
	}
	
	public static void pauseBackgroundMusic(){
		backgroundAudioSource.Pause();
	}
	
	public static void resumeBackgroundMusic(){
		backgroundAudioSource.Play();
	}
}
