using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class RTSLikeCamera : MonoBehaviour
{
    public Camera mainCamera;
    public Vector3 velocity;

    public void OnMouseOver()
    {
        mainCamera.transform.position += (velocity / Time.deltaTime);
        Debug.Log("It's happenning!!");
    }
}
