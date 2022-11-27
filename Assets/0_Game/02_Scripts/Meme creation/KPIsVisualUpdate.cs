using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KPIsVisualUpdate : MonoBehaviour
{
    private SceneToSceneDataKeeper dataKeeper;
    private GameLogic_CommAndMemeInfluence influence;


    public float textSize;
    public float numberSize;

    public TextMeshProUGUI dateUI;
    public string dateBeforeText;
    public string dateAfterText;

    public TextMeshProUGUI botsUI;
    public string botsBeforeText;
    public string botsAfterText;

    public TextMeshProUGUI viralityUI;
    public string viralityBeforeText;
    public string viralityAfterText;

    public TextMeshProUGUI fBPostChanceUI;
    public string fBPostChanceBeforeText;
    public string fBPostChanceAfterText;

    public TextMeshProUGUI fBPostLimitUI;
    public string fBPostLimitBeforeText;
    public string fBPostLimitAfterText;



    void Start()
    {
        dataKeeper = FindObjectOfType<SceneToSceneDataKeeper>();
        influence = FindObjectOfType<GameLogic_CommAndMemeInfluence>();

        int currentDay = dataKeeper.GetCurrentDay();
        dateUI.text = "<size=" + textSize + ">" + dateBeforeText
            + "<b></size><size=" + numberSize + ">" + currentDay.ToString()
            + "</b></size><size=" + textSize + ">" + dateAfterText;

        int botsAmount = influence.GetBotsAmount(dataKeeper.GetSpecificCommunityList(1).Count);
        botsUI.text = "<size=" + textSize + ">" + botsBeforeText
            + "<b></size><size=" + numberSize + ">" + botsAmount.ToString()
            + "</b></size><size=" + textSize + ">" + botsAfterText;

        int avrgVirality =
            Mathf.RoundToInt(
                ((influence.GetShareProbability(dataKeeper.GetSpecificCommunityList(6).Count).x
                + influence.GetShareProbability(dataKeeper.GetSpecificCommunityList(6).Count).y)
                / 2) * 100);
        viralityUI.text = "<size=" + textSize + ">" + viralityBeforeText
            + "<b></size><size=" + numberSize + ">" + avrgVirality.ToString()
            + "%</b></size><size=" + textSize + ">" + viralityAfterText;

        int fBProba = 5;
        fBPostChanceUI.text = "<size=" + textSize + ">" + fBPostChanceBeforeText
            + "<b></size><size=" + numberSize + ">" + fBProba.ToString()
            + "%</b></size><size=" + textSize + ">" + fBPostChanceAfterText;

        int maxFBPosts = influence.GetAuthorizedFailsAmount(dataKeeper.GetSpecificCommunityList(4).Count);
        fBPostLimitUI.text = "<size=" + textSize + ">" + fBPostLimitBeforeText
            + "<b></size><size=" + numberSize + ">" + maxFBPosts.ToString()
            + "</b></size><size=" + textSize + ">" + fBPostLimitAfterText;
    }

}
