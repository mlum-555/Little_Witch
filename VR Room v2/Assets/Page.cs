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
        if (thisRenderer == null) Debug.Log("null");
        secretPageRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void Awake()
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

    public void setAnimProg()
    {

    }
    public void copyPageProg(Page otherPage)
    {
        for (int i = 0; i < 3; i++)
        {
            anim(i, otherPage.getWeight(i));

        }
    }

    public float getWeight(int i)
    {
        return thisRenderer.GetBlendShapeWeight(i);
    }

    public void anim(int i, float prog)
    {
        
        if (thisRenderer == null) Debug.Log("null");
        thisRenderer.SetBlendShapeWeight(0, 100);
        thisRenderer.SetBlendShapeWeight(i, prog);
        copyAnimProg();
    }
}
