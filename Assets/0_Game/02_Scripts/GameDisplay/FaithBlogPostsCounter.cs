using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FaithBlogPostsCounter : MonoBehaviour
{
    public float numberOfPostsTextSize = 50;
    public float maximumPostsTextSize = 36;
    private int authorizedPosts;
    private int currentNumberOfPosts;
    private TextMeshProUGUI fBCounter;

    private void Start()
    {
        GameLogic_CommAndMemeInfluence influence = FindObjectOfType<GameLogic_CommAndMemeInfluence>();
        SceneToSceneDataKeeper dataKeeper = FindObjectOfType<SceneToSceneDataKeeper>();
        authorizedPosts = influence.GetAuthorizedFailsAmount(dataKeeper.GetSpecificCommunityList(4).Count);
        //Debug.Log("List 4 contains " + dataKeeper.GetSpecificCommunityList(4).Count + " Fans");
        //Debug.Log("So authorized posts are : " + authorizedPosts);
        fBCounter = this.GetComponent<TextMeshProUGUI>();
        DisplayText();
    }

    public void AddFBPosts(int numberOfNewFBPosts)
    {
        currentNumberOfPosts += numberOfNewFBPosts;
        DisplayText();
    }

    private void DisplayText()
    {
        fBCounter.text = "<b><size=" + numberOfPostsTextSize + ">" + currentNumberOfPosts
            + "</size><size=" + maximumPostsTextSize + "> /" + authorizedPosts;
    }

    public bool HasStoppedOfCringe()
    {
        if (currentNumberOfPosts >= authorizedPosts)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
