using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AugmentDescriptionClickbaitSpecialists : MonoBehaviour
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
        float shareProba = influence.GetShareProbability(dataKeeper.GetSpecificCommunityList(6).Count).y;
        this.GetComponent<TextMeshPro>().text = beforeText + "</i><size=" + sizeOfVariable + "><b>" + Mathf.RoundToInt(shareProba * 100).ToString() + "%</b></size>" + afterText;
    }
}
