using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextDay : MonoBehaviour
{
    public SceneToSceneDataKeeper DataKeeperReference;
    
    
    void Start()
    {
        int dayindex = DataKeeperReference.NextDay();
        this.GetComponent<TextMeshProUGUI>().text = new string("Day " + dayindex.ToString());
    }
}
