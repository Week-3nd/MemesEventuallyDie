using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode
{
    //Tree architecture
    public TreeNode Parent = null;
    public List<TreeNode> Children = new List<TreeNode>();

    //Node content
    //Display
    public GameObject AssociatedGameObject;
    //Gameplay
    public int ShareState = 0; // 0 = facebook | 1 = nothing | 2 = shared | 3 = special event...
    public int SpecialEventIndexInfluence = 0;
    //Fluff
    public string Username;
    public string Description;
    public int ProfilePicture;
    public Texture ProfileBanner;
    public float FollowersCount;
    public bool IsVerified;
    public bool HasCommunityBadge;
    


    public void GenerateNodeContent(float FailureThreshold, float SuccessThreshold)
    {
        float result = Random.value;
        if (result < FailureThreshold)
        {
            ShareState = 0;
        }
        else if (result > SuccessThreshold)
        {
            ShareState = 2;
        }
        else
        {
            ShareState = 1;
        }
        ProfilePicture = Random.Range(0, 5); //hardcodé pour le moment
    }

    public int GetDepth()
    {
        TreeNode current = this;
        int i = 0;
        //recursive
        while (current.Parent != null)
        {
            current = current.Parent;
            i++;
        }

        return i;
    }

    public string GetNodeTreeInfo()
    {
        
        return "ShareState : "+ShareState+" | Children : "+Children.ToString()+" | Depth : "+GetDepth();
    }
}
