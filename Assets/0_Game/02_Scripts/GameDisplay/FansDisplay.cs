using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FansDisplay : MonoBehaviour
{
    public SceneToSceneDataKeeper dataKeeper;

    private List<TreeNode> localFansList = new List<TreeNode>();

    void Start()
    {
        localFansList = dataKeeper.GetFansList();
        this.GetComponent<TextMeshProUGUI>().text = "<size=14><b>" + localFansList.Count.ToString()+ "</b></size> total fans";
    }
}
