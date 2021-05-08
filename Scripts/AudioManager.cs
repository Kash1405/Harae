// Adapted from Brackey's AudioManager Tutorial on YouTube
// Introduction to AUDIO in Unity - https://www.youtube.com/watch?v=6OT43pvUyfY


using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public Sound[] sounds;
	
    void Awake()
    {
        foreach(Sound gameSound in sounds)
		{
			 gameSound.source = gameObject.transform.GetChild(0).gameObject.AddComponent<AudioSource>();
			 gameSound.source.clip =  gameSound.clip;
			 gameSound.source.loop =  gameSound.loop;
			
			 gameSound.source.volume =  gameSound.volume;
			 gameSound.source.pitch =  gameSound.pitch;
		}
    }
	
	void Start()
	{
		Play("Theme");
	}
	
	public void Stop (string clipName)
	{
		Sound gameSound = Array.Find(sounds, sound => sound.clipName == clipName);
		if(gameSound == null) return;
		 gameSound.source.Stop();
	}

	public void Play (string clipName)
	{
		Sound gameSound = Array.Find(sounds, sound => sound.clipName == clipName);
		if(gameSound == null) return;
		 gameSound.source.Play();
	}
}
