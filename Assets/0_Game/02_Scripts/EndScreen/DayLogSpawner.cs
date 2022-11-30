using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLogSpawner : MonoBehaviour
{
    public GameObject dayLogObject;
    public Vector3 dayLogsSpacing;
    private Vector3 startPosition;
    private EndScreenInfoManager dataKeeper;

    private void Start()
    {
        dataKeeper = FindObjectOfType<EndScreenInfoManager>();
        SpawnDayLogs(dataKeeper.GetGameHistory());
    }

    public void SpawnDayLogs(List<DayData> gameHistory)
    {
        Vector3 iterationPosition = startPosition;

        foreach (DayData aDayData in gameHistory)
        {
            //Create object
            GameObject aDayLogObject = Instantiate(
                dayLogObject,
                iterationPosition,
                Quaternion.identity,
                transform);

            aDayLogObject.GetComponent<DayLogFiller>().PopulateDayLog(
                aDayData.Day,
                aDayData.Score,
                aDayData.NewFans,
                aDayData.FaithblogPosts);

            //Prepare next row
            iterationPosition += dayLogsSpacing;
        }
    }
}
