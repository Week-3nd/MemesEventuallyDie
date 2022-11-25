using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AugmentDescriptionCringe : MonoBehaviour
{
    private SceneToSceneDataKeeper dataKeeper;
    private GameLogic_CommAndMemeInfluence influence;
    public string beforeText;
    public string afterText;
    public int sizeOfVariable;


    private void Start()
    {
        dataKeeper = FindObjectOfType<SceneToSceneDataKeeper>();
        influence = FindObjectOfType<GameLogic_CommAndMemeInfluence>();


    }

    private void Update()
    {
        int cringeThreshold = influence.GetAuthorizedFailsAmount(dataKeeper.GetSpecificCommunityList(4).Count);
        this.GetComponent<TextMeshPro>().text = beforeText + "</i><size=" + sizeOfVariable + "><b>" + cringeThreshold.ToString() + "</b></size>" + afterText;
    }
}
