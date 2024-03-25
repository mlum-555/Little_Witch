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

        SkinnedMeshRenderer[] tempRend = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach(SkinnedMeshRenderer skm in tempRend)
        {
            if (skm != thisRenderer)
            {
                secretPageRenderer = skm;
            }
            
        }
        
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

        for (int i = 0; i < 3; i++)
        {

            secretPageRenderer.SetBlendShapeWeight(i, thisRenderer.GetBlendShapeWeight(i));
        }
        ///wait just go into shader graph and reverse the texture there
        //if page is page 2 then um yeah

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
        thisRenderer.SetBlendShapeWeight(i, prog);
        copyAnimProg();
    }


    public void flipSecret(int i)//if it's a page 1 or 2
    {
        if(i==2) secretPageRenderer.material.SetFloat("_flipLevel", 1);
        else if (i==1) secretPageRenderer.material.SetFloat("_flipLevel", 0);
    }
   
}
