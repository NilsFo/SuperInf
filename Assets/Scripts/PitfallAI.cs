using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitfallAI : ButtonListener
{
    public static int maxPhase = 2;
    public static int phase = 0;

    public bool isOpen = false;
    public Sprite openSprite;
    public Sprite closedSprite;
    public SpriteRenderer spriteRenderer;
    
    private bool _renderState;

    private void OnEnable()
    {
        if (isOpen)
        {
            spriteRenderer.sprite = openSprite;
        } 
        else
        {
            spriteRenderer.sprite = closedSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_renderState != isOpen)
        {
            if (isOpen)
            {
                spriteRenderer.sprite = openSprite;
            } 
            else
            {
                spriteRenderer.sprite = closedSprite;
            }
            _renderState = isOpen;
        }
    }

    public override void onButtonTrigger(ButtonAI source)
    {
        phase++;
    }
}
