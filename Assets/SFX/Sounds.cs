using UnityEngine;

[System.Serializable]
public class Sounds
{
    public string fileName;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(1f, 3f)]
    public float pitch;
    [HideInInspector]
    public AudioSource source;
    public bool loop;
}
