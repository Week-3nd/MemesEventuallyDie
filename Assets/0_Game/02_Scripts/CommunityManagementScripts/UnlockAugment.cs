using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockAugment : MonoBehaviour
{
    public int fanAmountToUnlock = 0;
    public TextMesh fanAmountToUnlockText;
    private SceneToSceneDataKeeper dataKeeper;
    
    
    // Start is called before the first frame update
    void Start()
    {
        dataKeeper = FindObjectOfType<SceneToSceneDataKeeper>();
        if (dataKeeper.GetFansList().Count >= fanAmountToUnlock)
        {
            this.gameObject.SetActive(false);
        }
        fanAmountToUnlockText.text = fanAmountToUnlock.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
