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
    public bool isBot = false;
    public int SpecialEventIndexInfluence = 0;
    //Fluff
    public int busteIndex;
    public int faceIndex;
    public int mouthIndex;
    public int noseIndex;
    public int eyeIndex;
    public int hairIndex;
    public int earIndex;
    public int skinToneIndex;
    public int bgColorIndex;
    public int tShirtColorIndex;


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
            if (fanRandom < FanChance && !isBot)
            {
                isFan = true;
            }
        }
        else
        {
            ShareState = 1;
        }

        if (isBot)
        {
            ShareState = 2;
            busteIndex = 0;
            faceIndex = 0;
            mouthIndex = 0;
            noseIndex = 0;
            eyeIndex = 0;
            hairIndex = 0;
            earIndex = 0;
            skinToneIndex = 0;
            bgColorIndex = 0;
        }
        else
        {
            busteIndex = 0;
            faceIndex = Random.Range(0, 10);
            mouthIndex = Random.Range(0, 10);
            noseIndex = Random.Range(0, 10);
            eyeIndex = Random.Range(0, 10);
            hairIndex = Random.Range(0, 10);
            earIndex = Random.Range(0, 10);
            skinToneIndex = Random.Range(0, 10);
            bgColorIndex = Random.Range(0, 10);
            tShirtColorIndex = Random.Range(0, 10);
        }
    }
    //hardcodé pour le moment


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

        return "ShareState : " + ShareState + " | Children : " + Children.ToString() + " | Depth : " + GetDepth();
    }
}
