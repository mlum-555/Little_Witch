using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit;

//code taken from https://www.reddit.com/r/Unity3D/comments/10z4qjj/comment/j81x75f/?utm_source=share&utm_medium=web3x&utm_name=web3xcss&utm_term=1&utm_content=share_button
public class ObjectSpawner : XRBaseInteractable
{
    [SerializeField]
    private GameObject grabbableObject;

    [SerializeField]
    private Transform transformToInstantiate;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Instantiate object
        GameObject newObject = Instantiate(grabbableObject, transformToInstantiate.position, Quaternion.identity);
            newObject.name = grabbableObject.name;
            newObject.SetActive(true);

        // Get grab interactable from prefab
        XRGrabInteractable objectInteractable = newObject.GetComponent<XRGrabInteractable>();

        // Select object into same interactor
        interactionManager.SelectEnter(args.interactorObject, objectInteractable);

        base.OnSelectEntered(args);
    }
}
