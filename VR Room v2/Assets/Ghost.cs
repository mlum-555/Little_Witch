using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Rendering;

public class Ghost : LookInteractable
{

    public float textSpeed;

    public GameObject popupSign;
    public GameObject textBase;

    public TextMeshPro dialogueText;

    Vector3 signBaseSize;

    public float animDur = 1;
    float signSizeMod = 0;
    float animTimer;

    public string defaultText;

    public string[] textList;
    public UnityEvent[] textEvents;
    //
    public int startOfVictoryText;
    public int[] textFaces;
    int currentTextNum = -1;


    public NavMeshAgent thisAgent;
    public GameObject destination;


    public int giveItemTextNum;

    //maybe make some int for like the one to do


    public Texture[] facesList;
    int currentFace;

    public Material ghostMaterial;

    Renderer thisRenderer;

    public List<GameObject> desiredIngredients = new List<GameObject>();

    public ParticleSystem victoryParticles;

    bool countingUp;

    //public Animator textAnimator;

    bool questCompleted = false;

    bool writingText;

    public GameObject rewardItem;

    public GameObject rewardDestination; //go to this position when request is fulfilled & they want to give the object
    //once they reach it & quest completed is true, then show their thing

    bool destReached, rewardDestReached, leaving;
    //wait no you could just remap the anim timer to the size mod
    // Start is called before the first frame update


    public TextMeshPro errorText;
    public float errorMessageDuration;

    bool rotating;

    CustomerHandler customerHandler;

    GameObject leaveDest;


    GameObject currDest;

    public GhostTalk talkHandler;

    public float lookTurnSpeed = 0.5f;


    public int specialGhost = 0;

    const int anythingGhost = 1, QUICKGHOST = 2;

    EventHandler eventHandler;


    public UnityEvent finalConditionEvent;
   
    bool finalCondition;

    bool objectTaken;
    AudioSource localSource;
    float baseVol;

    float maxSpeed;

    [ContextMenu("reset")]
    void Start()
    {

        
        
       
        // desiredIngredients.Sort();
        customerHandler = FindObjectOfType<CustomerHandler>();

        leaveDest = customerHandler.getExitPoint();

        thisRenderer = GetComponent<Renderer>();    
        Debug.Log("ghost initialized");
        dialogueText.text = defaultText;
        signBaseSize = popupSign.transform.localScale;
        popupSign.transform.localScale = signBaseSize * signSizeMod;
        textBase.SetActive(false);
        Debug.Log("ghost start finished");

        

       

        popupSign.SetActive(false);
        textBase.SetActive(false);
    }
    private void Awake()
    {

        eventHandler = FindObjectOfType<EventHandler>();
        customerHandler = FindObjectOfType<CustomerHandler>();
        thisRenderer = GetComponent<Renderer>();
        leaveDest = customerHandler.getExitPoint();

        setPos(leaveDest.transform.position);

        thisAgent.transform.rotation = leaveDest.transform.rotation;
        maxSpeed = thisAgent.speed;

        destReached = false;

        setNewDest(destination);
        //I think there's something preventing em from leaving
        //or not um. what
        //

        StartCoroutine(destFailsafe());
    }


    [ContextMenu("go to destination")]
   public void goToDest()
    {
        setNewDest(destination);
    }

    IEnumerator destFailsafe() //weird issue where 
    {
        for (float f = 0; f < 2; f += Time.deltaTime)
            setNewDest(destination);
        yield return null;

    }

    // Update is called once per frame
    void Update()
    {
        if (localSource != null) localSource.volume = (thisAgent.velocity.magnitude / maxSpeed) * baseVol; //volume of source changed based on how fast ghost is moving


        //max movement speed?
        if (destReached)
        {
            if (countingUp) sizeUp();
            else sizeDown();
        }
        

        if (rotating)
        {
            rotateTowardsDest();
            if (thisAgent.gameObject.transform.rotation == currDest.transform.rotation)
            {
                rotating = false;
            }//stops rotation if it reaches the target
        }

        if (!destReached)
        {
            if (checkPos(destination))
            {
                destReached = true;
                rotating = true;
                mainLocStart();

            }
        }

        if (questCompleted && rewardDestReached==false)
        {
            if(checkPos(rewardDestination))
            {
                rotateTowardsDest();
                rotating = true;
                rewardDestReached = true;
                giveItem();
            }
        }

        if (leaving)
        {
            if (checkPos(leaveDest))
            {
                //only checks for horizontal components to make things easier
                customerHandler.nextGhost();
            }
        }

    }



