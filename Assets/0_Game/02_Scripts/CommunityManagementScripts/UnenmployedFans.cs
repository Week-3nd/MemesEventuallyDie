using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnenmployedFans : MonoBehaviour
{
    // game logic
    private List<TreeNode> AvailableUsers; // utilisateurs affectés à cette tâche
    public GameObject userProfile; // référence vers le prefab de user
    private SceneToSceneDataKeeper dataKeeper;
    //public GameObject[] userSlots; // emplacements dans le monde pour aligner les nouveaux users

    // Afficher les fans disponibles sur une ligne
    private Vector3[] fansPositions;
    //public GameObject fanPositionsOrigin;
    public float fanSpacing = 5.0f;
    public int fanMaxAmountToDisplay;
    private int overflowingFanAmount = 0;
    public GameObject OverFlowFansObject;

    public TransformHolder[] AugmentsUI;



    // Start is called before the first frame update
    void Start()
    {
        fansPositions = new Vector3[fanMaxAmountToDisplay];
        dataKeeper = FindObjectOfType<SceneToSceneDataKeeper>();
        //dataKeeper.InitializeCommunityLists();
        //dataKeeper.PrintCommunityListsAmounts();
        PrintAvailableFans();
        //ReorderFans();
    }


    private void PrintAvailableFans()
    {
        AvailableUsers = dataKeeper.GetFansList();


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
        overflowingFanAmount = 0;
        foreach (TreeNode user in AvailableUsers)
        {
            Vector3 spawnPosition = new();
            if (i >= fanMaxAmountToDisplay)
            {
                //Debug.Log("Pas la place lul");
                overflowingFanAmount++;
                spawnPosition = OverFlowFansObject.transform.position; // overflowing fans are spawned under the icon showing they are too many
            }
            else
            {
                spawnPosition = fansPositions[i];
            }
            
            //instantiante object
            GameObject UserProfile = Instantiate(
                    userProfile,
                    spawnPosition,
                    Quaternion.identity,
                    this.transform);

            //Assign TreeNode information
            user.AssociatedGameObject = UserProfile;
            UserProfile.GetComponentInChildren<ProfilePictureGeneration>().PopulateProfilePicture(
                user.busteIndex, user.faceIndex, user.mouthIndex, user.noseIndex, user.eyeIndex, user.hairIndex, user.earIndex, user.skinToneIndex, user.bgColorIndex, user.tShirtColorIndex);
            UserProfile.GetComponentInChildren<ProfilePictureGeneration>().PopulateProfileBorder(user.ShareState);
            if (user.isFan)
            {
                UserProfile.GetComponentInChildren<ProfilePictureGeneration>().PopulateProfileBorder(3);
            }
            UserProfile.GetComponent<CircleCollider2D>().enabled = true;
            UserProfile.GetComponent<AssociatedTreeNode>().associatedNode = user;

            i++;
        }
        ReorderFans();

    }

    public void ReorderFans()
    {
        
        
        for (int index = 1; index <= 6; index++)
        {
            int j = 0;
            foreach (TreeNode fanNode in dataKeeper.GetSpecificCommunityList(index))
            {
                fanNode.AssociatedGameObject.transform.position = AugmentsUI[index-1].TransformList[j].transform.position;
                j++;
            }
        }

        
        
        
        
        
        // unemployed users
        AvailableUsers = dataKeeper.GetSpecificCommunityList(0);

        int i = 0;
        overflowingFanAmount = 0;

        foreach (TreeNode user in AvailableUsers)
        {
            if (i >= fanMaxAmountToDisplay)
            {
                //Debug.Log("Pas la place lul");
                overflowingFanAmount++;
                user.AssociatedGameObject.transform.position = OverFlowFansObject.transform.position;
            }
            else
            {
                user.AssociatedGameObject.transform.position = fansPositions[i];
            }
            i++;
        }



        UpdateOverflowingFansDisplay();
    }


    private void UpdateOverflowingFansDisplay()
    {
        // Add a number showing how many fans are not displayed
        if (overflowingFanAmount > 0)
        {
            OverFlowFansObject.SetActive(true);
            OverFlowFansObject.GetComponentInChildren<TextMeshPro>().text = "+" + overflowingFanAmount;
        }
        else
        {
            OverFlowFansObject.SetActive(false);
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
