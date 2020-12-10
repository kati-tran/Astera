using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Linq;


public class AudioManager : MonoBehaviour
{
	public Sound[] sounds;
	public int[] playNow; // index of the sound

	void Awake()
	{
		foreach (Sound s in sounds)
		{
			if (s.playFrom == null)
			{
				s.source = gameObject.AddComponent<AudioSource>();
			}

			else
			{
				s.source = s.playFrom.AddComponent<AudioSource>();
			}
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
			s.source.spatialBlend = s.spatialBlend;
			s.source.spread = s.spread;
			s.source.minDistance = s.minDistance;
			s.source.maxDistance = s.maxDistance;
		}
	}

	void Start()
	{
		foreach (int i in playNow)
		{
			this.fadeIn(sounds[i].name, 1f);
			//this.Play(sounds[i].name);
		}

	}

	public void Play (string name)
	{
		//Debug.Log("playing "+ name);
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		s.source.Play();
	}

	public void Stop (string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		s.source.Stop();
	}

	public void fadeOut(string name, float fadeSpeed)
	{
		StartCoroutine(FadeOut(name, fadeSpeed));
	}

	public IEnumerator FadeOut(string name, float fadeSpeed)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
 		float currentVolume = s.source.volume;

        // Check Music Volume and Fade Out
        while (s.source.volume > 0.01f)
        {
            s.source.volume -= Time.deltaTime / fadeSpeed;//secondsToFadeOut;
            yield return null;
        }
 
        // Make sure volume is set to 0
        s.source.volume = 0;
 
        // Stop Music
        s.source.Stop();

        //s.source.volume = currentVolume;
    }

    public void fadeIn(string name, float fadeSpeed)
    {
    	StartCoroutine(FadeIn(name, fadeSpeed));
    }

    public IEnumerator FadeIn(string name, float fadeSpeed)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        float currentVolume = s.source.volume;
        s.source.volume = 0;
        s.source.Play();
        while(s.source.volume < currentVolume)
        {
            s.source.volume = Mathf.Lerp(s.source.volume, currentVolume, fadeSpeed * Time.deltaTime);
            yield return currentVolume;
        }
        s.source.volume = currentVolume;

    }


    public bool isPlaying(string name)
    {
    	Sound s = Array.Find(sounds, sound => sound.name == name);
    	return s.source.isPlaying;
    }

}
