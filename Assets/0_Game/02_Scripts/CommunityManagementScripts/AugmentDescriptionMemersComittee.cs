using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AugmentDescriptionMemersComittee : MonoBehaviour
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
        int memesAmount = influence.GetMemesAmount(dataKeeper.GetSpecificCommunityList(2).Count);
        this.GetComponent<TextMeshPro>().text = beforeText + "</i><size=" + sizeOfVariable + "><b>" + memesAmount.ToString() + "</b></size>" + afterText;
    }
}
