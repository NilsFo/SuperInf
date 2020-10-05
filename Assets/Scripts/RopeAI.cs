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
    public float activateTime = 0;
    private float _activateTimer = 0;

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
        if(activateTime > 0) {
            if(_activateTimer > 0) {
                _activateTimer -= Time.deltaTime;
                if(_activateTimer <= 0) {
                    isBurned = false;
                    for (int i = 0; i < buttonListener.Length; i++)
                    {
                        ButtonListener myListener = buttonListener[i];
                        myListener.OnButtonTrigger(gameObject);
                        myListener.OnButtonTriggerExit(gameObject);
                    }
                }
            }

        }
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
        if (!isBurned && mySharp && (mySharp.sharpness == Sharpness.Burning || mySharp.sharpness == Sharpness.Normal))
        {
            for (int i = 0; i < buttonListener.Length; i++)
            {
                ButtonListener myListener = buttonListener[i];
                myListener.OnButtonTrigger(gameObject);
                myListener.OnButtonTriggerEnter(gameObject);
            }
            
            isBurned = true;
            if(activateTime > 0) {
                _activateTimer = activateTime;
            }
        }
    }
}
