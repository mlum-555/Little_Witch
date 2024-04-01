using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisBarrier : MonoBehaviour
{

    public ParticleSystem rejectionParticles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        ParticleSystem tempParts =Instantiate(rejectionParticles,collision.gameObject.transform);
        tempParts.gameObject.SetActive(true);
    }
}
