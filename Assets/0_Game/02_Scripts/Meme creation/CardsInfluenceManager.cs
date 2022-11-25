using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsInfluenceManager : MonoBehaviour
{
    public GameObject objectWith3Cards;
    public GameObject objectWith4Cards;
    public GameObject objectWith5Cards;

    private SceneToSceneDataKeeper dataKeeper;
    private GameLogic_CommAndMemeInfluence influence;

    private void Start()
    {
        //Initialisation
        dataKeeper = FindObjectOfType<SceneToSceneDataKeeper>();
        influence = FindObjectOfType<GameLogic_CommAndMemeInfluence>();

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
    }
}