    //figure out time remaining via uh current size

    public void startMoving()
    {
        setNewDest(destination);
    }

    public void setAudioSource(AudioSource src)
    {
        localSource = src;
        baseVol = src.volume;
    }

   
  

   IEnumerator textFadeIn(TextMeshPro textBase)
    {
        for (float tempTimer = 0; tempTimer<errorMessageDuration; tempTimer += Time.deltaTime)
        {
            textBase.alpha = tempTimer / errorMessageDuration; //should equal 1

            if(tempTimer>=errorMessageDuration-0.02) StartCoroutine(textFadeOut(textBase));
            yield return null;
        }

    }

    IEnumerator textFadeOut(TextMeshPro textBase)
    {
        for (float tempTimer = 0; tempTimer < errorMessageDuration; tempTimer += Time.deltaTime)
        {
            textBase.alpha = 1-(tempTimer / errorMessageDuration); //should equal 1
            yield return null;
        }

    }


    [ContextMenu("event check")]
    bool eventCheck()
    {
        if (finalConditionEvent == null || finalConditionEvent.GetPersistentEventCount()==0)
        {
            
            Debug.Log("no final condition");
            return true;
        }
        else
        {
            finalConditionEvent.Invoke(); //
            return finalCondition;
        }
        
        
    }
    public void setFinalConditionState(bool state) //to be called by other objects, events, etc.
        //maybe you could send out an event which goes back and affects this?
    {
        finalCondition = state;
    }


    //ok so. final condition should just be tied to ghost by ghost so it should be on the script with the function or whatever
    bool checkPos(GameObject dest) //check agent position against a destination
    {


        if((roundVal(thisAgent.transform.position.x) == roundVal(dest.transform.position.x)) && (roundVal(thisAgent.transform.position.z) == roundVal(dest.transform.position.z)))
        {
            return true;
        }
        return false;
    }

    float roundVal(float inp) //rounds values to 1 decimal place, used for checking transform positions
    {
        return Mathf.Round(inp * 10.0f) * 0.1f;
    }


    [ContextMenu ("leave")]
    public void leave()
    {
        leaving = true;
        textBase.SetActive(false);
        setNewDest(leaveDest);
    }


    void mainLocStart() //to be called when it reaches its main destinaiton
    {
        popupSign.SetActive(true);

    }


    void rotateTowardsDest()
    {
        thisAgent.gameObject.transform.rotation = Quaternion.Slerp(thisAgent.gameObject.transform.rotation, currDest.transform.rotation, Time.deltaTime*lookTurnSpeed);
        

    }
    //set rotation regularly first, then uh actually



    public void nextFace()
    {
        if (currentFace < facesList.Length-1) currentFace++;
        else currentFace = 0;

        Material tempGhostMat = new Material(ghostMaterial);
        tempGhostMat.mainTexture = facesList[currentFace];
        thisRenderer.material = tempGhostMat;
       
    }

    public void objectWasTaken()
    {
        objectTaken = true;
        if (currentTextNum == giveItemTextNum) newTextBox();

    }

    public void setPos(Vector3 newPos)
    {
        thisAgent.gameObject.transform.position = newPos;
    }

    public void setNewDest(GameObject newDest)
    {
        thisAgent.SetDestination(newDest.transform.position);
        currDest = newDest;
    }

    void changeFace(int face)
    {
        if (facesList.Length > textFaces[face])
        {
            Material tempGhostMat = new Material(ghostMaterial);
            tempGhostMat.mainTexture = facesList[textFaces[face]];
            thisRenderer.material = tempGhostMat;
        }
        //if not in the array size, just doesn't change the face
    }

    public void tempMessage(string msg) //displays a popup message
    {     
        errorText.text = msg;
        StartCoroutine(textFadeIn(errorText));
    }


