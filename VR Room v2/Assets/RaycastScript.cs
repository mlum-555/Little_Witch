using System.Collections;
using System.Collections.Generic;
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
        RaycastHit hit;
        if(Physics.Raycast(myCam.transform.position,myCam.transform.forward, out hit, 1000))
        {
            foreach(GameObject targ in targetList) 
            {
                if (hit.collider && hit.collider.gameObject == targ)
                {
                    if (currLookTarg != targ)
                    {
                        LookInteractable lookObj = hit.collider.GetComponentInChildren<LookInteractable>();
                        Debug.Log("thing looked at");
                        if (lookObj!=null)
                        {
                            Debug.Log("thing hit");
                            lookObj.LookTriggerThis();
                        }
                        //showParticle(targ);
                        //Renderer renderer = hit.collider.GetComponentInChildren<Renderer>();
                        //renderer.material = mat2;
                        currLookTarg = targ;
                    }
                    
                    

                }
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
            }
            
        }
       

    }



    void newParticle(GameObject partTarget)
    {
        ParticleSystem newPart = Instantiate(partBase, partTarget.transform);
        partList.Add(newPart);
        newPart.gameObject.SetActive(true);
        newPart.Play();
    }

}
