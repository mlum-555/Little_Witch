using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using static UnityEngine.ParticleSystem;

public class Cauldron : LookInteractable
{
    // Start is called before the first frame update


    public BoxCollider[] stirHitboxes;


    public GameObject waterCircle;
    public GameObject[] acceptedIngredients;
    public UnityEngine.Color[] ingredientColors;

    private List<GameObject> currIngredients = new List<GameObject>();

    public Material thisMat;

    public UnityEngine.Color defaultColor;
    public UnityEngine.Color waterCol;

    public ParticleSystem particle;
   // ParticleSystem.MainModule mainModule;

    public Material particleMat;

    Renderer waterRenderer;

    public GameObject respawnPosObj;

    public bool isStirred;

    public ParticleSystem explosionParticles;

    public ParticleSystem glowParticles;

    public GameObject rejectionRespawn;

    float stirProgress;

    public float bubbleVal, minBubbleVal, maxBubbleVal;
    //used for uh bubble value?
    


    public float soundLow, soundHigh; //low & high for the sounds and such

    public AudioSource soundSource1, soundSource2;
    //1 is ambient, 2 is more action/incidental based, ex. explosions

    public AudioClip explosionSound, stirSound, addSound, goodSound;

    public AudioClip basicCauldronSound, largeBubbling;


    float colorChangeInterval, colorChangeTimer, colorChangeVal; //set timer to -1 when not changing, or uhh hm
    //


    void Start()
    {
       /// mainModule = particle.main;
        waterCol = defaultColor;
        
        particleMat.color = defaultColor;
        waterRenderer = waterCircle.GetComponentInChildren<Renderer>();
        waterRenderer.material = new Material(waterRenderer.material);

        waterRenderer.material.SetColor("_Color", waterCol);
        //waterRenderer.material.EnableKeyword("_EMISSION");
        stopWaterEmission();
        clearIngredientList();
        soundSource1.clip = basicCauldronSound;
        soundSource1.Play();
        soundSource1.volume = soundLow;

        bubbleVal = minBubbleVal;
        //amount of changes needed or whatever for increasing stir?
    }

    // Update is called once per frame
    void Update()
    {
        waterRenderer.material.SetFloat("_bubbleLevel", bubbleVal);

    }

    private void OnTriggerEnter(Collider potentialIng)
    {
        if (potentialIng.gameObject.GetComponent<Potion>() == null)
        {
            addIngredient(potentialIng.gameObject);
        }
            
    }

    public void potStirred()
    {
        //particle.startSpeed = 3;

        if (!isStirred)
        {
            soundSource2.PlayOneShot(goodSound);
            isStirred = true;
            //  makeWaterEmissive();
            glowParticles.Play();
            soundSource1.clip = largeBubbling;
            soundSource1.Play();
        }
        

    }

    

    public void increasePotStir(float stirAddition)
    {
        if (stirProgress < 1)
        {
            playStirSound();
            stirProgress += stirAddition;
            bubbleVal = minBubbleVal + ((maxBubbleVal - minBubbleVal) * stirProgress);

            soundSource1.volume = soundLow + (stirProgress*(soundHigh - soundLow)); //pot gets progressively louder with stirring
            if (stirProgress >= 1)
            {
                bubbleVal = maxBubbleVal;
                stirProgress = 1;
                potStirred();
            }
            setWaterEmission(stirProgress);
        }
        
    }

    public UnityEngine.Color transferBrew()
    {
        
        UnityEngine.Color tempColor = waterCol;
        resetPot();
        return tempColor;
    }

    public bool getStirred()
    {
        return isStirred;   
    }
    
    public List<GameObject> getIngredients()
    {
       // currIngredients.Sort();
         return new List<GameObject>(currIngredients);
    }


    public void playStirSound()
    {
        soundSource2.PlayOneShot(stirSound);
        //might be good to have this as not one shot. lemme check
    }

