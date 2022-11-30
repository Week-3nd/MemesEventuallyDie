using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayLogFiller : MonoBehaviour
{
    public float normalTextSize;
    public float emphasizedTextSize;
    public float superEmphasizedTextSize;
    
    public TextMeshPro dayIndex;
    public string dayBefore;
    public string dayAfter;

    public TextMeshPro dayScore;
    public string scoreBefore;
    public string scoreAfter;

    public TextMeshPro dayNewFans;
    public string fansBefore;
    public string fansAfter;

    public TextMeshPro dayFBPosts;
    public string fBBefore;
    public string fBAfter;

    public void PopulateDayLog(int thisDayIndex, int thisDayScore, int thisDayNewFans, int thisDayFBPosts)
    {
        dayIndex.text =
            "<size=" + normalTextSize + ">" + dayBefore
            + "<b><size=" + emphasizedTextSize + ">" + thisDayIndex +
            "</b><size=" + normalTextSize + ">" + dayAfter;

        dayScore.text =
            "<size=" + normalTextSize + ">" + scoreBefore
            + "<b><size=" + superEmphasizedTextSize + ">" + thisDayScore +
            "</b><size=" + normalTextSize + ">" + scoreAfter;

        dayNewFans.text =
            "<size=" + normalTextSize + ">" + fansBefore
            + "<b><size=" + emphasizedTextSize + ">" + thisDayNewFans +
            "</b><size=" + normalTextSize + ">" + fansAfter;

        dayFBPosts.text =
            "<size=" + normalTextSize + ">" + fBBefore
            + "<b><size=" + emphasizedTextSize + ">" + thisDayFBPosts +
            "</b><size=" + normalTextSize + ">" + fBAfter;
    }

}
