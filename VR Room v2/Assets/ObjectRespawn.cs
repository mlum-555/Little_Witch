using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawn : MonoBehaviour
{




    Transform startPos;

    Vector3 startTrans;
    Quaternion startRot;
    public BoxCollider areaBounds;
    // Start is called before the first frame update
    void Start()
    {
        startTrans = transform.position;
        startRot = transform.rotation;  
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerExit(Collider other)
    {
        if (other == areaBounds)
        {
            respawn();
        }
    }

    void respawn()
    {
        transform.position = startTrans;
        transform.rotation = startRot;
    }
}
