using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformHolder : MonoBehaviour
{
    public List<GameObject> TransformList = new List<GameObject>();
    //public int usedSlots = 0;
    public int AugmentIndex;
    private SceneToSceneDataKeeper dataKeeper;

    private void Start()
    {
        dataKeeper = FindObjectOfType<SceneToSceneDataKeeper>();
    }


    public int GetUsedSlots()
    {
        return dataKeeper.GetSpecificCommunityList(AugmentIndex).Count;
    }
}
