using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnenmployedFans : MonoBehaviour
{
    // game logic
    private List<TreeNode> AvailableUsers; // utilisateurs affect�s � cette t�che
    public GameObject userProfile; // r�f�rence vers le prefab de user
    private SceneToSceneDataKeeper dataKeeper;
    //public GameObject[] userSlots; // emplacements dans le monde pour aligner les nouveaux users

    // Afficher les fans disponibles sur une ligne
    private Vector3[] fansPositions;
    //public GameObject fanPositionsOrigin;
    public float fanSpacing = 5.0f;
    public int fanMaxAmountToDisplay;
    private int overflowingFanAmount = 0;
    public GameObject OverFlowFansObject;

    public Sprite[] ProfilePictures;
    public Color[] ProfilePictureBorderColors;



    // Start is called before the first frame update
    void Start()
    {
        fansPositions = new Vector3[fanMaxAmountToDisplay];
        dataKeeper = FindObjectOfType<SceneToSceneDataKeeper>();
        dataKeeper.InitializeCommunityLists();
        //dataKeeper.PrintCommunityListsAmounts();
        PrintAvailableFans();
    }


    private void PrintAvailableFans()
    {
        AvailableUsers = dataKeeper.GetSpecificCommunityList(0);


        //former implementation : load all fans and remove those taken in other places
        /*
        AvailableUsers = dataKeeper.GetFansList();
        foreach (List<TreeNode> List in dataKeeper.GetCommunityLists())
        {
            foreach (TreeNode user in List)
            {
                AvailableUsers.Remove(user);
            }
        }
        // */

        //generate fans positions
        for (int index = 0; index < fanMaxAmountToDisplay; index++)
        {
            fansPositions[index] = transform.position + new Vector3((index * fanSpacing),0,0);
            //Debug.Log("Generated fan position at : " + fansPositions[index]);
        }

        //print fans from the list
        int i = 0;
        foreach (TreeNode user in AvailableUsers)
        {
            if (i >= fanMaxAmountToDisplay)
            {
                //Debug.Log("Pas la place lul");
                overflowingFanAmount++;
            }
            else
            {
                GameObject UserProfile = Instantiate(
                    userProfile,
                    fansPositions[i],
                    Quaternion.identity,
                    this.transform);

                //Assign TreeNode information
                user.AssociatedGameObject = UserProfile;
                UserProfile.GetComponentsInChildren<SpriteRenderer>()[0].sprite = ProfilePictures[user.ProfilePicture];
                UserProfile.GetComponentsInChildren<SpriteRenderer>()[1].color = ProfilePictureBorderColors[3];
                UserProfile.GetComponent<CircleCollider2D>().enabled = true;
                UserProfile.GetComponent<AssociatedTreeNode>().associatedNode = user;
            }

            i++;
        }

        // Add a number showing how many fans are not displayed
        if (overflowingFanAmount>0)
        {
            OverFlowFansObject.SetActive(true);
            OverFlowFansObject.GetComponentInChildren<TextMesh>().text = "+" + overflowingFanAmount;
        }
        else
        {
            OverFlowFansObject.SetActive(false);
        }
    }

    public void ReorderFans()
    {

        int i = 0;
        foreach (TreeNode user in AvailableUsers)
        {
            if (i >= fanMaxAmountToDisplay)
            {
                //Debug.Log("Pas la place lul");
                overflowingFanAmount++;
            }
            else
            {
                user.AssociatedGameObject.transform.position = fansPositions[i];
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
         /*
        //Draw 2 crossing lines where each spot is
        Vector3 topLeft = new Vector3(      -0.5f * fanSpacing,     0.5f * fanSpacing);
        Vector3 bottomLeft = new Vector3(   -0.5f * fanSpacing,     -0.5f * fanSpacing);
        Vector3 topRight = new Vector3(     0.5f * fanSpacing,      0.5f * fanSpacing);
        Vector3 bottomRight = new Vector3(  0.5f * fanSpacing,      -0.5f * fanSpacing);

        foreach (Vector3 coord in fansPositions)
        {
            Debug.DrawLine(coord + topLeft, coord + bottomRight,Color.yellow);
            Debug.DrawLine(coord + bottomLeft, coord + topRight, Color.yellow);
        }
        // */
    }
}
