using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //public List<AudioClip> audioClips = new List<AudioClip>();
    public List<AudioClip> footsteps = new List<AudioClip>();
    //public List<AudioClip> audioLoops = new List<AudioClip>();

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(string name) //reference SFB_AudioManager PlayAudio function
    {
        //play clips depending on movement - called from animation events
        //footsteps while moving
        //swing of daggers
    }

    public void PlayFootsteps()
    {
        var clip = GetRandomClip();
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    private AudioClip GetRandomClip()
    {
        return footsteps[Random.Range(0, footsteps.Count)];
    }
}
