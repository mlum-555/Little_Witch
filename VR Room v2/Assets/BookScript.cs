using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookScript : MonoBehaviour
{
    public SkinnedMeshRenderer skinRender;
    // Start is called before the first frame update

    public float animDur, pageAnimDur = 1;
    
    float animTimer, pageAnimTimer;
    bool countingUp = true;

    float bookAnimProgress = 1f;

    float pageAnimProgress = 1f;


    //public GameObject[] pages;

    


    //start with pages 0 and 1
    //maybe just make a new page class? idk
    //when turning starts, make the two new pages active
    //new page 1 should be 0 on everything I think, since it's left side
    //once the two new pages are done, set the two old pages to no longer active

    //new page 2 should be on the other side of old page 1; i.e. it should have the same blendshape weights
    //you should also get the skinnedmeshrenderers of them

    

    public float PageTurnSpeed;

    bool halfwayReached = false;

    bool animatingPage;


    public TextureCombiner pageTextureHolder;


    int pageTextureNum = 0;

    int currPageIndex = 0; //should update by 2 every call (?)
    //

    int currPage1, currPage2;

    public float turnSpeedP2;


    int currP1, currP2, nP1, nP2;


    public GameObject[] pageObjs;

    Page[] pages;
    //yeah just store the actual page references internally
    //you just need the game objects & page component, nothing more


    public AudioSource audioSource;
    public AudioClip pageTurnSound;

    void Start()
    {
        currP1 = 0;
        currP2 = 1;
        
        pages = new Page[pageObjs.Length];
        for(int  i = 0; i < pages.Length; i++)
        {
            pages[i] = pageObjs[i].GetComponent<Page>();
            if (pages[i] == null) Debug.Log("page "+i+" null");

            pageObjs[i].SetActive(false);
        }
        pageObjs[0].SetActive(true);
        pageObjs[1].SetActive(true);

        skinRender.SetBlendShapeWeight(0, 100 * bookAnimProgress);


        pages[currP1].anim(0, 100);
        pages[currP2].anim(0, 100);
        currPageIndex = currP1;


    }



    // Update is called once per frame
    void Update()
    {
        if (countingUp) sizeUp();
        else sizeDown();

        if (animatingPage) pageLeft();
    }


  void activateNextPages()
    {

        nP1 = (currPageIndex + 2) % pages.Length;
        nP2=(currPageIndex + 3) % pages.Length;

        //values should wrap around if they go out of bounds

        pageObjs[nP1].SetActive(true);
        pageObjs[nP2].SetActive(true);
    }

    void deactivateOldPages()
    {
        //deactivate page from current count & one above; 
        pageObjs[currP1].SetActive(false);
        pageObjs[currP2].SetActive(false);

        currPageIndex = nP1;

        currP1 = currPageIndex;
        currP2 = (currPageIndex + 1) % pages.Length;


    }
   



    void sizeDown()
    {
        if (animTimer > 0)
        {
            animTimer -= Time.deltaTime;
            animateBook();
        }
        else if (animTimer != 0)
        {
            animTimer = 0;
            animateBook();
        }
    }



    void sizeUp()
    {
        if (animTimer < animDur)
        {
            animTimer += Time.deltaTime;
            animateBook();

        }
        else if (animTimer != animDur)
        {
            animTimer = animDur;
            animateBook();
        }

    }
    void animateBook()
    {
        float t = Mathf.InverseLerp(0, animDur, animTimer);
        float output = Mathf.Lerp(0, 1, t);

        bookAnimProgress = animTimer / animDur;
        skinRender.SetBlendShapeWeight(0, 100 * bookAnimProgress);

        pages[currP1].anim(0,100 * bookAnimProgress);
        //page1Mesh.SetBlendShapeWeight(1, 100 * bookAnimProgress);

        pages[currP2].anim(1, 100 * bookAnimProgress);


        pages[currP2].anim(0, 100-(100 * bookAnimProgress));




    }



    void pageLeft()
    {
        if (pageAnimTimer < pageAnimDur)
        {
            pageAnimTimer += Time.deltaTime;
            animatePage();

        }
        else if (pageAnimTimer != pageAnimDur)
        {
            pageAnimTimer = pageAnimDur;
            animatePage();
            deactivateOldPages();
            resetPageAnimation();
            animatingPage = false;
        }
    }


    void resetPageAnimation()
    {
        pages[currP1].anim(0, 0);
        pages[currP1].anim(1, 0);
        pages[currP1].anim(2, 0);

        pages[currP2].anim(1, 100);
        pages[currP2].anim(0, 0);

        pages[nP1].copyPageProg(pages[currP2]);
        pages[nP2].copyPageProg(pages[currP1]);

        halfwayReached = false;
        pageAnimTimer = 0;
    }


    public void startPageTurning()
    {

        if (bookAnimProgress == 0)
        {
            activateNextPages();
            audioSource.PlayOneShot(pageTurnSound);
            resetPageAnimation() ;
            animatingPage = true;
        }

        
    }
        


    void animatePage()
    {
        float t = Mathf.InverseLerp(0, pageAnimDur, pageAnimTimer);
        float output = Mathf.Lerp(0, 1, t);

        pageAnimProgress = pageAnimTimer / pageAnimDur;
        if (!halfwayReached)
        {
            pages[currP1].anim(0, 100 * (pageAnimProgress*2));
            pages[currP1].anim(2, 100 * (pageAnimProgress * 2));
            if (pageAnimProgress>=0.5) halfwayReached = true;
        }

        else
        {
            pages[currP1].anim(0, 200-(100 * (pageAnimProgress * 2)));
            pages[currP1].anim(2, 200 - (100 * (pageAnimProgress * 2)));
            pages[currP1].anim(1, (100 * (pageAnimProgress*2))-100);
            
            //this looks jank because it snaps right to 50
        }

        pages[nP1].copyPageProg(pages[currP2]);
        pages[nP2].copyPageProg(pages[currP1]);
      
    }


    public void openBook()
    {
        //skinRender.SetBlendShapeWeight(0, 0);
        countingUp = false;
        Debug.Log("book opened");
    }

    public void closeBook()
    {
       // skinRender.SetBlendShapeWeight(0, 100);
        countingUp = true;
        Debug.Log("book closed");
    }


    public void toggleOpen()
    {
        countingUp = !countingUp;
    }

}
