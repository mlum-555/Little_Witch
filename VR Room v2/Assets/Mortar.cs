using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : MonoBehaviour
{
    public Rigidbody thisRigidbody;
    public float velocityThreshold;
    public Pestle thisPestle;

    public GameObject ingredientCircle;

    public SphereCollider crushArea;


    public ParticleSystem dustParticles;

    GameObject currIng;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void crushIngredient()
    {
        if(currIng != null)
        {
            if (thisRigidbody.velocity.magnitude > velocityThreshold)
            {


                //spawn in the dust according to the thing you crushed
                Destroy(currIng);
                currIng = null;


            }
        }
       
    }

    //https://gamedev.stackexchange.com/questions/151670/how-to-detect-collision-occurring-on-a-child-object-from-a-parent-script
    void OnCollisionEnter(Collision collision)
    {
        Collider myCollider = collision.GetContact(0).thisCollider;

        //wait uhh I'm not sure this actually shows the correct thing here

        GameObject tempObj = collision.gameObject;
        if (tempObj.CompareTag("Ingredient"))
        {
            currIng = tempObj;
        }

                //set target game object if it's an ingredient
    }

    private void OnCollisionExit(Collision collision)
    {
        Collider myCollider = collision.GetContact(0).thisCollider;

        if(collision.gameObject == currIng.gameObject)
        {
            currIng = null;
        }

    }

}
