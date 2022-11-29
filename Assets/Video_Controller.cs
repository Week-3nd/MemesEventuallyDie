using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Video_Controller : MonoBehaviour
{
    public string url;
    public VideoPlayer vidplayer;
    public RawImage uIImage;
    public float fadeToTransparentTiming;
    public float fadeToTransparentDuration;
    private float timer;
    private bool isPlaying;
    private Color uIColor;
    private Color invisible = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    public GameObject videoCanvas;
    private bool isFirstPlay = true;

    // Start is called before the first frame update
    void Start()
    {
        vidplayer.url = url;
        uIColor = uIImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && isFirstPlay)
        {
            isFirstPlay = false;
            Play();
        }

        if (isPlaying)
        {
            timer += Time.deltaTime;
            if (timer >= fadeToTransparentTiming)
            {
                uIImage.color = Color.Lerp(uIColor, invisible, (timer - fadeToTransparentTiming) / fadeToTransparentDuration);
                if (timer >= fadeToTransparentTiming + fadeToTransparentDuration)
                {
                    videoCanvas.SetActive(false);
                }
            }
        }

        
    }

    public void Play()
    {
        videoCanvas.SetActive(true);
        uIImage.color = uIColor;
        timer = 0.0f;
        vidplayer.Play();
        isPlaying = true;
    }
}
