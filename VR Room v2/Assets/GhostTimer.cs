using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GhostTimer : MonoBehaviour
{


    public TextMeshPro timerText;

    public float maxTime;

    float timer;

    bool countingDown;

    bool failed = false;

    public UnityEvent[] resetEvents;

    public Ghost baseGhost;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = timer.ToString("F2");
        if (countingDown && timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0) fail();
        }
    }

    private void Awake()
    {
        resetTimer();
    }

    void showTimer()
    {
        timerText.gameObject.SetActive(true);
        resetTimer();
    }

    public void startTimer()
    {
        
        countingDown = true;

    }

    void fail()
    {
        baseGhost.tempMessage("Not fast enough. Let's go again.");
        baseGhost.setFinalConditionState(false);
        resetTimer();
    }
    //okay so. when you click on the text box the first time, it should. um. wait. um. maybe just pop up a UI button and have that trigger it? that might be easier


    public void resetTimer()
    {
       foreach(UnityEvent e in resetEvents)
        {
            e.Invoke();
        }
        timer = maxTime;
        countingDown = false;
        failed = false;
    }

    //problem: the potion couldn't also be reset. um. ok let's not worry about that
    

    public void ghostConditionCheck()
    {

        //this would be called when the potion is given, or whatever.
        if (countingDown)
        {
            countingDown = false;
            baseGhost.setFinalConditionState(true);
        }


    }
    //also reset pot, I guess? when it fails

}
