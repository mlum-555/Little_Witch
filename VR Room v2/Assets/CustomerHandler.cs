using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerHandler : MonoBehaviour
{
    // Start is called before the first frame update



    /*
     * List of ghosts
     * NextGhost function called by each ghost (or not)
     * Ghosts should be able to turn around and leave the shop
     * Universal shop leave point
     * Some event when they uh yeah
     * 
     * */

    public GameObject LeaveDest;
    public Ghost[] CustomerList;

    int ghostCount = 0;

    AudioHandler audioHandler;


    void Start()
    {
        audioHandler = FindFirstObjectByType<AudioHandler>();

        foreach (Ghost g in CustomerList) //turn off all inactive ghosts
        {
            g.gameObject.SetActive(false);

        }
        firstGhost();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //add a looping audio source, then destroy it?

    void giveGhostSound(GameObject sorcGhost)
    {
        if (audioHandler != null)
        {
            audioHandler.giveSound(sorcGhost);
        }
    }

    void firstGhost()
    {
        CustomerList[ghostCount].gameObject.SetActive(true);
        CustomerList[ghostCount].setPos(LeaveDest.transform.position);
        giveGhostSound(CustomerList[ghostCount].gameObject);


    }

    public void nextGhost()
    {
        CustomerList[ghostCount].gameObject.SetActive(false);
        ghostCount++;
        CustomerList[ghostCount].gameObject.SetActive(true);
        CustomerList[ghostCount].setPos(LeaveDest.transform.position);

        audioHandler.playEntrance(LeaveDest);

        giveGhostSound(CustomerList[ghostCount].gameObject);
        //also put them at the uhh starting position
    }


    //deactivate current ghost once they leave or something idk that's a stretch goal
    //for now just activate the new ghost & such
    //should they just. yeah guess so

    public GameObject getExitPoint()
    {
        return LeaveDest;
    }
}
