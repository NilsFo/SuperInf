using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUp : MonoBehaviour
{

    public UnityEngine.UI.Image fadeOutSpriteRenderer;
    public UnityEngine.UI.Text text;

    public float timeToFade = 3f;

    private float _currentTime = 0f;

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime < timeToFade)
        {
            Color tmp = fadeOutSpriteRenderer.color;
            tmp.a = ((timeToFade-_currentTime)/timeToFade);
            fadeOutSpriteRenderer.color = tmp;
            
            tmp = text.color;
            tmp.a = ((timeToFade-_currentTime)/timeToFade);
            text.color = tmp;
        }
    }
}
