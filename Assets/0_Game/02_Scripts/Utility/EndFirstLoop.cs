using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFirstLoop : MonoBehaviour
{
    private SceneToSceneDataKeeper dataKeeper;

    private void Start()
    {
        dataKeeper = FindObjectOfType<SceneToSceneDataKeeper>();
    }

    public void EndTheFirstLoop()
    {
        dataKeeper.FirstLoopDone();
    }
}
