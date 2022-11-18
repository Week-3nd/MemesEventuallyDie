using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic_SpreadingLogic : MonoBehaviour
{
    // Network management
    [Tooltip("Value between 0 and 1. If random evaluation is higher : meme shared. If lower : meme not shared.")]
    public float SuccessProbability = 0.2f;
    private float NodeSuccessThreshold = 0.9f;
    [Tooltip("Value between 0 and 1. If node is success, then there is this chance of it transforming into a fan.")]
    public float FanProbabilityIfSuccess = 0.15f;

    [Tooltip("Value between 0 and 1. If random evaluation is higher : meme not shared. If lower : meme shared on facebook.")]
    public float FailureProbability = 0.05f;
    private float NodeFailureThreshold = 0.1f;
    private int NumberOfFailedNodes = 0;

    [Tooltip("For each successful share, number of times we attempt to share again.")]
    public int TentativesOfReshare = 4;

    [Tooltip("NumberOfFailedNodesBeforeStoppingEvaluation")]
    public int AuthorizedFails = 10;


    //Force until X sharers in a given gen
    [Tooltip("How many first generations to prevent from dying")]
    public int InvincibleGenerations = 5;

    //Tree
    public Tree SocialNetwork = new Tree();
    //Gen
    private bool canContinue = true;

    //Data management
    public SceneToSceneDataKeeper dataKeeper;



    private void Start()
    {
        NodeFailureThreshold = FailureProbability;
        NodeSuccessThreshold = 1 - SuccessProbability;
    }

    public void EvaluateNetwork()
    {
        //generate tree
        while (NumberOfFailedNodes < AuthorizedFails && canContinue)
        {
            //canContinue = HasWinsInRow(SocialNetwork.DepthLists[SocialNetwork.DepthLists.Count - 1]);
            AddDepthRow();
            
            
            if (SocialNetwork.DepthLists.Count <= InvincibleGenerations && !HasWinsInRow(SocialNetwork.DepthLists[SocialNetwork.DepthLists.Count - 1]))
            {
                int undeadNodeIndex = Random.Range(0, SocialNetwork.DepthLists[SocialNetwork.DepthLists.Count - 1].Count);
                SocialNetwork.DepthLists[SocialNetwork.DepthLists.Count - 1][undeadNodeIndex].ShareState = 2;
            }

            canContinue = HasWinsInRow(SocialNetwork.DepthLists[SocialNetwork.DepthLists.Count - 1]);
        }

        //once tree generated, output also other lists
        List<TreeNode> NewFans = new List<TreeNode>();
        foreach (TreeNode node in SocialNetwork.treeNodesList)
        {
            if (node.isFan)
            {
                NewFans.Add(node);
            }
        }
        dataKeeper.AddFansToList(NewFans);

        List<TreeNode> Sharers = new List<TreeNode>();
        foreach (TreeNode node in SocialNetwork.treeNodesList)
        {
            if (node.ShareState == 2)
            {
                Sharers.Add(node);
            }
        }
        
        Debug.Log("Depth : " + SocialNetwork.DepthLists.Count + " | Node count : " + SocialNetwork.treeNodesList.Count+" | Sharers : "+Sharers.Count+" including "+NewFans.Count+ " new fans. | Total fans : " + dataKeeper.GetFansList().Count);
    }

    private bool HasWinsInRow (List<TreeNode> treeNodes)
    {
        foreach (TreeNode NodeToCheck in treeNodes)
        {
            if (NodeToCheck.ShareState == 2)
            {
                return true;
            }
        }
        return false;
    }

    private void AddDepthRow()
    {
        //Debug.Log("Previous depth : " + SocialNetwork.DepthLists.Count);
        foreach (TreeNode currentNode in SocialNetwork.DepthLists[SocialNetwork.DepthLists.Count - 1])
        {
            if (currentNode.ShareState == 2)
            {
                for (int i = 0; i < TentativesOfReshare; i++)
                {
                    TreeNode ChildNode = SocialNetwork.AddChild(currentNode);
                    ChildNode.GenerateNodeContent(NodeFailureThreshold, NodeSuccessThreshold, FanProbabilityIfSuccess);
                    //handle the stopping code (the scoring isnt needed yet here
                    if (ChildNode.ShareState == 0)
                    {
                        NumberOfFailedNodes++;
                    }
                }
            }
        }
        //Debug.Log("New depth : " + SocialNetwork.DepthLists.Count);
    }
}
