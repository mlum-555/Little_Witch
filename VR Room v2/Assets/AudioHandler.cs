using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{

    public List<AudioClip> clipList = new List<AudioClip>();


    public AudioClip fallSound, ghostSound, entranceSound;

    public float ghostSoundLevel = 0.5f;

    public AudioSource fallSource;

   
    /*sounds to get:
     * 
     * cauldron sound
     * stirring sound
     * some kind of goodness sound
     * 
     * general ambiance
     * 
     */
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void objectFall(GameObject fallenObj)
    {

        
        AudioSource.PlayClipAtPoint(fallSound, fallenObj.transform.position);


    }


    public void giveSound(GameObject obj)
    {
        //placeholder here for just giving ghosts sound I guess
        AudioSource newSorc = obj.AddComponent<AudioSource>();
        newSorc.loop = true;
        newSorc.clip = ghostSound;
        newSorc.spatialBlend = 1;
        newSorc.volume = ghostSoundLevel;
        newSorc.Play();
        //HOW DO YOU DELETE OLD SOUND STUFF?
    }

    public void playEntrance(GameObject entrance)
    {
        AudioSource.PlayClipAtPoint(entranceSound, entrance.transform.position);
    }
}
