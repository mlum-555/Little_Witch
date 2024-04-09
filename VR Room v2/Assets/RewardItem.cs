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



    public void revealObject()
    {
        rewardObject.SetActive(true);
        particleHandler.spawnParts(this.gameObject, rewardObject);
        this.gameObject.SetActive(false);
    }

}
