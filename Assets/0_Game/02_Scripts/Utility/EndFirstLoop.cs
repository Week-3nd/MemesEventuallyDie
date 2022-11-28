using UnityEngine;

public class EndFirstLoop : MonoBehaviour
{
    private SceneToSceneDataKeeper dataKeeper;
    public GameObject objectToActivate;
    private bool isFirstLoopDone = false;

    private void Start()
    {
        dataKeeper = FindObjectOfType<SceneToSceneDataKeeper>();
        isFirstLoopDone = !dataKeeper.IsFirstLoop();
        //Debug.Log("if first loop : " + isFirstLoopDone);
    }

    private void Update()
    {
       // /*
       if (isFirstLoopDone)
       {
           objectToActivate.SetActive(true);
       }
       // */
    }

    public void EndTheFirstLoop()
    {
        dataKeeper.FirstLoopDone();
        objectToActivate.SetActive(true);
    }
}
