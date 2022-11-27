using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewFansCounter : MonoBehaviour
{
    private int fansCount = 0;
    private int remainingFans = 0;
    private int displayedFans = 0;
    public float timeBetweenIncrements = 0.05f;
    private float timer = 0.0f;
    private bool canCount = false;
    private TextMeshProUGUI uGUI;


    private void Start()
    {
        uGUI = this.GetComponent<TextMeshProUGUI>();
    }

    
    void Update()
    {
        if (canCount)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenIncrements)
            {
                remainingFans = fansCount - displayedFans;
                displayedFans += Mathf.Clamp(Mathf.RoundToInt(timer / timeBetweenIncrements), 0, remainingFans); // si on a passé plus du double de temps on ajoute 2
                uGUI.text = displayedFans.ToString();
                timer = 0;
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
}
