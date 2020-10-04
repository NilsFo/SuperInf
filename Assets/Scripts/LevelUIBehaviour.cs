using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIBehaviour : MonoBehaviour
{

    public Sprite recordingSprite;
    public Sprite pauseSprite;
    public Sprite playSprite;
    public float recordingBlinkSpeed=8f;

    public GameObject recStatusImage;

    private Image rectImg;
    private float deltaCounter=0;
    private bool recording = false;

    // Start is called before the first frame update
    void Start()
    {
        rectImg = recStatusImage.GetComponent<Image>();
        recStatusImage.SetActive(false);
        print("Level UI Status img: " + rectImg);

        if (!recStatusImage)
        {
            throw new Exception("Level UI has no correct REC img. Did you hook up the camera to the UI?");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (recording)
        {
            deltaCounter += Time.deltaTime;
            float s = Mathf.Sin(deltaCounter * recordingBlinkSpeed);
            bool b = s > 0;
            print("s: " + s + ". b: " + b);
            recStatusImage.SetActive(b);
        }
    }

    public void StartRecording()
    {
        print("UI is recording: "+ rectImg);
        recording = true;
    }

    public void StopRecording()
    {
        if (!recStatusImage)
        {
            throw new Exception("Level UI has no correct REC img. Did you hook up the camera to the UI?");
        }

        print("UI is no longer recording: "+ rectImg);
        recording = false;
        deltaCounter = 0;
        recStatusImage.SetActive(false);
    }
}
