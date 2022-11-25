using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AugmentDescriptionBots : MonoBehaviour
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
        int botsAmount = influence.GetBotsAmount(dataKeeper.GetSpecificCommunityList(1).Count);
        this.GetComponent<TextMeshPro>().text = beforeText + "</i><size=" + sizeOfVariable + "><b>" + botsAmount.ToString() + "</b></size>" + afterText;
    }
}
