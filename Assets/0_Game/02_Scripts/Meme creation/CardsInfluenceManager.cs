using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsInfluenceManager : MonoBehaviour
{
    private SceneToSceneDataKeeper dataKeeper;
    private GameLogic_CommAndMemeInfluence influence;
    private CardsCreation cardsCreation;

    // displaying the correct cards
    public GameObject objectWith3Cards;
    public GameObject objectWith4Cards;
    public GameObject objectWith5Cards;

    // populating cards
    public List<GameObject> card1Objects;
    public List<GameObject> card2Objects;
    public List<GameObject> card3Objects;
    public List<GameObject> card4Objects;
    public List<GameObject> card5Objects;


    private void Start()
    {
        //Initialisation
        dataKeeper = FindObjectOfType<SceneToSceneDataKeeper>();
        influence = FindObjectOfType<GameLogic_CommAndMemeInfluence>();
        cardsCreation = FindObjectOfType<CardsCreation>();

        // calculating which descriptions to show and which not to
        List<int> fullStatsList = cardsCreation.GetRandomStatsList();
        List<int> usedStatsList = new();
        //usedStatsList.Add(0); // dummy item to avoid errors
        int numberOfRevealedStats = influence.GetRevealedInfosAmount(dataKeeper.GetSpecificCommunityList(5).Count);
        //Debug.Log("Number of meme analysts : " + dataKeeper.GetSpecificCommunityList(5).Count);
        for (int i = 0; i < numberOfRevealedStats; i++)
        {
            usedStatsList.Add(fullStatsList[i]);
        }
         /*
        for (int i = 0; i < usedStatsList.Count; i++)
        {
            Debug.Log(usedStatsList[i]);
        } // */

        //Activating the correct object
        int numbersOfCards = influence.GetMemesAmount(dataKeeper.GetSpecificCommunityList(2).Count);

        switch (numbersOfCards)
        {
            case 3:
                objectWith3Cards.SetActive(true);
                objectWith4Cards.SetActive(false);
                objectWith5Cards.SetActive(false);
                break;
            case 4:
                objectWith3Cards.SetActive(false);
                objectWith4Cards.SetActive(true);
                objectWith5Cards.SetActive(false);
                break;
            case 5:
                objectWith3Cards.SetActive(false);
                objectWith4Cards.SetActive(false);
                objectWith5Cards.SetActive(true);
                break;
            default:
                Debug.Log("Wtf les amis!");
                break;
        }

        // populating cards with correct data
        foreach (GameObject cardObject in card1Objects)
        {
            CardData cardData = cardsCreation.GetCardsList()[0];
            cardObject.GetComponentInChildren<CardDataVisualize>().PopulateData(
                Mathf.RoundToInt(cardData.viralityBonus * 100),
                Mathf.RoundToInt(cardData.cringenessBonus * 100),
                cardData.universality,
                cardData.botShare);
            cardObject.GetComponentInChildren<CardDataVisualize>().DisplayStats(usedStatsList);
        }

        foreach (GameObject cardObject in card2Objects)
        {
            CardData cardData = cardsCreation.GetCardsList()[1];
            cardObject.GetComponentInChildren<CardDataVisualize>().PopulateData(
                Mathf.RoundToInt(cardData.viralityBonus * 100),
                Mathf.RoundToInt(cardData.cringenessBonus * 100),
                cardData.universality,
                cardData.botShare);
            cardObject.GetComponentInChildren<CardDataVisualize>().DisplayStats(usedStatsList);
        }

        foreach (GameObject cardObject in card3Objects)
        {
            CardData cardData = cardsCreation.GetCardsList()[2];
            cardObject.GetComponentInChildren<CardDataVisualize>().PopulateData(
                Mathf.RoundToInt(cardData.viralityBonus * 100),
                Mathf.RoundToInt(cardData.cringenessBonus * 100),
                cardData.universality,
                cardData.botShare);
            cardObject.GetComponentInChildren<CardDataVisualize>().DisplayStats(usedStatsList);
        }

        foreach (GameObject cardObject in card4Objects)
        {
            CardData cardData = cardsCreation.GetCardsList()[3];
            cardObject.GetComponentInChildren<CardDataVisualize>().PopulateData(
                Mathf.RoundToInt(cardData.viralityBonus * 100),
                Mathf.RoundToInt(cardData.cringenessBonus * 100),
                cardData.universality,
                cardData.botShare);
            cardObject.GetComponentInChildren<CardDataVisualize>().DisplayStats(usedStatsList);
        }

        foreach (GameObject cardObject in card5Objects)
        {
            CardData cardData = cardsCreation.GetCardsList()[4];
            cardObject.GetComponentInChildren<CardDataVisualize>().PopulateData(
                Mathf.RoundToInt(cardData.viralityBonus * 100),
                Mathf.RoundToInt(cardData.cringenessBonus * 100),
                cardData.universality,
                cardData.botShare);
            cardObject.GetComponentInChildren<CardDataVisualize>().DisplayStats(usedStatsList);
        }



    }


}
