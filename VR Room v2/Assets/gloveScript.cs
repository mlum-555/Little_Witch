using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gloveScript : MonoBehaviour
{

    AudioHandler audioHandler;
    // Start is called before the first frame update
    void Start()
    {
        audioHandler = FindAnyObjectByType<AudioHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void gloveTighten()
    {
        audioHandler.gloveTighten(this.gameObject);
    }
}
