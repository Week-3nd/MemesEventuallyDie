using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree
{
    public TreeNode root = new TreeNode();
    public List<TreeNode> treeNodesList = new List<TreeNode>();
    public List<List<TreeNode>> DepthLists = new List<List<TreeNode>>();

    public Tree()
    {
        //liste des nodes
        treeNodesList.Add(root);

        //liste des nodes de profondeur 0
        DepthLists.Add(new List<TreeNode>());
        DepthLists[0].Add(root);

        //initialisation de la racine
        root.ShareState = 2;
    }


    public TreeNode AddChild(TreeNode Parent)
    {
        //node creation
        TreeNode newNode = new TreeNode();
        newNode.Parent = Parent;
        Parent.Children.Add(newNode);

        //tree referencing
        treeNodesList.Add(newNode);
        if (DepthLists.Count - 1 < newNode.GetDepth())
        {
            DepthLists.Add(new List<TreeNode>());
        }
        DepthLists[newNode.GetDepth()].Add(newNode);

        //return node reference
        return newNode;
    }

    public TreeNode GetLeftmostNode(int depth)
    {
        TreeNode current = root;
        //recursive
        for (int i = 0; i < depth; i++)
        {
            current = current.Children[1];
        }
        return current;
    }
}
