using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class Potion : LookInteractable
{

    public Material liquidMaterial;

    // public Collider[] submergeColliders;
    //public bool[] colliderList;


    public Cauldron mainCauldron;


    public SphereCollider liquidCollider;

    public Collider mainSubmergeCollider;

    public UnityEngine.Color waterCol;

    Rigidbody thisRigidbody;

    public bool emissive;
   
    Renderer waterRenderer;

    ParticleSystem glowParticles;

    Material[] mats;

    int waterMatNum=0;

    Renderer thisRenderer;

    private List<GameObject> currIngredients = new List<GameObject>();

    public float emissiveStrength = 1;

    // Start is called before the first frame update
    void Start()
    {

        glowParticles = GetComponentInChildren<ParticleSystem>();

        //make uh the view collider work with all potions, not just the one

        thisRigidbody = GetComponent<Rigidbody>();


        thisRenderer = GetComponent<Renderer>();
       // colliderList = new bool[submergeColliders.Length]
       mats = thisRenderer.sharedMaterials;
        for(int i = 0; i < mats.Length; i++)
        {
            if (mats[i] != null)
            {
                if (mats[i] == liquidMaterial)
                {
                    waterMatNum = i;
                   // Debug.Log("water num found");
                    break;
                }
            }
        }


        // waterRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if(other == liquidCollider)
        {
            Debug.Log("gwuh");


            if (mainCauldron != null)
            {
                

            }



            if (mainCauldron.getStirred())
            {
                Debug.Log("should be emissive");
                emissive = true;

            }
            else emissive = false;

            //liquidMaterial.SetColor("_Color",mainCauldron.transferBrew());
            mats[waterMatNum] = new Material(liquidMaterial);


            //this doesn't work because 
            //you need to reference the material *slot*, not the material itself
            currIngredients = mainCauldron.getIngredients();

            //WAIT WHY DO YOU STILL HAVE THIS HERE

            float baseAlph = mats[waterMatNum].GetColor("_TintColor").a;


            //mats[waterMatNum].color = mainCauldron.transferBrew();

            Color newCol = mainCauldron.transferBrew();

            newCol.a = baseAlph;

            mats[waterMatNum].SetColor("_TintColor", newCol); //sets tint color, etc


            

            waterCol = mats[waterMatNum].GetColor("_TintColor");
            //WAIT nvm ok you did instantiate a new material and all that. guess the problem is in transferbrew if I were to guess. try it out rq


            //ok. seems to only happen when it's emissive (...??)


            Debug.Log("color shoud be chagned");
            respawnInAir(mainCauldron.getRespawnPos());

            if (emissive) makeWaterEmissive();
            else stopWaterEmission();

            thisRenderer.sharedMaterials = mats;
            //updates the list of used materials;
        }


    }

    public List<GameObject> getIngredients()
    {
        return new List<GameObject>(currIngredients);
    }

    public void respawnInAir(Vector3 vector)
    {

        thisRigidbody.position = vector;
        transform.position = vector;
        thisRigidbody.rotation = Quaternion.Euler(-90, 0, 0);
        thisRigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

    }

    public void unfreezeThis()
    {
       
        thisRigidbody.constraints = RigidbodyConstraints.None;

    }


    void makeWaterEmissive()
    {

        //oh. you didn't change water col. oops


        /*
        mats[waterMatNum].SetColor("_EmissionColor", waterCol);
        mats[waterMatNum].EnableKeyword("_EMISSION");
        */

        emissive = true;

        mats[waterMatNum].SetFloat("_ReflectionStrength", emissiveStrength);


    }


    public override void LookTriggerThis()
    {
        if (glowParticles != null && emissive)
        {
            if (!glowParticles.isPlaying)
            {
                glowParticles.Play();
               
            }

        }
    }
    public override void StopLookTrigger()
    {
        if (glowParticles != null)
        {
            glowParticles.Stop();

        }
    }


    void stopWaterEmission()
    {
        /*
        waterRenderer.material.SetColor("_EmissionColor", new UnityEngine.Color(0, 0, 0));
        waterRenderer.material.EnableKeyword("_EMISSION");
        */


        /*
        mats[waterMatNum].SetColor("_EmissionColor", new UnityEngine.Color(0, 0, 0));
        mats[waterMatNum].EnableKeyword("_EMISSION");

        */


        mats[waterMatNum].SetFloat("_ReflectionStrength", 0);

        emissive = false;
        glowParticles.Stop();
    }
    //you could also just put Weird particles around the potion. might be easier
    //

    //uhh. um. uhh. wait no you need to check if each collider is touching the desired trigger.
    //


}
