using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Augment_Generic : MonoBehaviour
{
    // cette classe aurait dû être utilisée mais en fait a été oubliée





    /*
    
    private List<TreeNode> AffectedUsers; // utilisateurs affectés à cette tâche
    public int ListIndex; // emplacement dans le datakeeper de la liste d'affected users
    public GameObject userProfile; // référence vers le prefab de user
    private SceneToSceneDataKeeper dataKeeper;
    public Collider2D DropZone; // zone qui englobe tout le grand rectangle
    public GameObject[] userSlots; // emplacements dans le monde pour aligner les nouveaux users


    // Start is called before the first frame update
    void Start()
    {
        dataKeeper = FindObjectOfType<SceneToSceneDataKeeper>();
        AffectedUsers = dataKeeper.GetCommunityLists()[ListIndex];
        int i = 0;
        foreach (TreeNode user in AffectedUsers)
        {
            Instantiate(
                userProfile,
                userSlots[i].transform.position,
                Quaternion.identity,
                this.transform);
            i++;
        }
    }


    private void AddUser(TreeNode user) // ???
    {
        if (userSlots.Length < AffectedUsers.Count)
        {
            user.AssociatedGameObject.transform.position = userSlots[AffectedUsers.Count].transform.position;
            AffectedUsers.Add(user);
            dataKeeper.AddUserToCommunityList(user, ListIndex);
        }
        else
        {
            // feedback de nope
        }
        
    }

    private void OnMouseOver()
    {
        
    }
    */
}
