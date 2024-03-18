using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Sign : XRBaseInteractable
{
    public Ghost parentGhost;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        parentGhost.clickOnSign();
    }
}
