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

    public GameObject[] ghosts;
    Ghost[] customerList;



    int ghostCount = 0;

    AudioHandler audioHandler;


    void Start()
    {

        audioHandler = FindFirstObjectByType<AudioHandler>();

        customerList = new Ghost[ghosts.Length];
        for (int i = 0; i < ghosts.Length; i++)
        {
            customerList[i] = ghosts[i].GetComponentInChildren<Ghost>();
            if (customerList[i] == null) Debug.Log("ghost not found");

            customerList[i].gameObject.SetActive(false);
            ghosts[i].SetActive(false);
            //oh god wait no this is the same issue as last time wehre the thing fails
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
        ghosts[ghostCount].gameObject.SetActive(true);
        customerList[ghostCount].gameObject.SetActive(true);

        //ohhh it's because um. the base is being set active before the other stuff? I think? I could be wrong

        customerList[ghostCount].setPos(LeaveDest.transform.position);
        //ok so position is being set here. now what
        giveGhostSound(customerList[ghostCount].gameObject);
        customerList[ghostCount].startMoving();
    }

    public void nextGhost()
    {
        customerList[ghostCount].gameObject.SetActive(false);
        ghosts[ghostCount].SetActive(false);

        ghostCount++;

        ghosts[ghostCount].SetActive(true);
        customerList[ghostCount].gameObject.SetActive(true);

        customerList[ghostCount] = ghosts[ghostCount].GetComponentInChildren<Ghost>();

        customerList[ghostCount].setPos(LeaveDest.transform.position);

        audioHandler.playEntrance(LeaveDest);

        giveGhostSound(customerList[ghostCount].gameObject);
        //also put them at the uhh starting position

        //I think the problem is that the starting position should only be tied to um the   
    }


    //deactivate current ghost once they leave or something idk that's a stretch goal
    //for now just activate the new ghost & such
    //should they just. yeah guess so

    public GameObject getExitPoint()
    {
        return LeaveDest;
    }
}
