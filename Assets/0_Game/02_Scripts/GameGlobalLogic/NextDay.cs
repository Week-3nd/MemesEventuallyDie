using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextDay : MonoBehaviour
{
    public SceneToSceneDataKeeper DataKeeperReference;
    
    
    void Start()
    {
        this.GetComponent<TextMeshProUGUI>().text = new string("Day <size=14><b>" + DataKeeperReference.GetCurrentDay().ToString())+ "</b></size>";
    }
}
