using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdvancingText : MonoBehaviour
{
    // Start is called before the first frame update

    //wait no. just make text and overwrite stuff


    public string[] uiText;

    public TextMeshProUGUI textBase;
    
    public GameObject backButton;


    bool isTutorialText;

    EventHandler eventHandler;
    int currTextNum;
    void Start()
    {
        eventHandler = FindAnyObjectByType<EventHandler>();
        if (uiText != null)
        {
            
            changeText();
        }
        backButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextText()
    {
        if (currTextNum < uiText.Length-1)
        {
            currTextNum++;
            changeText();
        }
        else //advance to final frame
        {
            if (this.gameObject.name == "Welcome Background") eventHandler.completeEvent(1);
            
            this.gameObject.SetActive(false);
        }

        
        
    }

    public void lastText()
    {
        if (currTextNum > 0) {
            currTextNum--;
            changeText();
        }

    }


    void changeText()
    {
        textBase.text = uiText[(currTextNum)];
        textBase.CrossFadeAlpha(0f, 0f, true);
        textBase.CrossFadeAlpha(1f, 1f, false);
        if (currTextNum > 0) backButton.SetActive(true);
        else backButton.SetActive(false);
    }





}
