using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnenmployedFans : MonoBehaviour
{
    private List<TreeNode> AvailableUsers; // utilisateurs affectés à cette tâche
    public GameObject userProfile; // référence vers le prefab de user
    private SceneToSceneDataKeeper dataKeeper;
    public GameObject[] userSlots; // emplacements dans le monde pour aligner les nouveaux users

    public Sprite[] ProfilePictures;
    public Color[] ProfilePictureBorderColors;

    // Start is called before the first frame update
    void Start()
    {
        // load all fans and remove those taken in other places
        dataKeeper = FindObjectOfType<SceneToSceneDataKeeper>();
        AvailableUsers = dataKeeper.GetFansList();
        foreach (List<TreeNode> List in dataKeeper.GetCommunityLists())
        {
            foreach (TreeNode user in List)
            {
                AvailableUsers.Remove(user);
            }
        }

        int i = 0;
        foreach (TreeNode user in AvailableUsers)
        {
            if (i >= userSlots.Length)
            {
                Debug.Log("Pas la place lul");
                break;
            }
            GameObject UserProfile = Instantiate(
                userProfile,
                userSlots[i].transform.position,
                Quaternion.identity,
                this.transform);

            //Assign TreeNode information
            user.AssociatedGameObject = UserProfile;
            UserProfile.GetComponent<SpriteRenderer>().sprite = ProfilePictures[user.ProfilePicture];
            UserProfile.GetComponentsInChildren<SpriteRenderer>()[1].color = ProfilePictureBorderColors[3];
            UserProfile.GetComponent<CircleCollider2D>().enabled = true;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
