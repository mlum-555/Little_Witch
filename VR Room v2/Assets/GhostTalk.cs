using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTalk : MonoBehaviour
{

    public AudioClip[] talkClips;
    // Start is called before the first frame update

    public float pitchMod;

    public AudioSource talkSource;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //once it's done playing, play a new clip
    public void playDialogue()
    {
        if (!talkSource.isPlaying)
        {
            talkSource.clip = chooseClip();
            talkSource.pitch = Random.Range(0.8f,1.2f) + pitchMod;
            talkSource.Play();

        }
    }

    AudioClip chooseClip()
    {
        return talkClips[Random.Range(0, talkClips.Length)];

    }
}
