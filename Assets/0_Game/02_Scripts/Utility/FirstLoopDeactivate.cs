using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLoopDeactivate : MonoBehaviour
{
    private SceneToSceneDataKeeper dataKeeper;
    public GameObject objectToDeactivate;
    private bool hasTried = false;

    private void Start()
    {
        dataKeeper = FindObjectOfType<SceneToSceneDataKeeper>();
    }

    private void Update()
    {
        if (!hasTried)
        {
            if (!dataKeeper.IsFirstLoop())
            {
                //Debug.Log("On est bien en first loop");
                objectToDeactivate.SetActive(true);
            }
            hasTried = true;
        }
        
    }
}
