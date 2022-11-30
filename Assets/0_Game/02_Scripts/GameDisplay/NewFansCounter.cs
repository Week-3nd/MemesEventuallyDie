using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;

public class NewFansCounter : MonoBehaviour
{
    private int fansCount = 0;
    private int remainingFans = 1;
    private int displayedFans = 0;
    public float timeBetweenIncrements = 0.05f;
    private float timer = 0.0f;
    private bool canCount = false;
    private TextMeshProUGUI uGUI;
    public float delayBeforeCount;
    private float delayTimer = 0.0f;

    public float fanNumberSize;
    public float fanTextSize;

    public StudioEventEmitter audioEvent;

    public GameObject nextButton;
    public GameObject victoryButton;
    private bool thisIsWin;

    private void Start()
    {
        uGUI = this.GetComponent<TextMeshProUGUI>();
    }

    
    void Update()
    {
        if (canCount)
        {
            timer += Time.deltaTime;
            delayTimer += Time.deltaTime;
            if (timer >= timeBetweenIncrements && delayTimer >= delayBeforeCount && remainingFans != 0)
            {
                remainingFans = fansCount - displayedFans;
                displayedFans += Mathf.Clamp(1, 0, remainingFans+1);
                timer = 0;
                
                //remainingFans = fansCount - displayedFans;
                if (remainingFans == 0) // stop audio
                {
                    canCount = false;
                    if (thisIsWin)
                    {
                        victoryButton.SetActive(true);
                    }
                    else
                    {
                        nextButton.SetActive(true);
                    }
                }
                else
                {
                    uGUI.text = "<b><size=" + fanNumberSize + ">" + displayedFans.ToString() + "</b></size><size=" + fanTextSize + "> new fans";
                    audioEvent.Play();
                }
            }
        }
    }

    public void DisplayNewFans ()
    {
        canCount = true;
    }

    public void SetFansCount(int numberOfFans)
    {
        fansCount = numberOfFans;
    }

    public void TypeOfEnd(bool isWin)
    {
        thisIsWin = isWin;
    }
}
