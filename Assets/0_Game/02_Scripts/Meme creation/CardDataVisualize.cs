using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardDataVisualize : MonoBehaviour
{
    public TextMeshPro viralityScore;
    public TextMeshPro cringenessScore;
    public TextMeshPro universalityScore;
    public TextMeshPro botCompatibilityScore;
    public Color negativeColor;
    public Color positiveColor;
    public Color neutralColor;

    public int cardIndex;
    public GameObject description01;
    public GameObject fakeDescription01;
    public GameObject description02;
    public GameObject fakeDescription02;
    public GameObject description03;
    public GameObject fakeDescription03;
    private StateAndFeedbacks stateAndFeedbacks;

    private void Start()
    {
        //stateAndFeedbacks = GetComponentInParent<StateAndFeedbacks>();
    }


    public void PopulateData(int virality, int cringeness, int universality, int botShare)
    {
        
        // use sign as it's an addition to a base value
        if (virality > 0)
        {
            viralityScore.text = "<color="+ ToRGBHex(positiveColor)+">+" + virality.ToString(); 
        }
        else if (virality == 0)
        {
            viralityScore.text = "<color=" + ToRGBHex(neutralColor) + ">" + virality.ToString();
        }
        else
        {
            viralityScore.text = "<color=" + ToRGBHex(negativeColor) + ">" + virality.ToString();
        }


        if (cringeness > 0)
        {
            cringenessScore.text = "<color=" + ToRGBHex(negativeColor) + ">+" + cringeness.ToString();
        }
        else if (cringeness == 0)
        {
            cringenessScore.text = "<color=" + ToRGBHex(neutralColor) + ">" + cringeness.ToString();
        }
        else
        {
            cringenessScore.text = "<color=" + ToRGBHex(positiveColor) + ">" + cringeness.ToString();
        }


        // don't use sign as it is the base value
        if (universality <= 5)
        {
            universalityScore.text = "<color=" + ToRGBHex(neutralColor) + ">" + universality.ToString();
        }
        else
        {
            universalityScore.text = "<color=" + ToRGBHex(positiveColor) + ">" + universality.ToString();
        }


        if (botShare <= 3)
        {
            botCompatibilityScore.text = "<color=" + ToRGBHex(neutralColor) + ">" + botShare.ToString();
        }
        else
        {
            botCompatibilityScore.text = "<color=" + ToRGBHex(positiveColor) + ">" + botShare.ToString();
        }

    }


    public static string ToRGBHex(Color c)
    {
        return string.Format("#{0:X2}{1:X2}{2:X2}", ToByte(c.r), ToByte(c.g), ToByte(c.b));
    }

    private static byte ToByte(float f)
    {
        f = Mathf.Clamp01(f);
        return (byte)(f * 255);
    }




    public void DisplayStats(List<int> indexes)
    {
        stateAndFeedbacks = GetComponentInParent<StateAndFeedbacks>();
        //Debug.Log("card index : " + stateAndFeedbacks.cardIndex);
        List<int> thisCardIndexes = new();
        thisCardIndexes.Add(1 + (3 * (cardIndex - 1)));
        thisCardIndexes.Add(2 + (3 * (cardIndex - 1)));
        thisCardIndexes.Add(3 + (3 * (cardIndex - 1)));
        //thisCardIndexes.Add(1);
        /*
        foreach (int indexxx in thisCardIndexes)
        {
            Debug.Log("Card " + cardIndex + ". Contains stat " + indexxx);
        }
        // */

        foreach (int index in indexes)
        {
            if (thisCardIndexes.Contains(index))
            {
                switch (thisCardIndexes.IndexOf(index))
                {
                    case 0:
                        description01.SetActive(true);
                        cringenessScore.gameObject.SetActive(true);
                        fakeDescription01.SetActive(false);
                        break;
                    case 1:
                        description02.SetActive(true);
                        universalityScore.gameObject.SetActive(true);
                        fakeDescription02.SetActive(false);
                        break;
                    case 2:
                        description03.SetActive(true);
                        botCompatibilityScore.gameObject.SetActive(true);
                        fakeDescription03.SetActive(false);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}