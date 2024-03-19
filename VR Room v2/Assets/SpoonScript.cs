using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoonScript : MonoBehaviour
{
    // Start is called before the first frame update

   // public BoxCollider[] colliderList;

    public GameObject[] colliderList;

    GameObject[] ownColliderList;


    public Cauldron mainCauldron;

    public Rigidbody ownRigidbody;


    public float minStirSpeed;
    public float maxStirSpeed;

    public float minStirMod = 0.05f; //min/max of sent values to cauldron
    public float maxStirMod = 0.2f;
    //no no no just remap

    Collider lastColliderHit;

    Vector3 lastPos = Vector3.zero;
    Vector3 positionalSpeed = Vector3.zero;

    Vector3 angularSpeed = Vector3.zero;

    Quaternion lastRot;
    void Start()
    {
        ownColliderList = new GameObject[colliderList.Length];
    }

    // Update is called once per frame


    

    void Update()
    {
        Vector3 distancePerFrame = transform.position - lastPos; // transform.position is in global space, not in local/parent
        lastPos = transform.position;

        positionalSpeed = distancePerFrame * Time.deltaTime; // could be anything to scale frame => sec => hour

       // Debug.Log("SP pos speed: "+positionalSpeed.magnitude);

        Quaternion angularDiff = transform.rotation * Quaternion.Inverse(lastRot);

            
        angularSpeed = angularDiff.eulerAngles * Time.deltaTime;

        lastRot = transform.rotation;

       // Debug.Log("Spoon rotation speed: " + angularSpeed.magnitude);


        
    }//this code is from https://forum.unity.com/threads/determine-speed-acceleration-of-an-object-when-its-held-parented-by-character.151770/






    //say: oh I've run into a collider!! did I run into it already???? no??? add it to the list!!! is the list full?? yippee!!!!







    /*
    private void OnTriggerEnter(Collider collision)
    {
        GameObject newCollider;
        for (int i=0; i<colliderList.Length; i++)
        {
            if (colliderList[i].gameObject.name == collision.gameObject.name)
            {
                if(ownColliderList[i] != collision.gameObject)
                {
                    ownColliderList[i] = collision.gameObject;
                    Debug.Log("new collider hit");

                    lastColliderHit = collision;
                    checkColliderList();
                }
                
                return;
            }
        }
        
    }
    */

    // Vector3 previousVelocity = Vector3.zero; // initialize this to the child's initial velocity

    private void OnTriggerEnter(Collider collision)
    {
        GameObject newCollider;
        for (int i = 0; i < colliderList.Length; i++)
        {
            if (colliderList[i].gameObject.name == collision.gameObject.name && collision != lastColliderHit) 
            {
                
               lastColliderHit = collision;
                stirSpoon();
                return;
            }
        }

    }
 

 
 

    //remap function taken from Jessy in https://forum.unity.com/threads/re-map-a-number-from-one-range-to-another.119437/
    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    void stirSpoon()
    {
        float stirSpeed = ownRigidbody.velocity.magnitude;
        //stirSpeed = ownRigidbody.angularVelocity.magnitude;
        stirSpeed = angularSpeed.magnitude;
        
        if ( stirSpeed > minStirSpeed)
        {
            if (stirSpeed > maxStirSpeed) stirSpeed = maxStirSpeed;

            stirSpeed = Remap(stirSpeed, minStirSpeed, maxStirSpeed, minStirMod, maxStirMod);

            mainCauldron.increasePotStir(stirSpeed);

        }

        //problem: the rigidbody doesn't inherit the speed of the xr rig stuff

    }


    void checkColliderList()
    {
        foreach(GameObject collider in ownColliderList) {

            if (collider == null) return;
        }
        //this runs if everything in the own collider list is filled in

        allCollidersHit();
    }

    void allCollidersHit()
    {
        for (int i = 0; i < ownColliderList.Length; i++)
        {
            ownColliderList[i] = null;
        }
       // Debug.Log("spoon hit all of the colliders");
        mainCauldron.potStirred();


        //think kenneth also said to maybe just check if the spoon is going one way & to just check one collider. that'd probably be better

    }
    //try checking for velocity??

    void checkVel()
    {

        


        //wait no maybe check between two values and  uhh normalize stuff
        //wait no unity sucks for mapping values nvm


    } //make sure it's not the last collider entered


}
