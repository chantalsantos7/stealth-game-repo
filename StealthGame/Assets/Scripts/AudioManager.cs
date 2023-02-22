using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class AudioClipsGroup
    {
        public string name;
        public AudioClip[] audioClips;
    }

    //public List<AudioClip> audioClips = new List<AudioClip>();
    public List<AudioClipsGroup> audioClips = new List<AudioClipsGroup>();
    //public List<AudioClip> audioLoops = new List<AudioClip>();

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(string name) //reference SFB_AudioManager PlayAudio function
    {
        //play clips depending on movement - called from animation events
        //audioClips while moving
        //swing of daggers
    }


    /*private AudioClip GetRandomClip()
    {
        return audioClips[Random.Range(0, audioClips.Count)];
    }

    private int AudioClipIndex(string name)
    {

    }*/
}
