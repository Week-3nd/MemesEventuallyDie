using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDisplay_SocialNetworkManager : MonoBehaviour
{
    //SocialNetwork back-end reference
    public GameLogic_SpreadingLogic BackEndReference;
    private Tree SocialNetwork;

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

    //Drawable Object Parameters
    public GameObject NetworkProfileObject;
    public Sprite[] ProfilePictures;
    public Color[] ProfilePictureBorderColors;

    //Arrow procedural Generation parameters
    public float ArrowWidth = 1.0f;
    public Material ArrowMaterial;




    public void LoadNetworkStructure()
    {
        SocialNetwork = BackEndReference.SocialNetwork;
    }


    public void DrawRow(int depth)
    {
        List<TreeNode> currentDepthList = SocialNetwork.DepthLists[depth];
        float HorizontalNeededSpace = (currentDepthList.Count-1) * HorizontalSpacing;
        CurrentHorizontalPosition = Origin.x - (HorizontalNeededSpace / 2);
        foreach (TreeNode currentNode in currentDepthList)
        {
            //Create object
            GameObject NewNode = Instantiate(NetworkProfileObject, new Vector3(CurrentHorizontalPosition, CurrentVerticalPosition, 0f), Quaternion.identity, NetworkParent.transform);

            //Assign TreeNode information
            currentNode.AssociatedGameObject = NewNode;
            NewNode.GetComponent<SpriteRenderer>().sprite = ProfilePictures[currentNode.ProfilePicture];
            NewNode.GetComponentsInChildren<SpriteRenderer>()[1].color = ProfilePictureBorderColors[currentNode.ShareState];

            //Create Arrow to parent
            if (currentNode.Parent != null)
            {
                Vector2 end = currentNode.Parent.AssociatedGameObject.transform.position - NewNode.transform.position;
                NewNode.GetComponentInChildren<GenerateArrow>().GenArrow(Vector2.zero, end, ArrowWidth,ArrowMaterial);
            }
            
            //Prepare next iteration of the loop
            CurrentHorizontalPosition += HorizontalSpacing;
        }
        CurrentVerticalPosition -= VerticalSpacing;
    }

    public void StartDrawing()
    {
        AuthorizationToDraw = true;
        RowTimer = TimeBetweenRows;
    }


    public void Start()
    {
        LoadNetworkStructure();
    }

    public void Update()
    {
        if (AuthorizationToDraw)
        {
            RowTimer += Time.deltaTime;
            if (RowTimer >= TimeBetweenRows && NextRow < SocialNetwork.DepthLists.Count)
            {
                RowTimer = 0.0f;
                DrawRow(NextRow);
                NextRow++;
            }
        }
    }
}
