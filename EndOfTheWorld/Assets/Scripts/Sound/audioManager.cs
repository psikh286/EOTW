using System;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public Sound[] sounds;
	
    public static audioManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        foreach (Sound s in sounds)
        {
            s.source = gameObject.GetComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
    }

	public void Play(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        
        s.source.PlayOneShot(s.clip); 
	}
}
