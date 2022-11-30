using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayTotalWin : MonoBehaviour
{
    public TextMeshPro winningText;
    public float normalTextSize;
    public float emphasizedTextSize;
    public string beforeText;
    public string afterText;

    private void Start()
    {
        SceneToSceneDataKeeper dataKeeper = FindObjectOfType<SceneToSceneDataKeeper>();

        winningText.text = "<size=" + normalTextSize + ">" + beforeText
            + "<b><size=" + emphasizedTextSize + ">" + dataKeeper.GetCurrentDay()
            + "</b><size=" + normalTextSize + ">" + afterText;
    }
}
