using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{

    SkinnedMeshRenderer secretPageRenderer;

    SkinnedMeshRenderer thisRenderer;


    // Start is called before the first frame update
    void Start()
    {
        thisRenderer = GetComponent<SkinnedMeshRenderer>();
        secretPageRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void copyAnimProg()
    {

        secretPageRenderer.SetBlendShapeWeight(0, thisRenderer.GetBlendShapeWeight(0));
    }
}
