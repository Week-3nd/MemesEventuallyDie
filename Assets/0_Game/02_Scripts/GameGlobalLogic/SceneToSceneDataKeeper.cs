using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SceneToSceneDataKeeper : MonoBehaviour
{
    /// <summary>
    /// Index of Day
    /// </summary>
    static private int DayIndex = 0;

    public void NextDay()
    {
        DayIndex++;
        //return DayIndex;
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
        CommunityLists[0].AddRange(NewFans);
    }


    /// <summary>
    /// Community lists contains all fans and their activity. A fan can only be in one of the lists at a time.
    /// </summary>
    static private List<List<TreeNode>> CommunityLists;

    // Listes : (augments as of Y22 M11 D21)
    // 0 = unemployed
    // 1 = Augment 1 (Early share squad)
    // 2 = Augment 2 (Memers committee)
    // 3 = Augment 3 (Conving stans)
    // 4 = Augment 4 (cringe Yogis)
    // 5 = Augment 5 (Meme analysts)
    // 6 = Augment 6 (clickbait specialists)

    public void InitializeCommunityLists()
    {
        if (CommunityLists == null) // initialization
        {
            CommunityLists = new List<List<TreeNode>>();
            for (int i = 0; i <= 6; i++)
            {
                CommunityLists.Add(new List<TreeNode>());
                //Debug.Log(i);
            }
        }
    }

    public List<List<TreeNode>> GetCommunityLists()
    {
        return CommunityLists;
    }

    public List<TreeNode> GetSpecificCommunityList(int index) // index = augment index
    {
        return CommunityLists[index];
    }

    public void MoveUserInCommunityList(TreeNode user, int DestinationListIndex)
    {
        foreach (List<TreeNode> list in CommunityLists)
        {
            list.Remove(user);
        }
        CommunityLists[DestinationListIndex].Add(user);
    }

    public void PrintCommunityListsAmounts()
    {
        int i = 0;
        foreach (List<TreeNode> list in CommunityLists)
        {
            Debug.Log("In list " + i + " . " + CommunityLists[i].Count + " users!");
            i++;
        }
    }


    /// <summary>
    /// Used to tell if some UI elements can be displayed
    /// </summary>
    static public bool isFirstLoop = true;

    public void FirstLoopDone()
    {
        isFirstLoop = false;
    }

    public bool IsFirstLoop()
    {
        return isFirstLoop;
    }
}
