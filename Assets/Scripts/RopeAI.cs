using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeAI : MonoBehaviour
{
    public bool isBurned = false;
    public Sprite goodSprite;
    public Sprite burnedSprite;
    public SpriteRenderer spriteRenderer;
    
    private bool _renderState;
    
    public ButtonListener[] buttonListener;

    private void OnEnable()
    {
        if (isBurned)
        {
            spriteRenderer.sprite = burnedSprite;
        } 
        else
        {
            spriteRenderer.sprite = goodSprite;
        }
    }
    
    void Update()
    {
        if (_renderState != isBurned)
        {
            if (isBurned)
            {
                spriteRenderer.sprite = burnedSprite;
            } 
            else
            {
                spriteRenderer.sprite = goodSprite;
            }
            _renderState = isBurned;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Sharp mySharp = other.gameObject.GetComponent<Sharp>();
        if (!isBurned && mySharp && mySharp.sharpness == Sharpness.Burning)
        {
            for (int i = 0; i < buttonListener.Length; i++)
            {
                ButtonListener myListener = buttonListener[i];
                myListener.onButtonTrigger(gameObject);
            }
            
            isBurned = true;
        }
    }
}
