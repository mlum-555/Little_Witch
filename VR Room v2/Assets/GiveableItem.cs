using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveableItem : MonoBehaviour
{

    GiveBox potentialHoldArea;

    bool held;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //pass the object to the customer grab box thing on its own trigger enter

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GiveArea"))
        {
            potentialHoldArea = other.gameObject.GetComponent<GiveBox>();

            potentialHoldArea.startGiveParts();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(potentialHoldArea != null)
        {
            
            if (other.gameObject != potentialHoldArea.gameObject && held==false)
            {
                potentialHoldArea.stopGiveParts();
                potentialHoldArea = null;
            }
        }
        
        
    }
    //ok problem


    public void giveItem()
    {
        if(potentialHoldArea != null)
        {
            potentialHoldArea.giveObject(this.gameObject);
            held = true;
        }

    }

    public void takeItem()
    {
        if (held)
        {
            potentialHoldArea.clearObject();
            potentialHoldArea = null;
            Debug.Log("item taken");
            held = false;
        }
    }

}
