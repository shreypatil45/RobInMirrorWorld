using System;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    public Sounds[] sounds;

    public bool IsMobile;
    // Start is called before the first frame update
    private void Awake()
    {
        if(FindObjectsOfType<Audiomanager>().Length > 1)
        {
            Destroy(gameObject);
        }    

        DontDestroyOnLoad(gameObject);
        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    void Start()
    {
        PlaySound("Music");
    }

    private void Update()
    {
        

    }
    public void PlaySound(string name)
    {
        Sounds s = Array.Find(sounds, sounds => sounds.fileName == name);
        if (s == null)
        {
            Debug.LogWarning("sound" + name + "not found");
            return;
        }
        s.source.Play();
    }
    
    public void StopSound(string name)
    {
        Sounds s = Array.Find(sounds, sounds => sounds.fileName == name);
        if (s == null)
        {
            Debug.LogWarning("sound" + name + "not found");
            return;
        }
        s.source.Stop();
    }
    public void SetVolume(string name,float x)
    {
        Sounds s = Array.Find(sounds, sounds => sounds.fileName == name);
        if (s == null)
        {
            Debug.LogWarning("sound" + name + "not found");
            return;
        }
        s.source.volume = x;
    }

}

