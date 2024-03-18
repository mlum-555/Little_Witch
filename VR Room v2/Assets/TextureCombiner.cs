using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


//code source for this: https://youtu.be/tdTfgo9_hd8?si=r6rjqyQWtE9tcxU3
public class TextureCombiner : MonoBehaviour
{
    
    public Texture2D[] finalTextures;

    public int baseTexDim = 1024;
    //  public TextureMergeBase[] toMerge;

    public List<List<Texture2D>> toMerge = new List<List<Texture2D>>();


    

    [Serializable]
    public class TextureMergeBase
    {
        public List<Texture2D> localList;
    }
    // Start is called before the first frame update
    //don't overwrite everything, just transfer this stuff into the old stuff


    public List<TextureMergeBase> textureList;

    void Start()
    {
        finalTextures = new Texture2D[textureList.Count];
        foreach (TextureMergeBase tex in  textureList)
        {
            toMerge.Add(tex.localList);

        }

        finalTextures = new Texture2D[toMerge.Count];
        for(int i = 0; i < toMerge.Count; i++)
        {
            merge(i);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public int getTotalTextures()
    {
        return finalTextures.Length;
    }

    public Texture2D getPageTexture(int index)
    {
        if (index < finalTextures.Length)
        {
            return finalTextures[index];
        }
        else return finalTextures[0];
    }

    void merge(int texNum)
    {

        Resources.UnloadUnusedAssets();

        Texture2D newText = new Texture2D(baseTexDim,baseTexDim);
        for(int i = 0; i < newText.width; i++)
        {
            for(int y = 0; y < newText.height; y++)
            {
                newText.SetPixel(i, y, new Color(1, 1, 1, 0));
            }
        }


        for(int i=0; i < toMerge[texNum].Count; i++)
        {
            for (int x = 0; x < toMerge[texNum][i].width; x++)
            {
                for (int y = 0; y < toMerge[texNum][i].height; y++)
                {
                    var newCol = toMerge[texNum][i].GetPixel(x, y).a == 0 ?
                                newText.GetPixel(x, y) :
                                toMerge[texNum][i].GetPixel(x, y);
                    newText.SetPixel(x, y, newCol);
                }
            }
        }
       newText.Apply();
        newText.name = "new texture";
        finalTextures[texNum] = newText;

    }

    //maybe each thing just has a preset position on the page?? idk
    //yeah that's probably easiest

}
