using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic_CommAndMemeInfluence : MonoBehaviour
{
    [Tooltip("If number of affected Fans reach X value, then add Y number of bots at first generation.")]
    public List<Vector2Int> botsDevelopersInfluence = new List<Vector2Int>();

    [Tooltip("If number of affected Fans reach X value, then generate Y number of memes.")]
    public List<Vector2Int> memersCommittee = new List<Vector2Int>();

    [Tooltip("If number of affected Fans reach X value, then use Y number as fan probability among sharers.")]
    public List<Vector2> convincingStans = new List<Vector2>();

    [Tooltip("If number of affected Fans reach X value, then authorize up to Y posts on faithblog.")]
    public List<Vector2Int> cringeYogisInfluence = new List<Vector2Int>();

    [Tooltip("If number of affected Fans reach X value, then display Y random stats among total on memes.")]
    public List<Vector2Int> memeAnalysts = new List<Vector2Int>();

    [Tooltip("If number of affected Fans reach X value, then use Y number as share probability.")]
    public List<Vector2> clickbaitSpecialists = new List<Vector2>();


    public int GetBotsAmount(int numberOfBotDevFans)
    {
        foreach (Vector2Int currentThreshold in botsDevelopersInfluence)
        {
            if (currentThreshold.x == numberOfBotDevFans)
            {
                return currentThreshold.y;
            }
            if (currentThreshold.x > numberOfBotDevFans)
            {
                return botsDevelopersInfluence[botsDevelopersInfluence.IndexOf(currentThreshold) - 1].y;
            }
        }
        Debug.Log("Non-existing bots developers fan amount asked");
        return 0;
    }


    public int GetMemesAmount(int numberOfMemersFans)
    {
        foreach (Vector2Int currentThreshold in memersCommittee)
        {
            if (currentThreshold.x == numberOfMemersFans)
            {
                return currentThreshold.y;
            }
            if (currentThreshold.x > numberOfMemersFans)
            {
                return memersCommittee[memersCommittee.IndexOf(currentThreshold) - 1].y;
            }
        }
        Debug.Log("Non-existing memers committee amount asked");
        return 0;
    }


    public float GetFanProbability(int numberOfConvincingFans)
    {
        foreach (Vector2 currentThreshold in convincingStans)
        {
            if (currentThreshold.x == numberOfConvincingFans)
            {
                return currentThreshold.y;
            }
            if (currentThreshold.x > numberOfConvincingFans)
            {
                return convincingStans[convincingStans.IndexOf(currentThreshold) - 1].y;
            }
        }
        Debug.Log("Non-existing convincing stans amount asked");
        return 0f;
    }


    public float GetShareProbability(int numberOfClickbaitSpecialists)
    {
        foreach (Vector2 currentThreshold in clickbaitSpecialists)
        {
            if (currentThreshold.x == numberOfClickbaitSpecialists)
            {
                return currentThreshold.y;
            }
            if (currentThreshold.x > numberOfClickbaitSpecialists)
            {
                return clickbaitSpecialists[clickbaitSpecialists.IndexOf(currentThreshold) - 1].y;
            }
        }
        Debug.Log("Non-existing clickbait specialists amount asked");
        return 0f;
    }
}
