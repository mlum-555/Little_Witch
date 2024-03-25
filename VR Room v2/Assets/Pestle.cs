using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pestle : MonoBehaviour
{
    public Rigidbody thisRigidbody;
    public float velocityThreshold;
    // Start is called before the first frame update

    Vector3 lastPos;

    Vector3 positionalSpeed;

    Mortar parentMortar;
    void Start()
    {
        lastPos = transform.position;
        parentMortar = GetComponentInParent<Mortar>();

    }

    // Update is called once per frame
    void Update()
    {
        //this code is derived from https://forum.unity.com/threads/determine-speed-acceleration-of-an-object-when-its-held-parented-by-character.151770/
        Vector3 distancePerFrame = transform.position - lastPos; // transform.position is in global space, not in local/parent
        lastPos = transform.position;

        positionalSpeed = distancePerFrame * Time.deltaTime; // could be anything to scale frame => sec => hour

        
    }
   



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ingredient"))
        {
            if (positionalSpeed.magnitude > velocityThreshold)
            {
                parentMortar.crushIngredient(collision.gameObject);
            }
            //check if parent mortar thing is the same
        }
    }
}
