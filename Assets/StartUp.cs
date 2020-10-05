using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUp : MonoBehaviour
{

    public SpriteRenderer fadeOutSpriteRenderer;

    public float timeToFade = 3f;

    private float _currentTime = 0f;

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > timeToFade)
        {
            Color tmp = fadeOutSpriteRenderer.color;
            tmp.a = 1f-((_currentTime-timeToFade)/10);
            fadeOutSpriteRenderer.color = tmp;
        }
    }
}