    void resetPot()
    {

        //something to get the audioclip

        bubbleVal = minBubbleVal;
        soundSource2.PlayOneShot(explosionSound);
        soundSource1.volume = soundLow;

        clearIngredientList();
        stopWaterEmission();
        isStirred = false;
        explosionParticles.Play();
        stirProgress = 0;

       
    }

    void addIngredient(GameObject ingredient)
    {
        
        for (int i=0; i<acceptedIngredients.Length; i++)
        {
            if (acceptedIngredients[i] != null && acceptedIngredients[i].gameObject.name == ingredient.name)
            {

                /*
                foreach(GameObject oldIng in currIngredients)
                {
                    if (oldIng.name == ingredient.name)
                    {
                        Destroy(ingredient);
                        return;
                    }
                }*/ //commenting out to let duplicate ingredients in

                if (!isStirred)
                {
                    soundSource2.PlayOneShot(addSound);
                    currIngredients.Add(acceptedIngredients[i]);
                    Debug.Log("ingredients: " + currIngredients.Count);
                    addToColor(ingredientColors[i]);
                }
                else{
                    
                    resetPot();
                } //pot will explode & reset if an ingredient is added when it's fully stirred

                Destroy(ingredient);
                return; //probably add something here to destroy the old ingredient? idk
                //wait if an ingredient is already in the list then uh. it doesn't accept it. I think.
            }
        }

        //if not an accepted ingredient:

        
        if(ingredient.name != "spoon")
        {
            resetPot();
            ingredient.transform.position = rejectionRespawn.transform.position;
        }

        
        Debug.Log("thing added");
        

    }
    //give each thing a color value, if there are no ingredients just set it to that one, and then average the color values of all ingredients?

    void addToColor(UnityEngine.Color newCol)
    {
        if (currIngredients.Count == 0)
        {
            waterCol = newCol;
        }
        else
        {
            waterCol = (waterCol - (waterCol/currIngredients.Count)) + (newCol / currIngredients.Count);
        }
        changeMatCol();
    }

    void clearIngredientList()
    {
        currIngredients.Clear();
        //waterCol = defaultColor;
        //mainModule.startColor = defaultColor;
        // var emitParams = new ParticleSystem.EmitParams();
        //emitParams.startColor = defaultColor;
        // particle.Emit(emitParams, 10);
        waterCol = defaultColor;
        changeMatCol();

        
    }

    void changeMatCol()
    {
        // mainModule.startColor=waterCol;
        //var emitParams = new ParticleSystem.EmitParams();
        // emitParams.startColor = waterCol;
        //particle.Emit(emitParams, 10);
       // thisMat.color = waterCol;

        //waterRenderer.material = thisMat;


        //make something like. water change speed? idk
        waterRenderer.material.SetColor("_Color", waterCol);

        

        

        // particleMat.color = waterCol;
    }

    void makeWaterEmissive()
    {
       // waterRenderer.material.SetColor("_EmissionColor", waterCol);
       // waterRenderer.material.EnableKeyword("_EMISSION");
    }

    void setWaterEmission(float amt)
    {
        waterRenderer.material.SetColor("_EmissionColor", new UnityEngine.Color(0, 0, 0)+ (waterCol*amt)); //changes the amount


    }

    void stopWaterEmission()
    {
        waterRenderer.material.SetColor("_EmissionColor", new UnityEngine.Color(0,0,0));
        glowParticles.Stop();
        //tempRenderer.material.EnableKeyword("_EMISSION");
    }



    
    public Vector3 getRespawnPos()
    {
        return respawnPosObj.transform.position;
    }
   public override void LookTriggerThis()
    {
        if (particle != null)
        {
            if (!particle.isPlaying)
            {
                particle.Play();
                Debug.Log("help");
            }
                
        }
    }
    public override void StopLookTrigger()
    {
        if (particle != null)
        {
            particle.Stop();
            
        }
    }

    //should each ingredient have a color value that gets multiplied with another? idk

    //actually probably do oncolliderenter in the ingredient list


}
