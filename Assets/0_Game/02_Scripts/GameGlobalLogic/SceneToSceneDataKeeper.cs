using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneToSceneDataKeeper : MonoBehaviour
{
    /// <summary>
    /// Index of Day
    /// </summary>
    static public int DayIndex = 0;

    public int NextDay()
    {
        DayIndex++;
        return DayIndex;
    }
    public int GetCurrentDay()
    {
        return DayIndex;
    }



    /// <summary>
    /// List of ALL fans, wether they are available or tasked for something
    /// </summary>
    static public List<TreeNode> FansList = new List<TreeNode>();

    public List<TreeNode> GetFansList()
    {
        return FansList;
    }
    public void AddFansToList(List<TreeNode> NewFans)
    {
        FansList.AddRange(NewFans);
    }


}
