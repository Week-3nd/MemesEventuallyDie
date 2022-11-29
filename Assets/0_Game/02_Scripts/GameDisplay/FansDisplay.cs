using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FansDisplay : MonoBehaviour
{
    public SceneToSceneDataKeeper dataKeeper;
    public float totalFansTextSize;

    void Start()
    {
        this.GetComponent<TextMeshPro>().text = "<size=" + totalFansTextSize + "><b>" + dataKeeper.GetFansList().Count.ToString() + "</b></size> total fans";
    }
}
