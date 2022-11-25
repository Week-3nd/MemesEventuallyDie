using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AugmentDescriptionMemeAnalysts : MonoBehaviour
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
        int memesStatsUncovered = influence.GetRevealedInfosAmount(dataKeeper.GetSpecificCommunityList(5).Count);
        this.GetComponent<TextMeshPro>().text = beforeText + "</i><size=" + sizeOfVariable + "><b>" + memesStatsUncovered.ToString() + "</b></size>" + afterText;
    }
}
