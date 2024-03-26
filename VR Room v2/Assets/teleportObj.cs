using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportObj : MonoBehaviour
{
    // Start is called before the first frame update

    AudioHandler audioHandler;
    void Start()
    {
        audioHandler = FindAnyObjectByType<AudioHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void teleportNoise()
    {
        audioHandler.playTeleport(this.gameObject);
    }
}
