using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : MonoBehaviour
{
    public Rigidbody thisRigidbody;
    public float velocityThreshold;
    public Pestle thisPestle;

    public GameObject ingredientCircle;

   // public SphereCollider crushArea;


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

    public void crushIngredient(GameObject toCrush)
    {
        if(currIng != null && currIng ==toCrush)
        {
            
            Renderer tempRend = currIng.GetComponent<Renderer>();

            GameObject newCirc = Instantiate(ingredientCircle);
            newCirc.SetActive(true);

            Renderer ingRenderer = newCirc.GetComponent<Renderer>();
                ingRenderer.material = new Material(tempRend.material);
            //spawn in the dust according to the thing you crushed

            //okay so no you want to make an instance of it, like a spawner I guess
            
                Destroy(currIng);
                currIng = null;

        }
       
    }

    //wait no it should like. create a crushed object. yeah

    //https://gamedev.stackexchange.com/questions/151670/how-to-detect-collision-occurring-on-a-child-object-from-a-parent-script
    void OnCollisionEnter(Collision collision)
    {

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
        if (collision != null && currIng !=null)
        {
            if (collision.gameObject == currIng.gameObject)
            {
                currIng = null;
            }
        }
        
    }

}
