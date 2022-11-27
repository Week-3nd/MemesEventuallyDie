using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseOverSetObjectActive : MonoBehaviour
{
    public GameObject descriptionObject;
    public float delayToShowDescription;
    private float timer = 0;

    private void OnMouseOver()
    {
        timer += Time.deltaTime;
        if (timer >= delayToShowDescription)
        {
            descriptionObject.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        timer = 0;
        descriptionObject.SetActive(false);
    }
}
