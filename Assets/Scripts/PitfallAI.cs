using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PitfallAI : ButtonListener
{
    private GameObject _playerObject;
    
    public bool isOpen = false;
    public bool needSignal = true;
    
    public Sprite openSprite;
    public Sprite closedSprite;
    public SpriteRenderer spriteRenderer;

    public UnityEvent onKillEvent;
    
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

    public override void OnButtonTrigger(GameObject source)
    {
    }

    public override void OnButtonTriggerEnter(GameObject source)
    {
        isOpen = !isOpen;
        if (_playerObject && isOpen)
        {
            onKillEvent.Invoke();
        }
    }

    public override void OnButtonTriggerExit(GameObject source)
    {
        if (needSignal)
        {
            isOpen = !isOpen;
        }
        if (_playerObject && isOpen)
        {
            onKillEvent.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _playerObject = other.gameObject;
            if (isOpen)
            {
                onKillEvent.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _playerObject = null;
    }
}
