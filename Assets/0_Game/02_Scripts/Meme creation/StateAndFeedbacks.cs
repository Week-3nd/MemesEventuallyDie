using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateAndFeedbacks : MonoBehaviour
{
    private bool isHovered = false;
    public bool isSelected = false;
    public GameObject card;
    public float scaleDuration = 0.2f;
    public float selectedOrHoveredScale = 1.1f;
    private float scaleTimer = 0.0f;
    public GameObject validateButton;
    


    // Update is called once per frame
    void Update()
    {
        if (isHovered || isSelected)
        {
            scaleTimer = Mathf.Clamp(scaleTimer + Time.deltaTime,0,scaleDuration);
        }
        else
        {
            scaleTimer = Mathf.Clamp(scaleTimer - Time.deltaTime, 0, scaleDuration);
        }

        float timeRatio = scaleTimer / scaleDuration;
        float scaleRatio = Mathf.Lerp(1, selectedOrHoveredScale, timeRatio);
        card.transform.localScale = new Vector3(scaleRatio, scaleRatio, 1);
        //Debug.Log("Time ratio : " + timeRatio + " | Scale ratio : " + scaleRatio);
    }

    private void OnMouseOver()
    {
        isHovered = true;
    }

    private void OnMouseExit()
    {
        isHovered = false;
    }

    private void OnMouseUpAsButton()
    {
        StateAndFeedbacks[] allOthers = FindObjectsOfType<StateAndFeedbacks>();
        foreach (StateAndFeedbacks otherState in allOthers)
        {
            otherState.isSelected = false;
        }
        this.isSelected = true;
        validateButton.SetActive(true);
    }
}
