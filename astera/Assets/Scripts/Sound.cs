using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
	public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    [Range(0f, 1f)]
    public float spatialBlend;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

    // for 3D
    public GameObject playFrom;
    public float spread;

    [Range(0f, 100f)]
    public float minDistance;
    [Range(0f, 200f)]
    public float maxDistance;



}
