using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FansDisplay : MonoBehaviour
{
    public SceneToSceneDataKeeper dataKeeper;

    void Start()
    {
        this.GetComponent<TextMeshPro>().text = "<size=14><b>" + dataKeeper.GetFansList().Count.ToString() + "</b></size> total fans";
    }
}
