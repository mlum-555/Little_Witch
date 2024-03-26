using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawn : MonoBehaviour
{




    Transform startPos;

    Vector3 startTrans;
    Quaternion startRot;
    public BoxCollider areaBounds;

    AudioHandler audioHandler;

    public ParticleSystem returnParticles;
    // Start is called before the first frame update
    void Start()
    {
        audioHandler = FindAnyObjectByType<AudioHandler>();
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
        ParticleSystem retParts1 = Instantiate (returnParticles);
        retParts1.transform.position = transform.position;
        retParts1.gameObject.SetActive(true);

        

        transform.position = startTrans;
        transform.rotation = startRot;
        audioHandler.playPoof(this.gameObject);

    }
}
