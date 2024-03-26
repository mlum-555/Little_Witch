using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{


    //    int[] eventsList = { 0, 1, 2, 3 };

    int totalEvents = 5;

    bool[] eventBools;


    // Start is called before the first frame update
    void Start()
    {
        eventBools = new bool[totalEvents];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void completeEvent(int id)
    {
        eventBools[id] = true;
        Debug.Log("Event " + id+" complete");

        //wait um.... uhhh
    }


    public bool checkEvent(int id)
    {
      if (eventBools[id])
        {
            return true;
        }
      return false;
    }

    //list of events, check if completed
}