    public bool checkItem(GameObject heldItem)
    {
        Potion thisPotion = heldItem.GetComponent<Potion>();
        if(thisPotion != null)
        {
            if(thisPotion.emissive == true)
            {
                Debug.Log("potion ingredients length " + thisPotion.getIngredients().Count);
                HashSet<GameObject> tempSet1 = new HashSet<GameObject>(desiredIngredients);

                HashSet<GameObject> tempSet2 = new HashSet<GameObject>(thisPotion.getIngredients());
                switch (specialGhost){
                    case 0: //this is the default ghost return
                        //is there a way to check if the contents are the same even if the order is different
                        if (tempSet1.SetEquals(tempSet2) && eventCheck())
                        {
                            
                            Debug.Log("Order Fulfilled");
                            orderFulfilled();
                            thisPotion.lockPotion();
                            return true;
                        }
                        else
                        {
                            tempMessage("This isn't what I ordered...");
                        }
                        break;

                    case anythingGhost:
                    {
                            //anything ghost will take any completed potion.
                            orderFulfilled();
                            return true;
                            
                    }

                }
            }
            else
            {
                tempMessage("Uh... I don't think this is stirred...");
            }
            
        }
        else
        {
            tempMessage("Thanks! ...Uh, what is this?");
        }
        return false;
    }

    public void clickOnGhost()
    {
        Debug.Log("ghost clicked on");
        if (textBase.activeSelf == true)
        {
            newTextBox();
        }
    }

    public void clickOnSign()
    {
        Debug.Log("SIGN clicked on");
        if (textBase.activeSelf == true)
        {
            newTextBox();
        }
    }


    void goToRewardDest()
    {
        
        setNewDest(rewardDestination);

    }


    void newTextBox()
    {

        if (!writingText)
        {

            writingText = true;
            dialogueText.text = "";

            if (currentTextNum >= textList.Length - 1)
            {
                leave();
            }
            else
            {
                if (!questCompleted)
                {
                    if (currentTextNum < startOfVictoryText - 1)
                    {

                        currentTextNum++;
                        tryEvent(currentTextNum);
                        changeFace(currentTextNum);
                    }
                }

                else if (currentTextNum == giveItemTextNum)
                {
                    if (objectTaken)
                    {
                        currentTextNum++;
                        tryEvent(currentTextNum);
                        changeFace(currentTextNum);
                    }
                    //don't advance

                }

                else if (currentTextNum < textList.Length - 1)
                {
                    writingText = true;
                    currentTextNum++;
                    tryEvent(currentTextNum);
                    changeFace(currentTextNum);
                }

                else
                {
                    //placeholder
                }

                if (currentTextNum == giveItemTextNum)
                {
                    goToRewardDest();
                }


            }
            StartCoroutine(writeWords());

        }


    }


    void giveItem()
    {

        rewardItem.SetActive(true);
    }

    IEnumerator writeWords()
    {
        dialogueText.text = "";
        foreach (char newChar in textList[currentTextNum].ToCharArray())
        {
            talkHandler.playDialogue();
            dialogueText.text += newChar;
            yield return new WaitForSeconds(textSpeed);
        }
        writingText = false;
    }


    [ContextMenu("fulfill order")]
    public void orderFulfilled()
    {
        questCompleted = true;
        if (victoryParticles != null)
        {
            victoryParticles.Play();
        }
        currentTextNum = startOfVictoryText - 1;
        newTextBox();
    }



    void tryEvent(int i)
    {
        if (i < textEvents.Length)
        {
            textEvents[i].Invoke();
        }
    }
 
    void sizeDown()
    {
        if (animTimer > 0)
        {
            animTimer -= Time.deltaTime;
            changeSignSize();

        }
        else animTimer = 0;

    }
    void sizeUp()
    {
        if (animTimer < animDur)
        {
            animTimer += Time.deltaTime;
            changeSignSize();

        }
        else animTimer = animDur;

    }

    public void signTouched()
    {
        if (textBase.activeSelf==false)
        {
            textBase.SetActive(true);


            newTextBox();

            popupSign.SetActive(false);
        }
        else textBase.SetActive(false);


        //nextFace();
    }

    public void signDeactivated()
    {
        textBase.SetActive(false);
    }

    void changeSignSize()
    {
        float t = Mathf.InverseLerp(0, animDur, animTimer);
        float output = Mathf.Lerp(0, 1, t);



        signSizeMod = animTimer / animDur;
        popupSign.transform.localScale = signBaseSize * signSizeMod;
    }

    public override void LookTriggerThis()
    {
        //popupSign.transform.localScale = signBaseSize;

        countingUp = true;

    }
    //set sign scale as base; do a multiplier of its base vector, which you store at the start
 

    public override void StopLookTrigger()
    {
        popupSign.transform.localScale = signBaseSize*0;
        countingUp = false;
        
    }
}
