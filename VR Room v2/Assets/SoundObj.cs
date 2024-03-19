using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundObj : MonoBehaviour
{
    // Start is called before the first frame update

    AudioHandler handler;

    public bool isGlass;
    void Start()
    {
        handler = FindFirstObjectByType(typeof(AudioHandler)).GetComponent<AudioHandler>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        handler.objectFall(this.gameObject, isGlass) ;
    }
}
