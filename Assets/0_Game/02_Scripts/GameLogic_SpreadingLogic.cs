using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic_SpreadingLogic : MonoBehaviour
{
    [Tooltip("Value between 0 and 1. If random evaluation is higher : meme shared. If lower : meme not shared.")]
    public float NodeSuccessThreshold = 0.9f;
    public int Score = 0;

    [Tooltip("Value between 0 and 1. If random evaluation is higher : meme not shared. If lower : meme shared on facebook.")]
    public float NodeFailureThreshold = 0.1f;
    private int NumberOfFailedNodes = 0;

    [Tooltip("For each successful share, number of times we attempt to share again.")]
    public int TentativesOfReshare = 4;

    [Tooltip("NumberOfFailedNodesBeforeStoppingEvaluation")]
    public int AuthorizedFails = 10;

    public Tree SocialNetwork = new Tree();



    //Drawing network parameters

    public Vector2 Origin = new Vector2(0f, 0f);
    public GameObject NetworkParent;
    public float HorizontalSpacing = 450f;
    private float CurrentHorizontalPosition = 0.0f;
    public float VerticalSpacing = 450f;
    private float CurrentVerticalPosition = 0.0f;
    public float TimeBetweenRows = 0.5f;
    private float RowTimer = 0.0f;
    public GameObject NetworkProfileObject;
    public Sprite[] ProfilePictures;

    public void EvaluateNetwork()
    {
        while (NumberOfFailedNodes < NodeFailureThreshold)
        {
            AddDepthRow();
        }
        Debug.Log("Score : "+Score+" (in depth "+SocialNetwork.DepthLists.Count+")  | Node count : "+SocialNetwork.treeNodesList.Count);
        DrawNetwork();
    }

    public void AddDepthRow()
    {
        //Debug.Log("Previous depth : " + SocialNetwork.DepthLists.Count);
        foreach (TreeNode currentNode in SocialNetwork.DepthLists[SocialNetwork.DepthLists.Count - 1])
        {
            if (currentNode.ShareState == 2)
            {
                Score++;
                for (int i = 0; i < TentativesOfReshare; i++)
                {
                    TreeNode ChildNode = SocialNetwork.AddChild(currentNode);
                    ChildNode.GenerateNodeContent(NodeFailureThreshold, NodeSuccessThreshold);
                }
            }
            else if (currentNode.ShareState == 0)
            {
                NumberOfFailedNodes++;
            }
        }
        //Debug.Log("New depth : " + SocialNetwork.DepthLists.Count);
    }




    ///////////////////////////////////////
    ///    Spawning Graphic elements    ///
    ///////////////////////////////////////
    
    public void DrawNetwork()
    {
        foreach (var currentDepthList in SocialNetwork.DepthLists)
        {
            float HorizontalNeededSpace = currentDepthList.Count * HorizontalSpacing;
            CurrentHorizontalPosition = Origin.x - (HorizontalNeededSpace / 2);
            foreach (var currentNode in currentDepthList)
            {
                GameObject NewNode = Instantiate(NetworkProfileObject,new Vector3(CurrentHorizontalPosition, CurrentVerticalPosition, 0f),Quaternion.identity, NetworkParent.transform);
                int SpriteIndex = Random.Range(0, ProfilePictures.Length);
                NewNode.GetComponent<SpriteRenderer>().sprite = ProfilePictures[(Random.Range(0, SpriteIndex))];
                Debug.Log(SpriteIndex);
                CurrentHorizontalPosition += HorizontalSpacing;
            }
            CurrentVerticalPosition -= VerticalSpacing;
        }
        CurrentHorizontalPosition = 0.0f;
        CurrentVerticalPosition = 0.0f;
    }
}
