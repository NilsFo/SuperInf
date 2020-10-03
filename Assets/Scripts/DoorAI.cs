using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAI : ButtonListener
{
    public bool needSignal = false;
    public bool closed = false;
    public Sprite openSprite;
    public Sprite closedSprite;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collider2D;
    

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer=gameObject.GetComponent<SpriteRenderer>();
        collider2D = gameObject.GetComponent<BoxCollider2D>();
        updateState();
    }

    public override void OnButtonTrigger(GameObject source)
    {
        if (!needSignal)
        {
            closed = !closed;
            updateState();
        }
        
    }

    public override void OnButtonTriggerEnter(GameObject source)
    {
        if (needSignal)
        {
            closed = false;
            updateState();
        }
    }

    public override void OnButtonTriggerExit(GameObject source)
    {
        if (needSignal)
        {
            closed = true;
            updateState();
        }
    }

    private void updateState()
    {
        if (closed)
        {
            spriteRenderer.sprite = closedSprite;
            collider2D.enabled = false;
        }
        else
        {
            spriteRenderer.sprite = openSprite;
            collider2D.enabled = true;
        }
    }
}
