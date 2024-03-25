using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLDbookscript : MonoBehaviour
{
    public SkinnedMeshRenderer skinRender;
    // Start is called before the first frame update

    public float animDur, pageAnimDur = 1;

    float animTimer, pageAnimTimer;
    bool countingUp = true;

    float bookAnimProgress = 1f;

    float pageAnimProgress = 1f;

    public GameObject page1, page2;
    public SkinnedMeshRenderer page1Mesh, page2Mesh;


    public GameObject[] pages;
    SkinnedMeshRenderer[] pageSkins;
    Page[] actualPages;


    //start with pages 0 and 1
    //maybe just make a new page class? idk
    //when turning starts, make the two new pages active
    //new page 1 should be 0 on everything I think, since it's left side
    //once the two new pages are done, set the two old pages to no longer active

    //new page 2 should be on the other side of old page 1; i.e. it should have the same blendshape weights
    //you should also get the skinnedmeshrenderers of them



    public SkinnedMeshRenderer page1Secret, page2Secret;


    public float PageTurnSpeed;


    bool halfwayReached = false;

    bool animatingPage;

    GameObject newP1, newP2;


    public TextureCombiner pageTextureHolder;


    int pageTextureNum = 0;

    int currPageIndex = 0; //should update by 2 every call (?)
    //

    int currPage1, currPage2;

    public float turnSpeedP2;


    public AudioSource audioSource;
    public AudioClip pageTurnSound;

    void Start()
    {
        pageSkins = new SkinnedMeshRenderer[pages.Length];
        actualPages = new Page[pages.Length];
        for (int i = 0; i < pages.Length; i++)
        {
            pageSkins[i] = pages[i].GetComponent<SkinnedMeshRenderer>();
            actualPages[i] = pages[i].GetComponent<Page>();
            pages[i].SetActive(false);
        }
        pages[0].SetActive(true);
        pages[1].SetActive(true);

        skinRender.SetBlendShapeWeight(0, 100 * bookAnimProgress);

        pageSkins[0].SetBlendShapeWeight(0, 100);
        pageSkins[1].SetBlendShapeWeight(0, 100);

        copyAnimProg(page1Secret, 1);
        copyAnimProg(page2Secret, 2);

        page1Mesh.material = new Material(page1Mesh.material);
        // page1Mesh.material.mainTexture = pageTextureHolder.getPageTexture(1);

        page2Mesh.material = new Material(page2Mesh.material);
        //  page2Mesh.material.mainTexture = pageTextureHolder.getPageTexture(0);
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

        newP1 = pages[(currPageIndex + 2) % pages.Length];
        newP2 = pages[(currPageIndex + 3) % pages.Length];
        //values should wrap around if they go out of bounds

        pages[(currPageIndex + 2) % pages.Length].SetActive(true);
        pages[(currPageIndex + 3) % pages.Length].SetActive(true);
        Debug.Log("yeowch");

        //yeah ok so page 1 new should be just blendshapes 0; page 2 should follow page 1(?)


        //also I think the uh what was it reverse thing isn't a good idea; wonder what else you could do



        //maybe just instantiate new objects with the pages instead of making new ones each time???
        //back up. you are going way too complicated on something inconsequential. leave this for now
        //also you still never addressed the actual issue of reversing the pages

        //uh what how am I supposed to um

    }

    void deactivateOldPages()
    {
        //deactivate page from current count & one above; 
        pages[currPageIndex].SetActive(false);
        pages[(currPageIndex + 1) % pages.Length].SetActive(false);

        currPageIndex = (currPageIndex + 2) % pages.Length;
    }



    public void pageTurnv0() //old
    {
        page2Mesh.SetBlendShapeWeight(0, 0);
        page2Mesh.SetBlendShapeWeight(1, 100);
        page2Mesh.SetBlendShapeWeight(2, 0);

        page1Mesh.SetBlendShapeWeight(0, 0);
        page1Mesh.SetBlendShapeWeight(1, 0);
        page1Mesh.SetBlendShapeWeight(2, 0);

        copyAnimProg(page1Secret, 1);
        copyAnimProg(page2Secret, 2);
        StartCoroutine(PageTurnP1());

    }
    public void pageTurn()
    {

        pageSkins[currPageIndex].SetBlendShapeWeight(0, 0);
        pageSkins[currPageIndex].SetBlendShapeWeight(1, 0);
        pageSkins[currPageIndex].SetBlendShapeWeight(2, 0);



        currPage2 = (currPageIndex + 1) % pages.Length;

        pageSkins[currPage2].SetBlendShapeWeight(0, 0);
        pageSkins[currPage2].SetBlendShapeWeight(1, 100);
        pageSkins[currPage2].SetBlendShapeWeight(2, 0);





        activateNextPages();


        //turn page 4 here

        int p3 = (currPageIndex + 2) % pages.Length;
        int p4 = (currPageIndex + 3) % pages.Length;

        pageSkins[p3].SetBlendShapeWeight(0, 0);
        pageSkins[p3].SetBlendShapeWeight(1, 0);
        pageSkins[p3].SetBlendShapeWeight(2, 0);

        pageSkins[p4].SetBlendShapeWeight(0, 0);
        pageSkins[p4].SetBlendShapeWeight(1, 0);
        pageSkins[p4].SetBlendShapeWeight(2, 0);



    }


    void nextPageTextures()
    {
        //no wait uhh ok so here's da plan
        //get the last page's texture before you update it

        page2Mesh.material = new Material(page2Mesh.material);
        //page2Mesh.material.mainTexture = pageTextureHolder.getPageTexture(pageTextureNum);

        //get rid of this area
        /*
        if (pageTextureNum < pageTextureHolder.getTotalTextures())
        {
            pageTextureNum++;
        }
        else pageTextureNum = 0;
        */

        page1Mesh.material = new Material(page1Mesh.material);
        //  page1Mesh.material.mainTexture = pageTextureHolder.getPageTexture(pageTextureNum);
    }


    //maybe put in an int; if it's negative, then uh
    IEnumerator PageTurnP1V0()
    {
        for (float i = 0; i <= 100; i += Time.deltaTime * turnSpeedP2)
        {
            pageSkins[currPageIndex].SetBlendShapeWeight(0, 0);
            pageSkins[currPageIndex].SetBlendShapeWeight(1, 0);
            pageSkins[currPageIndex].SetBlendShapeWeight(2, 0);

            currPage2 = (currPageIndex + 1) % pages.Length;

            pageSkins[currPage2].SetBlendShapeWeight(0, 0);
            pageSkins[currPage2].SetBlendShapeWeight(1, 100);
            pageSkins[currPage2].SetBlendShapeWeight(2, 0);

            if (i >= 100)
            {
                StartCoroutine(PageTurnP2());
                page1Mesh.SetBlendShapeWeight(0, 100);
                page1Mesh.SetBlendShapeWeight(2, 100);

                //ok we still need both pages; don't get rid of page 2; get a new set of pages, i guess
            }
            yield return new WaitForSeconds(PageTurnSpeed);
        }

    }

    IEnumerator PageTurnP1()
    {
        for (float i = 0; i <= 100; i += Time.deltaTime * turnSpeedP2)
        {
            pageSkins[currPageIndex].SetBlendShapeWeight(0, i);
            pageSkins[currPageIndex].SetBlendShapeWeight(2, i);
            currPage2 = (currPageIndex + 3) % pages.Length;
            copyAnimProg(pageSkins[currPageIndex], pageSkins[currPage2]);


            actualPages[currPageIndex].copyAnimProg();
            actualPages[currPage2].copyAnimProg();


            if (i >= 100)
            {
                StartCoroutine(PageTurnP2());
                pageSkins[currPageIndex].SetBlendShapeWeight(0, 100);
                pageSkins[currPageIndex].SetBlendShapeWeight(2, 100);
                copyAnimProg(pageSkins[currPageIndex], pageSkins[currPage2]);
                //ok we still need both pages; don't get rid of page 2; get a new set of pages, i guess
            }
            yield return new WaitForSeconds(PageTurnSpeed);
        }

    }
    //don't animate the page 2 mesh at all. animate new page 1 mesh

    IEnumerator PageTurnP3333333()
    {
        for (float i = 0; i <= 100; i += Time.deltaTime * turnSpeedP2)
        {
            page1Mesh.SetBlendShapeWeight(0, 100 - i);
            page1Mesh.SetBlendShapeWeight(1, i);
            page1Mesh.SetBlendShapeWeight(2, 100 - i);


            if (i >= 100)
            {
                page2Mesh.SetBlendShapeWeight(0, 0);
                page2Mesh.SetBlendShapeWeight(1, 100);
                page2Mesh.SetBlendShapeWeight(2, 0);

                page1Mesh.SetBlendShapeWeight(0, 0);
                page1Mesh.SetBlendShapeWeight(1, 0);
                page1Mesh.SetBlendShapeWeight(2, 0);

            }
            yield return new WaitForSeconds(PageTurnSpeed);
        }
    }

    IEnumerator PageTurnP2() //override p2 with this if you want
    {
        for (float i = 0; i <= 100; i += Time.deltaTime * turnSpeedP2)
        {
            pageSkins[currPageIndex].SetBlendShapeWeight(0, 100 - i);
            pageSkins[currPageIndex].SetBlendShapeWeight(1, i);
            pageSkins[currPageIndex].SetBlendShapeWeight(2, 100 - i);

            copyAnimProg(pageSkins[currPageIndex], pageSkins[currPage2]);

            actualPages[currPageIndex].copyAnimProg();
            actualPages[currPage2].copyAnimProg();
            if (i >= 100)
            {


                pageSkins[currPageIndex].SetBlendShapeWeight(0, 0);
                pageSkins[currPageIndex].SetBlendShapeWeight(1, 0);
                pageSkins[currPageIndex].SetBlendShapeWeight(2, 0);

                copyAnimProg(pageSkins[currPageIndex], pageSkins[currPage2]);
                deactivateOldPages();
            }
            yield return new WaitForSeconds(PageTurnSpeed);
        }
    }


    //uhhh







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

        page1Mesh.SetBlendShapeWeight(0, 100 * bookAnimProgress);
        //page1Mesh.SetBlendShapeWeight(1, 100 * bookAnimProgress);

        page2Mesh.SetBlendShapeWeight(0, 100 * bookAnimProgress);
        page2Mesh.SetBlendShapeWeight(1, 100 - (100 * bookAnimProgress));
        //100 = fully closed
        copyAnimProg(page1Secret, 1);
        copyAnimProg(page2Secret, 2);
    }

    void copyAnimProg(SkinnedMeshRenderer otherPage, int x)
    {

        if (x == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                otherPage.SetBlendShapeWeight(i, page1Mesh.GetBlendShapeWeight(i));

            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                otherPage.SetBlendShapeWeight(i, page2Mesh.GetBlendShapeWeight(i));

            }
        }
    }

    void copyAnimProg(SkinnedMeshRenderer basePage, SkinnedMeshRenderer newPage)
    {
        for (int i = 0; i < 3; i++)
        {
            newPage.SetBlendShapeWeight(i, basePage.GetBlendShapeWeight(i));

        }
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
        page1Mesh.SetBlendShapeWeight(0, 0);
        page1Mesh.SetBlendShapeWeight(1, 0);
        page1Mesh.SetBlendShapeWeight(2, 0);

        page2Mesh.SetBlendShapeWeight(1, 100);
        page2Mesh.SetBlendShapeWeight(0, 0);

        copyAnimProg(page1Secret, 1);
        copyAnimProg(page2Secret, 2);

        halfwayReached = false;
        pageAnimTimer = 0;

        nextPageTextures();
    }


    public void startPageTurning()
    {

        if (bookAnimProgress == 0)
        {
            activateNextPages();
            audioSource.PlayOneShot(pageTurnSound);
            resetPageAnimation();
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
            page1Mesh.SetBlendShapeWeight(0, 100 * (pageAnimProgress * 2));
            page1Mesh.SetBlendShapeWeight(2, 100 * (pageAnimProgress * 2));
            if (pageAnimProgress >= 0.5) halfwayReached = true;
        }

        else
        {
            page1Mesh.SetBlendShapeWeight(0, 200 - (100 * (pageAnimProgress * 2)));
            page1Mesh.SetBlendShapeWeight(2, 200 - (100 * (pageAnimProgress * 2)));
            page1Mesh.SetBlendShapeWeight(1, (100 * (pageAnimProgress * 2)) - 100);

            //this looks jank because it snaps right to 50
        }

        copyAnimProg(page1Secret, 1);
        copyAnimProg(page2Secret, 2);

        //when pageAnimProgress reaches 1, then uhh

        //set halfway reached false on initial call, or something?
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
