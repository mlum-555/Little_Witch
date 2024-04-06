using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookInteractable : MonoBehaviour
{
    bool lookTriggered;

    // Start is called before the first frame update
    void Start()
    {
        
    }



    public virtual void LookTriggerThis() { }

    public virtual void StopLookTrigger()
    {

    }

    public bool isLookTriggered()
    {
        return lookTriggered;
    }

    public void setLookTriggerState(bool state)
    {
        lookTriggered = state;
    }
}
