using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class OnHoverShowObject : MonoBehaviour
{

    public GameObject descriptionObject;
    public float delayToShowDescription;
    private float timer = 0;
    private StateAndFeedbacks cardState;
    private Camera theCamera;

    private void Start()
    {
        theCamera = FindObjectOfType<Camera>();
        cardState = this.GetComponentInParent<StateAndFeedbacks>();
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = theCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 newObjectPosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z);
        descriptionObject.transform.position = newObjectPosition;
    }

    private void OnMouseOver()
    {
        timer += Time.deltaTime;
        cardState.isHovered = true;

        if (timer >= delayToShowDescription)
        {
            descriptionObject.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        timer = 0;
        cardState.isHovered = false;
        descriptionObject.SetActive(false);
    }

    private void OnMouseUpAsButton()
    {
        cardState.OnMouseUpAsButton();
    }
}
