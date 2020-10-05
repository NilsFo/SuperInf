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
    public float recordingBlinkSpeed = 8f;

    public GameObject recStatusImage;
    public GameObject pickupMessage;

    private Image rectImg;
    private float deltaCounter=0;
    private bool recording = false;

    // Start is called before the first frame update
    void Start()
    {
        rectImg = recStatusImage.GetComponent<Image>();
        recStatusImage.SetActive(false);

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
            recStatusImage.SetActive(b);
        }
    }

    public void StartRecording()
    {
        recording = true;
    }

    public void StopRecording()
    {
        if (!recStatusImage)
        {
            throw new Exception("Level UI has no correct REC img. Did you hook up the camera to the UI?");
        }
        
        recording = false;
        deltaCounter = 0;
        recStatusImage.SetActive(false);
    }

    public void DisplayPickupInfo(bool show)
    {
        pickupMessage.SetActive(show);
    }
}
