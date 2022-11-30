using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;

public class FaithBlogPostsCounter : MonoBehaviour
{
    public float numberOfPostsTextSize = 50;
    public float maximumPostsTextSize = 36;
    private int authorizedPosts;
    private int currentNumberOfPosts;
    private TextMeshProUGUI fBCounter;
    public StudioEventEmitter audioNormal;
    public StudioEventEmitter audioCringe;

    private void Start()
    {
        GameLogic_CommAndMemeInfluence influence = FindObjectOfType<GameLogic_CommAndMemeInfluence>();
        SceneToSceneDataKeeper dataKeeper = FindObjectOfType<SceneToSceneDataKeeper>();
        authorizedPosts = influence.GetAuthorizedFailsAmount(dataKeeper.GetSpecificCommunityList(4).Count);
        //Debug.Log("List 4 contains " + dataKeeper.GetSpecificCommunityList(4).Count + " Fans");
        //Debug.Log("So authorized posts are : " + authorizedPosts);
        fBCounter = this.GetComponent<TextMeshProUGUI>();
        UpdateScore(false);
    }

    public void AddFBPosts(int numberOfNewFBPosts)
    {
        currentNumberOfPosts += numberOfNewFBPosts;
        UpdateScore(true);
    }

    private void UpdateScore(bool playSound)
    {
        fBCounter.text = "<b><size=" + numberOfPostsTextSize + ">" + currentNumberOfPosts
            + "</size><size=" + maximumPostsTextSize + "> /" + authorizedPosts;
        if (playSound)
        {
            if (currentNumberOfPosts >= authorizedPosts)
            {
                audioCringe.Play();
            }
            else
            {
                audioNormal.Play();
            }
        }
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
