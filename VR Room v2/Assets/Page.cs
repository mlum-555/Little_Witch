using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{

    public SkinnedMeshRenderer secretPageRenderer;

    public SkinnedMeshRenderer thisRenderer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void copyAnimProg()
    {

        secretPageRenderer.SetBlendShapeWeight(0, thisRenderer.GetBlendShapeWeight(0));
    }
}
