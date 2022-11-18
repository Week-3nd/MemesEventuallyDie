using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GameDisplay_SocialNetworkManager : MonoBehaviour
{
    //SocialNetwork back-end reference
    public GameLogic_SpreadingLogic BackEndReference;
    private Tree SocialNetwork;

    //Camera Reference
    public Camera FollowingCamera;
    private float TemporaryObjectCoordinatesToZoomFactor = 0.28125f; // = (9/16)/2 (screen ratio 16/9)
    private float MaxAskedZoom = 0.0f;

    //Drawing network parameters
    public bool AuthorizationToDraw = false;
    public Vector2 Origin = new Vector2(0f, 0f);
    public GameObject NetworkParent;
    public float HorizontalSpacing = 450f;
    private float CurrentHorizontalPosition = 0.0f;
    public float VerticalSpacing = 450f;
    private float CurrentVerticalPosition = 0.0f;
    public float TimeBetweenRows = 0.5f;
    private float RowTimer = 0.0f;
    private int NextRow = 0;

    //Drawing network complex position calculations
    private List<TreeNode> OrderedNodeList = new List<TreeNode>();


    //Drawable Object Parameters
    public GameObject NetworkProfileObject;
    public Sprite[] ProfilePictures;
    public Color[] ProfilePictureBorderColors;

    //Arrow procedural Generation parameters
    public float ArrowWidth = 1.0f;
    public Material ArrowMaterial;

    //Score output
    private int Score = 0;
    public int ShareScore = 1;
    public int FanScore = 3;
    public ScoreDisplay ScoreDisplayUI;


    
    public void StartDrawing()
    {
        FollowingCamera.GetComponent<CameraFollow>().SetRezoomRate(HorizontalSpacing);
        SocialNetwork = BackEndReference.SocialNetwork;
        CalculatePositionsT1(); // place to put function A
        AuthorizationToDraw = true;
        RowTimer = TimeBetweenRows;
    }

    private void Update()
    {
        if (AuthorizationToDraw)
        {
            RowTimer += Time.deltaTime;
            if (RowTimer >= TimeBetweenRows && NextRow < SocialNetwork.DepthLists.Count)
            {
                RowTimer = 0.0f;
                DrawRowT1(NextRow); // place to put function B
                NextRow++;
            }
            else if (NextRow >= SocialNetwork.DepthLists.Count)
            {
                AuthorizationToDraw = false;
                ScoreDisplayUI.LastScoreUpdate(
                    new Vector3(0, ((CurrentVerticalPosition + VerticalSpacing) / 2), -20),
                    (Mathf.Abs(CurrentVerticalPosition) + VerticalSpacing) * (16/9)/2);
            }
        }
    }
    




    // We can use one of these groups of functions
    



    // GROUP 1
    // Displays efficiently, though don't show links very nicely

    // function A
    private void CalculatePositionsT1()
    {
        int currentDepthIndex = SocialNetwork.DepthLists.Count - 1;
        //initialize by giving last row the minimal space between nodes
        foreach (TreeNode node in SocialNetwork.treeNodesList)
        {
            node.HorizontalPosition = HorizontalSpacing;
        }
         /*
        foreach (TreeNode currentNode in SocialNetwork.DepthLists[currentDepthIndex])
        {
            currentNode.NeededHorizontalSpace = HorizontalSpacing;
        }
        // */
        currentDepthIndex -= 1;

        // go from leafs to root and build the horizontal spacing for each node
        while (currentDepthIndex > 0)
        {
            foreach (TreeNode currentNode in SocialNetwork.DepthLists[currentDepthIndex])
            {
                foreach (TreeNode childNode in currentNode.Children)
                {
                    currentNode.HorizontalPosition += childNode.HorizontalPosition;
                }
            }
            currentDepthIndex -= 1;
        }
        int i = 0;
        foreach (TreeNode node in SocialNetwork.treeNodesList)
        {
            //Debug.Log("Node "+i+" | Depth : "+node.GetDepth()+" | Horizontal needed space : "+node.HorizontalPosition);
            i++;
        }
        
    }

    // function B
    private void DrawRowT1(int depth)
    {
        List<TreeNode> currentDepthList = SocialNetwork.DepthLists[depth];
        float HorizontalNeededSpace = (currentDepthList.Count - 1) * HorizontalSpacing;
        CurrentHorizontalPosition = Origin.x - (HorizontalNeededSpace / 2);
        int index = 0;
        foreach (TreeNode currentNode in currentDepthList)
        {
            //Create object
            GameObject NewNode = Instantiate(
                NetworkProfileObject,
                new Vector3(CurrentHorizontalPosition, CurrentVerticalPosition, 0f),
                Quaternion.identity,
                NetworkParent.transform);

            //Assign TreeNode information
            currentNode.AssociatedGameObject = NewNode;
            NewNode.GetComponent<SpriteRenderer>().sprite = ProfilePictures[currentNode.ProfilePicture];
            NewNode.GetComponentsInChildren<SpriteRenderer>()[1].color = ProfilePictureBorderColors[currentNode.ShareState];
            if (currentNode.isFan)
            {
                NewNode.GetComponentsInChildren<SpriteRenderer>()[1].color = ProfilePictureBorderColors[3];
            }
            if (currentNode.ShareState == 2)
            {
                NewNode.GetComponentInChildren<VisualEffect>().enabled = true;
            }

            //Extract Node score information
            if (currentNode.ShareState == 2)
            {
                
                if (currentNode.isFan)
                {
                    Score += FanScore;
                }
                else
                {
                    Score += ShareScore;
                }
                ScoreDisplayUI.SetScoreDisplay(Score);
            }

            //Create Arrow to parent
            if (currentNode.Parent != null)
            {
                Vector2 end = currentNode.Parent.AssociatedGameObject.transform.position - NewNode.transform.position;
                NewNode.GetComponentInChildren<GenerateArrow>().GenArrow(Vector2.zero, end, ArrowWidth, ArrowMaterial);
            }

            //Prepare next iteration of the loop
            CurrentHorizontalPosition += HorizontalSpacing;
            index++;
        }

        //Camera data
        float zoomFactor = TemporaryObjectCoordinatesToZoomFactor * (HorizontalNeededSpace + 2 * HorizontalSpacing);
        if (zoomFactor > MaxAskedZoom) { MaxAskedZoom = zoomFactor; }
        FollowingCamera.GetComponent<CameraFollow>().SetTarget(
            new Vector3(0, CurrentVerticalPosition, -20),
            zoomFactor,
            TimeBetweenRows);

        //Prepare next row
        CurrentVerticalPosition -= VerticalSpacing;
    }


    // GROUP 2
    // Builds a correct tree visualization but puts waaay too much space on the first levels

    // function A
    public void CalculatePositionsT2()
    {
        //Calculating each node's position to make it nice
        //STEP 1
        //Assign a column to each node 
        //
        //STEP 2
        //Move all parent nodes to the column of their first child
        //
        //STEP 3
        //Delete empty columns
        // Concept = Identify trees that need to be moved left by counting the line of firstborns length
        // (To draw on a sheet of paper for understanding)
        //
        //STEP 4
        //For all nodes without children (sharestate 0 or 1), move them close to their siblings

        //STEP 5
        //Center each parent over it's children
        //


        //STEP 1 : fill OrderedNodeList
        //Debug.Log("Node coordinates calculations : STEP 1 started");
        RecursiveTreeOrderStep1(SocialNetwork.root);
        Debug.Log("Node coordinates calculations : STEP 1 finished");

        //STEP 2
        //Debug.Log("Node coordinates calculations : STEP 2 started");
        RecursiveTreeOrderStep2(SocialNetwork.root);
        Debug.Log("Node coordinates calculations : STEP 2 finished");

        //STEP 3
        //Debug.Log("Node coordinates calculations : STEP 3 started");
        RecursiveTreeOrderStep3(SocialNetwork.root);
        Debug.Log("Node coordinates calculations : STEP 3 finished");

        //STEP 4
          /*
        foreach (TreeNode treeNode in OrderedNodeList)
        {
            if (treeNode.Children.Count == 0 && treeNode.Parent.Children.IndexOf(treeNode) != 0)
            {
                treeNode.HorizontalPosition =
                    treeNode.Parent.Children[treeNode.Parent.Children.IndexOf(treeNode) - 1].HorizontalPosition + HorizontalSpacing;
            }
            
        }
        // */

        //STEP 5
         //    /* //This line to be commented
        //Debug.Log("Node coordinates calculations : STEP 4 started");
        int inverseIndex = SocialNetwork.DepthLists.Count - 1;
        //Debug.Log("Number of Rows" + inverseIndex);
        for (; inverseIndex > -1; inverseIndex -= 1)
        {
            //Debug.Log("Current Row Horizontal position calculing : " + inverseIndex);
            foreach (TreeNode CurrentNode in SocialNetwork.DepthLists[inverseIndex])
            {
                if (CurrentNode.Children.Count != 0) // if it has children
                {
                    float avrgXposition = 0.0f;
                    foreach (TreeNode Children in CurrentNode.Children)
                    {
                        avrgXposition += Children.HorizontalPosition / CurrentNode.Children.Count;
                    }
                    CurrentNode.HorizontalPosition = avrgXposition;
                }
            }
        }
        Debug.Log("Node coordinates calculations : STEP 4 finished");
        // */
    }

    private void RecursiveTreeOrderStep1(TreeNode RootNode)
    {
        // add current node to list
        OrderedNodeList.Add(RootNode);
        RootNode.HorizontalPosition = (OrderedNodeList.Count - 1) * HorizontalSpacing;

        //if children : add them!
        if (RootNode.Children != null)
        {
            foreach (TreeNode ChildNode in RootNode.Children)
            {
                RecursiveTreeOrderStep1(ChildNode);
            }
        }
    }

    private void RecursiveTreeOrderStep2(TreeNode RootNode)
    {
        if (RootNode.Children != null)
        {
            foreach (TreeNode ChildNode in RootNode.Children)
            {
                RecursiveTreeOrderStep2(ChildNode);
            }
        }

        if (RootNode.Parent != null && RootNode.Parent.Children[0] == RootNode) // if first born
        {
            RootNode.Parent.HorizontalPosition = RootNode.HorizontalPosition;
        }
    }

    private void RecursiveTreeOrderStep3(TreeNode RootNode)
    {
        // being a leaf + part of a firstborn line is a way to identify what subtrees have to move
        //Debug.Log("Attempt!");
        if (RootNode.Children.Count == 0 && RootNode.Parent != null &&RootNode.Parent.Children[0] == RootNode)
        {
            //Debug.Log("Node is leaf");
            float MovingDelta = RootNode.LengthOfFirstbornLine() * HorizontalSpacing; // taille du mouvement à appliquer
            //Debug.Log("Length of Firstborn Line : " + RootNode.LengthOfFirstbornLine()+" | Moving delta : "+ MovingDelta);
            //trouver l'index de la node à partir de laquelle bouger dans l'ordered list
            int startingPoint = OrderedNodeList.IndexOf(RootNode) - RootNode.LengthOfFirstbornLine();
            for (int i = startingPoint; i < OrderedNodeList.Count; i++)
            {
                OrderedNodeList[i].HorizontalPosition -= MovingDelta;
                //Debug.Log("Affected node #" + i);
            }
        }
        else if (RootNode.Children.Count != 0) //si on a des enfants
        {
            //Debug.Log("Node isn't leaf");
            foreach(TreeNode ChildNode in RootNode.Children)
            {
                RecursiveTreeOrderStep3(ChildNode);
            }
        }
    }

    // function B
    private void DrawRowT2(int depth)
    {
        List<TreeNode> currentDepthList = SocialNetwork.DepthLists[depth];
        
        foreach (TreeNode currentNode in currentDepthList)
        {
            //Create object
            GameObject NewNode = Instantiate(
                NetworkProfileObject,
                new Vector3(currentNode.HorizontalPosition, CurrentVerticalPosition, 0f),
                Quaternion.identity,
                NetworkParent.transform);
            //Debug.Log("HorizontalPosition : " + currentNode.NeededHorizontalSpace);

            //Assign TreeNode information
            currentNode.AssociatedGameObject = NewNode;
            NewNode.GetComponent<SpriteRenderer>().sprite = ProfilePictures[currentNode.ProfilePicture];
            NewNode.GetComponentsInChildren<SpriteRenderer>()[1].color = ProfilePictureBorderColors[currentNode.ShareState];

            //Extract Node score information
            if (currentNode.ShareState == 2)
            {
                //Score++;
                ScoreDisplayUI.SetScoreDisplay(Score);
            }

            //Create Arrow to parent
            if (currentNode.Parent != null)
            {
                Vector2 end = currentNode.Parent.AssociatedGameObject.transform.position - NewNode.transform.position;
                NewNode.GetComponentInChildren<GenerateArrow>().GenArrow(Vector2.zero, end, ArrowWidth, ArrowMaterial);
            }
        }
        CurrentVerticalPosition -= VerticalSpacing;
    }


}
