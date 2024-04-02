using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Cust3 : Ghost
{

    public TextMeshProUGUI timerText;

    public float maxTime;

    float timer;

    bool countingDown;

    bool failed = false;

    public UnityEvent testEvent;

    // Start is called before the first frame update
    void Start()
    {
        timerText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = timer.ToString();
        if (countingDown && timer>0)
        {
            timer-=Time.deltaTime;

            if (timer <= 0) fail();
        }        
    }
    void showTimer()
    {
        timerText.gameObject.SetActive(true);
    }

    void startTimer()
    {
        countingDown = true;
       
    }

    void fail()
    {
        tempMessage("Not fast enough. Let's go again.");
        failed = true;
        countingDown = false;
    }
    //okay so. when you click on the text box the first time, it should. um. wait. um. maybe just pop up a UI button and have that trigger it? that might be easier


    void resetTimer()
    {
        timer = maxTime;
        countingDown = false;
        failed = false;
        
    }

    void stopTimer()
    {
        //wait no this is unneccessary. um
    }
    //what
    //ok so update the text checker next

    //is there an easy way to associate functions with text boxes? wait that'd be so much easier hold on

    public void checkItem(GameObject heldItem)
    {
        Potion thisPotion = heldItem.GetComponent<Potion>();
        if (thisPotion != null)
        {
            if (thisPotion.emissive == true)
            {

                        Debug.Log("potion ingredients length " + thisPotion.getIngredients().Count);
                        HashSet<GameObject> tempSet1 = new HashSet<GameObject>(desiredIngredients);

                        HashSet<GameObject> tempSet2 = new HashSet<GameObject>(thisPotion.getIngredients());

                        //is there a way to check if the contents are the same even if the order is different
                        if (tempSet1.SetEquals(tempSet2))
                        {

                    if (countingDown)
                    {
                        Debug.Log("Order Fulfilled");
                        countingDown = false;
                        orderFulfilled();
                    }

                            
                        }
                        else
                        {
                            tempMessage("This isn't what I ordered...");
                        }

            }
            else
            {
                tempMessage("Uh... I don't think this is stirred...");
            }

        }
        else
        {
            tempMessage("Thanks! ...Uh, what is this?");
        }
    }


    [ContextMenu("fulfill order")]
    public void orderFulfilled()
    {
        base.orderFulfilled();
    }

    //error message?
}
