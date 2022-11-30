using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenInfoManager : MonoBehaviour
{
    static private List<DayData> gameHistory = new();
    static private bool isFirstGame = true;
    
    public void AddDayData(DayData todaysData)
    {
        gameHistory.Add(todaysData);
    }

    public List<DayData> GetGameHistory()
    {
        return gameHistory;
    }

    public void ResetGameHistory()
    {
        gameHistory.Clear();
    }

    public bool GetIsFirstGame()
    {
        return isFirstGame;
    }
    public void FirstGameDone()
    {
        isFirstGame = false;
    }
}
