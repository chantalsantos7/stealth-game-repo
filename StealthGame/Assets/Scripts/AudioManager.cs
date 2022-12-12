using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
    public List<AudioClip> audioLoops = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio(string name) //reference SFB_AudioManager PlayAudio function
    {
        //play clips depending on movement - called from animation events
        //footsteps while moving
        //swing of daggers

    }
}
