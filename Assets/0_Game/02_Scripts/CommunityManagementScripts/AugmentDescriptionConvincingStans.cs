using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AugmentDescriptionConvincingStans : MonoBehaviour
{
    private SceneToSceneDataKeeper dataKeeper;
    private GameLogic_CommAndMemeInfluence influence;
    public string beforeText;
    public string middleText;
    public string afterText;
    public int sizeOfVariable;


    private void Start()
    {
        dataKeeper = FindObjectOfType<SceneToSceneDataKeeper>();
        influence = FindObjectOfType<GameLogic_CommAndMemeInfluence>();
    }

    private void Update()
    {
        List<Vector2Int> influenceList = influence.GetFanProbabilities(dataKeeper.GetSpecificCommunityList(3).Count);
        Vector2Int fansAmount = new Vector2Int(influenceList[1].y, influenceList[influenceList.Count - 1].y);

        this.GetComponent<TextMeshPro>().text =
            beforeText + "<size=" + sizeOfVariable + "><b>"
            + fansAmount.x.ToString() + "</b></size>"
            + middleText + "<size=" + sizeOfVariable + "><b>"
            + fansAmount.y.ToString() + "</b></size>"
            +afterText;
    }
}
