using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Position
    private Vector3 TargetPosition  = new Vector3(0, 0, -20);
    private Vector3 LastPosition = new Vector3(0, 0, -20);

    //Size
    private float TargetZoom = 4.0f;
    private float LastZoom = 4.0f;

    //SpeedManagement
    private float DurationToNextZoom = 0.5f;
    private float ZoomIterationTimer = 0.0f;
    private float TimeFactor = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        TargetPosition = this.transform.position;
        TargetZoom = this.GetComponent<Camera>().orthographicSize;
    }


    // Update is called once per frame
    void Update()
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
        }
    }


    public void SetTarget(Vector3 NewTargetPosition,float NewTargetZoom,float Duration)
    {
        LastPosition = this.transform.position;
        LastZoom = this.GetComponent<Camera>().orthographicSize;
        TargetPosition = NewTargetPosition;
        TargetZoom = NewTargetZoom; if (TargetZoom < 4) { TargetZoom = 4; }
        DurationToNextZoom = Duration;
        ZoomIterationTimer = 0.0f;
    }
}
