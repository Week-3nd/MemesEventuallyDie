using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadingSceneStarter : MonoBehaviour
{
    public GameLogic_SpreadingLogic spreadingLogic;
    public GameDisplay_SocialNetworkManager visualManager;
    public Canvas scoreCanvas;
    
    private void Start()
    {
        spreadingLogic.EvaluateNetwork();
        scoreCanvas.gameObject.SetActive(true);
        visualManager.StartDrawing();
    }
}
