using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EyeContact : LookInteractable
{
    // Start is called before the first frame update

    public TextMeshPro timerText;

    public float timeLeniency = 5f; //amount of time that can pass without eye contact at once

    float timer;

    bool countingDown;

    bool failed = false;

    public UnityEvent[] resetEvents;

    public Ghost baseGhost;

    bool eyeContactHeld;

    public ParticleSystem[] eyeParticles;

    bool started;

    void Start()
    {
        Debug.Log(eyeParticles.ToString());
        foreach (ParticleSystem e in eyeParticles)
        {
            e.Stop();

        }
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = timer.ToString("F2");
        if (!eyeContactHeld && timer > 0 && countingDown)
        {
            timer -= Time.deltaTime;

            if (timer <= 0) fail();
        }
    }
    private void Awake()
    {
        resetTimer();
        foreach (ParticleSystem e in eyeParticles)
        {
            Debug.Log("uh"+e.ToString());

        }
    }
    void fail()
    {
        baseGhost.tempMessage("Erm... did you forget about the customer?");
        baseGhost.setFinalConditionState(false);
        resetTimer();
        foreach (ParticleSystem e in eyeParticles)
        {
            e.Stop();

        }
    }

   

    void showTimer()
    {
        timerText.gameObject.SetActive(true);
        resetTimer();
    }

    public void startTimer()
    {

        countingDown = true;
        foreach(ParticleSystem e in eyeParticles) {
            e.Play();
        }
        failed = false;

    }


    public void resetTimer()
    {
        foreach (UnityEvent e in resetEvents)
        {
            e.Invoke();
        }
        timer = timeLeniency;
        countingDown = false;
        failed = true;
    }

    //problem: the potion couldn't also be reset. um. ok let's not worry about that


    public void ghostConditionCheck()
    {

        //this would be called when the potion is given, or whatever.
        if (timer>0 && countingDown)
        {

            foreach (ParticleSystem e in eyeParticles)
            {
                e.Stop();

            }
            baseGhost.setFinalConditionState(true);
        }


    }


    public override void LookTriggerThis() {

        if (countingDown)
        {
            eyeContactHeld = true;
            timer = timeLeniency;

            foreach (ParticleSystem e in eyeParticles)
            {
                e.Stop();

            }
        }
    }

    public override void StopLookTrigger()
    {
        if (countingDown)
        {
            eyeContactHeld = false;
            foreach (ParticleSystem e in eyeParticles)
            {
                e.Play();
            }
        }
           
    }
}
