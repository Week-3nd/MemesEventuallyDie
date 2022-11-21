using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode
{
    //Tree architecture
    public TreeNode Parent = null;
    public List<TreeNode> Children = new List<TreeNode>();

    //Display info
    public float HorizontalPosition = 0.0f;
    public int XUnitsOfSpace = 1; // max number of times the size of a profile horizontal axis
    public int ColumnIndex = 0;

    //Node content
    //Display
    public GameObject AssociatedGameObject;
    //Gameplay
    public int ShareState = 0; // 0 = facebook | 1 = nothing | 2 = shared
    public bool isFan = false;
    public int SpecialEventIndexInfluence = 0;
    //Fluff
    public string Username;
    public string Description;
    public int ProfilePicture;
    public Texture ProfileBanner;
    public float FollowersCount;
    public bool IsVerified;
    public bool HasCommunityBadge;


    //Descending tree function
    private List<TreeNode> Descendants = new List<TreeNode>();

    //Augments UI
    


    public void GenerateNodeContent(float FailureThreshold, float SuccessThreshold, float FanChance)
    {
        float result = Random.value;
        if (result < FailureThreshold)
        {
            ShareState = 0;
        }
        else if (result > SuccessThreshold)
        {
            ShareState = 2;
            float fanRandom = Random.value;
            if (fanRandom < FanChance)
            {
                isFan = true; ;
            }
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

    public int LengthOfFirstbornLine()
    {
        if (this.Parent != null && this.Parent.Children[0] == this) // Si parent existe ET son aîné·e est cette node
        {
            return 1 + this.Parent.LengthOfFirstbornLine(); // To verify
        }
        return 0; 
    }

    public List<TreeNode> GetDescendingTree()
    {
        Descendants.Add(this);
        if (Children != null)
        {
            foreach (TreeNode Node in Children)
            {
                Descendants.AddRange(Node.GetDescendingTree());
            }
        }
        return Descendants;
    }

    public List<List<TreeNode>> GetDescendingTreeByRow()
    {
        List<List<TreeNode>> DescendantsByRow = new List<List<TreeNode>>();
        List<TreeNode> TempDescendantsList = GetDescendingTree();
        foreach (TreeNode currentNode in TempDescendantsList)
        {

        }

        return DescendantsByRow;
    }

    public string GetNodeTreeInfo()
    {
        
        return "ShareState : "+ShareState+" | Children : "+Children.ToString()+" | Depth : "+GetDepth();
    }
}
