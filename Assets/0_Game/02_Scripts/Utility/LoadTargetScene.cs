using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadTargetScene : MonoBehaviour
{
    public int SceneBuildIndex;

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneBuildIndex);
    }
}
