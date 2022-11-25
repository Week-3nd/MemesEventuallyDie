using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic_CommAndMemeInfluence : MonoBehaviour
{
    [Tooltip("If number of affected Fans reach X value, then add Y number of bots at first generation.")]
    public List<Vector2Int> botsDevelopersInfluence = new List<Vector2Int>();

    [Tooltip("If number of affected Fans reach X value, then generate Y number of memes.")]
    public List<Vector2Int> memersCommittee = new List<Vector2Int>();

    [Tooltip("If number of affected Fans reach the X value, then you get Z fans for passing over Y number of sharers during spread.")]
    public List<Vector3Int> convincingStans = new List<Vector3Int>();

    [Tooltip("If number of affected Fans reach X value, then authorize up to Y posts on faithblog.")]
    public List<Vector2Int> cringeYogisInfluence = new List<Vector2Int>();

    [Tooltip("If number of affected Fans reach X value, then display Y random stats among total on memes.")]
    public List<Vector2Int> memeAnalysts = new List<Vector2Int>();

    [Tooltip("If number of affected Fans reach X value, then use Y as low share probability, Z as high share probability.")]
    public List<Vector3> clickbaitSpecialists = new List<Vector3>(); // base : x = 0, y = 0.2, z = 0.28



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
        return botsDevelopersInfluence[botsDevelopersInfluence.Count - 1].y;
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
        return memersCommittee[memersCommittee.Count - 1].y;
    }


    public List<Vector2Int> GetFanProbabilities(int numberOfConvincingFans)
    {
        List<Vector2Int> fanProbabilities = new();
        bool isExactThreshold = false;
        foreach (Vector3Int currentIndex in convincingStans)
        {
            if (currentIndex.x == numberOfConvincingFans)
            {
                isExactThreshold = true;
                fanProbabilities.Add(new Vector2Int(currentIndex.y, currentIndex.z));
            }
            if (currentIndex.x > numberOfConvincingFans && isExactThreshold == false)
            {
                //si on ne tombe pas pile sur un index à un moment :
                //on refait la boucle pour faire la liste de tous les éléments dont x est égal au palier immédiamtement inférieur
                int exactThreshold = convincingStans[convincingStans.IndexOf(currentIndex) - 1].x;
                foreach (Vector3Int iteration2 in convincingStans)
                {
                    if (iteration2.x == exactThreshold)
                    {
                        isExactThreshold = true;
                        fanProbabilities.Add(new Vector2Int(iteration2.y, iteration2.z));
                    }
                    if (iteration2.x > exactThreshold)
                    {
                        return fanProbabilities;
                    }
                }
            }

        }

        return fanProbabilities;
    }


    public int GetAuthorizedFailsAmount(int numberOfCringeYogis)
    {
        foreach (Vector2Int currentThreshold in cringeYogisInfluence)
        {
            if (currentThreshold.x == numberOfCringeYogis)
            {
                return currentThreshold.y;
            }
            if (currentThreshold.x > numberOfCringeYogis)
            {
                return cringeYogisInfluence[cringeYogisInfluence.IndexOf(currentThreshold) - 1].y;
            }
        }
        return cringeYogisInfluence[cringeYogisInfluence.Count - 1].y;
    }


    public int GetRevealedInfosAmount(int numberOfMemeAnalysts)
    {
        foreach (Vector2Int currentThreshold in memeAnalysts)
        {
            if (currentThreshold.x == numberOfMemeAnalysts)
            {
                return currentThreshold.y;
            }
            if (currentThreshold.x > numberOfMemeAnalysts)
            {
                return memeAnalysts[memeAnalysts.IndexOf(currentThreshold) - 1].y;
            }
        }
        return memeAnalysts[memeAnalysts.Count - 1].y;
    }


    public Vector2 GetShareProbability(int numberOfClickbaitSpecialists)
    {
        foreach (Vector3 currentThreshold in clickbaitSpecialists)
        {
            if (currentThreshold.x == numberOfClickbaitSpecialists)
            {
                return new Vector2(currentThreshold.y,currentThreshold.z);
            }
            if (currentThreshold.x > numberOfClickbaitSpecialists)
            {
                return new Vector2(clickbaitSpecialists[clickbaitSpecialists.IndexOf(currentThreshold) - 1].y, clickbaitSpecialists[clickbaitSpecialists.IndexOf(currentThreshold) - 1].z);
            }
        }
        return new Vector2(clickbaitSpecialists[clickbaitSpecialists.Count - 1].y, clickbaitSpecialists[clickbaitSpecialists.Count - 1].z);
    }
}
