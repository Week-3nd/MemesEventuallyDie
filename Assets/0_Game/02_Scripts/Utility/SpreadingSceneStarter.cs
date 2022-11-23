using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadingSceneStarter : MonoBehaviour
{
    public GameLogic_SpreadingLogic spreadingLogic;
    public GameDisplay_SocialNetworkManager visualManager;
    public Canvas scoreCanvas;
    public SceneToSceneDataKeeper dataKeeper;
    
    private void Start()
    {
        dataKeeper.NextDay();
        spreadingLogic.EvaluateNetwork();
        scoreCanvas.gameObject.SetActive(true);
        visualManager.StartDrawing();
    }
}
