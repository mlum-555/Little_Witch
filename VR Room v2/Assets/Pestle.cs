using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pestle : MonoBehaviour
{
    public Rigidbody thisRigidbody;
    public float velocityThreshold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void crushIngredient()
    {
        if (thisRigidbody.velocity.magnitude > velocityThreshold)
        {

        }
    }
}
