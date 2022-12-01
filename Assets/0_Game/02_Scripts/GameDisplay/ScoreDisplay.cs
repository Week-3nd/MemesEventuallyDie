using TMPro;
using UnityEngine;
using FMODUnity;

public class ScoreDisplay : MonoBehaviour
{
    private int RealScore = 0;
    private int DisplayedScore = 0;
    private int StoredScore = 0;
    public TextMeshProUGUI ScoreText;
    public AnimationCurvesStorer SpeedAccordingToStoredScoreReference;
    //public GameObject ObjectToActivateUponFinish;
    private float Timer = 0.0f;
    private float IncrementInterval = 10.0f;
    private bool LastScoreUpdateDone = false;
    private bool StopCalculating = false;
    public CameraFollow Camera;

    public float CameraLastDezoomDuration = 0.2f;
    private Vector3 CameraLastPosition = new Vector3();
    private float CameraLastZoomFactor = 4.0f;

    public NewFansCounter newFansCounter;
    public FaithBlogPostsCounter FaithBlogPostsCounter;



    // audio
    [SerializeField] private FMODUnity.EventReference _audioSpread;
    private FMOD.Studio.EventInstance audioSpread;

    public float minAudioPitch;
    public float maxAudioPitch;
    public float minScoreThreshold;
    public float maxScoreThreshold;
    private float previousAudioPitch = 0;
    private float nextAudioPitch = 0;
    private float currentAudioPitch = 0;


    private void Start()
    {
        audioSpread = FMODUnity.RuntimeManager.CreateInstance(_audioSpread);
        //audioSpread.setParameterByName("SpreadAudioControl", 1);
        if (audioSpread.isValid())
        {
            audioSpread.start();
            //audioSpread.setParameterByName("EndSpread", 0);
        }
    }


    private void Update()
    {
        if (!StopCalculating)
        {
            Timer += Time.deltaTime;
            StoredScore = RealScore - DisplayedScore;

            IncrementInterval = SpeedAccordingToStoredScoreReference.CurveList[0].Evaluate(StoredScore);
            currentAudioPitch = Mathf.Lerp(previousAudioPitch, nextAudioPitch, Mathf.Clamp01(Timer / IncrementInterval));
            audioSpread.setParameterByName("SpreadAudioControl", currentAudioPitch);
            Debug.Log("Current audio parameter " + currentAudioPitch.ToString());

            if (Timer >= IncrementInterval && DisplayedScore < RealScore)
            {
                int amount = Mathf.RoundToInt(Timer / IncrementInterval);
                DisplayedScore += amount;
                Timer = 0.0f;
                ScoreText.text = DisplayedScore.ToString();

                previousAudioPitch = nextAudioPitch;
                float floatScore = DisplayedScore;
                nextAudioPitch = Mathf.Lerp(minAudioPitch, maxAudioPitch, Mathf.Clamp01((floatScore - minScoreThreshold) / (maxScoreThreshold - minScoreThreshold)));
                //Debug.Log("Next audio parameter " + nextAudioPitch.ToString());
                //Debug.Log(DisplayedScore.ToString());
            }
            else if (Timer >= IncrementInterval && LastScoreUpdateDone && DisplayedScore == RealScore)
            {
                //ObjectToActivateUponFinish.SetActive(true);

                newFansCounter.DisplayNewFans();
                StopCalculating = true;
                Camera.SetTarget(CameraLastPosition, CameraLastZoomFactor, CameraLastDezoomDuration);
                Timer = 0.0f;
                Camera.NextZoomIsLast(FaithBlogPostsCounter.HasStoppedOfCringe());
                //Debug.Log("Should stop sounding");

            }

            if (LastScoreUpdateDone && DisplayedScore == RealScore && Timer>= IncrementInterval/2) // delay fait maison
            {

                audioSpread.setParameterByName("EndSpread", 2);
            }
        }
        


    }

    public void SetScoreDisplay(int ScoreToDisplay)
    {
        RealScore = ScoreToDisplay;
    }

    public void LastScoreUpdate(Vector3 FinalPosition, float FinalZoom)
    {
        CameraLastPosition = FinalPosition;
        CameraLastZoomFactor = FinalZoom;
        LastScoreUpdateDone = true;
    }

    

}
