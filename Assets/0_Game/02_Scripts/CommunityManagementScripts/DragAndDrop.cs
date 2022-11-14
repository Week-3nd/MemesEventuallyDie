using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    public float SweepingInTime = 0.5f;
    private float SweepingTimer;
    private bool isSweeping = false;
    private Vector3 onMouseDownDeltaToMouse = new Vector3();
    private Camera theOneAndOnlyCamera;

    private void Start()
    {
        theOneAndOnlyCamera = FindObjectOfType<Camera>();
    }

    private void OnMouseDown()
    {
        SweepingTimer = 0.0f;
        isSweeping = true;
        Vector3 mouse3DWorldPosition = theOneAndOnlyCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 Mouse2DWorldPosition = new Vector3(mouse3DWorldPosition.x, mouse3DWorldPosition.y, transform.position.z);
        onMouseDownDeltaToMouse = Mouse2DWorldPosition - this.transform.position;
    }

    private void OnMouseUp()
    {
        
    }

    private void OnMouseDrag()
    {
        Vector3 mouseWorldPosition = theOneAndOnlyCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 newObjectPosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z);

        if (isSweeping)
        {
            transform.position = Vector3.Lerp(newObjectPosition - onMouseDownDeltaToMouse, newObjectPosition, SweepingTimer / SweepingInTime);
        }
        else
        {
            transform.position = newObjectPosition;
        }
    }

    private void Update()
    {
        if (isSweeping)
        {
            SweepingTimer += Time.deltaTime;
            if (SweepingTimer >= SweepingInTime)
            {
                isSweeping = false;
            }
        } 
    }
}
