using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundObj : MonoBehaviour
{
    // Start is called before the first frame update

    AudioHandler handler;

    public bool isGlass, isSolid;

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
        //I know this is a dumb way to do it but it's less effort than making a switch & adapting everything to that
        if(isSolid) handler.solidFall(this.gameObject) ;

        else handler.objectFall(this.gameObject, isGlass);
    }


}
