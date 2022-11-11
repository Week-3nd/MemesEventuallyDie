using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    private int RealScore = 0;
    private int DisplayedScore = 0;
    private int StoredScore = 0;
    public TextMeshProUGUI ScoreText;
    public AnimationCurvesStorer SpeedAccordingToStoredScoreReference;
    public GameObject ObjectToActivateUponFinish;
    private float Timer = 0.0f;
    private float IncrementInterval = 10.0f;
    private bool LastScoreUpdateDone = false;

    public void SetScoreDisplay(int ScoreToDisplay)
    {
        RealScore = ScoreToDisplay;
    }

    public void Update()
    {
        Timer += Time.deltaTime;
        StoredScore = RealScore - DisplayedScore;

        IncrementInterval = SpeedAccordingToStoredScoreReference.CurveList[0].Evaluate(StoredScore);

        if (Timer >= IncrementInterval && DisplayedScore < RealScore)
        {
            int amount = Mathf.RoundToInt(Timer/IncrementInterval);
            DisplayedScore += amount;
            Timer = 0.0f;
            ScoreText.text = DisplayedScore.ToString();
            //Debug.Log(DisplayedScore.ToString());
        }
        else if (Timer >= IncrementInterval && LastScoreUpdateDone && DisplayedScore == RealScore)
        {
            ObjectToActivateUponFinish.SetActive(true);
        }
    }

    public void LastScoreUpdate()
    {
        LastScoreUpdateDone = true;
    }

}
