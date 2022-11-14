using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneToSceneDataKeeper : MonoBehaviour
{
    /// <summary>
    /// Index of Day
    /// </summary>
    static private int DayIndex = 0;

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
    static private List<TreeNode> FansList = new List<TreeNode>();

    public List<TreeNode> GetFansList()
    {
        return FansList;
    }
    public void AddFansToList(List<TreeNode> NewFans)
    {
        FansList.AddRange(NewFans);
    }



    static private List<List<TreeNode>> CommunityLists = new List<List<TreeNode>>();

    public List<List<TreeNode>> GetCommunityLists()
    {
        return CommunityLists;
    }
    public List<TreeNode> GetSpecificCommunityList(int index)
    {
        return CommunityLists[index];
    }

    public void AddUserToCommunityList(TreeNode user, int ListIndex)
    {
        CommunityLists[ListIndex].Add(user);
    }

}
