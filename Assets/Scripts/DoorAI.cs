using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAI : ButtonListener
{

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

    public override void onButtonTrigger(GameObject source)
    {
        closed = !closed;
        updateState();
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
