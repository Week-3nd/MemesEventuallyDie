using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsCreation : MonoBehaviour
{
    static private List<CardData> cards;
    static public int selectedCardIndex;


    // virality variables
    public float baseMinViralityBonus = -0.2f;
    public float baseMaxViralityBonus = 0.3f;
    public float extendedMinViralityBonus = -0.6f;
    public float extendedMaxViralityBonus = 0.7f;

    // cringeness variables
    public float baseMinCringenessBonus = -0.1f;
    public float baseMaxCringenessBonus = 0.2f;
    public float extendedMinCringenessBonus = -0.3f;
    public float extendedMaxCringenessBonus = 1.0f;

    // universality variables
    public int baseMinUniversality = 4;
    public int baseMaxUniversality = 5;
    public int extendedMinUniversality = 4;
    public int extendedMaxUniversality = 10;

    // bot share variables
    public int baseMinBotShares = 2;
    public int baseMaxBotShares = 3;
    public int extendedMinBotShares = 1;
    public int extendedMaxBotShares = 5;

    public void GenerateCards()
    {
        // first 3 cards have basic tradeoffs, card 4 & 5 have more random stats
        cards = new List<CardData>();
        for (int i = 0; i < 5; i++)
        {
            CardData newCard = new CardData();

            switch (i)
            {
                case < 3:
                    // rounding to get nice 0.1f steps
                    newCard.viralityBonus = Random.Range(Mathf.RoundToInt((baseMinViralityBonus * 100.0f)), Mathf.RoundToInt((baseMaxViralityBonus * 100.0f) + 1.0f)) / 100.0f; 
                    newCard.cringenessBonus = Random.Range(Mathf.RoundToInt(baseMinCringenessBonus * 100.0f), Mathf.RoundToInt((baseMaxCringenessBonus * 100.0f) + 1.0f )) / 100.0f;
                    // ints don't need rounding
                    newCard.universality = Random.Range(baseMinUniversality, baseMaxUniversality + 1);
                    newCard.botShare = Random.Range(baseMinBotShares, baseMaxBotShares + 1);
                     /*
                    Debug.Log("Card " + i
                        + " | Virality : " + newCard.viralityBonus
                        + " | Cringeness : " + newCard.cringenessBonus
                        + " | Universality : " + newCard.universality
                        + " | Bot share : " + newCard.botShare);
                    // */
                    break;
                case >= 3:
                    newCard.viralityBonus = Random.Range(Mathf.RoundToInt(extendedMinViralityBonus * 100.0f), Mathf.RoundToInt((extendedMaxViralityBonus * 100.0f) + 1.0f)) / 100.0f;
                    newCard.cringenessBonus = Random.Range(Mathf.RoundToInt(extendedMinCringenessBonus * 100.0f), Mathf.RoundToInt((extendedMaxCringenessBonus * 100.0f) + 1.0f)) / 100.0f;
                    newCard.universality = Random.Range(extendedMinUniversality, extendedMaxUniversality + 1);
                    newCard.botShare = Random.Range(extendedMinBotShares, extendedMaxBotShares + 1);
                    break;
            }

            cards.Add(newCard);
        }
        //Debug.Log("Cards generated. Amount : " + cards.Count);
    }

    public List<CardData> GetCardsList()
    {
        return cards;
    }

    public int GetSelectedCardIndex()
    {
        return selectedCardIndex;
    }
    public void SetSelectedCardIndex(int index)
    {
        selectedCardIndex = index;
    }
}
