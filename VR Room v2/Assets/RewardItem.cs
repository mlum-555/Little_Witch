using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RewardItem : XRBaseInteractable
{

    [SerializeField]
    private GameObject rewardObject;

    public ParticleHandler particleHandler;
    
    //you should instantiate ones, make em active, and have em destroy selves on stopping being active


    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {

        revealObject();

        this.gameObject.SetActive(false);

        //make the ghost's held object the new thing & transfer it over to em
        

        // Select object into same interactor
       

        base.OnSelectEntered(args);

        //it just has some target object; when grabbed, it makes that one visible & makes this one dissapear in a cloud of smoke

        //probably make some system wide smoke particle thing
    }

    void revealObject()
    {
        rewardObject.SetActive(true);
        particleHandler.spawnParts(this.gameObject, rewardObject);
    }

}
