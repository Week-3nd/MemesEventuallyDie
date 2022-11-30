using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class RTSLikeCamera : MonoBehaviour
{
    public Camera mainCamera;
    public Vector3 velocity;
    private float maxVertical;

    private void Start()
    {
        maxVertical = mainCamera.transform.position.y;
    }
    public void OnMouseOver()
    {
        mainCamera.transform.position += velocity * Time.deltaTime;
        // /*
        if (mainCamera.transform.position.y > maxVertical)
        {
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, maxVertical, mainCamera.transform.position.z);
        } // */
        //Debug.Log("It's happenning!!");
    }
}
