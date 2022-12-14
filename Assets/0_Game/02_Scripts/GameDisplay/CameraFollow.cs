using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class CameraFollow : MonoBehaviour
{
    //Position
    private Vector3 TargetPosition  = new Vector3(0, 0, -20);
    private Vector3 LastPosition = new Vector3(0, 0, -20);

    //Size
    private float TargetZoom = 4.0f;
    private float LastZoom = 4.0f;
    private float MaxZoom = 4.0f;

    //SpeedManagement
    private float DurationToNextZoom = 0.5f;
    private float ZoomIterationTimer = 0.0f;
    private float TimeFactor = 0.0f;

    [Tooltip("When a row has less users than the previous one, we don't zoom in fully but zoom slowly following this factor")]
    public int RezoomFactor = 2;
    private float RezoomRate = 1.0f;

    // on spreading end : one of the 2 will be displayed
    public GameObject cringeEnd;
    public StudioEventEmitter cringeEndAudio;
    public GameObject lonelyEnd;
    public StudioEventEmitter lonelyEndAudio;
    public GameObject happyEnding;
    public StudioEventEmitter happyEndingAudio;
    //public GameObject nextButton;
    //public GameObject winNextButton;
    private bool isLastZoom = false;
    private bool isCringeEnd;
    private float popUpTimer = 0.0f;
    public float timeToPrintEndBandeau = 1.5f;

    private bool doStuff = true;
    private bool isWin = false;
    public NewFansCounter newFansCounter;


    // Start is called before the first frame update
    void Start()
    {
        TargetPosition = this.transform.position;
        TargetZoom = this.GetComponent<Camera>().orthographicSize;
    }


    // Update is called once per frame
    void Update()
    {
        if (doStuff)
        {
            //Time scale calculation
            if (ZoomIterationTimer <= DurationToNextZoom)
            {
                ZoomIterationTimer += Time.deltaTime;
                TimeFactor = ZoomIterationTimer / DurationToNextZoom;

                //Position
                this.transform.position = new Vector3(
                    Mathf.Lerp(LastPosition.x, TargetPosition.x, TimeFactor),
                    Mathf.Lerp(LastPosition.y, TargetPosition.y, TimeFactor),
                    Mathf.Lerp(LastPosition.z, TargetPosition.z, TimeFactor));

                //Zoom
                this.GetComponent<Camera>().orthographicSize =
                    Mathf.Lerp(LastZoom, TargetZoom, TimeFactor);
            }
            else if (ZoomIterationTimer > DurationToNextZoom)
            {
                this.transform.position = TargetPosition;
                this.GetComponent<Camera>().orthographicSize = TargetZoom;

                // on last zoom : logic showing the bandeau for end condition & next scene button
                if (isLastZoom)
                {
                    if (popUpTimer == 0)
                    {
                        if (isWin)
                        {
                            happyEnding.SetActive(true);
                            happyEndingAudio.Play();
                        }
                        else
                        {
                            if (isCringeEnd)
                            {
                                cringeEnd.SetActive(true);
                                cringeEndAudio.Play();
                            }
                            else
                            {
                                lonelyEnd.SetActive(true);
                                lonelyEndAudio.Play();
                            }
                        }
                    }
                    popUpTimer += Time.deltaTime;

                    if (popUpTimer >= timeToPrintEndBandeau)
                    {
                        cringeEnd.SetActive(false);
                        lonelyEnd.SetActive(false);
                        happyEnding.SetActive(false);
                        newFansCounter.TypeOfEnd(isWin);
                        doStuff = false;
                    }
                }
            }
        }
    }


    public void SetTarget(Vector3 NewTargetPosition,float NewTargetZoom,float Duration)
    {
        LastPosition = this.transform.position;
        LastZoom = this.GetComponent<Camera>().orthographicSize;
        TargetPosition = NewTargetPosition;
        TargetZoom = NewTargetZoom; if (TargetZoom < 4) { TargetZoom = 4; }
        // /*
        if (TargetZoom > MaxZoom)
        {
            MaxZoom = TargetZoom;
        }
        if (TargetZoom < MaxZoom)
        {
            TargetZoom = MaxZoom;
            MaxZoom -= 0.28125f * RezoomRate * RezoomFactor; // = (9/16)/2 
        }
        // */
        DurationToNextZoom = Duration;
        ZoomIterationTimer = 0.0f;
    }

    public void SetRezoomRate(float NewRezoomRate)
    {
        RezoomRate = NewRezoomRate;
    }

    public void NextZoomIsLast(bool hasStoppedOfCringe) // true = FB, false = no more shares
    {
        isLastZoom = true;
        isCringeEnd = hasStoppedOfCringe;
        if (FindObjectOfType<GameDisplay_SocialNetworkManager>().GetScore() >= 1000)
        {
            isWin = true;
        }
    }
}
