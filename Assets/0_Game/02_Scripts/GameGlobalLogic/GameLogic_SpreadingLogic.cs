using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic_SpreadingLogic : MonoBehaviour
{
    // Network management
    //[Tooltip("Value between 0 and 1. If random evaluation is higher : meme shared. If lower : meme not shared.")]
    private float SuccessProbability = 0.32f;
    //private float NodeSuccessThreshold = 0.9f;
    //[Tooltip("Value between 0 and 1. If node is success, then there is this chance of it transforming into a fan.")]
    //private float FanProbabilityIfSuccess = 0.15f;
    public List<Vector2Int> FanAmountAccordingToShareAmount;

    [Tooltip("Value between 0 and 1. If random evaluation is higher : meme not shared. If lower : meme shared on facebook.")]
    public float FailureProbability = 0.05f;
    //private float NodeFailureThreshold = 0.1f;
    private int NumberOfFailedNodes = 0;

    [Tooltip("For each successful share, number of times we attempt to share again.")]
    public int TentativesOfReshare = 4;

    //[Tooltip("NumberOfFailedNodesBeforeStoppingEvaluation")]
    private int AuthorizedFails = 10;

    [Tooltip("FOR BOTS. For each successful share, number of times we attempt to share again.")]
    public int BotTentativesOfReshare = 2;
    private int firstGenBotsAmount = 0;

    //Force until X sharers in a given gen
    [Tooltip("How many first generations to prevent from dying")]
    public int InvincibleGenerations = 5;

    //Random manipulation
    [Tooltip("Under this threshold : high share chances. Above this threshold : low share chances")]
    public int sharersPerGenThreshold = 3;
    [Tooltip("High share chances")]
    public float highShareChances = 0.36f;
    [Tooltip("Low share chances")]
    public float LowShareChances = 0.25f;

    //Tree
    public Tree SocialNetwork = new Tree();
    //Gen
    private bool canContinue = true;

    //Data management
    public SceneToSceneDataKeeper dataKeeper;
    public GameLogic_CommAndMemeInfluence influence;



    private void Start()
    {
        //taking data from external influences
        highShareChances = influence.GetShareProbability(dataKeeper.GetSpecificCommunityList(6).Count).x;
        LowShareChances = influence.GetShareProbability(dataKeeper.GetSpecificCommunityList(6).Count).y;
        SuccessProbability = highShareChances;
        AuthorizedFails = influence.GetAuthorizedFailsAmount(dataKeeper.GetSpecificCommunityList(4).Count);
        FanAmountAccordingToShareAmount = influence.GetFanProbabilities(dataKeeper.GetSpecificCommunityList(3).Count);
        firstGenBotsAmount = influence.GetBotsAmount(dataKeeper.GetSpecificCommunityList(1).Count);
        
        /*
        Debug.Log("Share probability : " + SuccessProbability
            + " | Max faithblog posts : " + AuthorizedFails
            + " | Fan probabilty upon share : " + FanProbabilityIfSuccess
            + " | First gen bots : " + firstGenBotsAmount);
        // */

         /*
        //transforming data in usable information
        NodeFailureThreshold = FailureProbability;
        NodeSuccessThreshold = 1 - SuccessProbability;
        // */
    }




    public void EvaluateNetwork()
    {
        AddFirstRow();
        
        //generate tree
        while (NumberOfFailedNodes < AuthorizedFails && canContinue)
        {
            //Le depth row est ajouté avec une probabilité de partage selon sa taille
            UpdateShareRate();
            AddDepthRow();
            
            //On force les premières x générations à vivre ( x = InvincibleGenerations)
            if (SocialNetwork.DepthLists.Count <= InvincibleGenerations && !HasWinsInRow(SocialNetwork.DepthLists[SocialNetwork.DepthLists.Count - 1]))
            {
                int undeadNodeIndex = Random.Range(0, SocialNetwork.DepthLists[SocialNetwork.DepthLists.Count - 1].Count);
                SocialNetwork.DepthLists[SocialNetwork.DepthLists.Count - 1][undeadNodeIndex].ShareState = 2;
            }

            //on ne peut continuer que s'il y a qq1 qui a partagé dans le dernier row
            canContinue = HasWinsInRow(SocialNetwork.DepthLists[SocialNetwork.DepthLists.Count - 1]);
        }


        //number of Fans isn't determined by random, rather by defined thresholds according to share amount
        // 1 : generate a list of all sharers
        List<TreeNode> sharersList = new();
        foreach (TreeNode user in SocialNetwork.treeNodesList)
        {
            if (user.ShareState == 2)
            {
                sharersList.Add(user);
            }
        }

        // 2 : get how many fans we want to create 
        int numberOfFans = NumberOfFans(sharersList.Count);

        // 3 : create indexes for which sharers will become fans
        List<int> sharersToBecomeFansIndexes = new List<int>();
        while (sharersToBecomeFansIndexes.Count < numberOfFans)
        {
            int index = Random.Range(1, sharersList.Count); // we exclude 0 so that the initial post cannot become a fan
            if (!sharersToBecomeFansIndexes.Contains(index))
            {
                sharersToBecomeFansIndexes.Add(index);
            }
        }

        // 4 : make them fans
        foreach (int newFanIndex in sharersToBecomeFansIndexes)
        {
            sharersList[newFanIndex].isFan = true;
        }


        //once tree fully generated, output also other lists
        List<TreeNode> NewFans = new List<TreeNode>();
        foreach (TreeNode node in SocialNetwork.treeNodesList)
        {
            if (node.isFan)
            {
                NewFans.Add(node);
            }
        }
        //dataKeeper.InitializeCommunityLists();
        dataKeeper.AddFansToList(NewFans);

        List<TreeNode> Sharers = new List<TreeNode>();
        foreach (TreeNode node in SocialNetwork.treeNodesList)
        {
            if (node.ShareState == 2)
            {
                Sharers.Add(node);
            }
        }

         /*
        Debug.Log("Depth : " + SocialNetwork.DepthLists.Count
            + " | Node count : " + SocialNetwork.treeNodesList.Count
            + " | Sharers : " + Sharers.Count + " including " + NewFans.Count
            + " new fans. | Total fans : " + dataKeeper.GetFansList().Count);
        // */
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
                int reshareAttempts = TentativesOfReshare;
                if (currentNode.isBot)
                {
                    reshareAttempts = BotTentativesOfReshare;
                }

                for (int i = 0; i < reshareAttempts; i++)
                {
                    TreeNode ChildNode = SocialNetwork.AddChild(currentNode);
                    ChildNode.GenerateNodeContent(FailureProbability, SuccessProbability /*, FanProbabilityIfSuccess */);
                    //handle the stopping code (the scoring isnt needed here)
                    if (ChildNode.ShareState == 0)
                    {
                        NumberOfFailedNodes++;
                    }
                }
            }
        }
        //Debug.Log("New depth : " + SocialNetwork.DepthLists.Count);
    }

    private void AddFirstRow()
    {
        TreeNode root = SocialNetwork.root;

        for (int i = 0; i < firstGenBotsAmount; i++)
        {
            TreeNode ChildNode = SocialNetwork.AddChild(root);
            ChildNode.isBot = true;
            ChildNode.GenerateNodeContent(FailureProbability, SuccessProbability /*, FanProbabilityIfSuccess*/);
            //handle the stopping code (the scoring isnt needed here)
            if (ChildNode.ShareState == 0)
            {
                NumberOfFailedNodes++;
            }
        }

        for (int i = 0; i < TentativesOfReshare; i++)
        {
            TreeNode ChildNode = SocialNetwork.AddChild(root);
            ChildNode.GenerateNodeContent(FailureProbability, SuccessProbability /*, FanProbabilityIfSuccess*/);
            //handle the stopping code (the scoring isnt needed here)
            if (ChildNode.ShareState == 0)
            {
                NumberOfFailedNodes++;
            }
        }
    }

    private void UpdateShareRate()
    {
        // calculer nombre de partages dans une génération
        int numberOfShares = 0;
        foreach (TreeNode user in SocialNetwork.DepthLists[SocialNetwork.DepthLists.Count - 1])
        {
            if (user.ShareState == 2)
            {
                numberOfShares++;
            }
        }

        //Choisir le taux de partage selon la largeur de l'arbre (si égal au seuil : on applique le taux bas)
        if (numberOfShares < sharersPerGenThreshold)
        {
            SuccessProbability = highShareChances;
        }
        else
        {
            SuccessProbability = LowShareChances;
        }

         /*
        Debug.Log("Gen " + SocialNetwork.DepthLists.Count
            + ". S = " + numberOfShares
            + " | % = "
            + SuccessProbability
            + ".");
        // */
    }

    private int NumberOfFans(int numberOfSharers)
    {
        //Debug.Log(numberOfSharers + " shares.");
        foreach (Vector2Int currentThreshold in FanAmountAccordingToShareAmount)
        {
            if (currentThreshold.x == numberOfSharers)
            {   
                return currentThreshold.y;
            }
            if (currentThreshold.x > numberOfSharers)
            {
                return FanAmountAccordingToShareAmount[FanAmountAccordingToShareAmount.IndexOf(currentThreshold) - 1].y;
            }
        }
        return FanAmountAccordingToShareAmount[FanAmountAccordingToShareAmount.Count - 1].y;
    }
}
