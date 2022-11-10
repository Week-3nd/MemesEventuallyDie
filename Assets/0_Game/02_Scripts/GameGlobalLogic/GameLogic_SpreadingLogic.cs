using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic_SpreadingLogic : MonoBehaviour
{
    [Tooltip("Value between 0 and 1. If random evaluation is higher : meme shared. If lower : meme not shared.")]
    public float NodeSuccessThreshold = 0.9f;

    [Tooltip("Value between 0 and 1. If random evaluation is higher : meme not shared. If lower : meme shared on facebook.")]
    public float NodeFailureThreshold = 0.1f;
    private int NumberOfFailedNodes = 0;

    [Tooltip("For each successful share, number of times we attempt to share again.")]
    public int TentativesOfReshare = 4;

    [Tooltip("NumberOfFailedNodesBeforeStoppingEvaluation")]
    public int AuthorizedFails = 10;

    public int Score = 1;
    public Tree SocialNetwork = new Tree();

    private int CurrentDepth = 1;
    private int FormerDepth = 1;



    public void EvaluateNetwork()
    {
        while (NumberOfFailedNodes < AuthorizedFails)
        {
            CurrentDepth = AddDepthRow();
            if (CurrentDepth == FormerDepth) { break; }
            FormerDepth = CurrentDepth;
        }
        Debug.Log("Depth "+SocialNetwork.DepthLists.Count+")  | Node count : "+SocialNetwork.treeNodesList.Count);
    }

    public int AddDepthRow()
    {
        //Debug.Log("Previous depth : " + SocialNetwork.DepthLists.Count);
        foreach (TreeNode currentNode in SocialNetwork.DepthLists[SocialNetwork.DepthLists.Count - 1])
        {
            if (currentNode.ShareState == 2)
            {
                for (int i = 0; i < TentativesOfReshare; i++)
                {
                    TreeNode ChildNode = SocialNetwork.AddChild(currentNode);
                    ChildNode.GenerateNodeContent(NodeFailureThreshold, NodeSuccessThreshold);
                    if (ChildNode.ShareState == 2)
                    {
                        Score++;
                    }
                    else if (ChildNode.ShareState == 0)
                    {
                        NumberOfFailedNodes++;
                    }
                }
            }
        }
        return SocialNetwork.DepthLists.Count;
        //Debug.Log("New depth : " + SocialNetwork.DepthLists.Count);
    }
}
