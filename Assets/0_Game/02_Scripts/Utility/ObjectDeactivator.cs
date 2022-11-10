using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeactivator : MonoBehaviour
{
    public GameObject ObjectToDeactivate;

    public void DeactivateTarget()
    {
        ObjectToDeactivate.SetActive(false);
    }
}
