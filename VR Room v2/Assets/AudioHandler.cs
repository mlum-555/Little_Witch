using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{

    public List<AudioClip> clipList = new List<AudioClip>();


    public AudioClip fallSound, ghostSound, entranceSound, pickupSound, glassFallSound,gloveTightenSound, solidFallSound, poofSound,teleportSound;

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


    public void objectFall(GameObject fallenObj, bool isGlass)
    {


        if (isGlass)
        {
            AudioSource.PlayClipAtPoint(glassFallSound, fallenObj.transform.position);

        }
        else
        {
            AudioSource.PlayClipAtPoint(fallSound, fallenObj.transform.position);

        }


    }

    public void solidFall(GameObject fallenObj)
    {
        AudioSource.PlayClipAtPoint(solidFallSound, fallenObj.transform.position);
    }
    public void gloveTighten(GameObject glove)
    {
        AudioSource.PlayClipAtPoint(gloveTightenSound,glove.transform.position);
    }
    public void playPickupSound(GameObject pickupObj)
    {
        AudioSource.PlayClipAtPoint(pickupSound, pickupObj.transform.position);
    }

    public AudioSource giveSound(GameObject obj)
    {
        //placeholder here for just giving ghosts sound I guess
        AudioSource newSorc = obj.AddComponent<AudioSource>();
        //ah. ok one sec
        newSorc.loop = true;
        newSorc.clip = ghostSound;
        newSorc.spatialBlend = 1;
        newSorc.volume = ghostSoundLevel;
        newSorc.Play();
        return newSorc;
        //HOW DO YOU DELETE OLD SOUND STUFF?
    }

    public void playPoof(GameObject obj)
    {
        AudioSource.PlayClipAtPoint(poofSound, obj.transform.position);
    }


    public void playTeleport (GameObject teleporter)
    {
        AudioSource.PlayClipAtPoint(teleportSound, teleporter.transform.position);
    }

    public void playEntrance(GameObject entrance)
    {
        AudioSource.PlayClipAtPoint(entranceSound, entrance.transform.position);
    }
}
