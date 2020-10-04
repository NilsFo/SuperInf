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
    public SpriteRenderer spriteRenderer;
    public new BoxCollider2D collider2D;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        updateState();

    }


    public override void OnButtonTrigger(GameObject source)
    {
    }

    public override void OnButtonTriggerEnter(GameObject source)
    {
        if (needSignal)
        {
            closed = false;
        }
        else
        {
            closed = !closed;
        }
        updateState();
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
            collider2D.enabled = true;
            anim.SetBool("open", false);
            
        }
        else
        {
            spriteRenderer.sprite = openSprite;
            collider2D.enabled = false;
            anim.SetBool("open", true);
        }
    }
}
