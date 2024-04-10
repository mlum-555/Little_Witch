using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GiveBox : MonoBehaviour
{

    public GameObject holdPoint;
    //public BoxCollider thisCollider;

    GameObject heldObject;

    public Ghost parentGhost;


    public float animDur = 1;

    float animTimer;

    public ParticleSystem boxParts;
    

    float animProgress = 1f;

    Vector3 startingPoint;

    // Start is called before the first frame update

    bool noMoreParticles;
    void Start()
    {
        startingPoint= transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (heldObject != null)
        {  
            //interpolates held object between starting point and hold point when initially grabbed, otherwise just sets the positions the same
            if (animTimer == animDur) heldObject.transform.position = holdPoint.transform.position;
            else sizeUp();
        }
        if (noMoreParticles && boxParts.isPlaying) boxParts.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    public void startGiveParts()
    {
        boxParts.Play();
    }

    public void stopGiveParts()
    {
        boxParts.Stop();
    }

    public void giveObject(GameObject newObj)
    {
        if (newObj.CompareTag("GivableItem") && heldObject != newObj)
        {
            heldObject = newObj;
            startingPoint = heldObject.transform.position;
            animTimer = 0;
            stopGiveParts();

            if (parentGhost.checkItem(heldObject))
            {
                noMoreParticles = true;
              //  heldObject.SetActive(false);
              //  XRGrabInteractable tempGrab = heldObject.GetComponent<XRGrabInteractable>();
              //  if(tempGrab != null)tempGrab.enabled = false;
              //  this.gameObject.SetActive(false);
            }
        }
    }

    public void clearObject()
    {
        heldObject = null;
    }

    void sizeUp()
    {
        if (animTimer < animDur)
        {
            animTimer += Time.deltaTime;
            animateObject();

        }
        else if (animTimer != animDur)
        {
            animTimer = animDur;
            animateObject();
        }

    }

    void animateObject()
    {
        float t = Mathf.InverseLerp(0, animDur, animTimer);
        float output = Mathf.Lerp(0, 1, t);

        animProgress = animTimer / animDur;

        heldObject.transform.position = Vector3.Lerp(startingPoint, holdPoint.transform.position, animProgress);
       // heldObject.transform.rotation = Vector3.Lerp(startingPoint, holdPoint.transform.position, animProgress);

    }
}
