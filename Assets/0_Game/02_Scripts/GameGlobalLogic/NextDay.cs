using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextDay : MonoBehaviour
{
    public SceneToSceneDataKeeper DataKeeperReference;
    public float dayNumberSize = 14f;
    
    void Start()
    {
        this.GetComponent<TextMeshProUGUI>().text = new string("Day <size="+dayNumberSize+"><b>" + DataKeeperReference.GetCurrentDay().ToString())+ "</b></size>";
    }
}
