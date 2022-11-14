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
        this.GetComponent<TextMeshProUGUI>().text = "Fans : "+localFansList.Count.ToString();
    }
}
