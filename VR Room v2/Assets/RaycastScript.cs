using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RaycastScript : MonoBehaviour
{

    public Material mat1;
    public Material mat2;


    public Camera myCam;
    public GameObject[] targetList;

    public ParticleSystem partBase;

    private List<GameObject> particleSpawnedObjs = new List<GameObject>();


    private List<ParticleSystem> partList = new List<ParticleSystem>();


    private GameObject currLookTarg;

    public float raycastRadius = 5f;
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        sendRaycast();   
    }

    void sendRaycast()
    {
        RaycastHit[] hits;
       // RaycastHit hit;

        int layerMask = 1 << 10;

        hits = Physics.RaycastAll(transform.position, transform.forward, 10000, layerMask);
        List<GameObject> missedTargs = new List<GameObject>();
        missedTargs = targetList.ToList();


        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
           // Renderer rend = hit.transform.GetComponent<Renderer>();

          //  if (rend)
          //  {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                

            //remove if um. they aren't hit? I guess? or no if they are hit. 
            //ok run the

            foreach (GameObject targ in targetList)
                {
                    if (hit.collider && hit.collider.gameObject == targ)
                    {
                        missedTargs.Remove(targ);

                      
                        //if (currLookTarg != targ)
                       // {
                            LookInteractable lookObj = hit.collider.GetComponentInChildren<LookInteractable>();
                           // Debug.Log("thing looked at");
                            if (lookObj != null && !lookObj.isLookTriggered())
                            {
                               // Debug.Log("thing hit");
                                lookObj.LookTriggerThis();
                                lookObj.setLookTriggerState(true);
                             }
                            //showParticle(targ);
                            //Renderer renderer = hit.collider.GetComponentInChildren<Renderer>();
                            //renderer.material = mat2;
                            //currLookTarg = targ;
                      //  }



                    }

                    /*
                    else
                    {
                        
                        
                        if (currLookTarg == targ && hit.collider.gameObject != targ)
                        {
                            //move this to any objects not hit (or something?) or

                            LookInteractable lookObj = currLookTarg.GetComponentInChildren<LookInteractable>();
                            if (lookObj != null)
                            {
                                lookObj.StopLookTrigger();
                            }

                            // hideParticle(targ);
                            //Renderer render2 = targ.GetComponent<Renderer>();
                            //render2.material = mat1;
                            if (currLookTarg == targ) currLookTarg = null;
                        }

                    }
                    */

                    
                }
           
            // }
        }

        foreach (GameObject targ2 in missedTargs)
        {
            LookInteractable lookObj = targ2.GetComponentInChildren<LookInteractable>();
            if (lookObj != null && lookObj.isLookTriggered())
            {
                lookObj.StopLookTrigger();
                lookObj.setLookTriggerState(false);
            }

            // hideParticle(targ);
            //Renderer render2 = targ.GetComponent<Renderer>();
            //render2.material = mat1;
            // if (currLookTarg == targ2) currLookTarg = null;
        }

        // if (Physics.Raycast(myCam.transform.position,myCam.transform.forward, out hit, 10000,layerMask))
        //  {





    }



    void newParticle(GameObject partTarget)
    {
        ParticleSystem newPart = Instantiate(partBase, partTarget.transform);
        partList.Add(newPart);
        newPart.gameObject.SetActive(true);
        newPart.Play();
    }

}
